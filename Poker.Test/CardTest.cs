namespace Poker.Test
{
    using System;
    using Cards;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Use for testing the <see cref="ICard"/> class and their properties
    /// </summary>
    [TestClass]
    public class CardTest
    {
        /// <summary>
        /// Test if a card with a power below 1 can be created. If it can't it expects to throw an argument exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The card power cannot be below 1")]
        public void CreateCardWithNegativePowerThrowsException()
        {
            var card = new Card(null, -1, Suit.Clubs);
        }

        /// <summary>
        /// Test if a card with a power above 14 can be created. If it can't it expects to throw an argument exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The card power cannot be above 14")]
        public void CreateCardWithPowerGreaterThanAceThrowsException()
        {
            var card = new Card(null, 15, Suit.Clubs);
        }
    }
}
