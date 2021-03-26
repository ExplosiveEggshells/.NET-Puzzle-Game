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
        // TODO: Extract much of this crap out into a GameManager Class
        GameState game;

        public Form1()
        {
            InitializeComponent();
            SetMainMenuVisibility(true);
            SetGameVisibility(false);
        }

        private void SetMainMenuVisibility(bool state)
        {
            MenuPanelMaster.Enabled = state;
            MenuPanelMaster.Visible = state;
        }

        private void SetGameVisibility(bool state)
        {
            GamePanelMaster.Enabled = state;
            GamePanelMaster.Visible = state;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SetMainMenuVisibility(false);
            SetGameVisibility(true);

            game = new GameState(5, "ExampleGame", ref GamePanelUserBoard);
            GameButtonSave.Click += game.SaveState;
            using (StreamReader loadFile = new StreamReader("../../saves/ExampleGame.json"))
            {
                Stage load = JsonSerializer.Deserialize<Stage>(loadFile.ReadToEnd());
                game.LoadState(load);
            }
        }

        

        
    }
}
