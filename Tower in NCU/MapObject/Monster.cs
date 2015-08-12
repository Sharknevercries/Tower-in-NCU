using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower_in_NCU.Applet;
using Tower_in_NCU.Tower;

namespace Tower_in_NCU.MapObject
{
    class Monster : MapObject
    {
        private Battle _battle;
        private int _hp;
        private int _atk;
        private int _def;
        private int _gold;
        private int _exp;

        public Monster(List<Image.ImageUnit> frames, MapObjectType type) : base(frames, type)
        {
            _battle = Battle.GetInstance();
            switch (type)
            {
                case MapObjectType.GreenSlime:
                    SetStatus(30, 20, 5, 1, 1);
                    break;
                case MapObjectType.BlueSlime:
                    SetStatus(50, 25, 5, 2, 1);
                    break;
                case MapObjectType.RedSlime:
                    SetStatus(80, 30, 5, 3, 2);
                    break;
                case MapObjectType.LittleBat:
                    SetStatus(40, 35, 20, 5, 2);
                    break;
                case MapObjectType.BigBat:
                    SetStatus(70, 80, 30, 7, 4);
                    break;
                case MapObjectType.BlueWizard:
                    SetStatus(70, 15, 10, 4, 2);
                    break;
                case MapObjectType.Wizard:
                    SetStatus(150, 25, 25, 10, 3);
                    break;
                case MapObjectType.Skeleton:
                    SetStatus(200,100, 5, 7, 3);
                    break;
            }
        }

        public void SetStatus(int hp, int atk, int def, int gold, int exp)
        {
            _hp = hp;
            _atk = atk;
            _def = def;
            _gold = gold;
            _exp = exp;
        }
        
        public override bool Event(Player player, Floor floor)
        {
            Monster monster = (Monster)MemberwiseClone();
            _battle.Initialize(player, monster, floor);
            _battle.Active = true;
            player.StopMove();
            player.Active = false;
            return false;
        }

        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public int Atk
        {
            get { return _atk; }
            set { _atk = value; }
        }

        public int Def
        {
            get { return _def; }
            set { _def = value; }
        }
        
        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }

        public int Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }

    }
}
