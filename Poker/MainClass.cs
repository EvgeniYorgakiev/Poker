namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Forms;

    /// <summary>
    /// The class that is run first when the program starts
    /// </summary>
    public static class MainClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Game.Instance);
        }
    }
}
