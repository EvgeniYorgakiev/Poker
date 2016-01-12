using System.Drawing;

namespace Poker.Forms
{
    using System.Windows.Forms;
    using Cards;
    using Players.Humans;
    using Players.Bots;
    using System.Collections.Generic;
    using Constants;

    /// <summary>
    /// Represents the main game engine.
    /// </summary>
    public partial class Game : Form
    {
        private static Game instance = null;
        private static readonly object padlock = new object();
        private Deck deck;
        private HumanPlayer player;
        private List<Bot> bots;

        public Game()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Deck = new Deck();
            this.Bots = new List<Bot>()
            {
                new Bot(
                    new Point(Constant.Bot1CardXPosition, Constant.Bot1CardYPosition),
                    new Point(Constant.CardOffsetX, Constant.CardOffsetY)),
                new Bot(
                    new Point(Constant.Bot2CardXPosition, Constant.Bot2CardYPosition),
                    new Point(Constant.CardOffsetX, Constant.CardOffsetY)),
                new Bot(
                    new Point(Constant.Bot3CardXPosition, Constant.Bot3CardYPosition),
                    new Point(Constant.CardOffsetX, Constant.CardOffsetY)),
                new Bot(
                    new Point(Constant.Bot4CardXPosition, Constant.Bot4CardYPosition),
                    new Point(Constant.CardOffsetX, Constant.CardOffsetY)),
                new Bot(
                    new Point(Constant.Bot5CardXPosition, Constant.Bot5CardYPosition),
                    new Point(Constant.CardOffsetX, Constant.CardOffsetY)),
            };
            this.Player = new HumanPlayer(
                new Point(Constant.PlayerCardXPosition, Constant.PlayerCardYPosition),
                new Point(Constant.CardOffsetX, Constant.CardOffsetY));
            this.Deck.ThrowCards(this.Player, this.Bots);
        }

        /// <summary>
        /// Represents the instance of the game using the Singleton pattern
        /// </summary>
        public static Game Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Game();
                    }
                    return instance;
                }
            }
        }

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

        public HumanPlayer Player
        {
            get
            {
                return this.player;
            }
            set { this.player = value; }
        }

        public List<Bot> Bots
        {
            get { return this.bots; }
            set { this.bots = value; }
        }
    }
}