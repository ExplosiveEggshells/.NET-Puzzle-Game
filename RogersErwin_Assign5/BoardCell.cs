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
        private int currentValue;
        private int row;
        private int column;
        public delegate void BoardCell_ValueChanged(int row, int column);
        
        public BoardCell_ValueChanged Value_Changed;

        public BoardCell(Point pos, Size size, int row, int column) : base(pos, size)
        {
            this.row = row;
            this.column = column;
            currentValue = 0;
            textBox.Location = new Point(0, 0);
            textBox.Font = new Font("Courier New", panel.Height, FontStyle.Bold, GraphicsUnit.Pixel);

            textBox.Enter += TextBox_Enter;
            textBox.Leave += TextBox_Exit;
            textBox.KeyPress += TextBox_KeyPress;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            textBox.BackColor = Color.DarkGoldenrod;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                textBox.Text = e.KeyChar.ToString();
                currentValue = int.Parse(e.KeyChar.ToString());
            }
            else if (e.KeyChar == 8)
            {
                textBox.Text = "0";
                currentValue = 0;
            }

            Value_Changed(row, column);
        }

        /* TODO: Create a delegate that forces the column and row this cell
         * resides in to re-calculate it's sum*
         */

        private void TextBox_Exit(object sender, EventArgs e)
        {
            textBox.BackColor = Color.NavajoWhite;
        }

        public int Row { get { return row; } }
        public int Column { get { return column; } }
        public int Value { get { return currentValue; } set { currentValue = value; textBox.Text = currentValue.ToString(); } }
    }
}
