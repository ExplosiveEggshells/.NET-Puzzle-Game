using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace RogersErwin_Assign5
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            SetMainMenuVisibility(true);
            SetGameVisibility(false);
        }

        private void SetMainMenuVisibility(bool state)
        {
            MenuTitlePanel.Enabled = state;
            MenuTitlePanel.Visible = state;
            MenuStartPanel.Enabled = state;
            MenuStartPanel.Visible = state;
        }

        private void SetGameVisibility(bool state)
        {
            GamePanelBoard.Enabled = state;
            GamePanelBoard.Visible = state;
            GamePanelDashboard.Enabled = state;
            GamePanelDashboard.Visible = state;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SetMainMenuVisibility(false);
            SetGameVisibility(true);

            FillBoard(3);
        }

        private void FillBoard(int size)
        {
            List<Cell> cells = new List<Cell>();

            for(int i = 0; i < size; i++)
            {
                Point nextPos = new Point(60 * i, 0);
                Size ssize = new Size(60, 60);
                Cell cell = new Cell(nextPos, ssize);
                GamePanelBoard.Controls.Add(cell.CellPanel);
            }
        }
    }
}
