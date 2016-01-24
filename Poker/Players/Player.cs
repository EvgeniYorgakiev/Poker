﻿namespace Poker.Players
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Cards.Hands;
    using Factories;
    using Forms;
    using Interfaces;

    /// <summary>
    /// The player class. Can be inherited
    /// </summary>
    public abstract class Player : IPlayer
    {
        private const string ChipsText = "Chips: ";

        private List<ICard> cards;
        private Point cardStartingPoint;
        private Point cardDistanceFromEachother;
        private int chips;
        private int currentCall;
        private bool hasFolded;
        private Hand currentHand;
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
            this.Cards = new List<ICard>();
            this.Status = status;
            this.ChipsTextBox = chipsTextBox;
            this.Chips = chips;
            this.CurrentCall = 0;
        }

        /// <summary>
        /// The player's cards. Should be 2 on Texas Hold them
        /// </summary>
        public List<ICard> Cards
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
        /// The current call of the player
        /// </summary>
        public int CurrentCall
        {
            get
            {
                return this.currentCall;
            }

            set
            {
                this.currentCall = value;
            }
        }

        /// <summary>
        /// The current power of the player's hand. For more information see <see cref="Hand"/>
        /// </summary>
        public Hand CurrentHand
        {
            get
            {
                return this.currentHand;
            }

            set
            {
                this.currentHand = value;
            }
        }

        /// <summary>
        /// If the player has folded for the current hand
        /// </summary>
        public bool HasFolded
        {
            get
            {
                return this.hasFolded;
            }

            set
            {
                this.hasFolded = value;
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
        private TextBox ChipsTextBox
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

        /// <summary>
        /// Hides the cards for the current hand and removes the player from the current hand
        /// </summary>
        public void Fold()
        {
            this.HasFolded = true;
            for (int i = 0; i < this.Cards.Count; i++)
            {
                this.Cards[i].PictureBox.Visible = false;
            }
        }

        /// <summary>
        /// Determines the power of the player's hand taking in account the center cards too.
        /// </summary>
        /// <param name="neutralCards">The cards that are in the center of the board and shared for all players</param>
        public void DetermineHandPower(ICard[] neutralCards)
        {
            var knownCards = new List<ICard>();
            for (int i = 0; i < neutralCards.Length; i++)
            {
                if (neutralCards[i] != null && neutralCards[i].PictureBox.Image == neutralCards[i].Front)
                {
                    knownCards.Add(neutralCards[i]);
                }
            }

            for (int i = 0; i < this.Cards.Count; i++)
            {
                knownCards.Add(this.Cards[i]);
            }

            this.CurrentHand = HandPowerFactory.StrongestHand(knownCards);
        }

        /// <summary>
        /// The bot raises the bet
        /// </summary>
        /// <param name="raiseValue">The value that will be used for raising</param>
        public void Raise(int raiseValue)
        {
            this.CallBlind();
            int value = raiseValue;
            if (value > this.Chips)
            {
                value = this.Chips;
            }

            Game.Instance.RaiseBet(this, value);
        }

        /// <summary>
        /// The bot calls the blind and bets money
        /// </summary>
        protected void CallBlind()
        {
            Game.Instance.CallForPlayer(this);
        }
    }
}
