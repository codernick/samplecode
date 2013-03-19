namespace SquareItUp
{
    partial class frmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.myPhoneTimer = new System.Windows.Forms.Timer();
            this.myScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // myScore
            // 
            this.myScore.Location = new System.Drawing.Point(22, 4);
            this.myScore.Name = "myScore";
            this.myScore.Size = new System.Drawing.Size(100, 20);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.myScore);
            this.Menu = this.mainMenu1;
            this.Name = "frmHome";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer myPhoneTimer;
        private System.Windows.Forms.Label myScore;
    }
}

