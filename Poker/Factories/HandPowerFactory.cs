namespace Poker.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cards;
    using Cards.Hands;

    /// <summary>
    /// Used for determining the highest possible hand the player has.
    /// </summary>
    public static class HandPowerFactory
    {
        /// <summary>
        /// Retrieves the strongest hand for the player's cards
        /// </summary>
        /// <param name="cards">The cards the player can use</param>
        /// <returns>Returns the hand with the strongest cards</returns>
        public static Hand StrongestHand(List<Card> cards)
        {
            cards.Sort();
            if (cards.Count > 2)
            {
                return HighestHandWithMoreThan2Cards(cards);
            }
            else if (cards.Count == 2)
            {
                if (cards[0].Power == cards[1].Power)
                {
                    return new Hand(Power.OnePair, cards);
                }
                else
                {
                    var handCards = new List<Card>();
                    handCards.Add(cards[cards.Count - 1]); ////Last card is the highest since they are sorted
                    return new Hand(Power.HighCard, handCards);
                }
            }
            else
            {
                throw new ArgumentException("The player does not have cards.");
            }
        }

        /// <summary>
        /// Retrieves the strongest hand for the player's cards if he access to more than 2 cards
        /// </summary>
        /// <param name="cards">The cards the player can use</param>
        /// <returns>Returns the hand with the strongest cards</returns>
        private static Hand HighestHandWithMoreThan2Cards(List<Card> cards)
        {
            var hand = new List<Card>();
            if (TryRoyalFlush(cards, ref hand))
            {
                return new Hand(Power.RoyalFlush, hand);
            }
            else if (TryStraightFlush(cards, ref hand))
            {
                return new Hand(Power.StraightFlush, hand);
            }
            else if (TrySeveralOfAKind(cards, ref hand, 4))
            {
                return new Hand(Power.FourOfAKind, hand);
            }
            else if (TryTwoPairs(cards, ref hand, 3, 2))
            {
                return new Hand(Power.FullHouse, hand);
            }
            else if (TryFlush(cards, ref hand))
            {
                return new Hand(Power.Flush, hand);
            }
            else if (TryStraight(cards, ref hand))
            {
                return new Hand(Power.Straigth, hand);
            }
            else if (TrySeveralOfAKind(cards, ref hand, 3))
            {
                return new Hand(Power.ThreeOfAKind, hand);
            }
            else if (TryTwoPairs(cards, ref hand, 2, 2))
            {
                return new Hand(Power.TwoPair, hand);
            }
            else if (TrySeveralOfAKind(cards, ref hand, 2))
            {
                return new Hand(Power.OnePair, hand);
            }
            else
            {
                var handCards = new List<Card>();
                handCards.Add(cards[cards.Count - 1]);
                return new Hand(Power.HighCard, handCards);
            }
        }

        /// <summary>
        /// Try obtaining a royal flush
        /// </summary>
        /// <param name="cards">The cards the player has access to</param>
        /// <param name="hand">The cards that make up the hand</param>
        /// <param name="numberOfFirstPair">The number of cards we need for the first pair</param>
        /// <param name="numberOfSecondPair">The number of cards we need for the second pair</param>
        /// <returns>Returns true if a royal flush has been found and removes the useless cards from the hand</returns>
        private static bool TryTwoPairs(List<Card> cards, ref List<Card> hand, int numberOfFirstPair, int numberOfSecondPair)
        {
            var firstPair = new List<Card>();
            if (TrySeveralOfAKind(cards, ref firstPair, numberOfFirstPair))
            {
                var newCards = new List<Card>();
                for (int i = 0; i < cards.Count; i++)
                {
                    newCards.Add(cards[i]);
                }

                for (int i = 0; i < firstPair.Count; i++)
                {
                    newCards.Remove(firstPair[i]);
                }

                var secondPair = new List<Card>();
                if (TrySeveralOfAKind(newCards, ref secondPair, numberOfSecondPair))
                {
                    hand = hand.Concat(secondPair).ToList();
                    hand = hand.Concat(firstPair).ToList();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try obtaining a royal flush
        /// </summary>
        /// <param name="cards">The cards the player has access to</param>
        /// <param name="hand">The cards that make up the hand</param>
        /// <returns>Returns true if a royal flush has been found and removes the useless cards from the hand</returns>
        private static bool TryStraightFlush(List<Card> cards, ref List<Card> hand)
        {
            if (TryFlush(cards, ref hand))
            {
                if (TryStraight(hand, ref hand))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try obtaining a royal flush
        /// </summary>
        /// <param name="cards">The cards the player has access to</param>
        /// <param name="hand">The cards that make up the hand</param>
        /// <returns>Returns true if a royal flush has been found and removes the useless cards from the hand</returns>
        private static bool TryRoyalFlush(List<Card> cards, ref List<Card> hand)
        {
            if (TryFlush(cards, ref hand))
            {
                if (TryStraight(hand, ref hand))
                {
                    if (hand[0].Power == 10)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Try obtaining a four of a kind
        /// </summary>
        /// <param name="cards">The cards the player has access to</param>
        /// <param name="hand">The cards that make up the hand</param>
        /// <param name="numberOfAKind">The number of same cards required for the hand</param>
        /// <returns>Returns true if a four of a kind has been found and removes the useless cards from the hand</returns>
        private static bool TrySeveralOfAKind(List<Card> cards, ref List<Card> hand, int numberOfAKind)
        {
            var cardsByPower = new Dictionary<int, List<Card>>();
            for (int i = 0; i < cards.Count; i++)
            {
                if (!cardsByPower.ContainsKey(cards[i].Power))
                {
                    cardsByPower.Add(cards[i].Power, new List<Card>());
                }

                cardsByPower[cards[i].Power].Add(cards[i]);
            }

            var mostCardsWithSameSuit = new List<Card>();
            foreach (var currentCards in cardsByPower)
            {
                if (currentCards.Value.Count >= numberOfAKind)
                {
                    mostCardsWithSameSuit = currentCards.Value;
                }
            }

            if (mostCardsWithSameSuit.Count < numberOfAKind)
            {
                return false;
            }
            else
            {
                hand = mostCardsWithSameSuit.GetRange(mostCardsWithSameSuit.Count - numberOfAKind, numberOfAKind);
                return true;
            }
        }

        /// <summary>
        /// Try obtaining a flush
        /// </summary>
        /// <param name="cards">The cards the player has access to</param>
        /// <param name="hand">The cards that make up the hand</param>
        /// <returns>Returns true if a flush has been found and removes the useless cards from the hand</returns>
        private static bool TryFlush(List<Card> cards, ref List<Card> hand)
        {
            var suitCards = new Dictionary<Suit, List<Card>>();
            for (int i = 0; i < cards.Count; i++)
            {
                Suit currentSuit = cards[i].Suit;
                if (!suitCards.ContainsKey(currentSuit))
                {
                    suitCards.Add(currentSuit, new List<Card>());
                }

                suitCards[currentSuit].Add(cards[i]);
            }

            var mostCardsWithSameSuit = new List<Card>();
            foreach (var currentCards in suitCards)
            {
                if (currentCards.Value.Count >= 5)
                {
                    mostCardsWithSameSuit = currentCards.Value;
                    break;
                }
            }

            if (mostCardsWithSameSuit.Count < 5)
            {
                return false;
            }
            else
            {
                hand = mostCardsWithSameSuit.GetRange(mostCardsWithSameSuit.Count - 5, 5);
                return true;
            }
        }

        /// <summary>
        /// Try obtaining a straight
        /// </summary>
        /// <param name="cards">The cards the player has access to</param>
        /// <param name="hand">The cards that make up the hand</param>
        /// <returns>Returns true if a straight has been found and removes the useless cards from the hand</returns>
        private static bool TryStraight(List<Card> cards, ref List<Card> hand)
        {
            List<Card> currentCards = LongestSequenceOfCards(cards);
            if (currentCards.Count < 5)
            {
                return false;
            }
            else
            {
                hand = currentCards.GetRange(currentCards.Count - 5, 5);
                return true;
            }
        }

        /// <summary>
        /// The longest sequence of cards with a difference of 1
        /// </summary>
        /// <param name="cards">The cards in the hand</param>
        /// <returns>Returns a list of cards with a difference of only 1 from each other's neighbors</returns>
        private static List<Card> LongestSequenceOfCards(List<Card> cards)
        {
            List<Card> longestSequence = new List<Card>();
            longestSequence.Add(cards[0]);
            for (int i = 1; i < cards.Count; i++)
            {
                ////In the case it is 0 we just need to skip it
                if (cards[i].Power - cards[i - 1].Power == 1)
                {
                    longestSequence.Add(cards[i]);
                } 
                else if (cards[i].Power - cards[i - 1].Power > 1)
                {
                    break;
                }
            }

            if (cards[0].Power == 2 && cards[cards.Count - 1].Power == 14)
            {
                longestSequence.Insert(0, cards[cards.Count - 1]);
                for (int i = cards.Count - 2; i > 0; i--)
                {                    
                    ////In the case it is 0 we just need to skip it
                    if (cards[i + 1].Power - cards[i].Power == 1)
                    {
                        longestSequence.Insert(0, cards[i]);
                    }
                    else if (cards[i + 1].Power - cards[i].Power > 1)
                    {
                        break;
                    }
                }
            }

            return longestSequence;
        }
    }
}
