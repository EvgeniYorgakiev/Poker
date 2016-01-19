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
                this.Game.Bots[0].CurrentCall = 0;
                this.Game.Call = 500;
                this.Game.Bots[0].TakeTurn(0, GlobalConstants.MaximumValueToDecideToCall - 1);
                Assert.AreEqual(this.Game.Bots[0].CurrentCall, this.Game.Call, "The bot did not call when his behaviour requested it");
            }

            /// <summary>
            /// Test if the bot will raise with 10% of his chips when he should
            /// </summary>
            [TestMethod]
            public void TestRaiseWithSmallPercentageForBot()
            {
                this.Game.Bots[0].CurrentCall = 0;
                decimal raiseValue = GlobalConstants.SmallSumRaisePercentage / 100m;
                int supposedRaise = (int)(this.Game.Bots[0].Chips * raiseValue);
                int startingCall = 500;
                this.Game.Call = startingCall;
                this.Game.Bots[0].TakeTurn(0, GlobalConstants.MaximumValueToDecideToRaiseWithSmallSum - 1);
                Assert.AreEqual(
                    this.Game.Bots[0].CurrentCall - startingCall, 
                    supposedRaise,
                    "The bot did not raise with a small value when his behaviour requested it");
            }

            /// <summary>
            /// Test if the bot will raise with 10% of his chips when he should
            /// </summary>
            [TestMethod]
            public void TestRaiseWithBigPercentageForBot()
            {
                this.Game.Bots[0].CurrentCall = 0;
                decimal raiseValue = GlobalConstants.BigSumRaisePercentage / 100m;
                int supposedRaise = (int)(this.Game.Bots[0].Chips * raiseValue);
                int startingCall = 500;
                this.Game.Call = startingCall;
                this.Game.Bots[0].TakeTurn(0, GlobalConstants.MaximumValueToDecideToFold + 1);
                Assert.AreEqual(
                    this.Game.Bots[0].CurrentCall - startingCall,
                    supposedRaise,
                    "The bot did not raise with a big value when his behaviour requested it");
            }
        }
    }
}