/*
 * NAME: SumCell.cs
 * AUTHORS: Jake Rogers (z1826513), John Erwin (z1856469)
 * 
 * A 'SumCell' inherits from the standard 'Cell' class, but has some
 * slight differences in properties and colors to make them stand out.
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

    public class SumCell : Cell
    {

        public SumCell(Point pos, Size size) : base(pos, size)
        {
            textBox.Text = "0";
            textBox.Location = new Point(0, 0);

            // TODO: Better work on SumCell font scaling
            textBox.Font = new Font("Courier New", (int)(panel.Height * 0.80), FontStyle.Bold, GraphicsUnit.Pixel);

            CellPanel.BackColor = Color.FromArgb(173, 220, 255);
            CellTextBox.BackColor = Color.FromArgb(173, 220, 255);
        }
    }
}
