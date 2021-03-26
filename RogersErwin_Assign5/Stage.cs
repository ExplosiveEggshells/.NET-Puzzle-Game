using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogersErwin_Assign5
{
    public class Stage
    {
        public int[,] boardValues;
        public int gameSize;

        public Stage(int[,] boardValues, int gameSize)
        {
            this.boardValues = boardValues;
            this.gameSize = gameSize;
        }
    }
}
