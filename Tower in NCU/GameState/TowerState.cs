using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tower_in_NCU.Applet;

namespace Tower_in_NCU.GameState
{
    class TowerState : GameState
    {
        private Tower.Tower _tower;

        private enum AppletName
        {
            Player, Battle, Dialogue
        };

        private List<Applet.Applet> _applets;

        public TowerState(GameStateManager gsm) : base(gsm) { }

        public override void Draw(Graphics g)
        {
            _tower.Draw(g, (_applets[(int)AppletName.Player] as Player).CurrentFloor);
            for (int i = 0; i < _applets.Count; i++)
                _applets[i].Draw(g);
        }

        public override void Initialize()
        {
            _applets = new List<Applet.Applet>();
            _applets.Add(Player.GetInstance());
            _applets.Add(Battle.GetInstance());
            _applets.Add(Dialogue.GetInstance());
            _tower = Tower.Tower.GetInstance();
            _tower.Initialize();
            (_applets[(int)AppletName.Player] as Player).Initialize();
        }

        public override void KeyDown(KeyEventArgs e)
        {
            for (int i = 0; i < _applets.Count; i++)
                _applets[i].KeyDown(e);
        }

        public override void KeyUp(KeyEventArgs e)
        {
            for (int i = 0; i < _applets.Count; i++)
                _applets[i].KeyUp(e);
        }

        public override void Excute()
        {
            for (int i = 0; i < _applets.Count; i++)
                _applets[i].Excute();
        }
    }
}
