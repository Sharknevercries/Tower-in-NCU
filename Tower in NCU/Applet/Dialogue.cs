using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_in_NCU.Applet
{
    class Dialogue : Applet
    {
        private static Dialogue _dailogue;

        static Dialogue()
        {
            _dailogue = new Dialogue();
        }

        private Dialogue()
        {

        }   

        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }

        public override void Excute()
        {
            throw new NotImplementedException();
        }

        public override void KeyDown(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void KeyUp(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static Dialogue GetInstance() => _dailogue;
    }
}
