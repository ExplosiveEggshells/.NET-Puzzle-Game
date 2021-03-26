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
        // TODO: Extract much of this crap out into a GameManager Class
        BoardCell[,] boardCells;
        List<SumCell> rowSumCells;
        List<SumCell> columnSumCells;
        SumCell diagonalSumCell;

        int gameSize;
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
            gameSize = size;
            boardCells = new BoardCell[size, size];
            size++;
            rowSumCells = new List<SumCell>();
            columnSumCells = new List<SumCell>();
            int cellSize = GamePanelUserBoard.Width / size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Point nextPos = new Point(cellSize * j, cellSize * i);
                    Size nextSize = new Size(cellSize, cellSize);

                    Cell cell;
                    if (j == size - 1 && i == size - 1)
                    {
                        diagonalSumCell = new SumCell(nextPos, nextSize, SumType.Diagonal, 0);
                        cell = diagonalSumCell;
                    }
                    else if (j == size - 1)
                    {
                        SumCell sumCell = new SumCell(nextPos, nextSize, SumType.Row, i);
                        sumCell.Type = SumType.Row;
                        rowSumCells.Add(sumCell);
                        cell = sumCell;
                    }
                    else if (i == size - 1)
                    {
                        SumCell sumCell = new SumCell(nextPos, nextSize, SumType.Column, j);
                        sumCell.Type = SumType.Column;
                        columnSumCells.Add(sumCell);
                        cell = sumCell;
                    }
                    else
                    {
                        BoardCell boardCell = new BoardCell(nextPos, nextSize, i, j);
                        boardCells[i, j] = boardCell;
                        boardCell.Value_Changed += UpdateSums;
                        cell = boardCell;
                    }

                    GamePanelUserBoard.Controls.Add(cell.CellPanel);
                }
            }

        }

        private void UpdateSums(int row, int column)
        {
            int rowSum = 0;
            int columnSum = 0;
            
            for (int i = 0; i < gameSize; i++)
            {
                rowSum += boardCells[row, i].Value;
            }
            
            for (int i = 0; i < gameSize; i++)
            {
                columnSum += boardCells[i, column].Value;
            }

            rowSumCells[row].CellTextBox.Text = rowSum.ToString();
            columnSumCells[column].CellTextBox.Text = columnSum.ToString();

            if (row == column)
            {
                int diagonalSum = 0;
                for (int i = 0; i < gameSize; i++)
                {
                    diagonalSum += boardCells[i, i].Value;
                }

                diagonalSumCell.CellTextBox.Text = diagonalSum.ToString();
            }
        }
    }
}
