namespace PokerTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Cards;

    /// <summary>
    /// Use for testing the <see cref="Card"/> class and their properties
    /// </summary>
    [TestClass]
    public class CardTest
    {
        /// <summary>
        /// Test if a card with a power below 1 can be created. If it can't it expects to throw an argument exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The card power cannot be below 1")]
        public void TestCreateCardWithNegativePowerThrowsException()
        {
            var card = new Card(null, -1, Suit.Clubs);
        }

        /// <summary>
        /// Test if a card with a power above 14 can be created. If it can't it expects to throw an argument exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The card power cannot be above 14")]
        public void TestCreateCardWithPowerGreaterThanAceThrowsException()
        {
            var card = new Card(null, 15, Suit.Clubs);
        }
    }
}