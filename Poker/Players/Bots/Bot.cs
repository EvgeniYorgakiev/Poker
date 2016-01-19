﻿namespace Poker.Players.Bots
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Constants;
    using Forms;
    using Interfaces;

    /// <summary>
    /// Represents a bot in the poker game. 
    /// </summary>
    public class Bot : Player, IBot
    {
        private const int MinimumNumberDifference = 4;
        private const int MaximumNumberDifference = 4;

        private bool raisedThisTurn;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bot"/> class
        /// </summary>
        /// <param name="cardStartingPoint">The starting position of the player's cards</param>
        /// <param name="cardDistanceFromEachother">The offset of each card from the previous</param>
        /// <param name="chips">The number of chips the player will start with</param>
        /// <param name="status">The status label for the player</param>
        /// <param name="chipsTextBox">The textbox that will be showing the number of chips</param>
        public Bot(Point cardStartingPoint, Point cardDistanceFromEachother, int chips, Label status, TextBox chipsTextBox) 
            : base(cardStartingPoint, cardDistanceFromEachother, chips, status, chipsTextBox)
        {
            this.RaisedThisTurn = false;
        }

        /// <summary>
        /// Used to determine if the bot has already raised this turn so as not to fall in infinite loop of raising
        /// </summary>
        public bool RaisedThisTurn
        {
            get
            {
                return this.raisedThisTurn;
            }

            set
            {
                this.raisedThisTurn = value;
            }
        }

        /// <summary>
        /// The bot's AI logic for every turn
        /// </summary>
        /// <param name="botIndex">the index of the bot in order to create a correct message box</param>
        /// <param name="numberInsteadOfRandom">Used for specific testing instead of a random number</param>
        public void TakeTurn(int botIndex, int numberInsteadOfRandom = GlobalConstants.DefaultNumberInsteadOfRandom)
        {
            if (this.HasFolded)
            {
                return;
            }

            if (numberInsteadOfRandom == GlobalConstants.DefaultNumberInsteadOfRandom)
            {
                MessageBox.Show(string.Format("{0} {1}'s Turn", this.GetType().Name, botIndex));
            }

            this.DetermineHandPower(Game.Instance.Deck.NeutalCards);

            int randomBehaviourNumber = numberInsteadOfRandom;

            if (numberInsteadOfRandom == GlobalConstants.DefaultNumberInsteadOfRandom)
            {
                Random random = new Random();
                randomBehaviourNumber = random.Next(
                    (int)this.CurrentHand.HandPower - MinimumNumberDifference,
                    (int)this.CurrentHand.HandPower + MaximumNumberDifference);
            }

            this.TakeTurnBasedOnDecision(randomBehaviourNumber);

            Game.Instance.FixCall();
        }

        /// <summary>
        /// Decides what to do based on the bot's hand and a random number for a more random behavior
        /// </summary>
        /// <param name="randomBehaviourNumber">A random number that is based on the bot's hand power too</param>
        private void TakeTurnBasedOnDecision(int randomBehaviourNumber)
        {
            if (randomBehaviourNumber <= GlobalConstants.MaximumValueToDecideToFold)
            {
                this.Fold();
            }
            else if (randomBehaviourNumber < GlobalConstants.MaximumValueToDecideToCall)
            {
                this.CallBlind();
                this.RaisedThisTurn = false;
            }
            else if (!this.RaisedThisTurn)
            {
                decimal raiseValue = 0;
                if (randomBehaviourNumber < GlobalConstants.MaximumValueToDecideToRaiseWithSmallSum)
                {
                    raiseValue = GlobalConstants.SmallSumRaisePercentage / 100m;
                }
                else
                {
                    raiseValue = GlobalConstants.BigSumRaisePercentage / 100m;
                }

                this.Raise((int)(this.Chips * raiseValue));
                this.RaisedThisTurn = true;
            }
        }

        /// <summary>
        /// The bot raises the bet
        /// </summary>
        /// <param name="raiseValue">The value that will be used for raising</param>
        private void Raise(int raiseValue)
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
        private void CallBlind()
        {
            Game.Instance.CallForPlayer(this);
        }
    }
}
