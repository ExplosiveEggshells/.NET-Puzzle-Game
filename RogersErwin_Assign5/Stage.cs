using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogersErwin_Assign5
{
    public class Stage
    {
        public List<int> boardValues {get; set; }
        public int gameSize { get; set;}
        public string stageName { get; set; }

        public Stage(List<int> boardValues, int gameSize, string stageName)
        {
            this.boardValues = boardValues;
            this.gameSize = gameSize;
            this.stageName = stageName;
        }
    }
}
