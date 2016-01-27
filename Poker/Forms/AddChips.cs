namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Constants;

    /// <summary>
    /// The form for the chips adding option
    /// </summary>
    public partial class AddChips : Form
    {
        private const string QuitMessage = "Are you sure?";
        private const string QuitTitle = "Quit";
        private const string NumberOnlyField = "This is a number only field";

        private int chipsAdded;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddChips"/> class chips form
        /// </summary>
        public AddChips()
        {
            this.InitializeComponent();
            this.ControlBox = false;
            this.centerText.BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// The number of chips added to each player
        /// </summary>
        public int ChipsAdded
        {
            get
            {
                return this.chipsAdded;
            }

            set
            {
                this.chipsAdded = value;
            }
        }

        /// <summary>
        /// Check if it is possible to add chips from a string
        /// </summary>
        /// <param name="parsedValue">The value that will be returned if the parsing is successful</param>
        /// <param name="text">The text that holds the value trying to be parsed</param>
        /// <returns>True if it the text for the add chips is valid and false if it is not.</returns>
        public static bool CanAddChips(out int parsedValue, string text)
        {
            if (!int.TryParse(text, out parsedValue))
            {
                MessageBox.Show(NumberOnlyField);
                return false;
            }

            if (int.Parse(text) > GlobalConstants.MaximumChipsToAdd)
            {
                MessageBox.Show(string.Format(GlobalConstants.MaximumChipsText, GlobalConstants.MaximumChipsToAdd));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds chips to all players
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void AddChipsButton(object sender, EventArgs e)
        {
            int parsedValue = 0;
            if (CanAddChips(out parsedValue, this.chipsToAdd.Text))
            {
                this.ChipsAdded = parsedValue;
                this.Close();
            }
        }

        /// <summary>
        /// Closes the add chips form
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void CloseButton(object sender, EventArgs e)
        {
            var message = QuitMessage;
            var title = QuitTitle;
            var result = MessageBox.Show(
            message,
            title,
            MessageBoxButtons.YesNo, 
            MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    Application.Exit();
                    break;
            }
        }
    }
}
