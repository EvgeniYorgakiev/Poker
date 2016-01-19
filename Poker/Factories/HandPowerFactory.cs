namespace Poker.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cards;
    using Cards.Hands;
    using Interfaces;

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
        public static Hand StrongestHand(List<ICard> cards)
        {
            cards.Sort();
            if (cards.Count > 2)
            {
                return HighestHandWithMoreThan2Cards(cards);
            }
            else if (cards.Count >= 1)
            {
                if (cards.Count == 2)
                {
                    if (cards[0].Power == cards[1].Power)
                    {
                        return new Hand(Power.OnePair, cards);
                    }
                    else
                    {
                        var handCards = new List<ICard>();
                        handCards.Add(cards[cards.Count - 1]); ////Last card is the highest since they are sorted
                        return new Hand(Power.HighCard, handCards);
                    }
                }
                else
                {
                    var handCards = new List<ICard>();
                    handCards.Add(cards[0]);
                    return new Hand(Power.HighCard, handCards);
                }
            }
            else
            {
                throw new InvalidOperationException("The player does not have cards.");
            }
        }

        /// <summary>
        /// Retrieves the strongest hand for the player's cards if he access to more than 2 cards
        /// </summary>
        /// <param name="cards">The cards the player can use</param>
        /// <returns>Returns the hand with the strongest cards</returns>
        private static Hand HighestHandWithMoreThan2Cards(List<ICard> cards)
        {
            var hand = new List<ICard>();
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
                var handCards = new List<ICard>();
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
        private static bool TryTwoPairs(List<ICard> cards, ref List<ICard> hand, int numberOfFirstPair, int numberOfSecondPair)
        {
            var firstPair = new List<ICard>();
            if (TrySeveralOfAKind(cards, ref firstPair, numberOfFirstPair))
            {
                var newCards = new List<ICard>();
                for (int i = 0; i < cards.Count; i++)
                {
                    newCards.Add(cards[i]);
                }

                for (int i = 0; i < firstPair.Count; i++)
                {
                    newCards.Remove(firstPair[i]);
                }

                var secondPair = new List<ICard>();
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
        private static bool TryStraightFlush(List<ICard> cards, ref List<ICard> hand)
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
        private static bool TryRoyalFlush(List<ICard> cards, ref List<ICard> hand)
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
        private static bool TrySeveralOfAKind(List<ICard> cards, ref List<ICard> hand, int numberOfAKind)
        {
            var cardsByPower = new Dictionary<int, List<ICard>>();
            for (int i = 0; i < cards.Count; i++)
            {
                if (!cardsByPower.ContainsKey(cards[i].Power))
                {
                    cardsByPower.Add(cards[i].Power, new List<ICard>());
                }

                cardsByPower[cards[i].Power].Add(cards[i]);
            }

            var mostCardsWithSamePower = new List<ICard>();
            foreach (var currentCards in cardsByPower)
            {
                if (currentCards.Value.Count >= numberOfAKind)
                {
                    mostCardsWithSamePower = currentCards.Value;
                }
            }

            if (mostCardsWithSamePower.Count < numberOfAKind)
            {
                return false;
            }
            else
            {
                hand = mostCardsWithSamePower.GetRange(mostCardsWithSamePower.Count - numberOfAKind, numberOfAKind);
                return true;
            }
        }

        /// <summary>
        /// Try obtaining a flush
        /// </summary>
        /// <param name="cards">The cards the player has access to</param>
        /// <param name="hand">The cards that make up the hand</param>
        /// <returns>Returns true if a flush has been found and removes the useless cards from the hand</returns>
        private static bool TryFlush(List<ICard> cards, ref List<ICard> hand)
        {
            var suitCards = new Dictionary<Suit, List<ICard>>();
            for (int i = 0; i < cards.Count; i++)
            {
                Suit currentSuit = cards[i].Suit;
                if (!suitCards.ContainsKey(currentSuit))
                {
                    suitCards.Add(currentSuit, new List<ICard>());
                }

                suitCards[currentSuit].Add(cards[i]);
            }

            var mostCardsWithSameSuit = new List<ICard>();
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
        private static bool TryStraight(List<ICard> cards, ref List<ICard> hand)
        {
            List<ICard> currentCards = LongestSequenceOfCards(cards);
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
        private static List<ICard> LongestSequenceOfCards(List<ICard> cards)
        {
            var allSequence = AllSequence(cards);

            var longestSequence = LongestSequence(allSequence);

            TryToGoBackWithCarrying(cards, longestSequence);

            TryToGoForwardWithCarrying(cards, longestSequence);

            return longestSequence;
        }

        /// <summary>
        /// Try to for forward from an Ace to Two if possible in order to find the best sequence
        /// </summary>
        /// <param name="cards">A list of all possible cards</param>
        /// <param name="longestSequence">The current longest sequence</param>
        private static void TryToGoForwardWithCarrying(List<ICard> cards, List<ICard> longestSequence)
        {
            bool canCarryForward = cards[0].Power == 2 && longestSequence[longestSequence.Count - 1].Power == 14;
            if (canCarryForward)
            {
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
            }
        }

        /// <summary>
        /// Try to for backwards from Two to an Ace if possible in order to find the best sequence
        /// </summary>
        /// <param name="cards">A list of all possible cards</param>
        /// <param name="longestSequence">The current longest sequence</param>
        private static void TryToGoBackWithCarrying(List<ICard> cards, List<ICard> longestSequence)
        {
            bool canCarryBackwards = longestSequence[0].Power == 2 && cards[cards.Count - 1].Power == 14;
            if (canCarryBackwards)
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
        }

        /// <summary>
        /// Longest sequence of cards in a list of sequences
        /// </summary>
        /// <param name="allSequence">All of the possible sequences of cards</param>
        /// <returns>The longest sequence of cards with a difference of 1</returns>
        private static List<ICard> LongestSequence(List<List<ICard>> allSequence)
        {
            int longestSequenceIndex = 0;
            int longestSequenceNumber = allSequence[0].Count;
            for (int i = 1; i < allSequence.Count; i++)
            {
                if (longestSequenceNumber <= allSequence[i].Count)
                {
                    longestSequenceNumber = allSequence[i].Count;
                    longestSequenceIndex = i;
                }
            }

            var longestSequence = allSequence[longestSequenceIndex];

            return longestSequence;
        }

        /// <summary>
        /// Retrieves all possible sequences of cards with a difference of 1
        /// </summary>
        /// <param name="cards">All of the possible cards</param>
        /// <returns>Returns a list of all sequences</returns>
        private static List<List<ICard>> AllSequence(List<ICard> cards)
        {
            var allSequence = new List<List<ICard>>();
            allSequence.Add(new List<ICard>());
            allSequence[0].Add(cards[0]);
            for (int i = 1; i < cards.Count; i++)
            {
                ////In the case it is 0 we just need to skip it
                if (cards[i].Power - cards[i - 1].Power == 1)
                {
                    allSequence[allSequence.Count - 1].Add(cards[i]);
                }
                else if (cards[i].Power - cards[i - 1].Power > 1)
                {
                    allSequence.Add(new List<ICard>());
                    allSequence[allSequence.Count - 1].Add(cards[i]);
                }
            }

            return allSequence;
        }
    }
}
