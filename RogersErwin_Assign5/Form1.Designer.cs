namespace RogersErwin_Assign5
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
            this.MenuStartPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.MenuTitlePanel = new System.Windows.Forms.Panel();
            this.MenuTitle = new System.Windows.Forms.TextBox();
            this.GamePanelDashboard = new System.Windows.Forms.Panel();
            this.GameTextStage = new System.Windows.Forms.TextBox();
            this.GameTextTime = new System.Windows.Forms.TextBox();
            this.GameButtonReset = new System.Windows.Forms.Button();
            this.GameButtonPause = new System.Windows.Forms.Button();
            this.GameButtonHint = new System.Windows.Forms.Button();
            this.GameButtonSolve = new System.Windows.Forms.Button();
            this.GamePanelBoard = new System.Windows.Forms.Panel();
            this.MenuStartPanel.SuspendLayout();
            this.MenuTitlePanel.SuspendLayout();
            this.GamePanelDashboard.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStartPanel
            // 
            this.MenuStartPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.MenuStartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuStartPanel.Controls.Add(this.button1);
            this.MenuStartPanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MenuStartPanel.Location = new System.Drawing.Point(40, 223);
            this.MenuStartPanel.Name = "MenuStartPanel";
            this.MenuStartPanel.Size = new System.Drawing.Size(704, 462);
            this.MenuStartPanel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(123, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // MenuTitlePanel
            // 
            this.MenuTitlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.MenuTitlePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuTitlePanel.Controls.Add(this.MenuTitle);
            this.MenuTitlePanel.Location = new System.Drawing.Point(125, 64);
            this.MenuTitlePanel.Name = "MenuTitlePanel";
            this.MenuTitlePanel.Size = new System.Drawing.Size(523, 131);
            this.MenuTitlePanel.TabIndex = 0;
            // 
            // MenuTitle
            // 
            this.MenuTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.MenuTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MenuTitle.Font = new System.Drawing.Font("Candara Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.MenuTitle.Location = new System.Drawing.Point(75, 24);
            this.MenuTitle.Name = "MenuTitle";
            this.MenuTitle.ReadOnly = true;
            this.MenuTitle.Size = new System.Drawing.Size(379, 79);
            this.MenuTitle.TabIndex = 0;
            this.MenuTitle.TabStop = false;
            this.MenuTitle.Text = ".NET Sudoku";
            this.MenuTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GamePanelDashboard
            // 
            this.GamePanelDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(40)))));
            this.GamePanelDashboard.Controls.Add(this.GameTextStage);
            this.GamePanelDashboard.Controls.Add(this.GameTextTime);
            this.GamePanelDashboard.Controls.Add(this.GameButtonReset);
            this.GamePanelDashboard.Controls.Add(this.GameButtonPause);
            this.GamePanelDashboard.Controls.Add(this.GameButtonHint);
            this.GamePanelDashboard.Controls.Add(this.GameButtonSolve);
            this.GamePanelDashboard.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GamePanelDashboard.Location = new System.Drawing.Point(0, 0);
            this.GamePanelDashboard.Name = "GamePanelDashboard";
            this.GamePanelDashboard.Size = new System.Drawing.Size(785, 100);
            this.GamePanelDashboard.TabIndex = 1;
            // 
            // GameTextStage
            // 
            this.GameTextStage.Font = new System.Drawing.Font("Candara Light", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameTextStage.Location = new System.Drawing.Point(256, 23);
            this.GameTextStage.Name = "GameTextStage";
            this.GameTextStage.Size = new System.Drawing.Size(219, 50);
            this.GameTextStage.TabIndex = 1;
            this.GameTextStage.Text = "Easy (1)";
            this.GameTextStage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GameTextTime
            // 
            this.GameTextTime.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameTextTime.Location = new System.Drawing.Point(599, 29);
            this.GameTextTime.Name = "GameTextTime";
            this.GameTextTime.Size = new System.Drawing.Size(173, 38);
            this.GameTextTime.TabIndex = 1;
            this.GameTextTime.Text = "00:00:0000";
            this.GameTextTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // GameButtonReset
            // 
            this.GameButtonReset.Location = new System.Drawing.Point(175, 12);
            this.GameButtonReset.Name = "GameButtonReset";
            this.GameButtonReset.Size = new System.Drawing.Size(75, 67);
            this.GameButtonReset.TabIndex = 0;
            this.GameButtonReset.Text = "Reset";
            this.GameButtonReset.UseVisualStyleBackColor = true;
            // 
            // GameButtonPause
            // 
            this.GameButtonPause.Location = new System.Drawing.Point(518, 12);
            this.GameButtonPause.Name = "GameButtonPause";
            this.GameButtonPause.Size = new System.Drawing.Size(75, 67);
            this.GameButtonPause.TabIndex = 0;
            this.GameButtonPause.Text = "Pause";
            this.GameButtonPause.UseVisualStyleBackColor = true;
            // 
            // GameButtonHint
            // 
            this.GameButtonHint.Location = new System.Drawing.Point(94, 13);
            this.GameButtonHint.Name = "GameButtonHint";
            this.GameButtonHint.Size = new System.Drawing.Size(75, 67);
            this.GameButtonHint.TabIndex = 0;
            this.GameButtonHint.Text = "Hint";
            this.GameButtonHint.UseVisualStyleBackColor = true;
            // 
            // GameButtonSolve
            // 
            this.GameButtonSolve.Location = new System.Drawing.Point(13, 13);
            this.GameButtonSolve.Name = "GameButtonSolve";
            this.GameButtonSolve.Size = new System.Drawing.Size(75, 67);
            this.GameButtonSolve.TabIndex = 0;
            this.GameButtonSolve.Text = "Solve";
            this.GameButtonSolve.UseVisualStyleBackColor = true;
            // 
            // GamePanelBoard
            // 
            this.GamePanelBoard.Location = new System.Drawing.Point(0, 99);
            this.GamePanelBoard.Name = "GamePanelBoard";
            this.GamePanelBoard.Size = new System.Drawing.Size(788, 662);
            this.GamePanelBoard.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.GamePanelBoard);
            this.Controls.Add(this.GamePanelDashboard);
            this.Controls.Add(this.MenuTitlePanel);
            this.Controls.Add(this.MenuStartPanel);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "Form1";
            this.Text = ".NET Sudoku";
            this.MenuStartPanel.ResumeLayout(false);
            this.MenuTitlePanel.ResumeLayout(false);
            this.MenuTitlePanel.PerformLayout();
            this.GamePanelDashboard.ResumeLayout(false);
            this.GamePanelDashboard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuStartPanel;
        private System.Windows.Forms.Panel MenuTitlePanel;
        private System.Windows.Forms.TextBox MenuTitle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel GamePanelDashboard;
        private System.Windows.Forms.TextBox GameTextStage;
        private System.Windows.Forms.TextBox GameTextTime;
        private System.Windows.Forms.Button GameButtonReset;
        private System.Windows.Forms.Button GameButtonPause;
        private System.Windows.Forms.Button GameButtonHint;
        private System.Windows.Forms.Button GameButtonSolve;
        private System.Windows.Forms.Panel GamePanelBoard;
    }
}

