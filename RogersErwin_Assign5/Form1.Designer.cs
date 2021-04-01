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
            this.MediumDifficultyButton = new System.Windows.Forms.Button();
            this.HardDifficultyButton = new System.Windows.Forms.Button();
            this.EasyDifficultyButton = new System.Windows.Forms.Button();
            this.MenuTitlePanel = new System.Windows.Forms.Panel();
            this.MenuTitle = new System.Windows.Forms.TextBox();
            this.GamePanelDashboard = new System.Windows.Forms.Panel();
            this.GameTextStage = new System.Windows.Forms.TextBox();
            this.GameTextTime = new System.Windows.Forms.TextBox();
            this.GameButtonReset = new System.Windows.Forms.Button();
            this.GameButtonSave = new System.Windows.Forms.Button();
            this.GameButtonPause = new System.Windows.Forms.Button();
            this.GameButtonHint = new System.Windows.Forms.Button();
            this.GameButtonSolve = new System.Windows.Forms.Button();
            this.GamePanelUserBoard = new System.Windows.Forms.Panel();
            this.GamePanelMaster = new System.Windows.Forms.Panel();
            this.MenuPanelMaster = new System.Windows.Forms.Panel();
            this.MenuStartPanel.SuspendLayout();
            this.MenuTitlePanel.SuspendLayout();
            this.GamePanelDashboard.SuspendLayout();
            this.GamePanelMaster.SuspendLayout();
            this.MenuPanelMaster.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStartPanel
            // 
            this.MenuStartPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.MenuStartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuStartPanel.Controls.Add(this.MediumDifficultyButton);
            this.MenuStartPanel.Controls.Add(this.HardDifficultyButton);
            this.MenuStartPanel.Controls.Add(this.EasyDifficultyButton);
            this.MenuStartPanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MenuStartPanel.Location = new System.Drawing.Point(40, 223);
            this.MenuStartPanel.Name = "MenuStartPanel";
            this.MenuStartPanel.Size = new System.Drawing.Size(704, 462);
            this.MenuStartPanel.TabIndex = 0;
            // 
            // MediumDifficultyButton
            // 
            this.MediumDifficultyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MediumDifficultyButton.Location = new System.Drawing.Point(288, 70);
            this.MediumDifficultyButton.Name = "MediumDifficultyButton";
            this.MediumDifficultyButton.Size = new System.Drawing.Size(125, 40);
            this.MediumDifficultyButton.TabIndex = 3;
            this.MediumDifficultyButton.Text = "Medium";
            this.MediumDifficultyButton.UseVisualStyleBackColor = true;
            this.MediumDifficultyButton.Click += new System.EventHandler(this.DiffictultyButton_Click);
            // 
            // HardDifficultyButton
            // 
            this.HardDifficultyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HardDifficultyButton.Location = new System.Drawing.Point(491, 70);
            this.HardDifficultyButton.Name = "HardDifficultyButton";
            this.HardDifficultyButton.Size = new System.Drawing.Size(116, 40);
            this.HardDifficultyButton.TabIndex = 2;
            this.HardDifficultyButton.Text = "Hard";
            this.HardDifficultyButton.UseVisualStyleBackColor = true;
            this.HardDifficultyButton.Click += new System.EventHandler(this.DiffictultyButton_Click);
            // 
            // EasyDifficultyButton
            // 
            this.EasyDifficultyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EasyDifficultyButton.Location = new System.Drawing.Point(84, 70);
            this.EasyDifficultyButton.Name = "EasyDifficultyButton";
            this.EasyDifficultyButton.Size = new System.Drawing.Size(125, 40);
            this.EasyDifficultyButton.TabIndex = 1;
            this.EasyDifficultyButton.Text = "Easy";
            this.EasyDifficultyButton.UseVisualStyleBackColor = true;
            this.EasyDifficultyButton.Click += new System.EventHandler(this.DiffictultyButton_Click);
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
            this.GamePanelDashboard.Controls.Add(this.GameButtonSave);
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
            this.GameTextStage.Size = new System.Drawing.Size(155, 50);
            this.GameTextStage.TabIndex = 1;
            this.GameTextStage.Text = "Easy (1)";
            this.GameTextStage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GameTextTime
            // 
            this.GameTextTime.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameTextTime.Location = new System.Drawing.Point(613, 29);
            this.GameTextTime.Name = "GameTextTime";
            this.GameTextTime.Size = new System.Drawing.Size(159, 35);
            this.GameTextTime.TabIndex = 1;
            this.GameTextTime.Text = "00:00.000";
            this.GameTextTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GameButtonReset
            // 
            this.GameButtonReset.Location = new System.Drawing.Point(175, 12);
            this.GameButtonReset.Name = "GameButtonReset";
            this.GameButtonReset.Size = new System.Drawing.Size(75, 67);
            this.GameButtonReset.TabIndex = 0;
            this.GameButtonReset.Text = "Reset";
            this.GameButtonReset.UseVisualStyleBackColor = true;
            this.GameButtonReset.Click += new System.EventHandler(this.GameButtonReset_Click);
            // 
            // GameButtonSave
            // 
            this.GameButtonSave.Location = new System.Drawing.Point(451, 12);
            this.GameButtonSave.Name = "GameButtonSave";
            this.GameButtonSave.Size = new System.Drawing.Size(75, 67);
            this.GameButtonSave.TabIndex = 0;
            this.GameButtonSave.Text = "Save";
            this.GameButtonSave.UseVisualStyleBackColor = true;
            // 
            // GameButtonPause
            // 
            this.GameButtonPause.Location = new System.Drawing.Point(532, 12);
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
            // GamePanelUserBoard
            // 
            this.GamePanelUserBoard.Location = new System.Drawing.Point(62, 106);
            this.GamePanelUserBoard.Name = "GamePanelUserBoard";
            this.GamePanelUserBoard.Size = new System.Drawing.Size(643, 643);
            this.GamePanelUserBoard.TabIndex = 2;
            // 
            // GamePanelMaster
            // 
            this.GamePanelMaster.Controls.Add(this.GamePanelDashboard);
            this.GamePanelMaster.Controls.Add(this.GamePanelUserBoard);
            this.GamePanelMaster.Location = new System.Drawing.Point(0, 0);
            this.GamePanelMaster.Name = "GamePanelMaster";
            this.GamePanelMaster.Size = new System.Drawing.Size(785, 765);
            this.GamePanelMaster.TabIndex = 2;
            // 
            // MenuPanelMaster
            // 
            this.MenuPanelMaster.BackColor = System.Drawing.Color.Black;
            this.MenuPanelMaster.Controls.Add(this.MenuTitlePanel);
            this.MenuPanelMaster.Controls.Add(this.MenuStartPanel);
            this.MenuPanelMaster.Location = new System.Drawing.Point(0, 0);
            this.MenuPanelMaster.Name = "MenuPanelMaster";
            this.MenuPanelMaster.Size = new System.Drawing.Size(785, 765);
            this.MenuPanelMaster.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.GamePanelMaster);
            this.Controls.Add(this.MenuPanelMaster);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "Form1";
            this.Text = ".NET Sudoku";
            this.MenuStartPanel.ResumeLayout(false);
            this.MenuTitlePanel.ResumeLayout(false);
            this.MenuTitlePanel.PerformLayout();
            this.GamePanelDashboard.ResumeLayout(false);
            this.GamePanelDashboard.PerformLayout();
            this.GamePanelMaster.ResumeLayout(false);
            this.MenuPanelMaster.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuStartPanel;
        private System.Windows.Forms.Panel MenuTitlePanel;
        private System.Windows.Forms.TextBox MenuTitle;
        private System.Windows.Forms.Panel GamePanelDashboard;
        private System.Windows.Forms.TextBox GameTextStage;
        private System.Windows.Forms.TextBox GameTextTime;
        private System.Windows.Forms.Button GameButtonReset;
        private System.Windows.Forms.Button GameButtonPause;
        private System.Windows.Forms.Button GameButtonHint;
        private System.Windows.Forms.Button GameButtonSolve;
        private System.Windows.Forms.Panel GamePanelUserBoard;
        private System.Windows.Forms.Panel GamePanelMaster;
        private System.Windows.Forms.Panel MenuPanelMaster;
        private System.Windows.Forms.Button GameButtonSave;
        private System.Windows.Forms.Button EasyDifficultyButton;
        private System.Windows.Forms.Button MediumDifficultyButton;
        private System.Windows.Forms.Button HardDifficultyButton;
    }
}

