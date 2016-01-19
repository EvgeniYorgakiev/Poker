namespace PokerTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Cards;
    using Poker.Constants;
    using Poker.Forms;
    using Poker.Interfaces;

    /// <summary>
    /// Use for testing the <see cref="Deck"/> class and the methods inside of it
    /// </summary>
    [TestClass]
    public class DeckTest
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
        /// Test if a the deck shuffles correctly
        /// </summary>
        [TestMethod]
        public void TestShuffleDeck()
        {
            var deck = new Deck(GlobalConstants.CardPathFromUnitTest);
            deck.ShuffleCards();
            var deckOrdered = new Deck(GlobalConstants.CardPathFromUnitTest);
            Assert.AreNotEqual(deck.Cards, deckOrdered.Cards);
        }

        /// <summary>
        /// Test if 1 card is owned by 2 different players or is in a player hand and the center at the same time.
        /// </summary>
        [TestMethod]
        public void TestThrowCardsContainsDuplicates()
        {
            bool duplicatesFound = false;
            for (int i = 0; i < 5; i++)
            {
                this.Game.Deck.ThrowCards(this.Game.Player, this.Game.Bots, false);
                int numberOfCards = 0;
                HashSet<ICard> cards = new HashSet<ICard>();
                for (int j = 0; j < this.Game.Player.Cards.Count; j++)
                {
                    numberOfCards++;
                    cards.Add(this.Game.Player.Cards[j]);
                }

                for (int j = 0; j < this.Game.Bots.Count; j++)
                {
                    for (int c = 0; c < this.Game.Bots[j].Cards.Count; c++)
                    {
                        numberOfCards++;
                        cards.Add(this.Game.Bots[j].Cards[c]);
                    }
                }

                for (int j = 0; j < this.Game.Deck.NeutalCards.Length; j++)
                {
                    numberOfCards++;
                    cards.Add(this.Game.Deck.NeutalCards[j]);
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
