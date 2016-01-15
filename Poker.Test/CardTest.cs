namespace Poker.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Cards;


    [TestClass]
    public class CardTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The card power cannot be below 1")]
        public void CreateCardWithNegativePowerThrowsException()
        {
            var card = new Card(null, -1, Suit.Clubs);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The card power cannot be above 14")]
        public void CreateCardWithPowerGreaterThanAceThrowsException()
        {
            var card = new Card(null, 15, Suit.Clubs);
        }
    }
}
