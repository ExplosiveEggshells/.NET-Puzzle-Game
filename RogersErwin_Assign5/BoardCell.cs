/*
 * NAME: BoardCell.cs
 * AUTHORS: Jake Rogers (z1826513), John Erwin (z1856469)
 *
 * A 'BoardCell' inherits from 'Cell', but has additional members
 * to expand upon functionality and allows the user to edit it's
 * 'value'
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogersErwin_Assign5
{
    public class BoardCell : Cell
    {
        private int currentValue;   // Value that this cell contributes to it's respective row and column sums.
        private int row;            // Row this cell lies in.
        private int column;         // Column this cell lies in.

        public delegate void BoardCell_ValueChanged(int row, int column);
        public BoardCell_ValueChanged Value_Changed;    // Delegate-action indicating that this Cell's value has changed.

        public BoardCell(Point pos, Size size, int row, int column) : base(pos, size)
        {
            this.row = row;
            this.column = column;
            currentValue = 0;
            textBox.Location = new Point(0, 0);
            textBox.Font = new Font("Courier New", panel.Height, FontStyle.Bold, GraphicsUnit.Pixel);

            textBox.KeyPress += TextBox_KeyPress;
        }

        /*
         * Allows the user to set the value of this Cell equal to any number between
         * 0-9. Additionally, backspace will set it to 0 as well.
         * 
         * This interaction ignores any key which is not numeric or backspace.
         */
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 49 && e.KeyChar <= 57)         // If the key was 0-9...
            {
                textBox.Text = e.KeyChar.ToString();            // Change the value & text of this box to the key.
                currentValue = int.Parse(e.KeyChar.ToString());
            }

            Value_Changed(row, column);     // Trigger Value_Changed delegate.
        }

        public int Row { get { return row; } }
        public int Column { get { return column; } }
        public int Value { get { return currentValue; } set { currentValue = value; textBox.Text = currentValue.ToString(); } }

    }
}
