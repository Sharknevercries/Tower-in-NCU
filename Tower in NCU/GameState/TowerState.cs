using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tower_in_NCU.Applet;
using Tower_in_NCU.Audio;

namespace Tower_in_NCU.GameState
{
    class TowerState : GameState
    {
        private Tower.Tower _tower;
        private Audio.AudioPlayer _audioPlayer;

        private enum AppletName
        {
            Player, Battle, Dialogue, Shop
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
            _applets.Add(Applet.Shop.GetInstance());
            _tower = Tower.Tower.GetInstance();
            _audioPlayer = AudioPlayer.GetInstance();

            for (int i = 0; i < _applets.Count; i++)
                _applets[i].Initialize();
            _tower.Initialize();

            _audioPlayer.Play(AudioPlayer.BackgroundMusic.Exploration);
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
            if((_applets[(int)AppletName.Player] as Player).Hp <= 0)
            {
                gsm.SetState(GameStateManager.GameOverState);
                return;
            }

            for (int i = 0; i < _applets.Count; i++)
                _applets[i].Excute();
        }
    }
}
