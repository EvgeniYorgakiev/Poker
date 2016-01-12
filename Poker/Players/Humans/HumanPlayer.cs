using System.Drawing;

namespace Poker.Players.Humans
{
    /// <summary>
    /// Represents the human player in the poker game.
    /// </summary>
    public class HumanPlayer : Player
    {
        public HumanPlayer(Point cardStartingPoint, Point cardOffsetFromEachother)
            : base(cardStartingPoint, cardOffsetFromEachother)
        {
        }
    }
}
