namespace Poker.Players.Bots
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
        private const int MoneyBettingFactor = 3;
        private const int MinimumNumberDifference = -5;
        private const int MaximumNumberDifference = 15;
        private const decimal FactorForRaising = 1.3m;
        private const decimal FactorForCalling = 1.1m;

        private bool actedThisTurn;

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
            this.ActedThisTurn = false;
        }

        /// <summary>
        /// Used to determine if the bot has already raised this turn so as not to fall in infinite loop of raising
        /// </summary>
        public bool ActedThisTurn
        {
            get
            {
                return this.actedThisTurn;
            }

            set
            {
                this.actedThisTurn = value;
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
                randomBehaviourNumber = random.Next(MinimumNumberDifference, MaximumNumberDifference);
                randomBehaviourNumber += (int)this.CurrentHand.HandPower;
                randomBehaviourNumber += this.CurrentHand.Cards[0].Power;
            }

            this.TakeTurnBasedOnDecision(randomBehaviourNumber);

            this.ActedThisTurn = true;

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
            else
            {
                bool cardsInCenterAreRevealed = Game.Instance.Deck.NeutalCards[0].PictureBox.Image ==
                                                 Game.Instance.Deck.NeutalCards[0].Front;
                int moneyWillingToBet = (int)(this.Chips / (100m / randomBehaviourNumber)) / MoneyBettingFactor;
                moneyWillingToBet = this.RoundMoneyToZero(moneyWillingToBet);
                int moneyToBet = moneyWillingToBet;
                if (moneyToBet < this.MinimumMoneyRequiredForBlind())
                {
                    moneyToBet = this.MinimumMoneyRequiredForBlind();
                }

                if (moneyToBet > this.Chips)
                {
                    moneyToBet = this.Chips;
                }

                this.TryToCall(cardsInCenterAreRevealed, moneyWillingToBet, moneyToBet);
            }
        }

        /// <summary>
        /// The bot will try to determine if he should call or fold depending on the factors
        /// </summary>
        /// <param name="cardsInCenterAreRevealed">If there aren't any cards revealed in the center and yet someone placed a high raise there was a bluff.</param>
        /// <param name="moneyWillingToBet">The money the bot is willing to bet.</param>
        /// <param name="moneyToBet">The money the bot will bet.</param>
        private void TryToCall(bool cardsInCenterAreRevealed, int moneyWillingToBet, int moneyToBet)
        {
            int moneyNeededForCall = Game.Instance.Call - this.CurrentCall;
            if (moneyWillingToBet > moneyNeededForCall * FactorForRaising && !this.ActedThisTurn)
            {
                this.Raise(moneyToBet);
                return;
            }
            else if (moneyWillingToBet > moneyNeededForCall / FactorForCalling)
            {
                this.CallBlind();
                return;
            }
            else if (cardsInCenterAreRevealed)
            {
                this.Fold();
            }
            if (!cardsInCenterAreRevealed)
            {
                Random random = new Random();
                int randomBehaviour = random.Next(0, 2);
                if (randomBehaviour == 0)
                {
                    this.Fold();
                }
                else
                {
                    this.CallBlind(); ////If someone has raised with a lot of money in this scenario there is a strong possibility of a bluff so the bot will call the blind.
                }
            }
        }

        /// <summary>
        /// Using the current blind options finds the minimum required to raise.
        /// </summary>
        /// <returns>The minimum money required for a blind</returns>
        private int MinimumMoneyRequiredForBlind()
        {
            if (Game.Instance.IsUsingBigBlind)
            {
                return Game.Instance.CurrentBigBlind;
            }
            else
            {
                return Game.Instance.CurrentSmallBlind;
            }
        }

        /// <summary>
        /// Rounds the last digit do 0 to avoid bets like 387.
        /// </summary>
        /// <param name="moneyWillingToBet">The money the bot is willing to bet.</param>
        /// <returns>Returns the money with the last digit rounded to 0.</returns>
        private int RoundMoneyToZero(int moneyWillingToBet)
        {
            moneyWillingToBet /= 10;
            moneyWillingToBet *= 10;

            return moneyWillingToBet;
        }
    }
}
