namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Cards.Hands;

    public interface IPlayer
    {
        List<ICard> Cards { get; set; }

        Point CardStartingPoint { get; set; }

        Point CardDistanceFromEachother { get; set; }

        int Chips { get; set; }

        int CurrentCall { get; set; }

        bool HasFolded { get; set; }

        Hand CurrentHand { get; set; }

        Label Status { get; set; }

        void DetermineHandPower(ICard[] neutralCards);

        void Fold();
    }
}
