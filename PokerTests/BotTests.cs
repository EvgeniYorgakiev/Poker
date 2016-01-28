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
            /// Test if the bot will fold when he has a weak hand
            /// </summary>
            [TestMethod]
            public void TestFoldForBot()
            {
                Game.Deck.NeutalCards = new[]
                {
                    new Card(Card.Back, 3, Suit.Diamonds),
                    new Card(Card.Back, 5, Suit.Spades),
                    new Card(Card.Back, 6, Suit.Clubs),
                    new Card(Card.Back, 10, Suit.Hearts),
                    new Card(Card.Back, 14, Suit.Clubs),
                };
                Game.Bots[0].CurrentCall = 0;
                Game.Call = 500;
                Game.Bots[0].HasFolded = false;
                Game.Bots[0].TakeTurn(0, GlobalConstants.MaximumValueToDecideToFold - 1);
                Assert.IsTrue(Game.Bots[0].HasFolded, "The bot did not fold when his behaviour requested it");
            }

            /// <summary>
            /// Test if the bot will call when he should
            /// </summary>
            [TestMethod]
            public void TestCallForBot()
            {
                Game.Deck.NeutalCards = new[]
                {
                    new Card(Card.Back, 3, Suit.Diamonds),
                    new Card(Card.Back, 5, Suit.Spades),
                    new Card(Card.Back, 6, Suit.Clubs),
                    new Card(Card.Back, 10, Suit.Hearts),
                    new Card(Card.Back, 14, Suit.Clubs),
                };
                Game.Bots[0].CurrentCall = 0;
                Game.Bots[0].Chips = 10000;
                Game.Bots[0].HasFolded = false;
                Game.Bots[0].ActedThisTurn = false;
                Game.Call = 500;
                Game.Bots[0].TakeTurn(0, 14);
                Assert.AreEqual(Game.Bots[0].CurrentCall, Game.Call, "The bot did not call when his behaviour requested it");
            }

            /// <summary>
            /// Test if the bot will raise with 10% of his chips when he should
            /// </summary>
            [TestMethod]
            public void TestRaiseForBot()
            {
                Game.Deck.NeutalCards = new[]
                {
                    new Card(Card.Back, 3, Suit.Diamonds),
                    new Card(Card.Back, 5, Suit.Spades),
                    new Card(Card.Back, 6, Suit.Clubs),
                    new Card(Card.Back, 10, Suit.Hearts),
                    new Card(Card.Back, 14, Suit.Clubs),
                };
                Game.Bots[0].CurrentCall = 0;
                Game.Bots[0].Chips = 10000;
                int supposedRaise = 500;
                int startingCall = 500;
                Game.Call = startingCall;
                Game.Bots[0].HasFolded = false;
                Game.Bots[0].ActedThisTurn = false;
                Game.Bots[0].TakeTurn(0, 20);
                Assert.AreEqual(
                    Game.Bots[0].CurrentCall - startingCall,
                    supposedRaise,
                    "The bot did not raise with a small value when his behaviour requested it");
            }
        }
    }
}