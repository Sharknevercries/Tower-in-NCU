using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tower_in_NCU.MapObject;
using Tower_in_NCU.Tower;
using Tower_in_NCU.Image;
using Tower_in_NCU.Audio;

namespace Tower_in_NCU.Applet
{
    class Battle : Applet
    {
        private static Battle _battle = new Battle();

        private const string BackgroundImageName = "DialogueBackground";
        private ImageUnit _background;

        private Floor _floor;
        private Player _player;
        private Monster _monster;
        private Dialogue _dialogue;
        private AudioPlayer _aduioPlayer;

        private bool _playerTurn;
        private int _playerAttackTime;
        private int _enemyAttackTime;

        private int _counter;
        private const int MaxCounter = 2;
        
        private Battle()
        {
            _background = new ImageUnit(BackgroundImageName);
            _background.SetPosition(Floor.StartX, 150);
            _active = false;
        }

        public static Battle GetInstance() => _battle; 

        public void SetBattle(Player player, Monster monster, Floor floor)
        {
            _player = player;
            _monster = monster;
            _floor = floor;
            _counter = 0;
            _enemyAttackTime = 0;
            _playerAttackTime = 0;
            _playerTurn = true;
        }

        public override void Draw(Graphics g)
        {
            if (!_active)
                return;

            Font font = new Font("Arial", 12);

            _background.Draw(g, _background.Width, 130);
            _monster.SetPosition(new Point(230, 150));
            _monster.Draw(g);

            g.DrawString("生命", font, Brushes.White, new Point(210, 190));
            g.DrawString("攻擊力", font, Brushes.White, new Point(200, 222));
            g.DrawString("防禦力", font, Brushes.White, new Point(200, 254));

            g.DrawString(_monster.Hp + "", font, Brushes.White, new Point(280, 190));
            g.DrawString(_monster.Atk + "", font, Brushes.White, new Point(280, 222));
            g.DrawString(_monster.Def + "", font, Brushes.White, new Point(280, 254));

            _player.DrawPlayer(g, new Point(460, 150), Player.Face.Down);
            g.DrawString("生命", font, Brushes.White, new Point(430, 190));
            g.DrawString("攻擊力", font, Brushes.White, new Point(420, 222));
            g.DrawString("防禦力", font, Brushes.White, new Point(420, 254));

            g.DrawString(_player.Hp + "", font, Brushes.White, new Point(500, 190));
            g.DrawString(_player.Atk + "", font, Brushes.White, new Point(500, 222));
            g.DrawString(_player.Def + "", font, Brushes.White, new Point(500, 254));

        }

        public override void Excute()
        {
            if (!_active)
                return;

            if (_counter++ < MaxCounter)
                return;
            _counter = 0;

            if (_playerTurn)
            {
                PlayerAttack();
            }
            else
            {
                EnemyAttack();
            }

            if(_player.Hp <= 0)
            {
                BattleEnd(false);
            }

            if(_monster.Hp <= 0)
            {
                BattleEnd(true);
            }

        }

        public override void KeyDown(KeyEventArgs e)
        {
            
        }

        public override void KeyUp(KeyEventArgs e)
        {
            
        }

        private void BattleEnd(bool win)
        {
            if (win)
            {
                _aduioPlayer.Play(AudioPlayer.SoundEffect.Eliminate);
                _floor.SetMapObject(_player.NextPosition, MapObjectType.Floor1);
                _player.Gold += _monster.Gold;
                _player.Exp += _monster.Exp;
                string result = string.Format("獲得 {0} 金幣和 {1} 經驗", _monster.Gold, _monster.Exp);
                _dialogue.AddDialogue(Dialogue.DialogueLocation.Middle, "戰鬥結果", result, Dialogue.FaceLoaction.None, null);
            }
            _active = false;
            _player.Active = true;
        }

        public override void Initialize()
        {
            _dialogue = Dialogue.GetInstance();
            _aduioPlayer = AudioPlayer.GetInstance();
        }

        private void PlayerAttack()
        {
            int dealDamage = _player.Atk - _monster.Def;

            _aduioPlayer.Play(AudioPlayer.SoundEffect.PlayerAttack);
            if (dealDamage > 0)
                _monster.Hp -= dealDamage;

            if (++_playerAttackTime >= _player.AttackTime)
            {
                _playerAttackTime = 0;
                _playerTurn = !_playerTurn;
            }
        }

        private void EnemyAttack()
        {
            int dealDamage;

            _aduioPlayer.Play(AudioPlayer.SoundEffect.EnemyAttack);

            if (_monster.Ability.Contains(Monster.MonsterFeature.Penetrate))
            {
                dealDamage = _monster.Atk;
            }
            else
            {
                dealDamage = _monster.Atk - _player.Def;
            }

            if (dealDamage < 0)
                dealDamage = 0;

            _player.Hp -= dealDamage;
            
            if(++_enemyAttackTime >= _monster.AttackTime)
            {
                _enemyAttackTime = 0;
                _playerTurn = !_playerTurn;
            }

        }
    }
}
