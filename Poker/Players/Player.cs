﻿namespace Poker.Players
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Cards;

    /// <summary>
    /// The player class. Can be inherited
    /// </summary>
    public abstract class Player
    {
        private const string ChipsText = "Chips: ";

        private List<Card> cards;
        private Point cardStartingPoint;
        private Point cardDistanceFromEachother;
        private int chips;
        private Label status;
        private TextBox chipsTextBox;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class
        /// </summary>
        /// <param name="cardStartingPoint">The starting position of the player's cards</param>
        /// <param name="cardDistanceFromEachother">The offset of each card from the previous</param>
        /// <param name="chips">The number of chips the player will start with</param>
        /// <param name="status">The status label for the player</param>
        /// <param name="chipsTextBox">The textbox that will be showing the number of chips</param>
        protected Player(Point cardStartingPoint, Point cardDistanceFromEachother, int chips, Label status, TextBox chipsTextBox)
        {
            this.CardStartingPoint = cardStartingPoint;
            this.CardDistanceFromEachother = cardDistanceFromEachother;
            this.Cards = new List<Card>();
            this.Status = status;
            this.ChipsTextBox = chipsTextBox;
            this.Chips = chips;
        }

        /// <summary>
        /// The player's cards. Should be 2 on Texas Hold them
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
        /// The player's first card's position
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
        public Point CardDistanceFromEachother
        {
            get
            {
                return this.cardDistanceFromEachother;
            }

            set
            {
                this.cardDistanceFromEachother = value;
            }
        }

        /// <summary>
        /// The chips the player currently owns
        /// </summary>
        public int Chips
        {
            get
            {
                return this.chips;
            }

            set
            {
                this.ChipsTextBox.Text = ChipsText + value;
                this.chips = value;
            }
        }

        /// <summary>
        /// The player's label
        /// </summary>
        public Label Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
            }
        }

        /// <summary>
        /// The player's chip's text box
        /// </summary>
        public TextBox ChipsTextBox
        {
            get
            {
                return this.chipsTextBox;
            }

            set
            {
                this.chipsTextBox = value;
            }
        }
    }
}