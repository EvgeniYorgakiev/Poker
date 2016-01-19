namespace Poker.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Cards;
    using Cards.Hands;
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

        //// Wins
        private const string BotWinsText = "Bot {0} Wins";
        private const string PlayerWinsText = "Player Wins";
        private const string BotWinsWithHandText = "Bot {0} wins with {1}";
        private const string PlayerWinsWithHandText = "Player wins with {0}";
        private const string TieWithHandText = "Tie with {0}. The pot is split between";
        private const string PlayerText = " Player";
        private const string BotText = " Bot";

        private static readonly object Padlock = new object();
        private static Game instance;
        private int call;
        private int currentSmallBlind;
        private int currentBigBlind;
        private bool isUsingBigBlind;
        private IDeck deck;
        private IPlayer player;
        private List<IBot> bots;

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
            this.potTextbox.Text = (int.Parse(this.potTextbox.Text) + differenceInCall).ToString();
            currentPlayer.Chips -= differenceInCall;
            currentPlayer.CurrentCall = this.Call;
        }

        /// <summary>
        /// After all of the cards have been revealed and the betting has finished determines who the winner is.
        /// </summary>
        /// <returns>Returns all of the players that are in tie for the strongest hand</returns>
        public List<IPlayer> DetermineWinner()
        {
            Power strongestHand = this.Player.CurrentHand.HandPower;
            for (int i = 0; i < this.Bots.Count; i++)
            {
                if (this.Bots[i].CurrentHand.HandPower > strongestHand && !this.Bots[i].HasFolded)
                {
                    strongestHand = this.Bots[i].CurrentHand.HandPower;
                }
            }

            var winners = new List<IPlayer>();
            if (strongestHand == this.Player.CurrentHand.HandPower)
            {
                winners.Add(this.Player);
            }

            for (int i = 0; i < this.Bots.Count; i++)
            {
                if (strongestHand == this.Bots[i].CurrentHand.HandPower && !this.Bots[i].HasFolded)
                {
                    winners.Add(this.Bots[i]);
                }
            }

            var winnersInTie = new List<IPlayer>();
            if (winners.Count == 1)
            {
                winnersInTie.Add(winners[0]);
            }
            else
            {
                winnersInTie = WinningHandFactory.WinnersInTie(winners, strongestHand);
            }

            return winnersInTie;
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
            for (int i = 0; i < this.Bots.Count; i++)
            {
                this.Bots[i].CurrentCall = 0;
            }

            this.FixCall();
            this.potTextbox.Text = PotDefaultMoney;
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
            else if (parsedValue >= lowestValue && parsedValue <= maximumValue)
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
        /// <returns>True if the text in the text box is a valid number and false if it isn't</returns>
        private bool ValidNumber(TextBox textBox, ref int parsedValue, int lowestValue, int maximumValue)
        {
            if (textBox.Text.Contains(",") || textBox.Text.Contains("."))
            {
                MessageBox.Show(TextBoxNeedsRoundNumber);
            }
            else if (!int.TryParse(textBox.Text, out parsedValue))
            {
                MessageBox.Show(NumberOnlyField);
            }
            else if (parsedValue > maximumValue)
            {
                MessageBox.Show(MaximumValueOfNumberText + maximumValue);
            }
            else if (parsedValue < lowestValue)
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
            this.CallForPlayer(this.Player);
            this.OnCheck(sender, e);
        }

        /// <summary>
        /// The event triggers when the check or call buttons are clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnCheck(object sender, EventArgs e)
        {
            bool thereIsABotStillPlaying = false;
            for (int i = 0; i < this.Bots.Count; i++)
            {
                this.Bots[i].TakeTurn(i + 1);
                if (!this.Bots[i].HasFolded)
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

            this.EndTurn();
        }

        /// <summary>
        /// Used for when the turn has ended to know if it should reveal the other cards
        /// </summary>
        private void EndTurn()
        {
            if (this.Call == this.Player.CurrentCall)
            {
                if (this.ShouldRevealCards(this.Deck.NeutalCards, 0))
                {
                    this.RevealCards(
                        this.Deck.NeutalCards,
                        0,
                        CardsToRevealFirstTime);
                }
                else if (this.ShouldRevealCards(this.Deck.NeutalCards, CardsToRevealFirstTime))
                {
                    this.RevealCards(
                        this.Deck.NeutalCards,
                        CardsToRevealFirstTime,
                        CardsToRevealSecondTime + CardsToRevealFirstTime);
                }
                else if (this.ShouldRevealCards(this.Deck.NeutalCards, CardsToRevealSecondTime + CardsToRevealFirstTime))
                {
                    this.RevealCards(
                        this.Deck.NeutalCards,
                        CardsToRevealSecondTime + CardsToRevealFirstTime,
                        CardsToRevealThirdTime + CardsToRevealSecondTime + CardsToRevealFirstTime);
                }
                else
                {
                    var winnersInTie = this.DetermineWinner();

                    this.RevealCards();

                    this.ShowMessageWithWinner(winnersInTie);

                    this.EndHand(winnersInTie);
                }

                for (int i = 0; i < this.Bots.Count; i++)
                {
                    this.Bots[i].RaisedThisTurn = false;
                }
            }
        }

        /// <summary>
        /// Reveals all of the players hands
        /// </summary>
        private void RevealCards()
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
        /// Reveals all of the cards in the array between the start index and end index
        /// </summary>
        /// <param name="cards">The array of cards to reveal</param>
        /// <param name="startIndex">The starting index of cards to reveal in the array</param>
        /// <param name="endIndex">The final index of cards to reveal in the array</param>
        private void RevealCards(ICard[] cards, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                cards[i].PictureBox.Image = cards[i].Front;
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

        /// <summary>
        /// The event triggers when the fold button is clicked
        /// </summary>
        /// <param name="sender">The sender of the events</param>
        /// <param name="e">The event arguments</param>
        private void OnFold(object sender, EventArgs e)
        {
            this.Player.Fold();
            while (true)
            {
                int numberOfFoldedBots = 0;
                for (int i = 0; i < this.Bots.Count; i++)
                {
                    if (numberOfFoldedBots == this.Bots.Count - 1)
                    {
                        MessageBox.Show(string.Format(BotWinsText, i));
                        this.EndHand(new List<IPlayer> { this.Bots[i] });
                        return;
                    }

                    this.Bots[i].TakeTurn(i + 1);
                    if (this.Bots[i].HasFolded)
                    {
                        numberOfFoldedBots++;
                    }
                }

                if (numberOfFoldedBots == this.Bots.Count - 1)
                {
                    break;
                }
            }

            for (int i = 0; i < this.Bots.Count; i++)
            {
                if (!this.Bots[i].HasFolded)
                {
                    MessageBox.Show(string.Format(BotWinsText, i));
                    this.EndHand(new List<IPlayer> { this.Bots[i] });
                }
            }
        }
    }
}