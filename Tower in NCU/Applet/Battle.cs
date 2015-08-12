using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tower_in_NCU.MapObject;
using Tower_in_NCU.Tower;

namespace Tower_in_NCU.Applet
{
    class Battle : Applet
    {
        private static Battle _battle;

        private const string BackgroundImageName = "dialogueBackground";
        private Image.ImageUnit _background;

        private Floor _floor;
        private Player _player;
        private Monster _monster;
        private Dialogue _dialogue;

        private bool _playerTurn;

        private int _counter;
        private const int MaxCounter = 3;

        private Font _font;
        private Brush _brush;

        static Battle()
        {
            _battle = new Battle();
        }

        private Battle()
        {
            _background = new Image.ImageUnit(BackgroundImageName);
            _background.SetPosition(Floor.StartX, 150);
            _font = new Font("Arial", 14);
            _brush = new SolidBrush(Color.Black);
            _dialogue = Dialogue.GetInstance();
            _active = false;
        }

        public static Battle GetInstance() => _battle; 

        public void Initialize(Player player, Monster monster, Floor floor)
        {
            _player = player;
            _monster = monster;
            _floor = floor;
            _counter = 0;
            _playerTurn = true;
        }

        public override void Draw(Graphics g)
        {
            if (!_active)
                return;

            _background.Draw(g, Floor.Edge * Floor.ObjectSize, 130);
            _monster.SetPosition(new Point(230, 140));
            _monster.Draw(g);

            g.DrawString("生命", _font, _brush, new Point(210, 190));
            g.DrawString("攻擊力", _font, _brush, new Point(200, 222));
            g.DrawString("防禦力", _font, _brush, new Point(200, 254));

            g.DrawString(_monster.Hp + "", _font, _brush, new Point(280, 190));
            g.DrawString(_monster.Atk + "", _font, _brush, new Point(280, 222));
            g.DrawString(_monster.Def + "", _font, _brush, new Point(280, 254));

            _player.DrawPlayer(g, new Point(460, 140), Player.Face.Down);
            g.DrawString("生命", _font, _brush, new Point(430, 190));
            g.DrawString("攻擊力", _font, _brush, new Point(420, 222));
            g.DrawString("防禦力", _font, _brush, new Point(420, 254));

            g.DrawString(_player.Hp + "", _font, _brush, new Point(500, 190));
            g.DrawString(_player.Atk + "", _font, _brush, new Point(500, 222));
            g.DrawString(_player.Def + "", _font, _brush, new Point(500, 254));

        }

        public override void Excute()
        {
            if (!_active)
                return;

            if (_counter++ < MaxCounter)
                return;
            _counter = 0;

            int damageToPlayer = _monster.Atk - _player.Def;
            int damageToMonster = _player.Atk - _monster.Def;

            if (_playerTurn)
            {
                if (damageToMonster > 0)
                    _monster.Hp -= damageToMonster;
            }
            else
            {
                if (damageToPlayer > 0)
                    _player.Hp -= damageToPlayer;
            }

            _playerTurn = !_playerTurn;

            if(_player.Hp <= 0)
            {
                BattleEnd();
            }

            if(_monster.Hp <= 0)
            {
                BattleEnd();
            }

        }

        public override void KeyDown(KeyEventArgs e)
        {
            
        }

        public override void KeyUp(KeyEventArgs e)
        {
            
        }

        private void BattleEnd()
        {
            _floor.SetMapObject(_player.NextPosition, MapObjectType.Floor1);
            _player.Gold += _monster.Gold;
            _player.Exp += _monster.Exp;
            _player.Active = true;
            string result = string.Format("獲得 {0} 金幣和 {1} 經驗", _monster.Gold, _monster.Exp);
            _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "戰鬥結果", result, Dialogue.FaceLoaction.None);
            _active = false;
        }

    }
}
