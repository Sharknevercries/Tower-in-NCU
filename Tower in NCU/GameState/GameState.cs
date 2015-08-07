using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Tower_in_NCU.GameState
{
    abstract class GameState
    {
        protected GameStateManager gsm;

        public GameState(GameStateManager gsm)
        {
            this.gsm = gsm;
        }

        public abstract void Draw(Graphics g);
        public abstract void Initialize();
        public abstract void KeyDown(KeyEventArgs e);
    }
}
