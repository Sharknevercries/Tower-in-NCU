using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower_in_NCU.Applet;
using Tower_in_NCU.Tower;
using Tower_in_NCU.Image;

namespace Tower_in_NCU.MapObject
{
    class Shop : MapObject
    {
        private Applet.Shop _shop;

        public Shop(List<ImageUnit> frames, MapObjectType type) : base(frames, type)
        {
            _shop = Applet.Shop.GetInstance();
        }

        public Shop(ImageUnit img, MapObjectType type) : base(img, type)
        {
            _shop = Applet.Shop.GetInstance();
        }

        public override bool Event(Player player, Floor floor)
        {
            switch (_type)
            {
                case MapObjectType.ShopBody1:
                case MapObjectType.ShopBody2:
                    break;
                case MapObjectType.GoldShop:
                    _shop.SetShop(0);
                    _shop.Active = true;
                    player.Active = false;
                    break;
            }
            return false;
        }
    }
}
