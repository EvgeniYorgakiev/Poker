using System.Runtime.Remoting.Messaging;

namespace Poker.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Cards;
    using Forms;

    [TestClass]
    public class DeckTest
    {
        private const string CardPathFromUnitTest = "..\\..\\..\\Poker\\Resources\\Cards";
        private const string CardBackForUnitTesting = "..\\..\\..\\Poker\\Resources\\Back\\Back.png";
        private Game game = new Game(CardPathFromUnitTest);

        [TestInitialize]
        public void Initialize()
        {
            Card.CardBack = CardBackForUnitTesting;
        }

        [TestMethod]
        public void ShuffleDeck()
        {
            var deck = new Deck(CardPathFromUnitTest);
            deck.ShuffleCards();
            var deckOrdered = new Deck(CardPathFromUnitTest);
            Assert.AreNotEqual(deck.Cards, deckOrdered.Cards);
        }

        [TestMethod]
        public void ThrowCardsContainsDuplicates()
        {
            bool duplicatesFound = false;
            for (int i = 0; i < 5; i++)
            {
                game.Deck.ThrowCards(game.Player, game.Bots, false);
                int numberOfCards = 0;
                HashSet<Card> cards = new HashSet<Card>();
                for (int j = 0; j < game.Player.Cards.Count; j++)
                {
                    numberOfCards++;
                    cards.Add(game.Player.Cards[j]);
                }

                for (int j = 0; j < game.Bots.Count; j++)
                {
                    for (int c = 0; c < game.Bots[j].Cards.Count; c++)
                    {
                        numberOfCards++;
                        cards.Add(game.Bots[j].Cards[c]);
                    }
                }

                for (int j = 0; j < game.Deck.NeutalCards.Length; j++)
                {
                    numberOfCards++;
                    cards.Add(game.Deck.NeutalCards[j]);
                }

                if (cards.Count != numberOfCards)
                {
                    duplicatesFound = true;
                    break;
                }
            }
            Assert.IsFalse(duplicatesFound, "Duplicates have been found. The same card is owned by 2 different players or is in the center");
        }
    }
}
