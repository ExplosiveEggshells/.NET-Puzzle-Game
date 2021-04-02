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
        private string stageName;
        private int gameSize;
        private long millisecondsElapsed;
        private bool completed;
        private bool hasCheated;

        public delegate void Save_Finished();
        public Save_Finished save_finished;         // Delegate to indicate that the game has finished saving, such that it can safely be disposed now.
        public Stopwatch gameSW = new Stopwatch();  // Keeps track of time elapsed for scoring.
        public System.Timers.Timer swRenderTimer;   // Updates the time elapsed every few milliseconds.
        public long trueTime = 0;                   // Amount of time this SINGLE playtime has lasted + duration of ALL saved playtimes on this board.

        private Color flashColor = Color.Red;           // The color that board cells change to when flashed.
        private Color defaultColor = Color.NavajoWhite; // The color that board cells should be under no unique circumstances.
        private Color lockedColor = Color.White;        // The color that locked board cells should be.

        private System.Timers.Timer flashInterpolationTimer;    // Timer for stepping through frames of the flash animation
        private List<BoardCell> flashedCells;                   // Cached set of cells that are currently doing the flash animation
        private int flashMaxTicks = 33;                         // Maximum number of interpolation steps in the flash animation
        private int flashTickCount = 0;                         // Current amount of steps performed in the last flash.
        private double flashDuration = 1500;                    // Length in ms of the flash animation.

        // References to UI controls.
        private Button gameButtonProgress;
        private Button gameButtonPause;
        private Panel gameBoard;
        private TextBox stageNameTextBox;
        private TextBox gameTextTime;


        /*
         * Constructor passes in references to UI elements scoped in Form1.cs, as well
         * as a Stage to load it's initial state from.
         */
        public Game(Stage stage, ref Panel gameBoard, ref TextBox stageNameTextBox, ref TextBox gameTextTime, ref Button gameButtonPause, ref Button gameButtonProgress)
        {
            this.gameBoard = gameBoard;
            this.stageNameTextBox = stageNameTextBox;
            this.gameTextTime = gameTextTime;
            this.gameButtonPause = gameButtonPause;
            this.gameButtonProgress = gameButtonProgress;
            this.gameButtonProgress.Click += CheckProgress;
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
            completed = false;      // In the context of this assignment, completed games should never be loaded, so it's safe to assume that a game being loaded is in-progress.
            hasCheated = load.hasCheated;
            trueTime = millisecondsElapsed;

            gameSW.Start();
            TimerInitializer();
            PauseGame();

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
            PauseGame();
            DialogResult opt = MessageBox.Show("Are you sure you want to save and quit?", "Save & Quit", MessageBoxButtons.YesNo);
            if (opt != DialogResult.Yes)
            {
                ResumeGame();
                return;
            }
            SaveStateNoPrompt();
            save_finished();
        }

        /*
         * Saves the game, but skipping past the prompt to save
         * & exit as well as not exiting when finished.
         */
        public void SaveStateNoPrompt()
        {
            // Pack the 2D boardCells array into a 1D-Representation of the sudoku board (Json-friendly).
            List<int> values = new List<int>();
            foreach (BoardCell cell in boardCells)
            {
                values.Add(cell.Value);
            }

            //Save all data into the Stage object, then serialize it into a json string.
            Stage save = new Stage(values, solutionValues, lockedCells, gameSize, stageName, correctRowSums, correctColumnSums, correctDiagonalSum, trueTime, completed, hasCheated);
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
            gameButtonProgress.Click -= CheckProgress;
            gameButtonPause.Click -= PauseOrResumeGame;
            swRenderTimer.Dispose();
            diagonalSumCell.Dispose();
        }

        /*
         * Resumes the timer and displays the gameBoard
         * once again.
         */
        public void ResumeGame()
        {
            gameButtonPause.Text = "Resume";
            PauseOrResumeGame(gameButtonPause, null);
        }

        /*
         * Halts the timer and hides the gameBoard.
         */
        public void PauseGame()
        {
            gameButtonPause.Text = "Pause";
            PauseOrResumeGame(gameButtonPause, null);
        }

        /*
         * Will attempt to call Cheat(). If the user has not yet cheated on this board,
         * a confirmation dialog will pop up asking if they wish to invalidate this attempt.
         * 
         * Once the user has cheated once, this method will simply immediately divert to
         * Cheat().
         */
        public void AttemptCheat(object sender, EventArgs e)
        {
            PauseGame();
            if (!hasCheated)
            {
                DialogResult opt = MessageBox.Show("Are you sure you want to cheat?\nDoing so will invalidate the score on this attempt!", "Cheat Warning", MessageBoxButtons.YesNo);
                if (opt != DialogResult.Yes)
                {
                    ResumeGame();
                    return;
                }
                else
                {
                    hasCheated = true;
                }
            }

            ResumeGame();
            Cheat();
        }
      
        /*
         * Will call Win() if all of the sums are correct, otherwise,
         * print a message stating that the game is not yet correct.
         */
        public void AttemptSolve(object sender, EventArgs e)
        {
            bool checkSolve = true;
            for (int i = 0; i < gameSize; i++)
            {
                if (rowSumCells[i].Value != correctRowSums[i]) { checkSolve = false; }
                if (columnSumCells[i].Value != correctColumnSums[i]) { checkSolve = false; }
            }
            if (diagonalSumCell.Value != correctDiagonalSum) { checkSolve = false; }

            if (checkSolve)
            {
                Win();
            }
            else
            {
                PauseGame();
                MessageBox.Show("Looks like you've still got more work to do!");
                ResumeGame();
            }
        }

        /*
         * Called by the 'Progress' button, this will either display a visual
         * of how many cells need to be filled in still (if all cell the player has
         * entered are correct thus-far), or call FlashCell on the first
         * detected mistake (User-entered value != correct value for that slot).
         */
        public void CheckProgress(object sender, EventArgs e)
        {
            int counter = 0;
            int remainingCells = 0;
            for (int i = 0; i < gameSize; i++)
            {
                for (int j = 0; j < gameSize; j++, counter++)
                {
                    if (boardCells[i, j].Value != 0)
                    {
                        if (boardCells[i, j].Value != solutionValues[counter])
                        {
                            FlashCell(i, j);
                            return;
                        }
                    }
                    else
                    {
                        remainingCells++;
                    }
                }
            }

            if (remainingCells == 0)
            {
                PauseGame();
                MessageBox.Show("You've completed the puzzle, hit the 'solve' button!");
                ResumeGame();
            }
            else
            {
                PauseGame();
                MessageBox.Show("No mistakes found, " + remainingCells + " cells left to fill.");
                ResumeGame();
            }
        }

        /*
         * Stop the timer, save the game as completed, then display a results box and exit out of this board.
         */
        private void Win()
        {
            gameSW.Stop();
            completed = true;
            SaveStateNoPrompt();
            string difficultyPrefix = stageName[0].ToString();

            if (hasCheated)
            {
                MessageBox.Show(String.Format("You solved puzzle {0}!\nHowever, you cheated, so this score is invalidated!", stageName));
            }
            else
            {
                List<long> times = StageManager.GetTimesFromSavesByDifficulty(difficultyPrefix);

                long averageTime = 0;
                long shortestTime = trueTime;
                long sum = 0;
                foreach (long time in times)
                {
                    shortestTime = Math.Min(time, shortestTime);
                    sum += time;
                }
                averageTime = sum / times.Count;

                string averageTimeStr = FormatMillisecondTime(averageTime);
                string shortestTimeStr = FormatMillisecondTime(shortestTime);

                MessageBox.Show(String.Format(
                    "You solved puzzle {0}!\n--==Statistics for completions in this difficulty==--\nShortest Time: {1}\nAverage Time: {2}\nGames Completed (Without cheating): {3} / 3",
                    stageName, shortestTimeStr, averageTimeStr, times.Count)
                );
            }

            save_finished();
        }

        /*
         * Either pause or resume the game, toggling between the two states
         * 
         * Note: Paused state is dictated by the text on the GameButtonPaused,
         * where its text being 'pause' means the game is in motion and 'resume'
         * means the game is paused.
         */
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

        /*
         * Fills the TextBox representing the game time according to how
         * many milliseconds have elapsed during this game, or other extraneous
         * texts.
         */
        private void RenderTimer(object sender, ElapsedEventArgs e)
        {
            if (hasCheated)     // If the player has cheated in this board, print text to say so and do nothing else.
            {
                gameTextTime.Text = "INVALID...";
                return;
            }
            trueTime = millisecondsElapsed + gameSW.ElapsedMilliseconds;
            string timeString = FormatMillisecondTime(trueTime);

            gameTextTime.Text = timeString;

        }

        /*
         * 'Cheat' by doing one of two things:
         * 
         *      1) Pick a random, unfilled cell (cell with a value
         *      of '0') and fill it in with the correct value.
         *      
         *      2) Pick the first incorrectly filled in cell (ordered
         *      by row THEN column) and replace its value with the
         *      correct one.
         *  
         * Option 1 is preferred, but unavailable if the user has
         * already tried to 
         */
        private void Cheat()
        {
            int counter = 0;
            List<BoardCell> unfilledCell = new List<BoardCell>();
            foreach (BoardCell cell in boardCells)                  // Keep track of every cell which is unfilled.
            {
                if (cell.Value == 0)
                {
                    unfilledCell.Add(cell);
                }
            }

            if (unfilledCell.Count != 0)                            // If there are unfilled cells, pick one random one and fill it in with the correct value.
            {
                Random random = new Random();
                int rdm = random.Next(0, unfilledCell.Count);

                BoardCell rdmCell = unfilledCell[rdm];
                int solutionPos = (rdmCell.Row * gameSize) + rdmCell.Column;

                rdmCell.Value = solutionValues[solutionPos];
                UpdateSums(rdmCell.Row, rdmCell.Column);
                return;
            }

            for (int i = 0; i < gameSize; i++)                      // Find the first incorrect cell and replace its value with the correct one.
            {
                for (int j = 0; j < gameSize; j++)
                {
                    BoardCell currentCell = boardCells[i, j];
                    if (currentCell.Value != solutionValues[counter])
                    {
                        currentCell.Value = solutionValues[counter];
                        UpdateSums(i, j);
                        return;
                    }
                    counter++;
                }
            }

        }

        /*
         * Initializes multiple properties of the game Timer.
         */
        private void TimerInitializer()
        {
            gameSW.Start();
            swRenderTimer = new System.Timers.Timer(10);
            swRenderTimer.AutoReset = true;
            swRenderTimer.Elapsed += RenderTimer;
            swRenderTimer.Start();
        }

        /*
         * Sets the visibility & active status of the gameBoard to
         * 'state'
         */
        private void SetGamePanelUserBoardVisibility(bool state)
        {
            gameBoard.Enabled = state;
            gameBoard.Visible = state;
        }

        /*
         * Formats a long representing a time in milliseconds into
         * a string in #M#M:SS.ssss format.
         */
        private string FormatMillisecondTime(long ms)
        {
            long milliseconds = ms % 1000;
            long seconds = (ms / 1000) % 60;
            long minutes = (ms / 60000);

            return String.Format("{0:0}:{1:00}.{2:000}", minutes, seconds, milliseconds);
        }

        /*
         * Given a cell via row and column, 'flash' the row, column, and diagonal (if applicable)
         * by changing it's backcolor to an alarming color, and then starting a timer that will
         * slowly fade it back to it's original color.
         */
        private void FlashCell(int row, int column)
        {
            gameButtonProgress.Enabled = false;

            if (flashInterpolationTimer != null)    // If there was an interpolation timer in progress, stop it and dispose it.
            {
                flashInterpolationTimer.Stop();
                flashInterpolationTimer.Dispose();
            }

            flashedCells = new List<BoardCell>();

            flashedCells.Add(boardCells[row, column]);  // Add the provided coordinate to the list of cells to be flashed.

            for (int i = 0; i < gameSize; i++)          // For every cell in this row, add it to the list (excluding the originally provided one from method call)
            {
                if (i == column) { continue; }
                flashedCells.Add(boardCells[row, i]);
            }

            for (int i = 0; i < gameSize; i++)          // Same as above, but for columns
            {
                if (i == row) { continue; }
                flashedCells.Add(boardCells[i, column]);
            }

            if (row == column)                          // If this cell is on the diagonal, add the diagonals to the list.
            {
                for (int i = 0; i < gameSize; i++)
                {
                    if (i == column) { continue; }
                    flashedCells.Add(boardCells[i, i]);
                }
            }

            // Prepare the timer system for flash animation //
            flashTickCount = 0;
            flashInterpolationTimer = new System.Timers.Timer(flashDuration / flashMaxTicks);   // Set a timer to fade the flashed cell's color back to it's original one over time.
            flashInterpolationTimer.Elapsed += ColorFlashedCells;
            flashInterpolationTimer.AutoReset = true;
            flashInterpolationTimer.Start();
            ColorFlashedCells(null, null);                  // Flash the cells right away, making all flashed cells match the flashColor.
        }

        /*
         * Called by either FlashCell initially or by a Timer created by FlashCell,
         * this function will fade all currently flashedCells' backgrounds from
         * flashColor to default/locked color through flashMaxTicks numbers of steps
         * over a duration of flashDuration milliseconds.
         */
        private void ColorFlashedCells(object sender, ElapsedEventArgs e)
        {
            double interpolationValue = ((double)flashTickCount / (double)flashMaxTicks); // Color bias from flashColor to default/locked color (0.0 is flash color, 0.5 is halfway, 1.0 is default/locked)
            // Note: the interpolation value will increase further towards 1.0 per every step this function is called by the autoResetting timer, animating the colors back to normal.

            foreach (BoardCell cell in flashedCells)        // For every flashedCell...
            {
                if (cell.Selected)
                {
                    continue;
                }
                if (!cell.Locked)                           // If it isn't locked, Interpolate it's color back to defaultColor
                {
                    cell.CellTextBox.BackColor = ColorLerp(flashColor, defaultColor, interpolationValue);
                }
                else                                        // Otherwise, animate to lockedColor
                {
                    cell.CellTextBox.BackColor = ColorLerp(flashColor, lockedColor, interpolationValue);
                }
            }

            /*
             * If the tick count has been met, dispose the timer
             */
            if (flashTickCount == flashMaxTicks)
            {
                gameButtonProgress.Enabled = true;
                flashInterpolationTimer.AutoReset = false;
                flashInterpolationTimer.Close();
                flashInterpolationTimer.Dispose();
            }

            flashTickCount++;   // Increment the tick count to keep the loop moving and interpolate the colors further.
        }

        /*
         * returns color n, where n's r, g, and b values are the linear
         * interpolation of from's rbg's to the rbg values in 'to' by
         * 'percent' percent.
         * 
         * In simpler terms, will return the color 'percent' way between
         * from and to; ColorLerp(Color.Black, Color.White, 0.50) would
         * return grey (127, 127, 127)
         */
        private Color ColorLerp(Color from, Color to, double percent)
        {
            int r = from.R + (int)((double)(to.R - from.R) * percent);
            int g = from.G + (int)((double)(to.G - from.G) * percent);
            int b = from.B + (int)((double)(to.B - from.B) * percent);

            return Color.FromArgb(r, g, b);
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
                        cell.Locked = true;
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

        // Properties
        public string StageName { get { return stageName; } }
        public long MillisecondsElapsed { get { return millisecondsElapsed; } set { millisecondsElapsed = value; } }
    }
}
