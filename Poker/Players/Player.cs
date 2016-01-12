namespace Poker.Players
{
    using System.Collections.Generic;
    using Cards;
    using System.Drawing;

    /// <summary>
    /// The player class. Can be inherited
    /// </summary>
    public abstract class Player
    {
        private List<Card> cards;
        private Point cardStartingPoint;
        private Point cardOffsetFromEachother;

        protected Player(Point cardStartingPoint, Point cardOffsetFromEachother)
        {
            this.CardStartingPoint = cardStartingPoint;
            this.CardOffsetFromEachother = cardOffsetFromEachother;
            this.Cards = new List<Card>();
        }

        /// <summary>
        /// The player's cards. Should be 2 on Texas Hold 'em
        /// </summary>
        public List<Card> Cards
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
        /// The first player card's position
        /// </summary>
        public Point CardStartingPoint
        {
            get
            {
                return this.cardStartingPoint;
            }
            set
            {
                this.cardStartingPoint = value;
            }
        }

        /// <summary>
        /// The Distance between each card in the player's hand
        /// </summary>
        public Point CardOffsetFromEachother
        {
            get
            {
                return this.cardOffsetFromEachother;
            }
            set
            {
                this.cardOffsetFromEachother = value;
            }
        }
    }
}
