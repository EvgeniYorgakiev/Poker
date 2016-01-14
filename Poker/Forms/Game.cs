namespace Poker.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Cards;
    using Constants;
    using Players.Bots;
    using Players.Humans;

    /// <summary>
    /// Represents the main game engine.
    /// </summary>
    public partial class Game : Form
    {
        ////Cards
        private const int CardDistanceX = 100;
        private const int CardDistanceY = 0;

        ////Players
        private const int PlayerCardXPosition = 635;
        private const int PlayerCardYPosition = 410;

        private const int Bot1CardXPosition = 260;
        private const int Bot1CardYPosition = 410;

        private const int Bot2CardXPosition = 260;
        private const int Bot2CardYPosition = 130;

        private const int Bot3CardXPosition = 635;
        private const int Bot3CardYPosition = 130;

        private const int Bot4CardXPosition = 970;
        private const int Bot4CardYPosition = 130;

        private const int Bot5CardXPosition = 970;
        private const int Bot5CardYPosition = 410;

        //// Blinds
        private const int DefaultBigBlindCall = 500;
        private const int LowestBigBlind = 500;
        private const int MaximumBigBlind = 200000;
        private const int DefaultSmallBlindCall = 250;
        private const int LowestSmallBlind = 250;
        private const int MaximumSmallBlind = 100000;

        //// Calls
        private const string CallText = "Call "; 

        private static readonly object Padlock = new object();
        private static Game instance;
        private int call;
        private int currentSmallBlind;
        private int currentBigBlind;
        private bool isUsingBigBlind;
        private Deck deck;
        private HumanPlayer player;
        private List<Bot> bots;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class
        /// </summary>
        public Game()
        {
            instance = this;
            this.InitializeComponent();
            this.Player = new HumanPlayer(
                new Point(PlayerCardXPosition, PlayerCardYPosition),
                new Point(CardDistanceX, CardDistanceY),
                GlobalConstants.StartingNumberOfChips,
                this.playerStatus,
                this.playerTextboxChips);
            this.Bots = new List<Bot>()
            {
                new Bot(
                    new Point(Bot1CardXPosition, Bot1CardYPosition),
                    new Point(CardDistanceX, CardDistanceY),
                    GlobalConstants.StartingNumberOfChips,
                    this.bot1Status,
                    this.bot1TextboxChips),
            new Bot(
                    new Point(Bot2CardXPosition, Bot2CardYPosition),
                    new Point(CardDistanceX, CardDistanceY),
                    GlobalConstants.StartingNumberOfChips,
                    this.bot2Status,
                    this.bot2TextboxChips),
                new Bot(
                    new Point(Bot3CardXPosition, Bot3CardYPosition),
                    new Point(CardDistanceX, CardDistanceY),
                    GlobalConstants.StartingNumberOfChips,
                    this.bot3Status,
                    this.bot3TextboxChips),
                new Bot(
                    new Point(Bot4CardXPosition, Bot4CardYPosition),
                    new Point(CardDistanceX, CardDistanceY),
                    GlobalConstants.StartingNumberOfChips,
                    this.bot4Status,
                    this.bot4TextboxChips),
                new Bot(
                    new Point(Bot5CardXPosition, Bot5CardYPosition),
                    new Point(CardDistanceX, CardDistanceY),
                    GlobalConstants.StartingNumberOfChips,
                    this.bot5Status,
                    this.bot5TextboxChips)
            };
            this.IsUsingBigBlind = true;
            this.CurrentBigBlind = DefaultBigBlindCall;
            this.CurrentSmallBlind = DefaultSmallBlindCall;
            this.WindowState = FormWindowState.Maximized;
            this.Deck = new Deck();
            this.Deck.ThrowCards(this.Player, this.Bots);
        }

        /// <summary>
        /// Represents the instance of the game using the Singleton pattern
        /// </summary>
        public static Game Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (instance == null)
                    {
                        instance = new Game();
                    }

                    return instance;
                }
            }
        }

        /// <summary>
        /// The current call.
        /// </summary>
        public int Call
        {
            get
            {
                return this.call;
            }

            set
            {
                this.call = value;
            }
        }

        /// <summary>
        /// The current call.
        /// </summary>
        public int CurrentBigBlind
        {
            get
            {
                return this.currentBigBlind;
            }

            set
            {
                this.currentBigBlind = value;
            }
        }

        /// <summary>
        /// The current call.
        /// </summary>
        public int CurrentSmallBlind
        {
            get
            {
                return this.currentSmallBlind;
            }

            set
            {
                this.currentSmallBlind = value;
            }
        }

        /// <summary>
        /// If the current mode is using the big blind
        /// </summary>
        public bool IsUsingBigBlind
        {
            get
            {
                return this.isUsingBigBlind;
            }

            set
            {
                this.isUsingBigBlind = value;
            }
        }

        /// <summary>
        /// The deck in the game.
        /// </summary>
        public Deck Deck
        {
            get
            {
                return this.deck;
            }

            set
            {
                this.deck = value;
            }
        }

        /// <summary>
        /// The human player in the game
        /// </summary>
        public HumanPlayer Player
        {
            get
            {
                return this.player;
            }

            set
            {
                this.player = value;
            }
        }

        /// <summary>
        /// All of the bots that the player will be facing
        /// </summary>
        public List<Bot> Bots
        {
            get
            {
                return this.bots;
            }

            set
            {
                this.bots = value;
            }
        }

        /// <summary>
        /// Fixes the text on the call button
        /// </summary>
        public void FixCall()
        {
            if (this.IsUsingBigBlind)
            {
                this.Call = this.CurrentBigBlind;
            }
            else
            {
                this.Call = this.CurrentSmallBlind;
            }

            this.callButton.Text = CallText + this.Call;
        }
        
        /// <summary>
        /// The event triggers when the blind options button is clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnBlindOptionsClick(object sender, EventArgs e)
        {
            this.bigBlindTextBox.Text = 500.ToString();
            this.smallBlindTextBox.Text = 250.ToString();
            if (this.bigBlindTextBox.Visible == false)
            {
                this.bigBlindTextBox.Visible = true;
                this.smallBlindTextBox.Visible = true;
                this.bigBlindButton.Visible = true;
                this.smallBlindButton.Visible = true;
            }
            else
            {
                this.bigBlindTextBox.Visible = false;
                this.smallBlindTextBox.Visible = false;
                this.bigBlindButton.Visible = false;
                this.smallBlindButton.Visible = false;
            }
        }

        /// <summary>
        /// The event triggers when the big blind button is clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnBigBlindChange(object sender, EventArgs e)
        {
            this.isUsingBigBlind = true;
            int currentValue = this.CurrentBigBlind;
            this.ChangeBlind(this.bigBlindTextBox, ref currentValue, LowestBigBlind, MaximumBigBlind);
            if (currentValue == this.CurrentBigBlind)
            {
                this.bigBlindTextBox.Text = currentValue.ToString();
            }
            else
            {
                this.CurrentBigBlind = currentValue;
            }
        }

        /// <summary>
        /// The event triggers when the small blind button is clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnSmallBlindChange(object sender, EventArgs e)
        {
            this.isUsingBigBlind = false;
            int currentValue = this.CurrentSmallBlind;
            this.ChangeBlind(this.smallBlindTextBox, ref currentValue, LowestSmallBlind, MaximumSmallBlind);
            if (currentValue == this.CurrentSmallBlind)
            {
                this.smallBlindTextBox.Text = currentValue.ToString();
            }
            else
            {
                this.CurrentSmallBlind = currentValue;
            }
        }

        /// <summary>
        /// The event triggers when the raise button is clicked
        /// </summary>
        /// <param name="textBox">the textbox of the blind</param>
        /// <param name="currentValue">pass the value of the current blind as a reference if the bling change is successful</param>
        /// <param name="lowestValue">the lowest value the blind can take</param>
        /// <param name="maximumValue">the highest value the blind can take</param>
        private void ChangeBlind(TextBox textBox, ref int currentValue, int lowestValue, int maximumValue)
        {
            int parsedValue;
            if (textBox.Text.Contains(",") || textBox.Text.Contains("."))
            {
                MessageBox.Show("The blind can be only a round number !");
                return;
            }
            else if (!int.TryParse(textBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            else if (parsedValue > maximumValue)
            {
                MessageBox.Show("The maximum value of the current blind is " + maximumValue);
            }
            else if (parsedValue < lowestValue)
            {
                MessageBox.Show("The minimum value of the current blind is " + lowestValue);
            }
            else if (parsedValue >= lowestValue && parsedValue <= maximumValue)
            {
                currentValue = parsedValue;
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        /// <summary>
        /// The event triggers when the raise button is clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnRaise(object sender, EventArgs e)
        {
            this.Deck.ThrowCards(this.Player, this.Bots);
        }
    }
}