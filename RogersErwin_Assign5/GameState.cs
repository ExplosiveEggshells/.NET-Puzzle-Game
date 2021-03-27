using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace RogersErwin_Assign5
{
    public class GameState
    {
        private BoardCell[,] boardCells;
        private List<Point> lockedCells;
        private SumCell[] rowSumCells;
        private SumCell[] columnSumCells;
        private SumCell diagonalSumCell;

        private List<int> solutionValues;
        private List<int> correctRowSums;
        private List<int> correctColumnSums;
        private int correctDiagonalSum;

        private Panel gameBoard;
        private TextBox stageNameTextBox;
        private int gameSize;

        private string stageName;

        public GameState(int gameSize, string stageName, ref Panel gameBoard, ref TextBox stageNameTextBox)
        {
            this.gameSize = gameSize;
            this.stageName = stageName;
            this.gameBoard = gameBoard;
            this.stageNameTextBox = stageNameTextBox;
        }

        public void StartGame()
        {
            FillBoard(gameSize);
        }

        public void SaveState(object sender, EventArgs e)
        {
            List<int> values = new List<int>();
            foreach (BoardCell cell in boardCells)
            {
                values.Add(cell.Value);
            }

            Stage save = new Stage(values, solutionValues, lockedCells, gameSize, stageName, correctRowSums, correctColumnSums, correctDiagonalSum, true);
            string jsonString = JsonSerializer.Serialize(save);


            string path = String.Format("../../saves/{0}.json", stageName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter saveFile = new StreamWriter(path))
            {
                saveFile.Write(jsonString);
            }

            MessageBox.Show("Saved!");
        }

        public void LoadState(Stage load)
        {
            gameSize = load.gameSize;
            stageName = load.stageName;
            FillBoard(gameSize);

            int ptr = 0;
            for(int i = 0; i < gameSize; i++)
            {
                for (int j = 0; j < gameSize; j++, ptr++)
                {
                    boardCells[i,j].Value = load.boardValues[ptr];
                }
            }

            stageNameTextBox.Text = stageName;
            lockedCells = load.lockedCells;
            correctRowSums = load.correctRowSums;
            correctColumnSums = load.correctColumnSums;
            correctDiagonalSum = load.correctDiagonalSum;
            solutionValues = load.solutionValues;

            for(int i = 0; i < gameSize; i++)
            {
                for(int j = 0; j < gameSize; j++)
                {
                    UpdateSums(i,j);
                }
            }

            DisableLockedCells();
        }

        private void DisableLockedCells()
        {
            foreach (BoardCell cell in boardCells)
            {
                Point p = new Point(cell.Row, cell.Column);
                foreach (Point p2 in lockedCells)
                {
                    if (p.X == p2.X && p.Y == p2.Y)
                    {
                        cell.CellTextBox.Enabled = false;
                        cell.CellTextBox.BackColor = Color.White;
                    }
                }
            }
        }

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
                        diagonalSumCell.CellPanel.BackColor = Color.FromArgb(173, 220, 255);
                        diagonalSumCell.CellTextBox.BackColor = Color.FromArgb(173, 220, 255);
                        cell = diagonalSumCell;
                    }
                    else if (j == size - 1)
                    {
                        SumCell sumCell = new SumCell(nextPos, nextSize, SumType.Row, i);
                        sumCell.Type = SumType.Row;
                        rowSumCells[i] = sumCell;
                        sumCell.CellPanel.BackColor = Color.FromArgb(173, 220, 255);
                        sumCell.CellTextBox.BackColor = Color.FromArgb(173, 220, 255);
                        cell = sumCell;
                    }
                    else if (i == size - 1)
                    {
                        SumCell sumCell = new SumCell(nextPos, nextSize, SumType.Column, j);
                        sumCell.Type = SumType.Column;
                        columnSumCells[j] = sumCell;
                        sumCell.CellPanel.BackColor = Color.FromArgb(173, 220, 255);
                        sumCell.CellTextBox.BackColor = Color.FromArgb(173, 220, 255);
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
