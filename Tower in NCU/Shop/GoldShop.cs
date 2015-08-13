using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tower_in_NCU.Applet;

namespace Tower_in_NCU.Shop
{
    class GoldShop : Shop
    {

        public GoldShop(string titleText, string[] menuText, Player player, Applet.Shop shop) : base(titleText, menuText, player, shop)
        {

        }

        protected override void Select()
        {
            switch (_currentChoice)
            {
                case 0:
                    if (_player.Gold >= 25)
                    {
                        _player.Hp += 500;
                        _player.Gold -= 25;
                    }
                    break;
                case 1:
                    if (_player.Gold >= 25)
                    {
                        _player.Atk += 4;
                        _player.Gold -= 25;
                    }
                    break;
                case 2:
                    if (_player.Gold >= 25)
                    {
                        _player.Def += 4;
                        _player.Gold -= 25;
                    }
                    break;
                case 3:
                    _shop.Active = false;
                    _player.Active = true;
                    break;
                default:
                    throw new Exception("Not expected choice.");
            }
        }
    }
}
