using System.Threading.Tasks;

namespace Poker.Cards
{
    using System;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.IO;
    using Constants;
    using System.Collections.Generic;
    using Players.Bots;
    using Players.Humans;
    using Players;
    using System.Windows.Forms;
    using Forms;

    /// <summary>
    /// Represents a 52 card deck of the standard poker game
    /// </summary>
    public class Deck
    {
        private Card[] cards = new Card[Constant.DeckSize];

        public Deck()
        {
            var imageLocations = Directory.GetFiles(Constant.CardPath, "*" + Constant.CardExtension,
                SearchOption.TopDirectoryOnly);
            for (int i = 0; i < Cards.Length; i++)
            {
                int cardPower = this.GetCardPowerFromFileName(imageLocations[i]);
                Suit cardSuit = this.GetCardSuitFromFileName(imageLocations[i]);
                this.Cards[i] = new Card(Image.FromFile(imageLocations[i]), cardPower, cardSuit);
                this.Cards[i].PictureBox.Name = "pictureBox " + cardPower + " of " + cardSuit;
            }
        }

        /// <summary>
        /// Represents an array of all 52 cards in the standard deck
        /// </summary>
        public Card[] Cards
        {
            get
            {
                return this.cards;
            }
            set
            {
                this.cards = value;
            }
        }

        /// <summary>
        /// Retrieves the suit of card using the specified file name
        /// </summary>
        /// <param name="fileName">The file name of the current card</param>
        /// <returns>An enum of the suit</returns>
        private Suit GetCardSuitFromFileName(string fileName)
        {
            var matches = Regex.Matches(fileName, @"(?<=[_-]of[_-]).*(?=\.)");
            string suitAsString = matches[matches.Count - 1].Value;
            suitAsString = this.CapitalizeFirstLetter(suitAsString);
            Suit suitFromFileName = (Suit)Enum.Parse(typeof(Suit), suitAsString);

            return suitFromFileName;
        }

        /// <summary>
        /// Returns the input string with a capital letter
        /// </summary>
        /// <param name="inputString">The string we wish to capitalize.</param>
        /// <returns>The input string with a capital letter leaving the others intact</returns>
        private string CapitalizeFirstLetter(string inputString)
        {
            string stringWithCapitalLetter = inputString[0].ToString().ToUpper() + inputString.Substring(1);

            return stringWithCapitalLetter;
        }

        /// <summary>
        /// Retrieves the power of card using the specified file name
        /// </summary>
        /// <param name="fileName">The file name of the current card</param>
        /// <returns>The power of the card as an integer with 11 being Jack, 12 Queen, 13 King and 1 Ace</returns>
        private int GetCardPowerFromFileName(string fileName)
        {
            int lastSlashIndex = fileName.LastIndexOf('\\');
            string lastPartOfFileName = fileName.Substring(lastSlashIndex);
            string powerAsString = Regex.Match(lastPartOfFileName, @"(?<=\\)[0-9]*(?=[_-])").Value;
            int power = int.Parse(powerAsString);

            return power;
        }

        /// <summary>
        /// Shuffles the deck for random cards.
        /// </summary>
        private void ShuffleCards()
        {
            Random random = new Random();
            for (int i = 0; i < Constant.ShuffleTimes; i++)
            {
                int randomCardIndex1 = random.Next(0, this.Cards.Length);
                int randomCardIndex2 = random.Next(0, this.Cards.Length);
                SwapCards(randomCardIndex1, randomCardIndex2);
            }
        }

        /// <summary>
        /// Swaps 2 cards using their indexes in the deck
        /// </summary>
        /// <param name="card1Index">The index of the first card we want to swap in the deck</param>
        /// <param name="card2Index">The index of the second card we want to swap in the deck</param>
        private void SwapCards(int card1Index, int card2Index)
        {
            var oldCard = this.Cards[card1Index];
            this.Cards[card1Index] = this.Cards[card2Index];
            this.Cards[card2Index] = oldCard;
        }

        /// <summary>
        /// Throw the specified number of cards to the player.
        /// </summary>
        /// <param name="player">The player we wish to give cards to.</param>
        /// <param name="startingCardIndexInDeck">The starting index of the cards in the deck we will give to the player.</param>
        private async void ThrowPlayerCard(Player player, int startingCardIndexInDeck)
        {
            //We use a starting card index in the deck instead of random so that we don't end up giving the same card to 2 players
            for (int i = 0; i < Constant.NumberOfCardsPerPlayer; i++)
            {
                await Task.Delay(200);
                GivePlayerCard(player, startingCardIndexInDeck);
            }
        }

        /// <summary>
        /// Give the player a card
        /// </summary>
        /// <param name="player">The player we wish to give cards to.</param>
        /// <param name="startingCardIndexInDeck">The starting index of the cards in the deck we will give to the player.</param>
        private void GivePlayerCard(Player player, int startingCardIndexInDeck)
        {
            int numberOfCardForPlayer = player.Cards.Count;
            int cardIndex = startingCardIndexInDeck + numberOfCardForPlayer;
            var currentCard = this.Cards[cardIndex];
            currentCard.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            currentCard.PictureBox.Height = 130;
            currentCard.PictureBox.Width = 80;
            currentCard.IsVisible = true;
            if (player is Bot)
            {
                currentCard.IsVisible = false;
                currentCard.PictureBox.Image = Card.Back;
            }
            currentCard.PictureBox.Location = new Point(
                player.CardStartingPoint.X + numberOfCardForPlayer * player.CardOffsetFromEachother.X,
                player.CardStartingPoint.Y + numberOfCardForPlayer * player.CardOffsetFromEachother.Y);
            Game.Instance.Controls.Add(currentCard.PictureBox);
            player.Cards.Add(currentCard);
        }

        /// <summary>
        /// Throw everybody's cards for a new game
        /// </summary>
        public async void ThrowCards(HumanPlayer player, List<Bot> bots)
        {
            this.ShuffleCards();
            this.ThrowPlayerCard(player, 0);
            for (int i = 0; i < bots.Count ; i++)
            {
                await Task.Delay(300);
                this.ThrowPlayerCard(bots[i], (i + 1) * Constant.NumberOfCardsPerPlayer);
            }
        }
    }
}

