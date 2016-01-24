namespace Poker.Interfaces
{
    using Constants;

    public interface IBot : IPlayer
    {
        /// <summary>
        /// Used to determine if the bot has raised this turn so as not to fall in an infinite loop
        /// </summary>
        bool ActedThisTurn { get; set; }

        /// <summary>
        /// The bot's AI logic for every turn
        /// </summary>
        /// <param name="botIndex">the index of the bot in order to create a correct message box</param>
        /// <param name="numberInsteadOfRandom">Used for specific testing instead of a random number</param>
        void TakeTurn(int botIndex, int numberInsteadOfRandom = GlobalConstants.DefaultNumberInsteadOfRandom);
    }
}
