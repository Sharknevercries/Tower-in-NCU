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
    class Item : MapObject
    {
        public Item(Image.ImageUnit img, MapObjectType type) : base(img, type) { }

        public Item(List<Image.ImageUnit> frames, MapObjectType type) : base(frames, type) { }

        public override bool Event(Player player, Floor floor)
        {
            bool access = true;
            switch (_type)
            {
                case MapObjectType.Floor1:
                    break;
                case MapObjectType.Block1:
                    access = false;
                    break;
                case MapObjectType.DownStair1:
                case MapObjectType.DownStair2:
                    player.CurrentFloor--;
                    break;
                case MapObjectType.UpStair1:
                case MapObjectType.UpStair2:
                    player.CurrentFloor++;
                    break;
                case MapObjectType.YellowKey:
                    player.YellowKey++;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    break;
                case MapObjectType.BlueKey:
                    player.BlueKey++;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    break;
                case MapObjectType.RedKey:
                    player.RedKey++;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    break;
                case MapObjectType.MonsterBook:
                    return true;
                case MapObjectType.RedPotion:
                    player.Hp += 200;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    break;
                case MapObjectType.BluePotion:
                    player.Hp += 500;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    break;
                case MapObjectType.RedCrystal:
                    player.Atk += 3;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    break;
                case MapObjectType.BlueCrystal:
                    player.Def += 3;
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    break;
                case MapObjectType.YellowDoor:
                    if(player.YellowKey > 0)
                    {
                        player.YellowKey--;
                        floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                        break;
                    }
                    else
                    {
                        access = false;
                        break;
                    }
                case MapObjectType.BlueDoor:
                    if (player.BlueKey > 0)
                    {
                        player.BlueKey--;
                        floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                        break;
                    }
                    else
                    {
                        access = false;
                        break;
                    }
                case MapObjectType.TeleportStaf:
                    floor.SetMapObject(player.NextPosition, MapObjectType.Floor1);
                    break;
                case MapObjectType.Sword1:
                case MapObjectType.Sword2:
                case MapObjectType.Sword3:
                case MapObjectType.Sword4:
                case MapObjectType.Sword5:
                case MapObjectType.Shield1:
                case MapObjectType.Shield2:
                case MapObjectType.Shield3:
                case MapObjectType.Shield4:
                case MapObjectType.Shield5:
                    break;
            }
            return access;
        }
    }
}