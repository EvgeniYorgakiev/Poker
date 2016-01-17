namespace Poker.Cards.Hands
{
    /// <summary>
    /// Represents the power of the hand with HighCard being the weakest and RoyalFlush being the strongest
    /// </summary>
    public enum Power
    {
        HighCard = 1,
        OnePair = 3,
        TwoPair = 5,
        ThreeOfAKind = 6,
        Straigth = 7,
        Flush = 8,
        FullHouse = 9,
        FourOfAKind = 10,
        StraightFlush = 11,
        RoyalFlush = 12
    }
}
