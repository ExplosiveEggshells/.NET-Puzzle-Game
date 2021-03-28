/*
 * NAME: Stage.cs
 * AUTHORS: Jake Rogers (z1826513), John Erwin (z1856469)
 * 
 * The 'Stage' class is essentially a DTO for transfering, saving
 * and loading all of the state data within a Game object such as
 * cell values, name, size, completed, etc.
 * 
 * All of the datatypes within this class are publicly accessible with
 * get/set properties in order to make them compatible with JSON
 * serialization; necessary for saving Game states to files in a
 * clean and sensible way.
 * 
 * NOTE: Since Arrays are, for some reason, not compatible with .NET's
 * JsonSerializer, the grid of BoardCells are saved as a List of integers
 * instead of a 2D-Array of BoardCells. Fortunately, it is not too difficult
 * to convert from one to another, and said conversion is done within
 * Game.LoadState.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogersErwin_Assign5
{
    public class Stage
    {
        public List<int> boardValues {get; set; }           // List of Integers representing the values on the sudoku board. (This is a 2D-Array condensed into a 1D List.)
        public List<int> solutionValues { get; set; }       // List of integers representing the CORRECT values on the finished sudoku board for this stage.
        public List<Point> lockedCells { get; set; }        // A list of Points (representing rows and column pairs) indicating locked cells (values provided at game-start which should not be altered).
        public int gameSize { get; set;}                    // Size of the board in both rows and columns.
        public string stageName { get; set; }               // name of the stage. Used for file-naming and checking for in-progress stages.

        public List<int> correctRowSums { get; set; }       // List of row sums that would appear on the completed board for this stage, starting from top to bottom.
        public List<int> correctColumnSums { get; set; }    // Same, but for above going from left to right.
        public int correctDiagonalSum { get; set; }         // Same, but only for the one diagonal sum.

        public double millisecondsElapsed { get; set; }

        public bool completed { get; set; }                 // True if this board is solved, or false if in progess.

        /*
         * Default, empty constructor, most likely should not be used.
         */
        public Stage()
        {

        }

        public Stage(List<int> boardValues, List<int> solutionValues, List<Point> lockedValues, int gameSize, string stageName,
            List<int> correctRowSums, List<int> correctColumnSums, int correctDiagonalSum, double millisecondsElapsed)
        {
            this.boardValues = boardValues;
            this.solutionValues = solutionValues;
            this.lockedCells = lockedValues;
            this.gameSize = gameSize;
            this.stageName = stageName;
            this.correctRowSums = correctRowSums;
            this.correctColumnSums = correctColumnSums;
            this.correctDiagonalSum = correctDiagonalSum;
            this.millisecondsElapsed = millisecondsElapsed;
            completed = false;
        }
    }
}
