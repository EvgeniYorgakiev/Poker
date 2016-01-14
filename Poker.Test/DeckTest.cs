namespace Poker.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Cards;

    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void ShuffleDeck()
        {
            var deckShuffled = new Deck();
            deckShuffled.ShuffleCards();
            var deckOrdered = new Deck();
            Assert.AreNotEqual(deckShuffled.Cards, deckOrdered.Cards);
        }
    }
}
