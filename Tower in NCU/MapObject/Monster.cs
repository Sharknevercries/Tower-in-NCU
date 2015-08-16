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
        private static Battle _battle = Battle.GetInstance();
        private int _hp;
        private int _atk;
        private int _def;
        private int _gold;
        private int _exp;
        private int _atkTime;
        private List<MonsterFeature> _ability;

        public enum MonsterFeature
        {
            None,

            Penetrate,

            TwiceAttack, TripleAttack,
        }

        public Monster(List<Image.ImageUnit> frames, MapObjectType type) : base(frames, type)
        {
            switch (type)
            {
                case MapObjectType.GreenSlime:
                    SetStatus(30, 20, 5, 1, 1, 1, new List<MonsterFeature> { MonsterFeature.None });
                    break;
                case MapObjectType.BlueSlime:
                    SetStatus(50, 25, 5, 2, 1, 1, new List<MonsterFeature> { MonsterFeature.None });
                    break;
                case MapObjectType.RedSlime:
                    SetStatus(80, 30, 5, 3, 2, 1, new List<MonsterFeature> { MonsterFeature.None });
                    break;
                case MapObjectType.LittleBat:
                    SetStatus(40, 35, 20, 5, 2, 1, new List<MonsterFeature> { MonsterFeature.None });
                    break;
                case MapObjectType.BigBat:
                    SetStatus(70, 80, 30, 7, 4, 2, new List<MonsterFeature> { MonsterFeature.TwiceAttack });
                    break;
                case MapObjectType.Wizard:
                    SetStatus(70, 15, 10, 4, 2, 1, new List<MonsterFeature> { MonsterFeature.Penetrate });
                    break;
                case MapObjectType.YellowWizard:
                    SetStatus(150, 25, 25, 10, 3, 1, new List<MonsterFeature> { MonsterFeature.Penetrate });
                    break;
                case MapObjectType.Skeleton:
                    SetStatus(200, 100, 5, 7, 3, 1, new List<MonsterFeature> { MonsterFeature.None });
                    break;
            }
        }

        public void SetStatus(int hp, int atk, int def, int gold, int exp, int atkTime,  List<MonsterFeature> ability)
        {
            _hp = hp;
            _atk = atk;
            _def = def;
            _gold = gold;
            _exp = exp;
            _atkTime = atkTime;
            _ability = ability;
        }
        
        public override bool Event(Player player, Floor floor)
        {
            _battle.SetBattle(player, (Monster)MemberwiseClone(), floor);
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

        public int AttackTime
        {
            get { return _atkTime; }
            set { _atkTime = value; }
        }

        public List<MonsterFeature> Ability
        {
            get { return _ability; }
            set { _ability = value; }
        }

        public string Description
        {
            get
            {
                string ret = "";
                foreach (var ability in _ability)
                {
                    if (ret != "")
                        ret += "、";
                    switch (ability)
                    {
                        case MonsterFeature.None:
                            ret += "無";
                            break;
                        case MonsterFeature.Penetrate:
                            ret += "無視防禦";
                            break;
                        case MonsterFeature.TwiceAttack:
                            ret += "兩次攻擊";
                            break;
                        case MonsterFeature.TripleAttack:
                            ret += "三次攻擊";
                            break;
                        default:
                            throw new Exception("Not expected monster ability.");
                    }
                }
                return ret;
            }
        }

    }
}
