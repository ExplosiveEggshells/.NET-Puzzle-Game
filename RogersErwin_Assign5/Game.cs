/*
 * NAME: Game.cs
 * AUTHORS: Jake Rogers (z1826513) John Erwin (z1856469)
 * 
 * Game objects contain all of the logic for initializing the game and
 * game board when provided with a Stage object (which contain default
 * or saved game-states in a Json-friendly manner)
 */
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
using System.Diagnostics;
using System.Timers;

namespace RogersErwin_Assign5
{
    public class Game
    {
        // Documentation about these game-state properties can be found in Stage.cs.
        // Game-State properties
        private BoardCell[,] boardCells;
        private List<Point> lockedCells;
        private SumCell[] rowSumCells;
        private SumCell[] columnSumCells;
        private SumCell diagonalSumCell;

        private List<int> solutionValues;
        private List<int> correctRowSums;
        private List<int> correctColumnSums;
        private int correctDiagonalSum;

        public Stopwatch gameSW = new Stopwatch();
        public System.Timers.Timer swRenderTimer;
        public long trueTime = 0;

        private int gameSize;
        private string stageName;
        private long millisecondsElapsed;

        private bool completed;

        // References to UI controls.
        private Panel gameBoard;
        private TextBox stageNameTextBox;
        private TextBox gameTextTime;
        private Button gameButtonPause;

        // Properties
        public string StageName { get { return stageName; } }

        /*
         * Constructor passes in references to UI elements scoped in Form1.cs, as well
         * as a Stage to load it's initial state from.
         */
        public Game(Stage stage, ref Panel gameBoard, ref TextBox stageNameTextBox, ref TextBox gameTextTime, ref Button gameButtonPause)
        {
            this.gameBoard = gameBoard;
            this.stageNameTextBox = stageNameTextBox;
            this.gameTextTime = gameTextTime;
            this.gameButtonPause = gameButtonPause;

            this.gameButtonPause.Click += PauseOrResumeGame;

            LoadState(stage);
        }

        /*
         * Unpacks the Stage object 'load' and uses it to initalize all
         * game-state variables in this instance of Game.
         */
        public void LoadState(Stage load)
        {
            gameSize = load.gameSize;
            completed = false;      // In the context of this assignment, completed games should never be loaded, so it's safe to assume that a game being loaded is in-progress.
            FillBoard(gameSize);


            /*
             * Translate the 1D board into values at the appropriate spot in the boardCells array.
             */
            int ptr = 0;
            for (int i = 0; i < gameSize; i++)
            {
                for (int j = 0; j < gameSize; j++, ptr++)
                {
                    boardCells[i, j].Value = load.boardValues[ptr];
                }
            }

            // Copy over various data.
            stageName = load.stageName;
            stageNameTextBox.Text = stageName;
            lockedCells = load.lockedCells;
            correctRowSums = load.correctRowSums;
            correctColumnSums = load.correctColumnSums;
            correctDiagonalSum = load.correctDiagonalSum;
            solutionValues = load.solutionValues;
            millisecondsElapsed = load.millisecondsElapsed;
            trueTime = millisecondsElapsed;

            gameSW.Start();
            TimerInitializer();

            if (millisecondsElapsed != 0)
            {
                gameButtonPause.Text = "Resume";
                PauseOrResumeGame(gameButtonPause, null);
            }

            // Call UpdateSums on every cell in the diagonal, essentially intializing all sums.
            for (int i = 0; i < gameSize; i++)
            {
                UpdateSums(i, i);
            }

            DisableLockedCells();
        }

