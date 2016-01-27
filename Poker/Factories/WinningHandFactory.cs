﻿namespace Poker.Factories
{
    using System.Collections.Generic;
    using System.Linq;
    using Cards.Hands;
    using Forms;
    using Interfaces;

    public static class WinningHandFactory
    {
        /// <summary>
        /// After all of the cards have been revealed and the betting has finished determines who the winner is.
        /// </summary>
        /// <returns>Returns all of the players that are in tie for the strongest hand</returns>
        public static List<IPlayer> DetermineWinner()
        {
            Power strongestHand = Game.Instance.Player.CurrentHand.HandPower;
            for (int i = 0; i < Game.Instance.Bots.Count; i++)
            {
                if (Game.Instance.Bots[i].CurrentHand.HandPower > strongestHand && !Game.Instance.Bots[i].HasFolded)
                {
                    strongestHand = Game.Instance.Bots[i].CurrentHand.HandPower;
                }
            }

            var winners = new List<IPlayer>();
            if (strongestHand == Game.Instance.Player.CurrentHand.HandPower)
            {
                winners.Add(Game.Instance.Player);
            }

            for (int i = 0; i < Game.Instance.Bots.Count; i++)
            {
                if (strongestHand == Game.Instance.Bots[i].CurrentHand.HandPower && !Game.Instance.Bots[i].HasFolded)
                {
                    winners.Add(Game.Instance.Bots[i]);
                }
            }

            var winnersInTie = new List<IPlayer>();
            if (winners.Count == 1)
            {
                winnersInTie.Add(winners[0]);
            }
            else
            {
                winnersInTie = WinnersInTie(winners, strongestHand);
            }

            return winnersInTie;
        }

        /// <summary>
        /// Determines the winners in the tie. In the case of 2 players having the same cards but from a different suit they split the pot.
        /// </summary>
        /// <param name="winnersWithSameHand">All of the players that have a winning hand with the same power</param>
        /// <param name="strongestHandPower">The strongest hand that is owned by 1 player in the current game</param>
        /// <returns>All of the players that have the strongest hand and highest cards. Winners may be more than 1 if 2 or more have the same cards</returns>
        private static List<IPlayer> WinnersInTie(List<IPlayer> winnersWithSameHand, Power strongestHandPower)
        {
            var winnersInTie = new List<IPlayer>();
            if (strongestHandPower == Power.StraightFlush || strongestHandPower == Power.Straigth)
            {
                ////In the case of King, Ace, 2, 3, 4 and 2, 3, 4, 5 ,6 The first hand wins even though the last card is weaker
                BreakTieUsingCardIndex(winnersWithSameHand, winnersInTie, 0);
            }
            else
            {
                BreakTieUsingCardIndex(winnersWithSameHand, winnersInTie, winnersWithSameHand[0].CurrentHand.Cards.Count - 1);
            }

            return winnersInTie;
        }

        /// <summary>
        /// Breaks the tie using the card at the specified index in the players hands
        /// </summary>
        /// <param name="winnersWithSameHand">All of the players that have a winning hand with the same power</param>
        /// <param name="winnersInTie">All of the players that have the a winning hand with the same power and cards</param>
        /// <param name="cardIndex">The index of the card that will be used for comparison</param>
        private static void BreakTieUsingCardIndex(List<IPlayer> winnersWithSameHand, List<IPlayer> winnersInTie, int cardIndex)
        {
            int highestPowerOnFirstCard = winnersWithSameHand[0].CurrentHand.Cards[cardIndex].Power;
            for (int i = 1; i < winnersWithSameHand.Count; i++)
            {
                if (highestPowerOnFirstCard < winnersWithSameHand[i].CurrentHand.Cards[cardIndex].Power)
                {
                    highestPowerOnFirstCard = winnersWithSameHand[i].CurrentHand.Cards[cardIndex].Power;
                }
            }

            for (int i = 0; i < winnersWithSameHand.Count; i++)
            {
                if (highestPowerOnFirstCard == winnersWithSameHand[i].CurrentHand.Cards[cardIndex].Power)
                {
                    winnersInTie.Add(winnersWithSameHand[i]);
                }
            }

            if (winnersInTie.Count != 1)
            {
                if (cardIndex == winnersWithSameHand[0].CurrentHand.Cards.Count - 1 &&
                    0 != winnersWithSameHand[0].CurrentHand.Cards.Count - 1)
                {
                    winnersInTie.Clear();
                    BreakTieUsingCardIndex(winnersWithSameHand, winnersInTie, 0);
                }
                else
                {
                    BreakTieWithMaxCardInHand(winnersInTie);
                }
            }

            if (winnersInTie.Count != 1)
            {
                if (cardIndex == winnersWithSameHand[0].CurrentHand.Cards.Count - 1 &&
                    0 != winnersWithSameHand[0].CurrentHand.Cards.Count - 1)
                {
                    winnersInTie.Clear();
                    BreakTieUsingCardIndex(winnersWithSameHand, winnersInTie, 0);
                }
                else
                {
                    BreakTieWithMinCardInHand(winnersInTie);
                }
            }
        }

        /// <summary>
        /// Try to break the tie using the highest card in the players hands
        /// </summary>
        /// <param name="winnersInTie">All of the players with the same winning hands and cards making up those winning hands</param>
        private static void BreakTieWithMaxCardInHand(List<IPlayer> winnersInTie)
        {
            int highestCardInHand = winnersInTie[0].Cards.Max().Power;
            for (int i = 1; i < winnersInTie.Count; i++)
            {
                if (highestCardInHand < winnersInTie[i].Cards.Max().Power)
                {
                    highestCardInHand = winnersInTie[i].Cards.Max().Power;
                }
            }

            int index = 0;
            do
            {
                if (winnersInTie[index].Cards.Max().Power != highestCardInHand)
                {
                    winnersInTie.Remove(winnersInTie[index]);
                }
                else
                {
                    index++;
                }
            }
            while (index < winnersInTie.Count);
        }

        /// <summary>
        /// Try to break the tie using the lowest card in the players hands
        /// </summary>
        /// <param name="winnersInTie">All of the players with the same winning hands and cards making up those winning hands</param>
        private static void BreakTieWithMinCardInHand(List<IPlayer> winnersInTie)
        {
            int lowestCardInHand = winnersInTie[0].Cards.Min().Power;
            for (int i = 1; i < winnersInTie.Count; i++)
            {
                if (lowestCardInHand < winnersInTie[i].Cards.Min().Power)
                {
                    lowestCardInHand = winnersInTie[i].Cards.Min().Power;
                }
            }

            int index = 0;
            do
            {
                if (winnersInTie[index].Cards.Min().Power != lowestCardInHand)
                {
                    winnersInTie.Remove(winnersInTie[index]);
                }
                else
                {
                    index++;
                }
            }
            while (index < winnersInTie.Count);
        }
    }
}
