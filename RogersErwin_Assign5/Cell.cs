/*
 * NAME: Cell.cs
 * AUTHORS: Jake Rogers (z1826513), John Erwin (z1856469)
 *
 * A 'Cell' object is a square-panel with a text-box filling it entirely.
 * 
 * When constructed, they are given a position and size to bound to inherit
 * so that they can be correctly positioned on a panel in a grid. Their properties
 * are also set to standard values for all Cell objects.
 * 
 * This class alone isn't made for much, instead, it is meant to be inherited from
 * by BoardCell and SumCell to create a standard for all cells.
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
    public class Cell
    {
        protected Panel panel;
        protected TextBox textBox;

        public Cell(Point pos, Size size)
        {
            panel = new Panel();
            textBox = new TextBox();
            panel.Controls.Add(textBox);

            panel.Location = pos;
            panel.Size = size;

            SetDefaultProps();
        }

        public void Dispose()
        {
            textBox.Dispose();
            panel.Dispose();
        }
        
        protected void SetDefaultProps()
        {
            panel.BackColor = Color.NavajoWhite;
            panel.BorderStyle = BorderStyle.FixedSingle;

            textBox.Size = panel.Size;
            textBox.Location = new Point(0, 0);
            textBox.TextAlign = HorizontalAlignment.Center;

            textBox.ReadOnly = true;
            textBox.TabStop = false;

            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = Color.NavajoWhite;
            textBox.ForeColor = Color.Black;

            //Font-size is set to the height of the panel, making the textbox fill the entire panel.
            textBox.Font = new Font("Courier New", panel.Height, FontStyle.Bold, GraphicsUnit.Point);

            textBox.Text = "0";
        }

        public Panel CellPanel { get { return panel; } }
        public TextBox CellTextBox { get { return textBox; } }
    }
}