        /*
         * Does the reverse of above: Packs this Game's state down
         * into a Stage object and then saves it to the ../../saves
         * folder so that it will be loaded the next time this difficulty
         * is selected (provided it isn't complete).
         */
        public void SaveState(object sender, EventArgs e)
        {
            // Pack the 2D boardCells array into a 1D-Representation of the sudoku board (Json-friendly).
            List<int> values = new List<int>();
            foreach (BoardCell cell in boardCells)
            {
                values.Add(cell.Value);
            }

            //Save all data into the Stage object, then serialize it into a json string.
            Stage save = new Stage(values, solutionValues, lockedCells, gameSize, stageName, correctRowSums, correctColumnSums, correctDiagonalSum, trueTime);
            string jsonString = JsonSerializer.Serialize(save);

            string path = String.Format("../../saves/{0}.json", stageName); // Create the path and filename for this save
            if (File.Exists(path))                                          // If a save with the same tag already exists, overwrite it.
            {
                File.Delete(path);
            }
            using (StreamWriter saveFile = new StreamWriter(path))          // Save the json string to the path.
            {
                saveFile.Write(jsonString);
            }

            MessageBox.Show("Saved!");
        }

        /*
         * Disposes all control elements associated to this
         * game instance
         */
        public void DisposeGame()
        {
            for (int i = 0; i < gameSize; i++)
            {
                for (int j = 0; j < gameSize; j++)
                {
                    boardCells[i, j].Dispose();
                    boardCells[i, j].Value_Changed -= UpdateSums;
                    boardCells[i, j] = null;
                }
            }

            foreach (SumCell cell in rowSumCells)
            {
                cell.Dispose();
            }

            foreach (SumCell cell in columnSumCells)
            {
                cell.Dispose();
            }

            swRenderTimer.Dispose();

            diagonalSumCell.Dispose();
        }

        public void ResumeGame()
        {
            gameButtonPause.Text = "Resume";
            PauseOrResumeGame(gameButtonPause, null);
        }

        public void PauseGame()
        {
            gameButtonPause.Text = "Pause";
            PauseOrResumeGame(gameButtonPause, null);
        }
        
        private void PauseOrResumeGame(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.Text.Equals("Pause"))
            {
                // pause timer
                gameSW.Stop();
                SetGamePanelUserBoardVisibility(false);
                button.Text = "Resume";
            }
            else
            {
                // resume timer
                gameSW.Start();
                SetGamePanelUserBoardVisibility(true);
                button.Text = "Pause";
            }
        }

        private void RenderTimer(object sender, ElapsedEventArgs e)
        {
            trueTime = millisecondsElapsed + gameSW.ElapsedMilliseconds;
            long milliseconds = trueTime % 1000;
            long seconds = (trueTime / 1000) % 60;
            long minutes = (trueTime / 60000);

            if (minutes >= 100)
            {
                gameTextTime.Text = "SLOW BOY...";
            }
            else
            {
                gameTextTime.Text = String.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
            }

        }

        private void TimerInitializer()
        {
            gameSW.Start();
            swRenderTimer = new System.Timers.Timer(10);
            swRenderTimer.AutoReset = true;
            swRenderTimer.Elapsed += RenderTimer; //RenderTimer signiture needs to be modified to work with .Elapsed delegate
            swRenderTimer.Start();
        }

        private void SetGamePanelUserBoardVisibility(bool state)
        {
            gameBoard.Enabled = state;
            gameBoard.Visible = state;
        }

        /*
         * Using the List of lockedCells Points constructed in the
         * StageManager class, find their corresponding cells on the BoardCells
         * array and 'lock' them by disabling them and setting their color to a
         * indicating color.
         */
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

