namespace Poker.Cards
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
    using Constants;

    /// <summary>
    /// Represents a playing card
    /// </summary>
    public class Card
    {
        private PictureBox pictureBox;
        private int power;
        private Suit suit;
        private bool isVisible;
        private static Image back;

        public Card(Image image, int power, Suit suit)
        {
            this.PictureBox = new PictureBox();
            this.PictureBox.Image = image;
            this.Power = power;
            this.Suit = suit;
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
        /// If the card's face can be seen
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }

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

        private static Image GetBackOfCard()
        {
            var image = Image.FromFile(Constant.CardBack);

            return image;
        }
    }
}
