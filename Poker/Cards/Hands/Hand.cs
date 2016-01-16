namespace Poker.Cards.Hands
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a hand of several cards and the hand's power
    /// </summary>
    public class Hand
    {
        private Power handPower;
        private List<Card> cards;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hand"/> class
        /// </summary>
        /// <param name="handPower">The <see cref="Power"/> of the hand</param>
        /// <param name="cards">The list of cards that make up the current hand</param>
        public Hand(Power handPower, List<Card> cards)
        {
            this.HandPower = handPower;
            this.Cards = cards;
        }

        public Power HandPower
        {
            get
            {
                return this.handPower;
            }

            set
            {
                this.handPower = value;
            }
        }

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
    }
}
