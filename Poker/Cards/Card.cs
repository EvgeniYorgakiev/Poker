namespace Poker.Cards
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Interfaces;

    /// <summary>
    /// Represents a playing card
    /// </summary>
    public class Card : ICard
    {
        private static string cardBackPathPath = "..\\..\\Resources\\Back\\Back.png";
        private static Image back;
        private Image front;
        private PictureBox pictureBox;
        private int power;
        private Suit suit;

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class
        /// </summary>
        /// <param name="image">A windows forms image of the card</param>
        /// <param name="power">The power of the card being from 1-14 with 1 and 14 being Ace</param>
        /// <param name="suit">The suit of the card</param>
        public Card(Image image, int power, Suit suit)
        {
            this.PictureBox = new PictureBox();
            this.Front = image;
            this.PictureBox.Image = this.Front;
            this.Power = power;
            this.Suit = suit;
        }

        /// <summary>
        /// The path for the image of the back of the card
        /// </summary>
        public static string CardBackPath
        {
            get
            {
                return cardBackPathPath;
            }

            set
            {
                cardBackPathPath = value;
            }
        }

        /// <summary>
        /// The back of the card
        /// </summary>
        public static Image Back
        {
            get
            {
                if (back == null)
                {
                    back = GetBackOfCard();
                }

                return back;
            }
        }

        /// <summary>
        /// The front of the card
        /// </summary>
        public Image Front
        {
            get
            {
                return this.front;
            }

            private set
            {
                this.front = value;
            }
        }

        /// <summary>
        /// The image the card uses. Use picture box so that we have access to more elements like location for the image.
        /// </summary>
        public PictureBox PictureBox
        {
            get
            {
                return this.pictureBox;
            }

            set
            {
                this.pictureBox = value;
            }
        }

        /// <summary>
        /// The power of the card with 11 being Jack, 12 Queen, 13 King and 14 Ace.
        /// </summary>
        public int Power
        {
            get
            {
                return this.power;
            }

            set
            {
                if (value == 1)
                {
                    this.power = 14;
                }
                else if (value < 1 || value > 14)
                {
                    throw new ArgumentException(
                        "Invalid power for the card. It must be between 1 and 14. 1 and 14 are considered the same");
                }
                else
                {
                    this.power = value;
                }
            }
        }

        /// <summary>
        /// The suit of the card.
        /// </summary>
        public Suit Suit
        {
            get
            {
                return this.suit;
            }

            set
            {
                this.suit = value;
            }
        }

        /// <summary>
        /// Compares 1 card to the other using their power
        /// </summary>
        /// <param name="other">The other card we are comparing the current one with</param>
        /// <returns>An integer indicating their compare value</returns>
        public int CompareTo(ICard other)
        {
            return this.Power.CompareTo(other.Power);
        }

        public override string ToString()
        {
            string powerAsString;
            switch (this.Power)
            {
                case 11:
                    {
                        powerAsString = "Jack";
                        break;
                    }

                case 12:
                    {
                        powerAsString = "Queen";
                        break;
                    }

                case 13:
                    {
                        powerAsString = "King";
                        break;
                    }

                case 14:
                    {
                        powerAsString = "Ace";
                        break;
                    }

                default:
                    {
                        powerAsString = this.Power.ToString();
                        break;
                    }
            }

            var cardAsString = string.Format("{0} of {1}", powerAsString, this.Suit);

            return cardAsString;
        }

        /// <summary>
        /// Retrieves the back of the card
        /// </summary>
        /// <returns>Returns the image of how the back of the card looks like</returns>
        private static Image GetBackOfCard()
        {
            var image = Image.FromFile(CardBackPath);

            return image;
        }
    }
}
