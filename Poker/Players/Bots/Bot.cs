namespace Poker.Players.Bots
{
    using System.Drawing;
    using System.Windows.Forms;
    using Forms;

    /// <summary>
    /// Represents a bot in the poker game. 
    /// </summary>
    public class Bot : Player
    {
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
        }

        /// <summary>
        /// The bot's AI logic for every turn
        /// </summary>
        /// <param name="botIndex">the index of the bot in order to create a correct message box</param>
        public void TakeTurn(int botIndex)
        {
            if (this.HasFolded)
            {
                return;
            }

            MessageBox.Show(string.Format("{0} {1}'s Turn", this.GetType().Name, botIndex));
            this.Raise();
            Game.Instance.FixCall();
        }

        /// <summary>
        /// The bot raises the bet
        /// </summary>
        public void Raise()
        {
            this.CallBlind();
            int value = 1000;
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
