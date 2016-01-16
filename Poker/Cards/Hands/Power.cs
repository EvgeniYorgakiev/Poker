namespace Poker.Cards.Hands
{
    /// <summary>
    /// Represents the power of the hand with HighCard being the weakest and RoyalFlush being the strongest
    /// </summary>
    public enum Power
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straigth,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }
}
