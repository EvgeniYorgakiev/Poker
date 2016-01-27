namespace Poker.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using Cards;
    using Constants;
    using Factories;
    using Interfaces;
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
        private const int CardsToRevealFirstTime = 3;
        private const int CardsToRevealSecondTime = 1;
        private const int CardsToRevealThirdTime = 1;

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
        private const string TextBoxNeedsRoundNumber = "The blind can be only a round number !";
        private const string NumberOnlyField = "This is a number only field";
        private const string MaximumValueOfNumberText = "The maximum value of the this number can be ";
        private const string MinimumValueOfNumberText = "The minimum value of the this number can be ";
        private const string ChangesSavedText =
            "The changes have been saved ! They will become available the next hand you play. ";

        //// Calls
        private const string CallText = "Call ";
        private const string PotDefaultMoney = "0";
        private const string InvalidRaiseText = "You must raise atleast twice as the current raise !";
        private const string RaiseCannotBeLowerThanCallText = "You cannot raise with less than the value required for a call";
        private const string AllInText = "All in";

        //// Wins
        private const string BotWinsText = "Bot {0} Wins";
        private const string PlayerWinsText = "Player Wins";
        private const string BotWinsWithHandText = "Bot {0} wins with {1}";
        private const string PlayerWinsWithHandText = "Player wins with {0}";
        private const string TieWithHandText = "Tie with {0}. The pot is split between";
        private const string PlayerText = " Player";
        private const string BotText = " Bot";
        private const string WinText = "Would You Like To Play Again ?";
        private const string WinCaption = "You Won, Congratulations ! ";

        //// Timer
        private const int TicksInASecond = 10;
        private const int TimeForPlayerTurn = 60;

        private static readonly object Padlock = new object();
        private static Game instance;
        private int call;
        private int currentSmallBlind;
        private int currentBigBlind;
        private bool isUsingBigBlind;
        private IDeck deck;
        private IPlayer player;
        private List<IBot> bots;
        private Timer timer;
        private int time = TicksInASecond * TimeForPlayerTurn;
        private bool isPlayerTurn;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class
        /// </summary>
        public Game() : this(GlobalConstants.CardPath, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class with a path specified used for testing
        /// </summary>
        /// <param name="path">The path for the card images</param>
        /// <param name="wait">If the game should wait for async methods</param>
        public Game(string path, bool wait)
        {
            instance = this;
            this.InitializeComponent();
            this.Player = new HumanPlayer(
                new Point(PlayerCardXPosition, PlayerCardYPosition),
                new Point(CardDistanceX, CardDistanceY),
                GlobalConstants.StartingNumberOfChips,
                this.playerStatus,
                this.playerTextboxChips);
            this.Bots = new List<IBot>
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
            this.Deck = new Deck(path);
            this.Deck.ThrowCards(this.Player, this.Bots, wait);
            this.Timer = new Timer();
            this.Timer.Tick += this.OnTimerTick;
            this.Timer.Start();
            this.IsPlayerTurn = true;
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
                        return new Game();
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
        public IDeck Deck
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
        public IPlayer Player
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
        public List<IBot> Bots
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
        /// The timer for the player's turn
        /// </summary>
        public Timer Timer
        {
            get
            {
                return this.timer;
            }

            set
            {
                this.timer = value;
            }
        }

        /// <summary>
        /// The time remaining for the player's turn
        /// </summary>
        public int Time
        {
            get
            {
                return this.time;
            }

            set
            {
                this.time = value;
            }
        }

        /// <summary>
        /// If it is currently the player's turn
        /// </summary>
        public bool IsPlayerTurn
        {
            get
            {
                return this.isPlayerTurn;
            }

            set
            {
                this.isPlayerTurn = value;
            }
        }

        /// <summary>
        /// Fixes the text on the call button
        /// </summary>
        public void FixCall()
        {
            if (this.Call == 0)
            {
                if (this.IsUsingBigBlind)
                {
                    this.Call = this.CurrentBigBlind;
                }
                else
                {
                    this.Call = this.CurrentSmallBlind;
                }
            }

            if (this.Call != this.Player.CurrentCall)
            {
                this.callButton.Text = CallText + (this.Call - this.Player.CurrentCall);
            }
            else
            {
                this.callButton.Text = CallText;
            }
        }

        /// <summary>
        /// Enables or disables the buttons depending on the parameters
        /// </summary>
        /// <param name="foldEnabled">Enables or disables the fold button</param>
        /// <param name="checkEnabled">Enables or disables the check button</param>
        /// <param name="callEnabled">Enables or disables the call button</param>
        /// <param name="raiseEnabled">Enables or disables the raise button</param>
        public void EnableButtons(bool foldEnabled, bool checkEnabled, bool callEnabled, bool raiseEnabled)
        {
            this.callButton.Enabled = callEnabled;
            this.foldButton.Enabled = foldEnabled;
            this.checkButton.Enabled = checkEnabled;
            this.raiseButton.Enabled = raiseEnabled;
        }

        /// <summary>
        /// The player raises the bet updating the textboxes and the chips
        /// </summary>
        /// <param name="currentPlayer">The player that raises the bet</param>
        /// <param name="raiseValue">The value the player wishes to raises with.</param>
        public void RaiseBet(IPlayer currentPlayer, int raiseValue)
        {
            this.potTextbox.Text = (int.Parse(this.potTextbox.Text) + raiseValue).ToString();
            currentPlayer.Chips -= raiseValue;
            this.Call += raiseValue;
            currentPlayer.CurrentCall = this.Call;
        }

        /// <summary>
        /// The player calls on the blind without raising updating the textboxes and the chips
        /// </summary>
        /// <param name="currentPlayer">The player that calls</param>
        public void CallForPlayer(IPlayer currentPlayer)
        {
            int differenceInCall = this.Call - currentPlayer.CurrentCall;
            if (currentPlayer.Chips - differenceInCall < 0)
            {
                differenceInCall = currentPlayer.Chips;
            }

            this.potTextbox.Text = (int.Parse(this.potTextbox.Text) + differenceInCall).ToString();
            currentPlayer.Chips -= differenceInCall;
            currentPlayer.CurrentCall = this.Call;
        }

        /// <summary>
        /// Give the pot money to the winner and start a new hand
        /// </summary>
        /// <param name="winners">The winners of the current hand. Can be more than 1. In that case the money is split</param>
        private void EndHand(List<IPlayer> winners)
        {
            int potMoney = int.Parse(this.potTextbox.Text);
            int moneyPerPlayer = potMoney / winners.Count;
            for (int i = 0; i < winners.Count; i++)
            {
                winners[i].Chips += moneyPerPlayer;
            }

            this.Call = 0;
            this.Player.CurrentCall = 0;
            this.Player.Status.Text = string.Empty;
            for (int i = 0; i < this.Bots.Count; i++)
            {
                this.Bots[i].CurrentCall = 0;
                this.Bots[i].Status.Text = string.Empty;
            }

            this.FixCall();
            this.potTextbox.Text = PotDefaultMoney;
            this.raiseButton.Text = GlobalConstants.RaiseText;

            bool atleastOneBotHasChips = false;
            for (int i = 0; i < this.Bots.Count; i++)
            {
                if (this.Bots[i].Chips > 0)
                {
                    atleastOneBotHasChips = true;
                }
            }

            if (this.Player.Chips <= 0)
            {
                this.AddChipsWhenLost();
            }

            if (atleastOneBotHasChips)
            {
                this.Deck.ThrowCards(this.Player, this.Bots);
            }
            else
            {
                this.WinGame();
            }
        }

        /// <summary>
        /// When all of the bots have 0 chips and the player has won the game.
        /// </summary>
        private void WinGame()
        {
            DialogResult dialogResult = MessageBox.Show(WinText, WinCaption, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Restart();
            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Resets the player's timer for the current turn
        /// </summary>
        private void ResetTimer()
        {
            this.Time = TicksInASecond * TimeForPlayerTurn;
        }

        /// <summary>
        /// The event triggers when the blind options button is clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnBlindOptionsClick(object sender, EventArgs e)
        {
            this.bigBlindTextBox.Text = DefaultBigBlindCall.ToString();
            this.smallBlindTextBox.Text = DefaultSmallBlindCall.ToString();
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
            int parsedValue = 0;
            if (!this.ValidNumber(textBox, ref parsedValue, lowestValue, maximumValue))
            {
                return;
            }

            if (parsedValue >= lowestValue && parsedValue <= maximumValue)
            {
                currentValue = parsedValue;
                MessageBox.Show(ChangesSavedText);
            }
        }

        /// <summary>
        /// Check if the text in a text box is a valid number for that text box
        /// </summary>
        /// <param name="textBox">The text box that holds the text</param>
        /// <param name="parsedValue">The value of the text box if it is a valid number</param>
        /// <param name="lowestValue">The lowest possible value the number can have</param>
        /// <param name="maximumValue">The maximum value the number can have</param>
        /// <param name="hasMaximumValue">If the text field has a maximum value allowed</param>
        /// <param name="hasMinimumValue">If the text field has a minimum value allowed</param>
        /// <returns>True if the text in the text box is a valid number and false if it isn't</returns>
        private bool ValidNumber(
            TextBox textBox,
            ref int parsedValue, 
            int lowestValue = 0,
            int maximumValue = 0,
            bool hasMaximumValue = true, 
            bool hasMinimumValue = true)
        {
            if (textBox.Text.Contains(",") || textBox.Text.Contains("."))
            {
                MessageBox.Show(TextBoxNeedsRoundNumber);
            }
            else if (!int.TryParse(textBox.Text, out parsedValue))
            {
                MessageBox.Show(NumberOnlyField);
            }
            else if (parsedValue > maximumValue && hasMaximumValue)
            {
                MessageBox.Show(MaximumValueOfNumberText + maximumValue);
            }
            else if (parsedValue < lowestValue && hasMinimumValue)
            {
                MessageBox.Show(MinimumValueOfNumberText + lowestValue);
            }
            else
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The event triggers when the call button is clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnCall(object sender, EventArgs e)
        {
            this.EnableButtons(false, false, false, false);
            this.Player.CallBlind();
            this.OnCheck(sender, e);
        }

        /// <summary>
        /// The event triggers when the check or call buttons are clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnCheck(object sender, EventArgs e)
        {
            this.isPlayerTurn = false;
            bool thereIsABotStillPlaying = false;
            for (int i = 0; i < this.Bots.Count; i++)
            {
                this.Bots[i].TakeTurn(i + 1);
                if (!this.Bots[i].HasFolded && this.Bots[i].Chips > 0)
                {
                    thereIsABotStillPlaying = true;
                }
            }

            if (!thereIsABotStillPlaying)
            {
                MessageBox.Show(string.Format(PlayerWinsText));
                this.EndHand(new List<IPlayer>() { this.Player });
            }

            if (this.Player.CurrentCall == this.Call)
            {
                this.EnableButtons(true, true, false, true);
            }
            else
            {
                this.EnableButtons(true, false, true, true);
            }

            this.FixCall();
            this.TryToEndTurn();
            this.isPlayerTurn = true;
        }

        /// <summary>
        /// Tries to end the turn. If there is somebody with a call lower than that of the call required to play the turn cannot end.
        /// </summary>
        private void TryToEndTurn()
        {
            this.ResetTimer();
            bool somebodyHasRaised = this.Call != this.Player.CurrentCall;
            if (this.Player.HasFolded)
            {
                somebodyHasRaised = false;
                for (int i = 0; i < this.Bots.Count; i++)
                {
                    if (this.Bots[i].CurrentCall != this.Call && !this.Bots[i].HasFolded)
                    {
                        somebodyHasRaised = true;
                    }
                }
            }

            if (somebodyHasRaised)
            {
                return;
            }

            this.EndTurn();

            for (int i = 0; i < this.Bots.Count; i++)
            {
                this.Bots[i].ActedThisTurn = false;
            }
        }

        /// <summary>
        /// Ends the turn and depending on the number of revealed cards either reveals cards or ends the hand and determines the winner.
        /// </summary>
        private void EndTurn()
        {
            if (this.ShouldRevealCards(this.Deck.NeutalCards, 0))
            {
                this.Deck.RevealCards(
                    this.Deck.NeutalCards,
                    0,
                    CardsToRevealFirstTime);
            }
            else if (this.ShouldRevealCards(this.Deck.NeutalCards, CardsToRevealFirstTime))
            {
                this.Deck.RevealCards(
                    this.Deck.NeutalCards,
                    CardsToRevealFirstTime,
                    CardsToRevealSecondTime + CardsToRevealFirstTime);
            }
            else if (this.ShouldRevealCards(this.Deck.NeutalCards, CardsToRevealSecondTime + CardsToRevealFirstTime))
            {
                this.Deck.RevealCards(
                    this.Deck.NeutalCards,
                    CardsToRevealSecondTime + CardsToRevealFirstTime,
                    CardsToRevealThirdTime + CardsToRevealSecondTime + CardsToRevealFirstTime);
            }
            else
            {
                if (!this.Player.HasFolded)
                {
                    this.Player.DetermineHandPower(this.Deck.NeutalCards);
                }

                var winnersInTie = WinningHandFactory.DetermineWinner();

                this.RevealAllPlayerCards();

                this.ShowMessageWithWinner(winnersInTie);

                this.EndHand(winnersInTie);
            }
        }

        /// <summary>
        /// Reveals all of the players hands
        /// </summary>
        private void RevealAllPlayerCards()
        {
            for (int i = 0; i < this.Bots.Count; i++)
            {
                if (!this.Bots[i].HasFolded)
                {
                    for (int j = 0; j < this.Bots[i].Cards.Count; j++)
                    {
                        this.Bots[i].Cards[j].PictureBox.Image = this.Bots[i].Cards[j].Front;
                    }
                }
            }
        }

        /// <summary>
        /// Prints a message based on the winners
        /// </summary>
        /// <param name="winners">All of the winners with winning hands</param>
        private void ShowMessageWithWinner(List<IPlayer> winners)
        {
            if (winners.Count == 1)
            {
                if (winners[0] is HumanPlayer)
                {
                    MessageBox.Show(string.Format(PlayerWinsWithHandText, winners[0].CurrentHand.HandPower));
                }
                else
                {
                    for (int i = 0; i < this.Bots.Count; i++)
                    {
                        if (winners[0] == this.Bots[i])
                        {
                            MessageBox.Show(string.Format(BotWinsWithHandText, i + 1, winners[0].CurrentHand.HandPower));
                        }
                    }
                }
            }
            else
            {
                string tieText = string.Format(TieWithHandText, winners[0].CurrentHand.HandPower);
                for (int i = 0; i < winners.Count; i++)
                {
                    if (winners[0] is HumanPlayer)
                    {
                        tieText += PlayerText;
                    }
                    else
                    {
                        for (int j = 0; j < this.Bots.Count; j++)
                        {
                            if (winners[0] == this.Bots[j])
                            {
                                tieText += BotText + " " + (j + 1);
                            }
                        }
                    }
                }

                MessageBox.Show(string.Format(tieText));
            }
        }

        /// <summary>
        /// Determines if the card at the given index should be revealed
        /// </summary>
        /// <param name="cards">The array of cards to reveal</param>
        /// <param name="index">The index of the card to reveal</param>
        /// <returns>Returns true if the card is facing down and should be revealed or false if not.</returns>
        private bool ShouldRevealCards(ICard[] cards, int index)
        {
            bool shouldReveal = cards[index].PictureBox.Image != cards[index].Front;

            return shouldReveal;
        }

        /// <summary>
        /// The event triggers when the raise button is clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnRaise(object sender, EventArgs e)
        {
            int raiseValue = 0;
            if (!this.ValidNumber(this.raiseTextBox, ref raiseValue, hasMaximumValue: false, hasMinimumValue: false))
            {
                return;
            }

            decimal biggestRaise = this.FindBiggestRaise() * GlobalConstants.FactorForRaising;
            if (biggestRaise >= this.Player.Chips)
            {
                this.raiseButton.Text = AllInText;
            }

            if (raiseValue < biggestRaise)
            {
                this.raiseTextBox.Text = biggestRaise.ToString(CultureInfo.InvariantCulture);
                MessageBox.Show(InvalidRaiseText);
                return;
            }

            if (raiseValue < this.CurrentBigBlind && this.IsUsingBigBlind)
            {
                this.raiseTextBox.Text = this.CurrentBigBlind.ToString();
                MessageBox.Show(RaiseCannotBeLowerThanCallText);
                return;
            }

            if (raiseValue < this.CurrentSmallBlind && !this.IsUsingBigBlind)
            {
                this.raiseTextBox.Text = this.CurrentBigBlind.ToString();
                MessageBox.Show(RaiseCannotBeLowerThanCallText);
                return;
            }

            this.Player.Raise(raiseValue);
            this.OnCheck(sender, e);
        }

        /// <summary>
        /// Raised when the raise textbox is changed. When the textbox has a value higher than the player chips changes Raise text to All in.
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnRaiseTextChange(object sender, EventArgs e)
        {
            int raiseValue;
            if (int.TryParse(this.raiseTextBox.Text, out raiseValue))
            {
                if (raiseValue >= this.Player.Chips)
                {
                    this.raiseButton.Text = AllInText;
                }
            }
        }

        /// <summary>
        /// Finds the value of the biggest raise in order to forbid raising below twice the biggest raise.
        /// </summary>
        /// <returns>The value of the biggest raise</returns>
        private int FindBiggestRaise()
        {
            int raiseByBotsValue = 0;
            for (int i = 0; i < this.Bots.Count; i++)
            {
                string[] statusParts = this.Bots[i].Status.Text.Split(' ');
                if (statusParts[0] == GlobalConstants.RaiseText)
                {
                    raiseByBotsValue = int.Parse(statusParts[1]);
                }
            }

            return raiseByBotsValue;
        }

        /// <summary>
        /// The event triggers when the fold button is clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnFold(object sender, EventArgs e)
        {
            this.Player.Fold();
            this.EnableButtons(false, false, false, false);
            while (true)
            {
                int numberOfFoldedBots = 0;
                for (int i = 0; i < this.Bots.Count; i++)
                {
                    this.Bots[i].TakeTurn(i + 1);
                    if (this.Bots[i].HasFolded)
                    {
                        numberOfFoldedBots++;
                    }
                }

                if (numberOfFoldedBots == this.Bots.Count - 1)
                {
                    for (int i = 0; i < this.Bots.Count; i++)
                    {
                        if (!this.Bots[i].HasFolded)
                        {
                            MessageBox.Show(string.Format(BotWinsText, i + 1));
                            this.EndHand(new List<IPlayer> { this.Bots[i] });
                        }
                    }

                    break;
                }

                this.TryToEndTurn();
                
                if (int.Parse(this.potTextbox.Text) == 0)
                {
                    break;
                }
            }

            this.EnableButtons(true, false, true, true);
        }

        /// <summary>
        /// The event triggers every tick of the timer.
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            if (this.IsPlayerTurn)
            {
                this.Time--;
            }

            if (this.Time >= 0)
            {
                this.timerProgressBar.Value = (int)(1000 * ((decimal)this.Time / (TimeForPlayerTurn * TicksInASecond)));
            }
            else if (this.Time < 0 && !this.Player.HasFolded)
            {
                this.OnFold(sender, e);
            }
        }

        /// <summary>
        /// The event triggers when the add chips form appears.
        /// </summary>
        private void AddChipsWhenLost()
        {
            var addChipsForm = new AddChips();
            addChipsForm.ShowDialog();
            this.AddChipsToPlayers(addChipsForm.ChipsAdded);
        }

        /// <summary>
        /// The event triggers when the add chips form appears.
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnAddChips(object sender, EventArgs e)
        {
            int chipsAdded = 0;
            if (AddChips.CanAddChips(out chipsAdded, this.addChipsTextBox.Text))
            {
                this.AddChipsToPlayers(chipsAdded);
            }
        }

        /// <summary>
        /// Adds the number of chips specified to every player
        /// </summary>
        /// <param name="chipsAdded">The number of chips to add to every player</param>
        private void AddChipsToPlayers(int chipsAdded)
        {
            if (chipsAdded > 0)
            {
                this.Player.Chips += chipsAdded;
                for (int i = 0; i < this.Bots.Count; i++)
                {
                    this.Bots[i].Chips += chipsAdded;
                }
            }
        }
    }
}