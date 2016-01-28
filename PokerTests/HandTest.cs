namespace PokerTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Cards;
    using Poker.Cards.Hands;
    using Poker.Constants;
    using Poker.Forms;
    using Poker.Interfaces;

    /// <summary>
    /// Use for testing the <see cref="Deck"/> class and the methods inside of it
    /// </summary>
    [TestClass]
    public class HandTest
    {
        private static Game game;

        public static Game Game
        {
            get
            {
                return game;
            }

            private set
            {
                game = value;
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            if (Game == null)
            {
                Card.CardBackPath = GlobalConstants.CardBackForUnitTesting;
                Game = new Game(GlobalConstants.CardPathFromUnitTest, false);
            }
        }

        /// <summary>
        /// Test if it will throw an exception with 0 cards
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "The player must have atleast 1 card before determining his hand power")]
        public void Test_DetermineHandPower_With0Cards()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>();
            Game.Deck.NeutalCards = new ICard[5];
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
        }

        /// <summary>
        /// Test if it will throw an exception with 0 cards
        /// </summary>
        [TestMethod]
        public void Test_DetermineHandPower_With1Card()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>()
            {
                new Card(Card.Back, 2, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new ICard[5];
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.HighCard &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, Game.Player.Cards),
                "The required flush was not found when it existed. Expected: " + string.Join(" ", Game.Player.Cards));
        }

        /// <summary>
        /// Test if finding a flush works
        /// </summary>
        [TestMethod]
        public void Test_FindingFlush_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>
            {
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.Flush &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required flush was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding a straight works
        /// </summary>
        [TestMethod]
        public void Test_FindingStraigh_With2DrawnCardsAnd5Centert()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 5, Suit.Diamonds),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 7, Suit.Spades),
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>
            {
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Diamonds),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 7, Suit.Spades),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.Straigth &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required straight was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding a straight with carrying backwards works
        /// </summary>
        [TestMethod]
        public void Test_FindingStraightWithCarryingBackwards_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 7, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Diamonds),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.Straigth &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required straight was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }
        
        /// <summary>
        /// Test if finding a straight with carrying forward works
        /// </summary>
        [TestMethod]
        public void Test_FindingStraightWithCarryingForward_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 12, Suit.Diamonds),
                new Card(Card.Back, 7, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>
            {
                new Card(Card.Back, 12, Suit.Diamonds),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.Straigth &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required straight was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding a straight flush works
        /// </summary>
        [TestMethod]
        public void Test_FindingStraightFlush_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 7, Suit.Clubs),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>
            {
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 7, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.StraightFlush &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required straight flush was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding a royal flush works
        /// </summary>
        [TestMethod]
        public void Test_FindingRoyalFlush_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 11, Suit.Clubs),
                new Card(Card.Back, 12, Suit.Clubs),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>()
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 11, Suit.Clubs),
                new Card(Card.Back, 12, Suit.Clubs),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.RoyalFlush &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required royal flush was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding a four of a kind works
        /// </summary>
        [TestMethod]
        public void Test_FindingFourOfAKind_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Spades),
                new Card(Card.Back, 14, Suit.Hearts),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>()
            {
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Spades),
                new Card(Card.Back, 14, Suit.Hearts),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.FourOfAKind &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required four of a kind was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding a three of a kind works
        /// </summary>
        [TestMethod]
        public void Test_FindingThreeOfAKind_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Spades),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 6, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>()
            {
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Spades),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.ThreeOfAKind &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required two of a kind was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding a full house works
        /// </summary>
        [TestMethod]
        public void Test_FindingFullHouse_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 14, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>()
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Hearts),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.FullHouse &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required full house was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding two pairs works
        /// </summary>
        [TestMethod]
        public void Test_FindingTwoPairs_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>()
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.TwoPair &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required two pairs were not found when they existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding a two of a kind works
        /// </summary>
        [TestMethod]
        public void Test_FindingOnePair_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Spades),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 2, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>()
            {
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.OnePair &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required one pair was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Test if finding a high card works
        /// </summary>
        [TestMethod]
        public void Test_FindingHighCard_With2DrawnCardsAnd5Center()
        {
            Game.Deck.RemoveAllCardsOnBoard(Game.Player, Game.Bots);
            Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Spades),
            };
            Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 3, Suit.Diamonds),
                new Card(Card.Back, 5, Suit.Spades),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Hearts),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Game.Player.DetermineHandPower(Game.Deck.NeutalCards);
            var cardsRequired = new List<ICard>()
            {
                new Card(Card.Back, 14, Suit.Spades),
            };
            Assert.IsTrue(
                Game.Player.CurrentHand.HandPower == Power.HighCard &&
                this.CardHandsAreEqual(Game.Player.CurrentHand.Cards, cardsRequired),
                "The required high card was not found when it existed. Expected " + string.Join(" ", cardsRequired) + "\nReceived " + string.Join(" ", Game.Player.CurrentHand.Cards));
        }

        /// <summary>
        /// Determines if 2 hands are the same
        /// </summary>
        /// <param name="hand1">The first hand we are comparing</param>
        /// <param name="hand2">The second hand we are comparing</param>
        /// <returns>True if the 2 hands are the same and false if they are not</returns>
        private bool CardHandsAreEqual(List<ICard> hand1, List<ICard> hand2)
        {
            if (hand1.Count != hand2.Count)
            {
                return false;
            }

            for (int i = 0; i < hand1.Count; i++)
            {
                if (hand1[i].Power != hand2[i].Power)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
