using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RogersErwin_Assign5
{
    public class GameState
    {
        private BoardCell[,] boardCells;
        private SumCell[] rowSumCells;
        private SumCell[] columnSumCells;
        private SumCell diagonalSumCell;

        private Panel gameBoard;
        private int gameSize;

        public GameState(int gameSize, ref Panel gameBoard)
        {
            this.gameSize = gameSize;
            this.gameBoard = gameBoard;
        }

        public void StartGame()
        {
            FillBoard(gameSize);
        }

        //public string SaveGameState()
        //{
        //    string jsonString = JsonSerializer.
        //}

        private void FillBoard(int size)
        {
            gameSize = size;
            boardCells = new BoardCell[size, size];
            rowSumCells = new SumCell[size];
            columnSumCells = new SumCell[size];
            size++;
            int cellSize = gameBoard.Width / size;

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
                        rowSumCells[i] = sumCell;
                        cell = sumCell;
                    }
                    else if (i == size - 1)
                    {
                        SumCell sumCell = new SumCell(nextPos, nextSize, SumType.Column, j);
                        sumCell.Type = SumType.Column;
                        columnSumCells[j] = sumCell;
                        cell = sumCell;
                    }
                    else
                    {
                        BoardCell boardCell = new BoardCell(nextPos, nextSize, i, j);
                        boardCells[i, j] = boardCell;
                        boardCell.Value_Changed += UpdateSums;
                        cell = boardCell;
                    }

                    gameBoard.Controls.Add(cell.CellPanel);
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
