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
        public List<int> boardValues {get; set; }
        public List<int> solutionValues { get; set; }
        public List<Point> lockedCells { get; set; }
        public int gameSize { get; set;}
        public string stageName { get; set; }

        public List<int> correctRowSums { get; set; }
        public List<int> correctColumnSums { get; set; }
        public int correctDiagonalSum { get; set; }

        public bool modified { get; set; }
        public bool completed { get; set; }

        public Stage()
        {

        }

        public Stage(List<int> boardValues, List<int> solutionValues, List<Point> lockedValues, int gameSize, string stageName,
            List<int> correctRowSums, List<int> correctColumnSums, int correctDiagonalSum, bool modified)
        {
            this.boardValues = boardValues;
            this.solutionValues = solutionValues;
            this.lockedCells = lockedValues;
            this.gameSize = gameSize;
            this.stageName = stageName;
            this.correctRowSums = correctRowSums;
            this.correctColumnSums = correctColumnSums;
            this.correctDiagonalSum = correctDiagonalSum;
            this.modified = modified;
            completed = false;
        }
    }
}
