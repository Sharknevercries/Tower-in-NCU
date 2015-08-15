using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Tower_in_NCU.Applet
{
    abstract class Applet
    {
        protected bool _active;

        abstract public void Excute();
        abstract public void Initialize();
        abstract public void Draw(Graphics g);
        abstract public void KeyDown(KeyEventArgs e);
        abstract public void KeyUp(KeyEventArgs e);        

        public bool Active
        {
            set { _active = value; }
        }
    }
}
