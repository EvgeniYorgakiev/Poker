namespace Poker.Constants
{
    /// <summary>
    /// All of the constants that will be used as public and cannot be bound to 1 class only
    /// </summary>
    public static class GlobalConstants
    {
        public const string CardPath = "..\\..\\Resources\\Cards";
        public const string CardPathFromUnitTest = "..\\..\\..\\Poker\\Resources\\Cards";
        public const string CardBackForUnitTesting = "..\\..\\..\\Poker\\Resources\\Back\\Back.png";
        public const int StartingNumberOfChips = 10000;
        public const int DefaultNumberInsteadOfRandom = -100;

        ////Decisions numbers
        public const int MaximumValueToDecideToFold = 3;
    }
}
