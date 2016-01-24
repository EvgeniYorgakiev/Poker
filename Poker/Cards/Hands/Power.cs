namespace Poker.Cards.Hands
{
    /// <summary>
    /// Represents the power of the hand with HighCard being the weakest and RoyalFlush being the strongest
    /// </summary>
    public enum Power
    {
        HighCard = 0,
        OnePair = 10,
        TwoPair = 20,
        ThreeOfAKind = 30,
        Straigth = 40,
        Flush = 50,
        FullHouse = 60,
        FourOfAKind = 70,
        StraightFlush = 80,
        RoyalFlush = 90
    }
}
