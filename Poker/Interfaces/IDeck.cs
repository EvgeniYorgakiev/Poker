namespace Poker.Interfaces
{
    using System.Collections.Generic;

    public interface IDeck
    {
        /// <summary>
        /// Represents an array of all 52 cards in the standard deck
        /// </summary>
        ICard[] Cards { get; set; }

        /// <summary>
        /// Represents an array of the 5 cards that are in the center of the table
        /// </summary>
        ICard[] NeutalCards { get; set; }

        /// <summary>
        /// Throw everybody's cards for a new game
        /// </summary>
        /// <param name="player">The human player in the game</param>
        /// <param name="bots">All of the bots that the player will be facing</param>
        /// <param name="wait">If the method should wait for visual effects</param>
        void ThrowCards(IPlayer player, List<IBot> bots, bool wait = true);

        /// <summary>
        /// Shuffles the deck for random cards.
        /// </summary>
        void ShuffleCards();

        /// <summary>
        /// Removes the player's currently held cards making room for another hand
        /// </summary>
        /// <param name="player">The player that will receive new cards</param>
        /// <param name="bots">The bots the player will be facing</param>
        void RemoveAllCardsOnBoard(IPlayer player, List<IBot> bots);
    }
}
