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

            FillBoard(5);
        }

        private void FillBoard(int size)
        {
            size++;
            List<Cell> cells = new List<Cell>();
            List<SumCell> sumCells = new List<SumCell>();

            int cellSize = GamePanelUserBoard.Width / size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Point nextPos = new Point(cellSize * j, cellSize * i);
                    Size ssize = new Size(cellSize, cellSize);

                    Cell cell;
                    if (j == size - 1 && i == size - 1)
                    {
                        SumCell sumCell = new SumCell(nextPos, ssize, SumType.Diagonal, 0);
                        cell = sumCell;
                    }
                    else if (j == size - 1)
                    {
                        SumCell sumCell = new SumCell(nextPos, ssize, SumType.Row, i);
                        sumCell.Type = SumType.Row;
                        cell = sumCell;
                    }
                    else if (i == size - 1)
                    {
                        SumCell sumCell = new SumCell(nextPos, ssize, SumType.Column, j);
                        sumCell.Type = SumType.Column;
                        cell = sumCell;
                    }
                    else
                    {
                        cell = new Cell(nextPos, ssize);
                    }

                    GamePanelUserBoard.Controls.Add(cell.CellPanel);
                    cells.Add(cell);
                }


            }
        }
    }
}
