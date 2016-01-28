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
                Game.Deck.ThrowCards(Game.Player, Game.Bots, false);
                int numberOfCards = 0;
                HashSet<ICard> cards = new HashSet<ICard>();
                for (int j = 0; j < Game.Player.Cards.Count; j++)
                {
                    numberOfCards++;
                    cards.Add(Game.Player.Cards[j]);
                }

                for (int j = 0; j < Game.Bots.Count; j++)
                {
                    for (int c = 0; c < Game.Bots[j].Cards.Count; c++)
                    {
                        numberOfCards++;
                        cards.Add(Game.Bots[j].Cards[c]);
                    }
                }

                for (int j = 0; j < Game.Deck.NeutalCards.Length; j++)
                {
                    numberOfCards++;
                    cards.Add(Game.Deck.NeutalCards[j]);
                }

                if (cards.Count != numberOfCards)
                {
                    duplicatesFound = true;
                    break;
                }
            }

            Assert.IsFalse(duplicatesFound, "Duplicates have been found. The same card is owned by 2 different players or is in the center");
        }

        /// <summary>
        /// Test if revealing the cards works as intended.
        /// </summary>
        [TestMethod]
        public void TestRevealCards()
        {
            Game.Deck.RevealCards(Game.Deck.NeutalCards, 0, Game.Deck.NeutalCards.Length);
            bool atleastOneCardIsHidden = false;
            for (int i = 0; i < Game.Deck.NeutalCards.Length; i++)
            {
                if (Game.Deck.NeutalCards[i].PictureBox.Image != Game.Deck.NeutalCards[i].Front)
                {
                    atleastOneCardIsHidden = true;
                }
            }

            Assert.IsFalse(atleastOneCardIsHidden, "There is a card hidden when all of the cards are supposed to be revealed");
        }
    }
}
