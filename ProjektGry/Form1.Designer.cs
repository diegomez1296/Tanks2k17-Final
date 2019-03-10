namespace ProjektGry
{
    partial class Form1
    {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.MainTime = new System.Windows.Forms.Timer(this.components);
            this.TimeReload = new System.Windows.Forms.Timer(this.components);
            this.MainPanel = new System.Windows.Forms.Panel();
            this.Player2HP40 = new System.Windows.Forms.PictureBox();
            this.Player1HP40 = new System.Windows.Forms.PictureBox();
            this.Bullet2Reload = new System.Windows.Forms.PictureBox();
            this.BulletReload = new System.Windows.Forms.PictureBox();
            this.Player2HP10 = new System.Windows.Forms.PictureBox();
            this.Player2HP30 = new System.Windows.Forms.PictureBox();
            this.Player1HP30 = new System.Windows.Forms.PictureBox();
            this.Player2HP20 = new System.Windows.Forms.PictureBox();
            this.Player1HP20 = new System.Windows.Forms.PictureBox();
            this.Player1HP10 = new System.Windows.Forms.PictureBox();
            this.Tank2_Nick = new System.Windows.Forms.Label();
            this.Tank1_Nick = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Player2HP40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1HP40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bullet2Reload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BulletReload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2HP10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2HP30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1HP30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2HP20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1HP20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1HP10)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTime
            // 
            this.MainTime.Enabled = true;
            this.MainTime.Interval = 1;
            this.MainTime.Tick += new System.EventHandler(this.MainTime_Tick);
            // 
            // TimeReload
            // 
            this.TimeReload.Enabled = true;
            this.TimeReload.Interval = 1000;
            this.TimeReload.Tick += new System.EventHandler(this.TimeReload_Tick);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel.MaximumSize = new System.Drawing.Size(600, 600);
            this.MainPanel.MinimumSize = new System.Drawing.Size(600, 600);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(600, 600);
            this.MainPanel.TabIndex = 0;
            // 
            // Player2HP40
            // 
            this.Player2HP40.BackColor = System.Drawing.Color.Black;
            this.Player2HP40.Image = ((System.Drawing.Image)(resources.GetObject("Player2HP40.Image")));
            this.Player2HP40.Location = new System.Drawing.Point(210, 630);
            this.Player2HP40.Name = "Player2HP40";
            this.Player2HP40.Size = new System.Drawing.Size(29, 30);
            this.Player2HP40.TabIndex = 40;
            this.Player2HP40.TabStop = false;
            // 
            // Player1HP40
            // 
            this.Player1HP40.BackColor = System.Drawing.Color.Black;
            this.Player1HP40.Image = ((System.Drawing.Image)(resources.GetObject("Player1HP40.Image")));
            this.Player1HP40.Location = new System.Drawing.Point(350, 630);
            this.Player1HP40.Name = "Player1HP40";
            this.Player1HP40.Size = new System.Drawing.Size(29, 30);
            this.Player1HP40.TabIndex = 39;
            this.Player1HP40.TabStop = false;
            // 
            // Bullet2Reload
            // 
            this.Bullet2Reload.BackColor = System.Drawing.Color.DarkRed;
            this.Bullet2Reload.ErrorImage = null;
            this.Bullet2Reload.Image = ((System.Drawing.Image)(resources.GetObject("Bullet2Reload.Image")));
            this.Bullet2Reload.Location = new System.Drawing.Point(40, 610);
            this.Bullet2Reload.Name = "Bullet2Reload";
            this.Bullet2Reload.Size = new System.Drawing.Size(39, 40);
            this.Bullet2Reload.TabIndex = 33;
            this.Bullet2Reload.TabStop = false;
            // 
            // BulletReload
            // 
            this.BulletReload.BackColor = System.Drawing.Color.DarkRed;
            this.BulletReload.Image = ((System.Drawing.Image)(resources.GetObject("BulletReload.Image")));
            this.BulletReload.InitialImage = null;
            this.BulletReload.Location = new System.Drawing.Point(520, 610);
            this.BulletReload.Name = "BulletReload";
            this.BulletReload.Size = new System.Drawing.Size(39, 40);
            this.BulletReload.TabIndex = 32;
            this.BulletReload.TabStop = false;
            // 
            // Player2HP10
            // 
            this.Player2HP10.BackColor = System.Drawing.Color.Black;
            this.Player2HP10.Image = ((System.Drawing.Image)(resources.GetObject("Player2HP10.Image")));
            this.Player2HP10.Location = new System.Drawing.Point(90, 630);
            this.Player2HP10.Name = "Player2HP10";
            this.Player2HP10.Size = new System.Drawing.Size(29, 30);
            this.Player2HP10.TabIndex = 35;
            this.Player2HP10.TabStop = false;
            // 
            // Player2HP30
            // 
            this.Player2HP30.BackColor = System.Drawing.Color.Black;
            this.Player2HP30.Image = ((System.Drawing.Image)(resources.GetObject("Player2HP30.Image")));
            this.Player2HP30.Location = new System.Drawing.Point(170, 630);
            this.Player2HP30.Name = "Player2HP30";
            this.Player2HP30.Size = new System.Drawing.Size(29, 30);
            this.Player2HP30.TabIndex = 34;
            this.Player2HP30.TabStop = false;
            // 
            // Player1HP30
            // 
            this.Player1HP30.BackColor = System.Drawing.Color.Black;
            this.Player1HP30.Image = ((System.Drawing.Image)(resources.GetObject("Player1HP30.Image")));
            this.Player1HP30.Location = new System.Drawing.Point(390, 630);
            this.Player1HP30.Name = "Player1HP30";
            this.Player1HP30.Size = new System.Drawing.Size(29, 30);
            this.Player1HP30.TabIndex = 32;
            this.Player1HP30.TabStop = false;
            // 
            // Player2HP20
            // 
            this.Player2HP20.BackColor = System.Drawing.Color.Black;
            this.Player2HP20.Image = ((System.Drawing.Image)(resources.GetObject("Player2HP20.Image")));
            this.Player2HP20.Location = new System.Drawing.Point(130, 630);
            this.Player2HP20.Name = "Player2HP20";
            this.Player2HP20.Size = new System.Drawing.Size(29, 30);
            this.Player2HP20.TabIndex = 33;
            this.Player2HP20.TabStop = false;
            // 
            // Player1HP20
            // 
            this.Player1HP20.BackColor = System.Drawing.Color.Black;
            this.Player1HP20.Image = ((System.Drawing.Image)(resources.GetObject("Player1HP20.Image")));
            this.Player1HP20.Location = new System.Drawing.Point(430, 630);
            this.Player1HP20.Name = "Player1HP20";
            this.Player1HP20.Size = new System.Drawing.Size(29, 30);
            this.Player1HP20.TabIndex = 30;
            this.Player1HP20.TabStop = false;
            // 
            // Player1HP10
            // 
            this.Player1HP10.BackColor = System.Drawing.Color.Black;
            this.Player1HP10.Image = ((System.Drawing.Image)(resources.GetObject("Player1HP10.Image")));
            this.Player1HP10.Location = new System.Drawing.Point(470, 630);
            this.Player1HP10.Name = "Player1HP10";
            this.Player1HP10.Size = new System.Drawing.Size(29, 30);
            this.Player1HP10.TabIndex = 31;
            this.Player1HP10.TabStop = false;
            // 
            // Tank2_Nick
            // 
            this.Tank2_Nick.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Tank2_Nick.Location = new System.Drawing.Point(90, 600);
            this.Tank2_Nick.Name = "Tank2_Nick";
            this.Tank2_Nick.Size = new System.Drawing.Size(150, 30);
            this.Tank2_Nick.TabIndex = 41;
            this.Tank2_Nick.Text = "Tank2";
            this.Tank2_Nick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Tank1_Nick
            // 
            this.Tank1_Nick.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Tank1_Nick.Location = new System.Drawing.Point(350, 600);
            this.Tank1_Nick.Name = "Tank1_Nick";
            this.Tank1_Nick.Size = new System.Drawing.Size(150, 30);
            this.Tank1_Nick.TabIndex = 42;
            this.Tank1_Nick.Text = "Tank1";
            this.Tank1_Nick.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(599, 661);
            this.Controls.Add(this.Tank1_Nick);
            this.Controls.Add(this.Tank2_Nick);
            this.Controls.Add(this.Player2HP40);
            this.Controls.Add(this.Player1HP40);
            this.Controls.Add(this.Bullet2Reload);
            this.Controls.Add(this.BulletReload);
            this.Controls.Add(this.Player2HP10);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.Player2HP30);
            this.Controls.Add(this.Player1HP30);
            this.Controls.Add(this.Player2HP20);
            this.Controls.Add(this.Player1HP20);
            this.Controls.Add(this.Player1HP10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(615, 700);
            this.MinimumSize = new System.Drawing.Size(615, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tanks 2k17";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Player2HP40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1HP40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bullet2Reload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BulletReload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2HP10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2HP30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1HP30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player2HP20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1HP20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player1HP10)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Timer MainTime;
        private System.Windows.Forms.Timer TimeReload;
        private System.Windows.Forms.PictureBox Player1HP10;
        private System.Windows.Forms.PictureBox Player1HP20;
        private System.Windows.Forms.PictureBox Player2HP10;
        private System.Windows.Forms.PictureBox Player2HP30;
        private System.Windows.Forms.PictureBox Player2HP20;
        private System.Windows.Forms.PictureBox Player1HP30;
        private System.Windows.Forms.PictureBox BulletReload;
        private System.Windows.Forms.PictureBox Bullet2Reload;
        private System.Windows.Forms.PictureBox Player1HP40;
        private System.Windows.Forms.PictureBox Player2HP40;
        private System.Windows.Forms.Label Tank2_Nick;
        private System.Windows.Forms.Label Tank1_Nick;
    }
}

