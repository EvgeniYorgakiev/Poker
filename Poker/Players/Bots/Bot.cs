using System.Drawing;

namespace Poker.Players.Bots
{
    /// <summary>
    /// Represents a bot in the poker game. 
    /// </summary>
    public class Bot : Player
    {
        public Bot(Point cardStartingPoint, Point cardOffsetFromEachother) 
            : base(cardStartingPoint, cardOffsetFromEachother)
        {

        }
    }
}
