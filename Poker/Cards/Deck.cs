using Poker.Constants;

namespace Poker.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Forms;
    using Players;
    using Players.Bots;
    using Players.Humans;

    /// <summary>
    /// Represents a 52 card deck of the standard poker game
    /// </summary>
    public class Deck
    {
        private const string CardExtension = ".png";

        private const int DeckSize = 52;
        private const int NeutralCardsNumber = 5;
        private const int ShuffleTimes = 10 * 52;
        private const int NumberOfCardsPerPlayer = 2;
        private const int NumberOfPlayers = 6;

        private const int StartingXPositionForCenterCards = 475;
        private const int StartingYPositionForCenterCards = 270;
        private const int CardDistanceX = 100;
        private const int CardDistanceY = 0;
        private const int DefaultCardHeight = 110;
        private const int DefaultCardWidth = (int)(DefaultCardHeight / 1.625);

        private Card[] cards = new Card[DeckSize];
        private Card[] neutralCards = new Card[NeutralCardsNumber];

        /// <summary>
        /// Initializes a new instance of the <see cref="Deck"/> class
        /// </summary>
        public Deck() : this(GlobalConstants.CardPath)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Deck"/> class. Used for unit testing
        /// </summary>
        public Deck(string path)
        {
            var imageLocations = Directory.GetFiles(
                path,
                "*" + CardExtension,
                SearchOption.TopDirectoryOnly);
            for (int i = 0; i < this.Cards.Length; i++)
            {
                int cardPower = this.GetCardPowerFromFileName(imageLocations[i]);
                Suit cardSuit = this.GetCardSuitFromFileName(imageLocations[i]);
                this.Cards[i] = new Card(Image.FromFile(imageLocations[i]), cardPower, cardSuit)
                {
                    PictureBox =
                    {
                        Name = "pictureBox " + cardPower + " of " + cardSuit
                    }
                };
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
        /// Represents an array of the 5 cards that are in the center of the table
        /// </summary>
        public Card[] NeutalCards
        {
            get
            {
                return this.neutralCards;
            }

            set
            {
                this.neutralCards = value;
            }
        }

        /// <summary>
        /// Throw everybody's cards for a new game
        /// </summary>
        /// <param name="player">The human player in the game</param>
        /// <param name="bots">All of the bots that the player will be facing</param>
        /// <param name="wait">If the method should wait for visual effects</param>
        public async void ThrowCards(HumanPlayer player, List<Bot> bots, bool wait = true)
        {
            Game.Instance.FixCall();

            Game.Instance.EnableButtons(false, false, false, false);

            this.ShuffleCards();

            this.RemoveAllCardsOnBoard(player, bots);

            this.ThrowPlayerCard(player, 0, wait);

            for (int i = 0; i < bots.Count; i++)
            {
                if (bots[i].Chips > 0)
                {
                    if (wait)
                    {
                        await Task.Delay(300);
                    }
                    this.ThrowPlayerCard(bots[i], (i + 1) * NumberOfCardsPerPlayer, wait);
                }
            }

            await this.ThrowCenterCards(wait);

            Game.Instance.EnableButtons(true, false, true, true);
        }

        /// <summary>
        /// Shuffles the deck for random cards.
        /// </summary>
        public void ShuffleCards()
        {
            Random random = new Random();
            for (int i = 0; i < ShuffleTimes; i++)
            {
                int randomCardIndex1 = random.Next(0, this.Cards.Length);
                int randomCardIndex2 = random.Next(0, this.Cards.Length);
                this.SwapCards(randomCardIndex1, randomCardIndex2);
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
        /// <param name="wait">If the method should wait for visual effects</param>
        private async void ThrowPlayerCard(Player player, int startingCardIndexInDeck, bool wait = true)
        {
            this.RemovePlayerCards(player);
            player.HasFolded = false;
            ////We use a starting card index in the deck instead of random so that we don't end up giving the same card to 2 players
            for (int i = 0; i < NumberOfCardsPerPlayer; i++)
            {
                if (wait)
                {
                    await Task.Delay(200);
                }
                this.GivePlayerCard(player, startingCardIndexInDeck);
            }
        }

        /// <summary>
        /// Throw the center cards.
        /// </summary>
        /// <param name="wait">If the method should wait for visual effects</param>
        private async Task ThrowCenterCards(bool wait = true)
        {
            int cardIndex = NumberOfCardsPerPlayer * NumberOfPlayers;
            Point locationOfFirstCard = new Point(StartingXPositionForCenterCards, StartingYPositionForCenterCards);
            Point distanceBetweenCards = new Point(CardDistanceX, CardDistanceY);
            for (int i = 0; i < this.NeutalCards.Length; i++)
            {
                if (wait)
                {
                    await Task.Delay(200);
                }
                this.NeutalCards[i] = this.NewCard(cardIndex + i, i, locationOfFirstCard, distanceBetweenCards, false);
            }
        }

        /// <summary>
        /// Removes the player's currently held cards making room for another hand
        /// </summary>
        /// <param name="player">The player that will receive new cards</param>
        /// <param name="bots">The bots the player will be facing</param>
        private void RemoveAllCardsOnBoard(HumanPlayer player, List<Bot> bots)
        {
            for (int i = 0; i < this.NeutalCards.Length; i++)
            {
                if (this.NeutalCards[i] != null)
                {
                    Game.Instance.Controls.Remove(this.NeutalCards[i].PictureBox);
                }
            }

            this.RemovePlayerCards(player);
            for (int i = 0; i < bots.Count; i++)
            {
                this.RemovePlayerCards(bots[i]);
            }
        }

        /// <summary>
        /// Removes the player's currently held cards making room for another hand
        /// </summary>
        /// <param name="player">The player that will receive new cards</param>
        private void RemovePlayerCards(Player player)
        {
            for (int i = 0; i < player.Cards.Count; i++)
            {
                player.Cards[i].PictureBox.Visible = true;
                Game.Instance.Controls.Remove(player.Cards[i].PictureBox);
            }

            player.Cards = new List<Card>();
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
            bool isFacingUp;
            if (player is Bot)
            {
                isFacingUp = false;
            }
            else
            {
                isFacingUp = true;
            }

            var currentCard = this.NewCard(
                cardIndex, 
                numberOfCardForPlayer,
                player.CardStartingPoint,
                player.CardDistanceFromEachother,
                isFacingUp);
            player.Cards.Add(currentCard);
        }

        /// <summary>
        /// Creates a new card on the board
        /// </summary>
        /// <param name="cardIndex">The index of the card in the array</param>
        /// <param name="numberOfCard">The number of the card in the row it is</param>
        /// <param name="location">The location of the first card in the row</param>
        /// <param name="distance">The distance between the cards</param>
        /// <param name="isFacingUp">If the card is facing up or down</param>
        /// <returns>A new card with all of it's necessary fields set.</returns>
        private Card NewCard(int cardIndex, int numberOfCard, Point location, Point distance, bool isFacingUp)
        {
            var currentCard = this.Cards[cardIndex];
            currentCard.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            currentCard.PictureBox.Height = DefaultCardHeight;
            currentCard.PictureBox.Width = DefaultCardWidth;
            currentCard.PictureBox.Location = new Point(
                location.X + (numberOfCard * distance.X),
                location.Y + (numberOfCard * distance.Y));
            if (isFacingUp)
            {
                currentCard.PictureBox.Image = currentCard.Front;
            }
            else
            {
                currentCard.PictureBox.Image = Card.Back;
            }

            Game.Instance.Controls.Add(currentCard.PictureBox);

            return currentCard;
        }
    }
}