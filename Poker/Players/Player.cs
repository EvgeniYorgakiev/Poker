using System.Drawing;
using System.Windows.Forms;
using Poker.Constants;

namespace Poker.Players
{
    public abstract class Player
    {
        private TextBox textBox;
        private Panel panel;
        private int chips;
        private AnchorStyles anchorStyles;

        protected Player(TextBox textBox, AnchorStyles anchorStyles)
        {
            this.Panel = new Panel();
            this.Chips = Constant.StartingChips;
            this.TextBox = textBox;
            this.TextBox.Enabled = true;
            this.TextBox.Text = Constant.Chips + Chips;
            this.AnchorStyles = (AnchorStyles.Bottom | AnchorStyles.Left);
        }

        public Panel Panel
        {
            get
            {
                return panel;
            }
            set
            {
                this.panel = value;
            }
        }

        public int Chips
        {
            get
            {
                return chips;
            }
            set
            {
                chips = value;
            }
        }

        public TextBox TextBox
        {
            get
            {
                return textBox;
            }
            set
            {
                textBox = value;
            }
        }

        public AnchorStyles AnchorStyles
        {
            get
            {
                return anchorStyles;
            }
            set
            {
                anchorStyles = value;
            }
        }

        public virtual void ThrowCards(PictureBox[] cardHolder, int[] reserve, int i, ref int horizontal, ref int vertical,
                               Image cardImage, Control.ControlCollection controls)
        {
            cardHolder[i].Tag = reserve[i];
            cardHolder[i].Image = cardImage;
            cardHolder[i].Anchor = this.AnchorStyles;
            cardHolder[i].Location = new Point(horizontal, vertical);
            horizontal += cardHolder[i].Width;
            controls.Add(this.Panel);
            int indexForOffset = i;
            if (i%2 == 1)
            {
                indexForOffset--;
            }
            this.Panel.Location = new Point(
                cardHolder[indexForOffset].Left - Constant.CardOffsetX, 
                cardHolder[indexForOffset].Top - Constant.CardOffsetY);
            this.Panel.BackColor = Color.DarkBlue;
            this.Panel.Height = Constant.PanelX;
            this.Panel.Width = Constant.PanelY;
            this.Panel.Visible = false;
        }
    }
}
