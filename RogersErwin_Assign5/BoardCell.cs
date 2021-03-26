using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogersErwin_Assign5
{
    class BoardCell : Cell
    {
        private int currentValue;
        private int row;
        private int column;

        public BoardCell(Point pos, Size size, int row, int column) : base(pos, size)
        {
            this.row = row;
            this.column = column;
            currentValue = 0;

            textBox.Text = String.Format("{0}", row);
            textBox.Location = new Point(0, 0);
            textBox.Font = new Font("Courier New", panel.Height, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        public int Row { get { return row; } }
        public int Column { get { return column; } }
        public int Value { get { return currentValue; } set { currentValue = value; } }
    }
}
