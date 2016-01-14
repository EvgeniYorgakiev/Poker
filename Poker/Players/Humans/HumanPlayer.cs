namespace Poker.Players.Humans
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Represents the human player in the poker game.
    /// </summary>
    public class HumanPlayer : Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HumanPlayer"/> class
        /// </summary>
        /// <param name="cardStartingPoint">The starting position of the player's cards</param>
        /// <param name="cardDistanceFromEachother">The offset of each card from the previous</param>
        /// <param name="chips">The number of chips the player will start with</param>
        /// <param name="status">The status label for the player</param>
        /// <param name="chipsTextBox">The textbox that will be showing the number of chips</param>
        public HumanPlayer(Point cardStartingPoint, Point cardDistanceFromEachother, int chips, Label status, TextBox chipsTextBox)
            : base(cardStartingPoint, cardDistanceFromEachother, chips, status, chipsTextBox)
        {
        }
    }
}
