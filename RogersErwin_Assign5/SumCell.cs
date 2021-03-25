using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogersErwin_Assign5
{
    public enum SumType
    {
        Row,
        Column,
        Diagonal
    }

    public class SumCell : Cell
    {
        private SumType sumType;
        private int id;

        public SumCell(Point pos, Size size, SumType type, int id) : base(pos, size)
        {
            sumType = type;
            this.id = id;
            textBox.Text = sumType.ToString() + id.ToString();
        }

        public SumType Type { get { return sumType; } set { sumType = value; } }
        public int ID { get { return id; } set { id = value; } }
    }
}
