namespace Poker
{
    /// <summary>
    /// The partial class for the design of the add chips form
    /// </summary>
    public partial class AddChips
    {
        private System.Windows.Forms.Label centerText;
        private System.Windows.Forms.Button addPlayersChipsButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox chipsToAdd;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.centerText = new System.Windows.Forms.Label();
            this.addPlayersChipsButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.chipsToAdd = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // centerText
            // 
            this.centerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.centerText.Location = new System.Drawing.Point(48, 49);
            this.centerText.Name = "centerText";
            this.centerText.Size = new System.Drawing.Size(176, 23);
            this.centerText.TabIndex = 0;
            this.centerText.Text = "You ran out of chips !";
            this.centerText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // addPlayersChipsButton
            // 
            this.addPlayersChipsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addPlayersChipsButton.Location = new System.Drawing.Point(12, 226);
            this.addPlayersChipsButton.Name = "addPlayersChipsButton";
            this.addPlayersChipsButton.Size = new System.Drawing.Size(75, 23);
            this.addPlayersChipsButton.TabIndex = 1;
            this.addPlayersChipsButton.Text = "Add Chips";
            this.addPlayersChipsButton.UseVisualStyleBackColor = true;
            this.addPlayersChipsButton.Click += new System.EventHandler(this.AddChipsButton);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exitButton.Location = new System.Drawing.Point(197, 226);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.CloseButton);
            // 
            // chipsToAdd
            // 
            this.chipsToAdd.Location = new System.Drawing.Point(91, 229);
            this.chipsToAdd.Name = "chipsToAdd";
            this.chipsToAdd.Size = new System.Drawing.Size(100, 20);
            this.chipsToAdd.TabIndex = 3;
            // 
            // AddChips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.chipsToAdd);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.addPlayersChipsButton);
            this.Controls.Add(this.centerText);
            this.Name = "AddChips";
            this.Text = "You Ran Out Of Chips";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}