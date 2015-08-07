using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_in_NCU.GameState
{
    class TowerState : GameState
    {
        private Tower.Tower tower;

        public TowerState(GameStateManager gsm) : base(gsm) { }

        public override void Draw(Graphics g)
        {
            tower.GetFloor(0).Draw(g);
        }

        public override void Initialize()
        {
            tower = Tower.Tower.GetInstance();
            tower.Initialize();
        }

        public override void KeyDown(KeyEventArgs e)
        {
            
        }
    }
}
