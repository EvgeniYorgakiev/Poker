namespace PokerTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Cards;
    using Poker.Constants;
    using Poker.Factories;
    using Poker.Forms;
    using Poker.Interfaces;

    /// <summary>
    /// Use for testing the <see cref="WinningHandFactory"/> class
    /// </summary>
    [TestClass]
    public class WinningHandTest
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
        /// Test if a contested win with 1 high card is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithHighCard()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 2, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[3].Fold();
            this.Game.Bots[4].Fold();
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Spades),
                new Card(Card.Back, 6, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[2]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners),
                "Winner with a high card was determined incorrectly.");
        }

        /// <summary>
        /// Test multiple winners if they all have a high card
        /// </summary>
        [TestMethod]
        public void TestMultipleWinners()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[3].Fold();
            this.Game.Bots[4].Fold();
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Spades),
                new Card(Card.Back, 6, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[0]);
            expectedWinners.Add(this.Game.Bots[1]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners), 
                "Winner with a high card was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with 1 pair is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithOnePair()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 2, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[3].Fold();
            this.Game.Bots[4].Fold();
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Spades),
                new Card(Card.Back, 6, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[2]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners), 
                "Winner with a one pair was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with 2 pairs is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithTwoPairs()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[3].Fold();
            this.Game.Bots[4].Fold();
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Spades),
                new Card(Card.Back, 6, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[1]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners), 
                "Winner with two pairs was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with 3 of a kind is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithThreeOfAKind()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[3].Fold();
            this.Game.Bots[4].Fold();
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Spades),
                new Card(Card.Back, 6, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Player);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners),
                "Winner with three of a kind was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning straight at center is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithStraight()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[4].Fold();
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Spades),
                new Card(Card.Back, 6, Suit.Hearts),
                new Card(Card.Back, 7, Suit.Hearts),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[3]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners), 
                "Winner with straight was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning straight with carrying is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithStraightAndCarrying()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 9, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 6, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Spades),
                new Card(Card.Back, 6, Suit.Hearts),
                new Card(Card.Back, 5, Suit.Hearts),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[4].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[4]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners), 
                "Winner with straight using carrying over was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with straight at center is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithStraightAtCenter()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 12, Suit.Clubs),
                new Card(Card.Back, 13, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 11, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Spades),
                new Card(Card.Back, 4, Suit.Hearts),
                new Card(Card.Back, 5, Suit.Hearts),
                new Card(Card.Back, 6, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[4].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[1]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners),
                "Winner with straight at center of the table was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with flush is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithFlush()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 12, Suit.Diamonds),
                new Card(Card.Back, 13, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Diamonds),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 11, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Diamonds),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Spades),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[4].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Player);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners),
                "Winner with flush was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with full house is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithFullHouse()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 12, Suit.Diamonds),
                new Card(Card.Back, 13, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Diamonds),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 11, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Diamonds),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Spades),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[4].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[0]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners),
                "Winner with full house was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with four of a kind is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithFourOfAKind()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 12, Suit.Diamonds),
                new Card(Card.Back, 13, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 11, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Diamonds),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Spades),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 4, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[4].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[2]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners),
                "Winner with four of a kind was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with straight flush is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithStraightFlush()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 8, Suit.Clubs),
                new Card(Card.Back, 9, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 12, Suit.Diamonds),
                new Card(Card.Back, 13, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Spades),
                new Card(Card.Back, 4, Suit.Clubs),
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 6, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[4].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Bots[4]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners),
                "Winner with straight flush was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with royal flush is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithRoyalFlush()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 10, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 12, Suit.Diamonds),
                new Card(Card.Back, 13, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Spades),
                new Card(Card.Back, 12, Suit.Clubs),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[4].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Player);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners),
                "Winner with royal flush was determined incorrectly");
        }

        /// <summary>
        /// Test if a winning with multiple royal flush is correct
        /// </summary>
        [TestMethod]
        public void TestWinnerWithMultipleRoyalFlush()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[1].Cards = new List<ICard>
            {
                new Card(Card.Back, 12, Suit.Diamonds),
                new Card(Card.Back, 13, Suit.Clubs),
            };
            this.Game.Bots[2].Cards = new List<ICard>
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Bots[3].Cards = new List<ICard>
            {
                new Card(Card.Back, 10, Suit.Diamonds),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 2, Suit.Clubs),
                new Card(Card.Back, 3, Suit.Clubs),
            };
            this.Game.Deck.NeutalCards = new[]
            {
                new Card(Card.Back, 2, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Spades),
                new Card(Card.Back, 12, Suit.Clubs),
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[1].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[2].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[4].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Player);
            expectedWinners.Add(this.Game.Bots[0]);
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners),
                "Winner with multiple royal flush was determined incorrectly");
        }

        /// <summary>
        /// Determines if 2 sequences of winners have the same players
        /// </summary>
        /// <param name="winners">The winners determined by the code</param>
        /// <param name="expectedWinners">The expectedWinners</param>
        /// <returns>Returns true if the winners are the same players</returns>
        private bool WinnersAreTheSame(List<IPlayer> winners, List<IPlayer> expectedWinners)
        {
            if (winners.Count != expectedWinners.Count)
            {
                return false;
            }

            for (int i = 0; i < winners.Count; i++)
            {
                if (!winners.Contains(expectedWinners[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}