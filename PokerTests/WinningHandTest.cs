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
        public void Test_WinnerWithHighCard_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with a high card was determined incorrectly. Expected winner Bots 2 with hand " +
                this.Game.Bots[2].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[2].CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test multiple winners if they all have a high card
        /// </summary>
        [TestMethod]
        public void Test_MultipleWinners_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultsText =
                "Winner with a high card was determined incorrectly. Expected winner Bots 0 and Bot 1 with hand " +
                this.Game.Bots[0].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[0].CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultsText);
        }

        /// <summary>
        /// Test if a winning with 1 pair is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithOnePair_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with a one pair was determined incorrectly. Expected winner Bots 2 with hand " +
                this.Game.Bots[2].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[2].CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }
        /// <summary>
        /// Test if a winning with 2 pairs is correct when 2 people have the same higher pair
        /// </summary>
        [TestMethod]
        public void Test_ConstestedWinnerWithOnePair_With2DrawnCardsAnd5Center()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 5, Suit.Clubs),
                new Card(Card.Back, 10, Suit.Clubs),
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
                new Card(Card.Back, 10, Suit.Clubs),
                new Card(Card.Back, 12, Suit.Clubs),
            };
            this.Game.Bots[4].Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 14, Suit.Clubs),
            };
            this.Game.Bots[0].Fold();
            this.Game.Bots[1].Fold();
            this.Game.Bots[2].Fold();
            this.Game.Bots[4].Fold();
            this.Game.Deck.NeutalCards = new ICard[]
            {
                new Card(Card.Back, 4, Suit.Diamonds),
                new Card(Card.Back, 6, Suit.Spades),
                new Card(Card.Back, 11, Suit.Hearts),
                new Card(Card.Back, 9, Suit.Hearts),
                new Card(Card.Back, 11, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[3].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Player);
            string expectedResultText =
                "Winner with two pairs was determined incorrectly. Expected winner Player with hand " +
                this.Game.Player.CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Player.CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with 2 pairs is correct when 2 people have the same higher pair
        /// </summary>
        [TestMethod]
        public void Test_ConstestedWinnerWithTwoPairs_With2DrawnCardsAnd5Center()
        {
            this.Game.Player.Cards = new List<ICard>
            {
                new Card(Card.Back, 13, Suit.Clubs),
                new Card(Card.Back, 13, Suit.Clubs),
            };
            this.Game.Bots[0].Cards = new List<ICard>
            {
                new Card(Card.Back, 7, Suit.Clubs),
                new Card(Card.Back, 7, Suit.Clubs),
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
            this.Game.Bots[1].Fold();
            this.Game.Bots[2].Fold();
            this.Game.Bots[3].Fold();
            this.Game.Bots[4].Fold();
            this.Game.Deck.NeutalCards = new ICard[]
            {
                new Card(Card.Back, 3, Suit.Diamonds),
                new Card(Card.Back, 3, Suit.Spades),
                new Card(Card.Back, 14, Suit.Hearts),
                new Card(Card.Back, 5, Suit.Hearts),
                new Card(Card.Back, 8, Suit.Clubs),
            };
            this.Game.Player.DetermineHandPower(this.Game.Deck.NeutalCards);
            this.Game.Bots[0].DetermineHandPower(this.Game.Deck.NeutalCards);
            var winners = this.Game.DetermineWinner();
            var expectedWinners = new List<IPlayer>();
            expectedWinners.Add(this.Game.Player);
            string expectedResultText =
                "Winner with two pairs was determined incorrectly. Expected winner Player with hand " +
                this.Game.Player.CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Player.CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with 2 pairs is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithTwoPairs_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with two pairs was determined incorrectly. Expected winner Bots 1 with hand " +
                this.Game.Bots[1].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[1].CurrentHand.Cards) + "cards";
            Assert.IsTrue(
                this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with 3 of a kind is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithThreeOfAKind_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with three of a kind was determined incorrectly. Expected winner Player with hand " +
                this.Game.Player.CurrentHand.HandPower + " and " + string.Join(" ", this.Game.Player.CurrentHand.Cards) +
                "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning straight at center is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithStraigh_With2DrawnCardsAnd5Centert()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with straight was determined incorrectly. Expected winner Bots 3 with hand " +
                this.Game.Bots[3].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[3].CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning straight with carrying is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithStraightAndCarrying_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with straight using carrying over was determined incorrectly. Expected winner Bots 4 with hand " +
                this.Game.Bots[4].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[4].CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with straight at center is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithStraightAtCenter_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with straight at center of the table was determined incorrectly. Expected winner Bots 1 with hand " +
                this.Game.Bots[1].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[1].CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with flush is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithFlus_With2DrawnCardsAnd5Centerh()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with flush was determined incorrectly. Expected winner Player with hand " +
                this.Game.Player.CurrentHand.HandPower + " and " + string.Join(" ", this.Game.Player.CurrentHand.Cards) +
                "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with full house is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithFullHouse_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with full house was determined incorrectly. Expected winner Bot 0 with hand " +
                this.Game.Bots[0].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[0].CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with four of a kind is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithFourOfAKind_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with four of a kind was determined incorrectly. Expected winner Bot 2 with hand " +
                this.Game.Bots[2].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[2].CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with straight flush is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithStraightFlush_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with straight flush was determined incorrectly. Expected winner Bot 4 with hand " +
                this.Game.Bots[4].CurrentHand.HandPower + " and " +
                string.Join(" ", this.Game.Bots[4].CurrentHand.Cards) + "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with royal flush is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithRoyalFlush_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with royal flush was determined incorrectly. Expected winner Player with hand " +
                this.Game.Player.CurrentHand.HandPower + " and " + string.Join(" ", this.Game.Player.CurrentHand.Cards) +
                "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
        }

        /// <summary>
        /// Test if a winning with multiple royal flush is correct
        /// </summary>
        [TestMethod]
        public void Test_WinnerWithMultipleRoyalFlush_With2DrawnCardsAnd5Center()
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
            this.Game.Deck.NeutalCards = new ICard[]
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
            string expectedResultText =
                "Winner with multiple royal flush was determined incorrectly. Expected winners Player and Bot 1 with hand " +
                this.Game.Player.CurrentHand.HandPower + " and " + string.Join(" ", this.Game.Player.CurrentHand.Cards) +
                "cards";
            Assert.IsTrue(this.WinnersAreTheSame(winners, expectedWinners), expectedResultText);
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