namespace Poker.Test
{
    using System.Collections.Generic;
    using Cards;
    using Cards.Hands;
    using Constants;
    using Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Use for testing the <see cref="Deck"/> class and the methods inside of it
    /// </summary>
    [TestClass]
    public class HandTest
    {
        private Game game;

        public Game Game
        {
            get
            {
                return this.game;
            }

            private set
            {
                this.game = value;
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            Card.CardBackPath = GlobalConstants.CardBackForUnitTesting;
            this.Game = new Game(GlobalConstants.CardPathFromUnitTest, false);
        }
        
        /// <summary>
        /// Test if finding a flush works
        /// </summary>
        [TestMethod]
        public void TestFindingFlush()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>
            {
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.Flush &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required flush was not found when it existed");
        }

        /// <summary>
        /// Test if finding a straight works
        /// </summary>
        [TestMethod]
        public void TestFindingStraight()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 5, Suit.Diamonds),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 7, Suit.Spades),
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>
            {
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Diamonds),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 7, Suit.Spades),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.Straigth &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required straight was not found when it existed");
        }

        /// <summary>
        /// Test if finding a straight with carrying works
        /// </summary>
        [TestMethod]
        public void TestFindingStraightWithCarrying()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 7, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Diamonds),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.Straigth &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required straight was not found when it existed");
        }

        /// <summary>
        /// Test if finding a straight flush works
        /// </summary>
        [TestMethod]
        public void TestFindingStraightFlush()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 7, Suit.Clubs),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>
            {
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 7, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.StraightFlush &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required straight flush was not found when it existed");
        }

        /// <summary>
        /// Test if finding a royal flush works
        /// </summary>
        [TestMethod]
        public void TestFindingRoyalFlush()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 11, Suit.Clubs),
                new Card(Card.Back, 12, Suit.Clubs),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>()
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 11, Suit.Clubs),
                new Card(Card.Back, 12, Suit.Clubs),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.RoyalFlush &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required royal flush was not found when it existed");
        }

        /// <summary>
        /// Test if finding a four of a kind works
        /// </summary>
        [TestMethod]
        public void TestFindingFourOfAKind()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Spades),
                new Card(Card.Back, 14, Suit.Hearts),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>()
            {
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Spades),
                new Card(Card.Back, 14, Suit.Hearts),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.FourOfAKind &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required four of a kind was not found when it existed");
        }

        /// <summary>
        /// Test if finding a three of a kind works
        /// </summary>
        [TestMethod]
        public void TestFindingThreeOfAKind()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Spades),
                new Card(Card.Back, 3, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>()
            {
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Spades),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.ThreeOfAKind &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required two of a kind  was not found when it existed");
        }

        /// <summary>
        /// Test if finding a full house works
        /// </summary>
        [TestMethod]
        public void TestFindingFullHouse()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 14, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>()
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 14, Suit.Hearts),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.FullHouse &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required full house were not found when they existed");
        }

        /// <summary>
        /// Test if finding two pairs works
        /// </summary>
        [TestMethod]
        public void TestFindingTwoPairs()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>()
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Spades),
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.TwoPair &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required two pairs were not found when they existed");
        }

        /// <summary>
        /// Test if finding a two of a kind works
        /// </summary>
        [TestMethod]
        public void TestFindingOnePair()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Spades),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 2, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>()
            {
                new Card(Card.Back, 14, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Diamonds),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.OnePair &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required one pair was not found when it existed");
        }

        /// <summary>
        /// Test if finding a high card works
        /// </summary>
        [TestMethod]
        public void TestFindingHighCard()
        {
            this.Game.Deck.RemoveAllCardsOnBoard(this.Game.Player, this.Game.Bots);
            this.Game.Player.Cards = new List<Card>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Spades),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 3, Suit.Diamonds),
                new Card(Card.Back, 5, Suit.Spades),
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Hearts),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            var cardsRequired = new List<Card>()
            {
                new Card(Card.Back, 14, Suit.Spades),
            };
            Assert.IsTrue(
                this.Game.Player.CurrentHand.HandPower == Power.HighCard &&
                this.CardHandsAreEqual(this.Game.Player.CurrentHand.Cards, cardsRequired),
                "The required high card was not found when it existed");
        }

        /// <summary>
        /// Determines if 2 hands are the same
        /// </summary>
        /// <param name="hand1">The first hand we are comparing</param>
        /// <param name="hand2">The second hand we are comparing</param>
        /// <returns>True if the 2 hands are the same and false if they are not</returns>
        private bool CardHandsAreEqual(List<Card> hand1, List<Card> hand2)
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
