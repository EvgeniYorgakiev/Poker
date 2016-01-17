namespace Poker.Interfaces
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Cards;

    public interface ICard : IComparable<ICard>
    {
        /// <summary>
        /// The front of the card
        /// </summary>
        Image Front { get; }

        /// <summary>
        /// The image the card uses. Use picture box so that we have access to more elements like location for the image.
        /// </summary>
        PictureBox PictureBox { get; set; }

        /// <summary>
        /// The power of the card with 11 being Jack, 12 Queen, 13 King and 14 Ace.
        /// </summary>
        int Power { get; set; }

        /// <summary>
        /// The suit of the card.
        /// </summary>
        Suit Suit { get; set; }
    }
}
