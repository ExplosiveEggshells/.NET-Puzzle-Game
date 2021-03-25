using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogersErwin_Assign5
{
    class Cell
    {
        private Panel panel;
        private TextBox textBox;

        public Cell(Point pos, Size size)
        {
            panel = new Panel();
            textBox = new TextBox();
            panel.Controls.Add(textBox);

            panel.Location = pos;
            panel.Size = size;

            SetDefaultProps();
        }

        private void SetDefaultProps()
        {
            panel.BackColor = Color.NavajoWhite;
            panel.BorderStyle = BorderStyle.FixedSingle;

            textBox.Size = panel.Size;
            textBox.Location = new Point(0, panel.Size.Height / 3);
            textBox.TextAlign = HorizontalAlignment.Center;
            
            textBox.ReadOnly = true;
            textBox.TabStop = false;

            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = Color.NavajoWhite;
            textBox.ForeColor = Color.Black;

            textBox.Font = new Font("Courier New", 24, FontStyle.Bold, GraphicsUnit.Point);

            textBox.Text = "0";
        }

        public Panel CellPanel { get { return panel; } }
        public TextBox CellTextBox { get { return textBox; } }
    }
}