        /*
         * Creates a number of cells and sumCells on the gamePanel appropriate
         * for the size provided. Cells will be scaled and positioned according
         * to the grid size.
         */
        private void FillBoard(int size)
        {
            gameSize = size;
            boardCells = new BoardCell[size, size];
            rowSumCells = new SumCell[size];
            columnSumCells = new SumCell[size];

            // Size is incremented by one to account for the extra row and column for SumCells.
            size++;

            int cellSize = gameBoard.Width / size;  //Size of each cell is (width of the panel / size). The panel *MUST* be 1:1 in order for this to function properly.

            Size nextSize = new Size(cellSize, cellSize);                               // Size of the next cell
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)      // Iterate through [size,size]
                {
                    Point nextPos = new Point(cellSize * j, cellSize * i);                      // Position of the next Cell

                    Cell cell;                                                                  // Create a cell object that will polymorph into a SumCell or BoardCell.
                    if (j == size - 1 && i == size - 1)                                         // If this cell is both in the sum row and sum column, it is the diagonal SumCell.
                    {
                        diagonalSumCell = new SumCell(nextPos, nextSize);
                        cell = diagonalSumCell;
                    }
                    else if (j == size - 1)                                                     // If this cell is just in the row sums column, it is a SumCell and is added to rowSumCells
                    {
                        SumCell sumCell = new SumCell(nextPos, nextSize);
                        rowSumCells[i] = sumCell;
                        cell = sumCell;
                    }
                    else if (i == size - 1)                                                     // If this cell is just in the column sums row, it is a SumCell and is added to the columnSumCells.
                    {
                        SumCell sumCell = new SumCell(nextPos, nextSize);
                        columnSumCells[j] = sumCell;
                        cell = sumCell;
                    }
                    else                                                                        // Otherwise, this is a boardCell.
                    {
                        BoardCell boardCell = new BoardCell(nextPos, nextSize, i, j);
                        boardCells[i, j] = boardCell;
                        boardCell.Value_Changed += UpdateSums;                                      // Register the Value_Changed event on the cell to trigger the UpdateSums method.
                        cell = boardCell;
                    }

                    gameBoard.Controls.Add(cell.CellPanel);                                     // Add the polymorphed-cell to the gameBoard as a child.
                }
            }

        }

        /*
         * Updates the sum on a specified row and column, also updating
         * the diagonal sum if row == column.
         */
        private void UpdateSums(int row, int column)
        {
            int rowSum = 0;
            int columnSum = 0;
            int diagonalSum = 0;

            // 'Complete' means every cell in that row/col/diagonal is non-zero.
            bool rowComplete = true;
            bool colComplete = true;
            bool diaComplete = true;

            for (int i = 0; i < gameSize; i++) // Sum all values in this row, setting complete to false if any are 0.
            {
                if (boardCells[row, i].Value == 0)
                {
                    rowComplete = false;
                }
                rowSum += boardCells[row, i].Value;
            }

            for (int i = 0; i < gameSize; i++) // Same as for-loop above, except for columns.
            {
                if (boardCells[i, column].Value == 0)
                {
                    colComplete = false;
                }
                columnSum += boardCells[i, column].Value;
            }

            // Update the appropriate rowSumCell and columnSumCell to match the new sum.
            rowSumCells[row].CellTextBox.Text = rowSum.ToString();
            columnSumCells[column].CellTextBox.Text = columnSum.ToString();

            //Furthermore, do similar logic if this row & column fall on the diagonal.
            if (row == column)
            {
                for (int i = 0; i < gameSize; i++)
                {
                    if (boardCells[i, i].Value == 0)
                    {
                        diaComplete = false;
                    }
                    diagonalSum += boardCells[i, i].Value;
                }

                diagonalSumCell.CellTextBox.Text = diagonalSum.ToString();
                UpdateSumCellColor(diagonalSumCell, diagonalSum, correctDiagonalSum, diaComplete);
            }

            // Update the color of the text on the sum cells in question.
            UpdateSumCellColor(rowSumCells[row], rowSum, correctRowSums[row], rowComplete);
            UpdateSumCellColor(columnSumCells[column], columnSum, correctColumnSums[column], colComplete);
        }

        /*
         * Changes the color of the sum-text to correctly match the following
         * criteria:
         * 
         * + Check if sum is equal to target.
         *      + If so, text is green.
         *      + If not, check if sum is greater than target OR the group this sum represents is 'complete'
         *          + If so, text is red.
         *          + If not, text is black.
         */
        private void UpdateSumCellColor(SumCell sumCell, int sum, int target, bool setComplete)
        {
            if (sum == target)
            {
                sumCell.CellTextBox.ForeColor = Color.Green;
            }
            else if (sum > target || setComplete)
            {
                sumCell.CellTextBox.ForeColor = Color.Red;
            }
            else
            {
                sumCell.CellTextBox.ForeColor = Color.Black;
            }
        }

        public long MillisecondsElapsed { get { return millisecondsElapsed; } set { millisecondsElapsed = value; } }
    }
}
