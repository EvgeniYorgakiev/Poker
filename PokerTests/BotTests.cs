namespace PokerTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Cards;
    using Poker.Constants;
    using Poker.Forms;

    public class BotTests
    {
        /// <summary>
        /// Use for testing the <see cref="Deck"/> class and the methods inside of it
        /// </summary>
        [TestClass]
        public class DeckTest
        {
            private Game game;

            public Game Game
            {
                get { return this.game; }

                private set { this.game = value; }
            }

            [TestInitialize]
            public void Initialize()
            {
                Card.CardBackPath = GlobalConstants.CardBackForUnitTesting;
                this.Game = new Game(GlobalConstants.CardPathFromUnitTest, false);
            }

            /// <summary>
            /// Test if the bot will fold when he has a weak hand
            /// </summary>
            [TestMethod]
            public void TestFoldForBot()
            {
                this.Game.Deck.NeutalCards = new[]
                {
                    new Card(Card.Back, 3, Suit.Diamonds),
                    new Card(Card.Back, 5, Suit.Spades),
                    new Card(Card.Back, 6, Suit.Clubs),
                    new Card(Card.Back, 10, Suit.Hearts),
                    new Card(Card.Back, 14, Suit.Clubs),
                };
                this.Game.Bots[0].CurrentCall = 0;
                this.Game.Call = 500;
                this.Game.Bots[0].TakeTurn(0, GlobalConstants.MaximumValueToDecideToFold - 1);
                Assert.IsTrue(this.Game.Bots[0].HasFolded, "The bot did not fold when his behaviour requested it");
            }

            /// <summary>
            /// Test if the bot will call when he should
            /// </summary>
            [TestMethod]
            public void TestCallForBot()
            {
                this.Game.Deck.NeutalCards = new[]
                {
                    new Card(Card.Back, 3, Suit.Diamonds),
                    new Card(Card.Back, 5, Suit.Spades),
                    new Card(Card.Back, 6, Suit.Clubs),
                    new Card(Card.Back, 10, Suit.Hearts),
                    new Card(Card.Back, 14, Suit.Clubs),
                };
                this.Game.Bots[0].CurrentCall = 0;
                this.Game.Bots[0].Chips = 10000;
                this.Game.Call = 500;
                this.Game.Bots[0].TakeTurn(0, 14);
                Assert.AreEqual(this.Game.Bots[0].CurrentCall, this.Game.Call, "The bot did not call when his behaviour requested it");
            }

            /// <summary>
            /// Test if the bot will raise with 10% of his chips when he should
            /// </summary>
            [TestMethod]
            public void TestRaiseWithSmallPercentageForBot()
            {
                this.Game.Deck.NeutalCards = new[]
                {
                    new Card(Card.Back, 3, Suit.Diamonds),
                    new Card(Card.Back, 5, Suit.Spades),
                    new Card(Card.Back, 6, Suit.Clubs),
                    new Card(Card.Back, 10, Suit.Hearts),
                    new Card(Card.Back, 14, Suit.Clubs),
                };
                this.Game.Bots[0].CurrentCall = 0;
                this.Game.Bots[0].Chips = 10000;
                int supposedRaise = 1000;
                int startingCall = 500;
                this.Game.Call = startingCall;
                this.Game.Bots[0].TakeTurn(0, 30);
                Assert.AreEqual(
                    this.Game.Bots[0].CurrentCall - startingCall,
                    supposedRaise,
                    "The bot did not raise with a small value when his behaviour requested it");
            }
        }
    }
}