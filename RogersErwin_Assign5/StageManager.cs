/*
 * NAME: StageManager.cs
 * AUTHORS: Jake Rogers (z1826513), John Erwin (z1856469)
 * 
 * This class is responsible for doing three things:
 * 
 * 1) Find all of the Sudoku boards addressed by
 * directory.txt and convert the content within them to
 * valid Stage objects (Game States) so that they can be 
 * later loaded by Game objects.
 * 
 * 2) Move the Stage objects into one of three lists of
 * Stages based on their difficulties.
 * 
 * 3) Return the next proper Stage from a difficulty set that
 * should be loaded to a caller.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogersErwin_Assign5
{
    public class StageManager
    {
        public List<Stage> EasyStages;      // List of all Easy Stages addressed by directory.txt
        public List<Stage> MediumStages;    // Same but for Medium
        public List<Stage> HardStages;      // Same but for Hard.

        public StageManager()
        {
            EasyStages = new List<Stage>();
            MediumStages = new List<Stage>();
            HardStages = new List<Stage>();
        }

        /*
         * Populates the three Lists included with this Manager object with
         * Stage objects respective to their difficulties.
         * 
         * To do this, a StreamReader opens up every .txt file addressed by
         * directory.txt and parses the data within to appropriately convert
         * that data to Stage objects, then adds them to their appropiate list
         * based on the size of the board.
         */
        public void BuildStageLists()
        {
            List<string> paths = new List<string>();

            // Get a list of paths to every Sudoku board.
            using (StreamReader reader = new StreamReader("../../directory.txt"))
            {
                while (!reader.EndOfStream)
                {
                    paths.Add(reader.ReadLine());
                }
            }

            
            foreach (string path in paths)                                      // For every sudoku board path...
            {
                using (StreamReader reader = new StreamReader("../../" + path))     // Open a file to it...        
                {
                    List<int> boardValues = new List<int>();                            // Create variables that will later be assigned and then constructed into a Stage object.
                    List<int> solutionValues = new List<int>();                         // NOTE: 
                    List<Point> lockedCells = new List<Point>();
                    int gameSize;

                    List<int> correctRowSums = new List<int>();
                    List<int> correctColumnSums = new List<int>();
                    int correctDiagonalSum;

                    if (path.Contains("easy")) { gameSize = 3;}                         // Set gameSize based off of the name of the file.
                    else if (path.Contains("medium")) { gameSize = 5;}
                    else { gameSize = 7; }

                    int cellCount = gameSize * gameSize;                                // Store the total number of cells
                    int count = 0;                                                      // Keep track of entries read.
                    while (!reader.EndOfStream)                                         // Read until at end of file...
                    {
                        int next = reader.Read() - 48;                                      // Get the next char and convert it to an integer.
                        if (next == -38) { continue; }                                      // If that char was a backspace, skip this iteration.
                        if (count < cellCount)                                              // If we're below the cellCount, we're reading a value on the board to begin with, add it to the appropiate list.
                        {
                            boardValues.Add(next);                                          
                        } else
                        {
                            solutionValues.Add(next);                                       // Otherwise, we're reading the solution board.
                        }
                        count++;
                    }
                    string tag = path.Substring(path.IndexOf("/")+1, 2);                // The 'tag' or level name is two characters after the '/' in the path name, (E.G e1)


                    /* Sum up the each row in the solution and add them to the List of correct row sums.
                     * 
                     * While we're in this loop, we also keep track of which values were provided with
                     * the default board and mark them as 'locked' cells that should not be changed
                     * by the user.
                     * 
                     * Reminder: solution/boardValues is a List of integers representing a 2D-Array, which 
                     * is why the math to iterate through it is complex.
                     */
                    for (int i = 0; i < gameSize; i++)              // For each row...
                    {
                        int rowSum = 0;                                 // Create a new sum
                        for (int j = 0; j < gameSize; j++)                  // For each Column...
                        {
                            rowSum += solutionValues[(i * gameSize) + j];       // Add the value of each column in that row to the sum.

                            int x = boardValues[(i * gameSize) + j];        

                            if (x != 0)                                         // If the boardValue here is not 0, this cell should be locked.
                            {
                                lockedCells.Add(new Point(i, j));
                            }
                        }

                        correctRowSums.Add(rowSum);                     // Add the sum to the list of correct row sums.
                    }

                    /* Similar to the for-loop above, except for getting column sums. The
                     * iteration math is changed to reflect that.
                     */
                    for (int i = 0; i < gameSize; i++)
                    {
                        int columnSum = 0;
                        for (int j = 0; j < gameSize; j++)
                        {
                            columnSum += solutionValues[i + gameSize*j];
                        }
                        correctColumnSums.Add(columnSum);
                    }

                    /* Similar to the for-loop above, except for getting the singular 
                     * diagonal sum. The iteration math is changed to reflect that.
                     */
                    correctDiagonalSum = 0;
                    for (int i = 0; i < gameSize; i++)
                    {
                        correctDiagonalSum += solutionValues[(i*gameSize) + i];
                    }

                    // Now that all variables have been assigned, create a new stage object.
                    Stage nextStage = new Stage(boardValues, solutionValues, lockedCells, gameSize, tag, correctRowSums, correctColumnSums, correctDiagonalSum, 0, false, false);
                    
                    // Finally, add thie newly created stage object to it's appropiate list.
                    if (gameSize == 3) { EasyStages.Add(nextStage); }
                    else if (gameSize == 5) { MediumStages.Add(nextStage); }
                    else { HardStages.Add(nextStage); }
                }
            }
        }

        /*
         * Returns the appropriate Stage from the specified list (Easy, Medium, or Hard)
         * based on the following criterion:
         * 
         * + For each stage in this list, check to see if there is a .json save in
         * ../../saves matching it's tag. (For EasyStages[0], that would be 'e1.json')
         *      + If so...
         *          + Return that save for this stage if it is in-progress (completed == false)
         *          + Skip to the next stage in this list if that save for this stage is completed.
         *      + Otherwise...
         *          + Return the current stage in this list (The default board) to start a fresh attempt at it.
         *          
         * + If all stages in this list have a completed .json save in ../../saves, return null.
         */
        public Stage GetNextDifficulty(List<Stage> stageList)
        {
            foreach (Stage stage in stageList)
            {
                string path = String.Format("../../saves/{0}.json", stage.stageName);
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        Stage potentialStage = JsonSerializer.Deserialize<Stage>(reader.ReadToEnd());
                        if (!potentialStage.completed)
                        {
                            return potentialStage;
                        }
                    }
                } else
                {
                    return stage;
                }
            }

            return null;
        }

        /*
         * Returns a list of completion times for all games in a given difficulty
         * (denoted by 'difficultyPrefix' either being 'e', 'm', or 'h'), provided
         * that those saved games are indeed completed are were not cheated in.
         */
        public static List<long> GetTimesFromSavesByDifficulty(string difficultyPrefix)
        {
            List<long> times = new List<long>();
            List<string> filePaths = new List<string>();
            for(int i = 1; i <= 3; i++)
            {
                string path = String.Format("../../saves/{0}{1}.json", difficultyPrefix, i);
                if (File.Exists(path)) { 
                    filePaths.Add(path);
                }
            }


            foreach (string path in filePaths)
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    Stage stage = JsonSerializer.Deserialize<Stage>(reader.ReadToEnd());
                    if (stage.completed && !stage.hasCheated)
                    {
                        times.Add(stage.millisecondsElapsed);
                    }
                }
            }

            return times;
        }

        public int DeleteSavesByDifficulty(string difficultyPrefix)
        {
            int count = 0;
            for (int i = 1; i <= 3; i++)
            {
                string path = String.Format("../../saves/{0}{1}.json", difficultyPrefix, i);
                if (File.Exists(path))
                {
                    File.Delete(path);
                    count++;
                }
            }

            return count;
        }
    }
}
