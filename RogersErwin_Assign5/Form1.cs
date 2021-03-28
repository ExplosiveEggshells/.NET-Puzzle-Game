/*
 * NAME: Form1.cs
 * AUTHORS: Jake Rogers (z1826513), John Erwin (z1856469)
 *
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;


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

        /*
         * Temporary Function used to start the game. 
         */
        private void Button1_Click(object sender, EventArgs e)
        {
            SetMainMenuVisibility(false);
            SetGameVisibility(true);

            Stage nextEasy = stageManager.GetNextDifficulty(stageManager.EasyStages);   // Get the next stage from EasyStages
            if (nextEasy != null)
            {
                game = new Game(nextEasy, ref GamePanelUserBoard, ref GameTextStage, ref GameButtonProgress);
                GameButtonSave.Click += game.SaveState;
            }
            else
            {
                MessageBox.Show("You've completed every stage in this difficulty!");
            }
        }




    }
}
