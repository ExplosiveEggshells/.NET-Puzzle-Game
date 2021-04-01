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

        private void DiffictultyButton_Click(object sender, EventArgs e)
        {
            //SetMainMenuVisibility(false);
            //SetGameVisibility(true);

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
                StartNextGame(nextStage);
            }
            else
            {
                MessageBox.Show("You've completed every stage in this difficulty!");
            }
        }

        private void ResetGame()
        {
            if (game == null) { return; }

            game.PauseGame();
            DialogResult opt = MessageBox.Show("Would you like to reset the game?\nThis will delete any saved games for this difficulty.", "", MessageBoxButtons.YesNo);

            if (opt == DialogResult.Yes)
            {
                game.ResumeGame();
                string path = string.Format("../../saves/{0}.json", game.StageName);

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
                    DisposeCurrentGame();
                    StartNextGame(nextStage);
                }
            } else
            {
                game.ResumeGame();
            }
        }

        private void StartNextGame(Stage stage)
        {
            game = new Game(stage, ref GamePanelUserBoard, ref GameTextStage, ref GameTextTime, ref GameButtonPause, ref GameButtonProgress);
            GameButtonSave.Click += game.SaveState;
            game.save_finished += DisposeCurrentGame;
            SetGameVisibility(true);
            SetMainMenuVisibility(false);
        }

        private void DisposeCurrentGame()
        {
            game.DisposeGame();
            GameButtonSave.Click -= game.SaveState;
            game.save_finished -= DisposeCurrentGame;
            SetGameVisibility(false);
            SetMainMenuVisibility(true);
        }

        private void GameButtonReset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}
