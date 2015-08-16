using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tower_in_NCU.Image;
using Tower_in_NCU.MapObject;
using Tower_in_NCU.Tower;

namespace Tower_in_NCU.Applet
{
    class MonsterBook : Applet
    {
        private static MonsterBook _monsterBook = new MonsterBook();

        private Tower.Tower _tower;
        private Player _player;

        private List<MapObjectType> _monsters;

        private ImageUnit _leftArrow;
        private ImageUnit _rightArrow;
        private ImageUnit _background;

        private const int MonstersPerPage = 5;
        private int _currentPage;
        private int _maxPage;

        private MonsterBook()
        {
            _background = new ImageUnit("DialogueBackground");
            _background.SetPosition(Floor.StartX, 0);
            _leftArrow = new ImageUnit("LeftArrow");
            _leftArrow.SetPosition(Floor.StartX + 5 * Floor.ObjectSize, Floor.ObjectSize * 11);
            _rightArrow = new ImageUnit("RightArrow");
            _rightArrow.SetPosition(Floor.StartX + 6 * Floor.ObjectSize, Floor.ObjectSize * 11);
        }

        public static MonsterBook GetInstance() => _monsterBook;

        public override void Draw(Graphics g)
        {
            if (!_active)
                return;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            _background.Draw(g, Floor.Edge * Floor.ObjectSize, Floor.Edge * Floor.ObjectSize);
            for(int i = _currentPage * MonstersPerPage, j = 0; i < _monsters.Count && j < MonstersPerPage; i++, j++)
            {
                int[] rowHeight = { 20, 20, 24 };
                int[] sumHeight = { 20, 40, 60 };
                Monster monster = (Monster)MapObjectFactory.CreateMapObject(_monsters[i]);
                Font font = new Font("Arial", 9);
                monster.SetPosition(Floor.StartX + Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize);
                monster.Draw(g);
                // First row
                g.DrawString("生命 : ", font, Brushes.White,
                    new RectangleF(Floor.StartX + 2 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize, 2 * Floor.ObjectSize, rowHeight[0]), sf);
                g.DrawString("" + monster.Hp, font, Brushes.White,
                    new RectangleF(Floor.StartX + 4 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize, 1 * Floor.ObjectSize, rowHeight[0]), sf);
                g.DrawString("攻擊力 : ", font, Brushes.White,
                    new RectangleF(Floor.StartX + 5 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize, 2 * Floor.ObjectSize, rowHeight[0]), sf);
                g.DrawString("" + monster.Atk, font, Brushes.White,
                    new RectangleF(Floor.StartX + 7 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize, 1 * Floor.ObjectSize, rowHeight[0]), sf);
                g.DrawString("防禦力 : ", font, Brushes.White,
                    new RectangleF(Floor.StartX + 8 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize, 2 * Floor.ObjectSize, rowHeight[0]), sf);
                g.DrawString("" + monster.Def, font, Brushes.White,
                    new RectangleF(Floor.StartX + 10 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize, 1 * Floor.ObjectSize, rowHeight[0]), sf);

                // Second row
                g.DrawString("金幣 : ", font, Brushes.White,
                    new RectangleF(Floor.StartX + 2 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize + sumHeight[0], 2 * Floor.ObjectSize, rowHeight[1]), sf);
                g.DrawString("" + monster.Gold, font, Brushes.White,
                    new RectangleF(Floor.StartX + 4 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize + sumHeight[0], 1 * Floor.ObjectSize, rowHeight[1]), sf);
                g.DrawString("經驗 : ", font, Brushes.White,
                    new RectangleF(Floor.StartX + 5 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize + sumHeight[0], 2 * Floor.ObjectSize, rowHeight[1]), sf);
                g.DrawString("" + monster.Exp, font, Brushes.White,
                    new RectangleF(Floor.StartX + 7 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize + sumHeight[0], 1 * Floor.ObjectSize, rowHeight[1]), sf);
                g.DrawString("損傷 : ", font, Brushes.White,
                    new RectangleF(Floor.StartX + 8 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize + sumHeight[0], 2 * Floor.ObjectSize, rowHeight[1]), sf);
                g.DrawString("" + ComputeApproximateDamage(_player, monster), font, Brushes.White,
                    new RectangleF(Floor.StartX + 10 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize + sumHeight[0], 2 * Floor.ObjectSize, rowHeight[1]), sf);

                // Third row
                g.DrawString("特性 : ", font, Brushes.White,
                    new RectangleF(Floor.StartX + 2 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize + sumHeight[1], 2 * Floor.ObjectSize, rowHeight[2]), sf);
                g.DrawString(monster.Description, font, Brushes.White,
                    new RectangleF(Floor.StartX + 4 * Floor.ObjectSize, (j * 2 + 1) * Floor.ObjectSize + sumHeight[1], 7 * Floor.ObjectSize, rowHeight[2]), sf);
            }

            if(_currentPage + 1 < _maxPage)
            {
                _rightArrow.Draw(g, Floor.ObjectSize, Floor.ObjectSize);
            }
            
            if(_currentPage > 0)
            {
                _leftArrow.Draw(g, Floor.ObjectSize, Floor.ObjectSize);
            }



        }

        public override void Excute()
        {
            
        }

        public override void Initialize()
        {
            _tower = Tower.Tower.GetInstance();
            _player = Player.GetInstance();
        }

        public override void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.M:
                    _active = !_active;
                    _player.Active = _active ? false : true;
                    if (_active)
                        SetMonsterBook();
                    break;
                case Keys.Left:
                    if (_active)
                    {
                        if (_currentPage > 0)
                        {
                            _currentPage--;
                        }
                    }
                    break;
                case Keys.Right:
                    if (_active)
                    {
                        if (_currentPage + 1 < _maxPage)
                        {
                            _currentPage++;
                        }
                    }
                    break;
            }
        }

        public override void KeyUp(KeyEventArgs e)
        {
        }

        public void SetMonsterBook()
        {
            _monsters = _tower.ExistedMonster(_player.CurrentFloor).ToList();
            _maxPage = (int)Math.Ceiling((double)_monsters.Count / MonstersPerPage);
            _currentPage = 0;
        }

        private int ComputeApproximateDamage(Player player, Monster monster)
        {
            int ret;
            int dealDamageToMonster;
            int dealDamageToPlayer;

            dealDamageToMonster = player.Atk - monster.Def;
            if (dealDamageToMonster <= 0)
            {
                return 99999999;
            }

            if (monster.Ability.Contains(Monster.MonsterFeature.Penetrate))
            {
                dealDamageToPlayer = monster.Atk;
            }
            else
            {
                dealDamageToPlayer = monster.Atk - player.Def;
            }

            if (dealDamageToPlayer <= 0)
            {
                return 0;
            }

            dealDamageToPlayer *= monster.AttackTime;
            dealDamageToMonster *= player.AttackTime;

            ret = (int)(Math.Ceiling((double)monster.Hp / dealDamageToMonster) - 1) * dealDamageToPlayer;

            return ret;
        } 
    }
}
