/*
 * NAME: Form1.cs
 * AUTHORS: Jake Rogers (z1826513), John Erwin (z1856469)
 *
 * 
 */
using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;
using System.IO;


namespace RogersErwin_Assign5
{
    public partial class Form1 : Form
    {
        Game game;
        StageManager stageManager;
        static Stopwatch myTimer = new Stopwatch();
        System.Timers.Timer swRenderTimer;

        public Stopwatch MyTimer { get { return myTimer; } }

        public Form1()
        {
            InitializeComponent();
            SetMainMenuVisibility(true);
            SetGameVisibility(false);

            stageManager = new StageManager();
            stageManager.BuildStageLists();
        }

        /*
         * Sets the enabled and visible state of the main menu to state.
         */
        private void SetMainMenuVisibility(bool state)
        {
            MenuPanelMaster.Enabled = state;
            MenuPanelMaster.Visible = state;
        }

        /*
         * Same as above, but for the Game menu.
         */
        private void SetGameVisibility(bool state)
        {
            GamePanelMaster.Enabled = state;
            GamePanelMaster.Visible = state;
        }

        private void SetGamePanelUserBoardVisibility(bool state)
        {
            GamePanelUserBoard.Enabled = state;
            GamePanelUserBoard.Visible = state;
        }

        private void DiffictultyButton_Click(object sender, EventArgs e)
        {
            SetMainMenuVisibility(false);
            SetGameVisibility(true);
            TimerInitializer();

            Button btn = sender as Button;
            GetGameDiffictulyFromButton(ref btn);
        }

        private void GetGameDiffictulyFromButton(ref Button btn)
        {
            Stage nextStage;
            if (btn.Name.Equals("EasyDifficultyButton"))
                nextStage = stageManager.GetNextDifficulty(stageManager.EasyStages);   // Get the next stage from EasyStages
            else if (btn.Name.Equals("MediumDifficultyButton"))
                nextStage = stageManager.GetNextDifficulty(stageManager.MediumStages);   // Get the next stage from EasyStages
            else
                nextStage = stageManager.GetNextDifficulty(stageManager.HardStages);   // Get the next stage from EasyStages

            if (nextStage != null)
            {
                game = new Game(nextStage, ref GamePanelUserBoard, ref GameTextStage);
                GameButtonSave.Click += game.SaveState;
            }
            else
            {
                MessageBox.Show("You've completed every stage in this difficulty!");
            }
        }

        private void GameButtonPause_click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.Text.Equals("Pause"))
            {
                // pause timer
                MyTimer.Stop();
                SetGamePanelUserBoardVisibility(false);
                button.Text = "Resume";
            }
            else
            {
                // resume timer
                MyTimer.Start();
                SetGamePanelUserBoardVisibility(true);
                button.Text = "Pause";
            }
        }

        private void RenderTimer(object sender, ElapsedEventArgs e)
        {
            GameTextTime.Text = myTimer.Elapsed.ToString();
        }

        private void TimerInitializer()
        {
            MyTimer.Start();
            swRenderTimer = new System.Timers.Timer(10);
            swRenderTimer.AutoReset = true;
            swRenderTimer.Elapsed += RenderTimer; //RenderTimer signiture needs to be modified to work with .Elapsed delegate
            swRenderTimer.Start();
        }

        private void ResetGame()
        {
            MyTimer.Stop();
            SetGamePanelUserBoardVisibility(false);
            DialogResult opt = MessageBox.Show("Would you like to reset the game?\nThis will delete any saved games for this difficulty.","", MessageBoxButtons.YesNo);

            if (opt == DialogResult.Yes)
            {
                string path = string.Format("../../saves/{0}.json", GameTextStage.Text);

                if (File.Exists(path)) File.Delete(path); // If a save with the same tag already exists, overwrite it.                

                Stage nextStage;
                switch (game.StageName[0])
                {
                    case 'e':
                        nextStage = stageManager.GetNextDifficulty(stageManager.EasyStages);
                        break;
                    case 'm':
                        nextStage = stageManager.GetNextDifficulty(stageManager.MediumStages);
                        break;
                    case 'h':
                        nextStage = stageManager.GetNextDifficulty(stageManager.HardStages);
                        break;
                    default:
                        nextStage = null;
                        break;
                }
                if (nextStage != null)
                {
                    game = new Game(nextStage, ref GamePanelUserBoard, ref GameTextStage);
                    GameButtonSave.Click += game.SaveState;
                    MyTimer.Restart();
                }
            }
            MyTimer.Start();
            SetGamePanelUserBoardVisibility(true);
        }

        private void GameButtonReset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}
