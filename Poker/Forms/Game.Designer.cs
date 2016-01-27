namespace Poker.Forms
{
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// The partial class for the design of the game
    /// </summary>
    public partial class Game
    {
        private Button foldButton;
        private Button checkButton;
        private Button callButton;
        private Button raiseButton;
        private ProgressBar timerProgressBar;
        private TextBox playerTextboxChips;
        private Button addChipsButton;
        private TextBox addChipsTextBox;
        private TextBox bot5TextboxChips;
        private TextBox bot4TextboxChips;
        private TextBox bot3TextboxChips;
        private TextBox bot2TextboxChips;
        private TextBox bot1TextboxChips;
        private TextBox potTextbox;
        private Button optionsButton;
        private Button bigBlindButton;
        private TextBox smallBlindTextBox;
        private Button smallBlindButton;
        private TextBox bigBlindTextBox;
        private Label bot5Status;
        private Label bot4Status;
        private Label bot3Status;
        private Label bot2Status;
        private Label bot1Status;
        private Label playerStatus;
        private Label potLabel;
        private TextBox raiseTextBox;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.foldButton = new System.Windows.Forms.Button();
            this.checkButton = new System.Windows.Forms.Button();
            this.callButton = new System.Windows.Forms.Button();
            this.raiseButton = new System.Windows.Forms.Button();
            this.timerProgressBar = new System.Windows.Forms.ProgressBar();
            this.playerTextboxChips = new System.Windows.Forms.TextBox();
            this.addChipsButton = new System.Windows.Forms.Button();
            this.addChipsTextBox = new System.Windows.Forms.TextBox();
            this.bot5TextboxChips = new System.Windows.Forms.TextBox();
            this.bot4TextboxChips = new System.Windows.Forms.TextBox();
            this.bot3TextboxChips = new System.Windows.Forms.TextBox();
            this.bot2TextboxChips = new System.Windows.Forms.TextBox();
            this.bot1TextboxChips = new System.Windows.Forms.TextBox();
            this.potTextbox = new System.Windows.Forms.TextBox();
            this.optionsButton = new System.Windows.Forms.Button();
            this.bigBlindButton = new System.Windows.Forms.Button();
            this.smallBlindTextBox = new System.Windows.Forms.TextBox();
            this.smallBlindButton = new System.Windows.Forms.Button();
            this.bigBlindTextBox = new System.Windows.Forms.TextBox();
            this.bot1Status = new System.Windows.Forms.Label();
            this.bot2Status = new System.Windows.Forms.Label();
            this.bot3Status = new System.Windows.Forms.Label();
            this.bot4Status = new System.Windows.Forms.Label();
            this.bot5Status = new System.Windows.Forms.Label();
            this.playerStatus = new System.Windows.Forms.Label();
            this.potLabel = new System.Windows.Forms.Label();
            this.raiseTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // foldButton
            // 
            this.foldButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.foldButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.foldButton.Location = new System.Drawing.Point(335, 660);
            this.foldButton.Name = "foldButton";
            this.foldButton.Size = new System.Drawing.Size(130, 62);
            this.foldButton.TabIndex = 0;
            this.foldButton.Text = "Fold";
            this.foldButton.UseVisualStyleBackColor = true;
            this.foldButton.Click += new System.EventHandler(this.OnFold);
            // 
            // checkButton
            // 
            this.checkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkButton.Location = new System.Drawing.Point(494, 660);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(134, 62);
            this.checkButton.TabIndex = 2;
            this.checkButton.Text = "Check";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.OnCheck);
            // 
            // callButton
            // 
            this.callButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.callButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.callButton.Location = new System.Drawing.Point(667, 661);
            this.callButton.Name = "callButton";
            this.callButton.Size = new System.Drawing.Size(126, 62);
            this.callButton.TabIndex = 3;
            this.callButton.Text = "Call";
            this.callButton.UseVisualStyleBackColor = true;
            this.callButton.Click += new System.EventHandler(this.OnCall);
            // 
            // raiseButton
            // 
            this.raiseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.raiseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.raiseButton.Location = new System.Drawing.Point(835, 661);
            this.raiseButton.Name = "raiseButton";
            this.raiseButton.Size = new System.Drawing.Size(124, 62);
            this.raiseButton.TabIndex = 4;
            this.raiseButton.Text = "Raise";
            this.raiseButton.UseVisualStyleBackColor = true;
            this.raiseButton.Click += new System.EventHandler(this.OnRaise);
            // 
            // timerProgressBar
            // 
            this.timerProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.timerProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.timerProgressBar.Location = new System.Drawing.Point(335, 631);
            this.timerProgressBar.Maximum = 1000;
            this.timerProgressBar.Name = "timerProgressBar";
            this.timerProgressBar.Size = new System.Drawing.Size(667, 23);
            this.timerProgressBar.TabIndex = 5;
            this.timerProgressBar.Value = 1000;
            // 
            // playerTextboxChips
            // 
            this.playerTextboxChips.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerTextboxChips.Enabled = false;
            this.playerTextboxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerTextboxChips.Location = new System.Drawing.Point(630, 553);
            this.playerTextboxChips.Name = "playerTextboxChips";
            this.playerTextboxChips.Size = new System.Drawing.Size(163, 23);
            this.playerTextboxChips.TabIndex = 6;
            this.playerTextboxChips.Text = "Chips : 0";
            // 
            // addChipsButton
            // 
            this.addChipsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChipsButton.Location = new System.Drawing.Point(12, 697);
            this.addChipsButton.Name = "addChipsButton";
            this.addChipsButton.Size = new System.Drawing.Size(75, 25);
            this.addChipsButton.TabIndex = 7;
            this.addChipsButton.Text = "AddChips";
            this.addChipsButton.UseVisualStyleBackColor = true;
            this.addChipsButton.Click += new System.EventHandler(this.OnAddChips);
            // 
            // addChipsTextBox
            // 
            this.addChipsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChipsTextBox.Location = new System.Drawing.Point(93, 700);
            this.addChipsTextBox.Name = "addChipsTextBox";
            this.addChipsTextBox.Size = new System.Drawing.Size(125, 20);
            this.addChipsTextBox.TabIndex = 8;
            // 
            // bot5TextboxChips
            // 
            this.bot5TextboxChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bot5TextboxChips.Enabled = false;
            this.bot5TextboxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot5TextboxChips.Location = new System.Drawing.Point(965, 553);
            this.bot5TextboxChips.Name = "bot5TextboxChips";
            this.bot5TextboxChips.Size = new System.Drawing.Size(152, 23);
            this.bot5TextboxChips.TabIndex = 9;
            this.bot5TextboxChips.Text = "Chips : 0";
            // 
            // bot4TextboxChips
            // 
            this.bot4TextboxChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot4TextboxChips.Enabled = false;
            this.bot4TextboxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot4TextboxChips.Location = new System.Drawing.Point(973, 57);
            this.bot4TextboxChips.Name = "bot4TextboxChips";
            this.bot4TextboxChips.Size = new System.Drawing.Size(123, 23);
            this.bot4TextboxChips.TabIndex = 10;
            this.bot4TextboxChips.Text = "Chips : 0";
            // 
            // bot3TextboxChips
            // 
            this.bot3TextboxChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot3TextboxChips.Enabled = false;
            this.bot3TextboxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot3TextboxChips.Location = new System.Drawing.Point(633, 57);
            this.bot3TextboxChips.Name = "bot3TextboxChips";
            this.bot3TextboxChips.Size = new System.Drawing.Size(125, 23);
            this.bot3TextboxChips.TabIndex = 11;
            this.bot3TextboxChips.Text = "Chips : 0";
            // 
            // bot2TextboxChips
            // 
            this.bot2TextboxChips.Enabled = false;
            this.bot2TextboxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot2TextboxChips.Location = new System.Drawing.Point(279, 57);
            this.bot2TextboxChips.Name = "bot2TextboxChips";
            this.bot2TextboxChips.Size = new System.Drawing.Size(133, 23);
            this.bot2TextboxChips.TabIndex = 12;
            this.bot2TextboxChips.Text = "Chips : 0";
            // 
            // bot1TextboxChips
            // 
            this.bot1TextboxChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bot1TextboxChips.Enabled = false;
            this.bot1TextboxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bot1TextboxChips.Location = new System.Drawing.Point(279, 553);
            this.bot1TextboxChips.Name = "bot1TextboxChips";
            this.bot1TextboxChips.Size = new System.Drawing.Size(142, 23);
            this.bot1TextboxChips.TabIndex = 13;
            this.bot1TextboxChips.Text = "Chips : 0";
            // 
            // potTextbox
            // 
            this.potTextbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.potTextbox.Enabled = false;
            this.potTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.potTextbox.Location = new System.Drawing.Point(1123, 321);
            this.potTextbox.Name = "potTextbox";
            this.potTextbox.Size = new System.Drawing.Size(125, 23);
            this.potTextbox.TabIndex = 14;
            this.potTextbox.Text = "0";
            // 
            // optionsButton
            // 
            this.optionsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.optionsButton.Location = new System.Drawing.Point(12, 12);
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(75, 36);
            this.optionsButton.TabIndex = 15;
            this.optionsButton.Text = "BB/SB";
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.OnBlindOptionsClick);
            // 
            // bigBlindButton
            // 
            this.bigBlindButton.Location = new System.Drawing.Point(12, 254);
            this.bigBlindButton.Name = "bigBlindButton";
            this.bigBlindButton.Size = new System.Drawing.Size(75, 23);
            this.bigBlindButton.TabIndex = 16;
            this.bigBlindButton.Text = "Big Blind";
            this.bigBlindButton.UseVisualStyleBackColor = true;
            this.bigBlindButton.Visible = false;
            this.bigBlindButton.Click += new System.EventHandler(this.OnBigBlindChange);
            // 
            // smallBlindTextBox
            // 
            this.smallBlindTextBox.Location = new System.Drawing.Point(12, 228);
            this.smallBlindTextBox.Name = "smallBlindTextBox";
            this.smallBlindTextBox.Size = new System.Drawing.Size(75, 20);
            this.smallBlindTextBox.TabIndex = 17;
            this.smallBlindTextBox.Text = "250";
            this.smallBlindTextBox.Visible = false;
            // 
            // smallBlindButton
            // 
            this.smallBlindButton.Location = new System.Drawing.Point(12, 199);
            this.smallBlindButton.Name = "smallBlindButton";
            this.smallBlindButton.Size = new System.Drawing.Size(75, 23);
            this.smallBlindButton.TabIndex = 18;
            this.smallBlindButton.Text = "Small Blind";
            this.smallBlindButton.UseVisualStyleBackColor = true;
            this.smallBlindButton.Visible = false;
            this.smallBlindButton.Click += new System.EventHandler(this.OnSmallBlindChange);
            // 
            // bigBlindTextBox
            // 
            this.bigBlindTextBox.Location = new System.Drawing.Point(12, 283);
            this.bigBlindTextBox.Name = "bigBlindTextBox";
            this.bigBlindTextBox.Size = new System.Drawing.Size(75, 20);
            this.bigBlindTextBox.TabIndex = 19;
            this.bigBlindTextBox.Text = "500";
            this.bigBlindTextBox.Visible = false;
            // 
            // bot1Status
            // 
            this.bot1Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bot1Status.Location = new System.Drawing.Point(279, 579);
            this.bot1Status.Name = "bot1Status";
            this.bot1Status.Size = new System.Drawing.Size(142, 32);
            this.bot1Status.TabIndex = 29;
            // 
            // bot2Status
            // 
            this.bot2Status.Location = new System.Drawing.Point(279, 83);
            this.bot2Status.Name = "bot2Status";
            this.bot2Status.Size = new System.Drawing.Size(133, 32);
            this.bot2Status.TabIndex = 31;
            // 
            // bot3Status
            // 
            this.bot3Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot3Status.Location = new System.Drawing.Point(633, 83);
            this.bot3Status.Name = "bot3Status";
            this.bot3Status.Size = new System.Drawing.Size(125, 32);
            this.bot3Status.TabIndex = 28;
            // 
            // bot4Status
            // 
            this.bot4Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bot4Status.Location = new System.Drawing.Point(973, 83);
            this.bot4Status.Name = "bot4Status";
            this.bot4Status.Size = new System.Drawing.Size(123, 32);
            this.bot4Status.TabIndex = 27;
            // 
            // bot5Status
            // 
            this.bot5Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bot5Status.Location = new System.Drawing.Point(965, 579);
            this.bot5Status.Name = "bot5Status";
            this.bot5Status.Size = new System.Drawing.Size(152, 32);
            this.bot5Status.TabIndex = 26;
            // 
            // playerStatus
            // 
            this.playerStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerStatus.Location = new System.Drawing.Point(630, 579);
            this.playerStatus.Name = "playerStatus";
            this.playerStatus.Size = new System.Drawing.Size(163, 32);
            this.playerStatus.TabIndex = 30;
            // 
            // potLabel
            // 
            this.potLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.potLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.potLabel.Location = new System.Drawing.Point(1171, 297);
            this.potLabel.Name = "potLabel";
            this.potLabel.Size = new System.Drawing.Size(31, 21);
            this.potLabel.TabIndex = 0;
            this.potLabel.Text = "Pot";
            // 
            // raiseTextBox
            // 
            this.raiseTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.raiseTextBox.Location = new System.Drawing.Point(965, 703);
            this.raiseTextBox.Name = "raiseTextBox";
            this.raiseTextBox.Size = new System.Drawing.Size(108, 20);
            this.raiseTextBox.TabIndex = 0;
            this.raiseTextBox.TextChanged += new System.EventHandler(this.OnRaiseTextChange);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.raiseTextBox);
            this.Controls.Add(this.potLabel);
            this.Controls.Add(this.bot1Status);
            this.Controls.Add(this.bot2Status);
            this.Controls.Add(this.bot3Status);
            this.Controls.Add(this.bot4Status);
            this.Controls.Add(this.bot5Status);
            this.Controls.Add(this.playerStatus);
            this.Controls.Add(this.bigBlindTextBox);
            this.Controls.Add(this.smallBlindButton);
            this.Controls.Add(this.smallBlindTextBox);
            this.Controls.Add(this.bigBlindButton);
            this.Controls.Add(this.optionsButton);
            this.Controls.Add(this.potTextbox);
            this.Controls.Add(this.bot1TextboxChips);
            this.Controls.Add(this.bot2TextboxChips);
            this.Controls.Add(this.bot3TextboxChips);
            this.Controls.Add(this.bot4TextboxChips);
            this.Controls.Add(this.bot5TextboxChips);
            this.Controls.Add(this.addChipsTextBox);
            this.Controls.Add(this.addChipsButton);
            this.Controls.Add(this.playerTextboxChips);
            this.Controls.Add(this.timerProgressBar);
            this.Controls.Add(this.raiseButton);
            this.Controls.Add(this.callButton);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.foldButton);
            this.DoubleBuffered = true;
            this.Name = "Game";
            this.Text = "GLS Texas Poker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}