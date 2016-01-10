using System.Drawing;

namespace Poker.Players.Bots
{
    using System.Windows.Forms;

    public class Bot : Player
    {
        private readonly int horizontalCardPosition;
        private readonly int verticalCardPosition;
        private bool folded;

        public Bot(TextBox textBox, AnchorStyles anchorStyles, int horizontalCardX, int verticalCardX) 
            : base(textBox, anchorStyles)
        {
            this.horizontalCardPosition = horizontalCardX;
            this.verticalCardPosition = verticalCardX;
        }

        public int HorizontalCardPosition
        {
            get { return horizontalCardPosition; }
        }

        public int VerticalCardPosition
        {
            get { return verticalCardPosition; }
        }

        public bool Folded
        {
            get { return folded; }
            set { folded = value; }
        }

        public override void ThrowCards(PictureBox[] cardHolder, int[] reserve, int i, ref int horizontal, ref int vertical, 
            Image cardImage, Control.ControlCollection controls)
        {
            if (i%2 == 1)
            {
                horizontal = this.HorizontalCardPosition;
                vertical = this.VerticalCardPosition;
            }
            base.ThrowCards(cardHolder, reserve, i, ref horizontal, ref vertical, cardImage, controls);
        }
    }
}
