using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower_in_NCU.Tower;
using Tower_in_NCU.Image;

namespace Tower_in_NCU.MapObject
{
    static class MapObjectFactory
    {
        public enum MapObjectImage
        {
            Floor1, Block1, UpStair1, UpStair2, DownStair1,
            YellowKey, BlueKey, RedKey, MonsterBook, DownStair2,
            RedPotion, BluePotion, RedCrystal, BlueCrystal, xxxNeedtoFix,
            YellowDoor, BlueDoor, RedDoor, TeleportStaf, s,
            Sword1, Sword2, Sword3, Sword4, Sword5,
            Shield1, Shield2, Shield3, Shield4, Shield5,
            a, b, c, d, e,
            f, g, h, i, j,
            Shop, ShopBody1, ShopBody2,
            NPC1,
            NCP2,
            k,
            l,
            m,
            n,
            GreenSlime, BlueSlime, RedSlime, LittleBat, BigBat,
            RedBat, Wizard, YellowWizard, RedWizard, Skeleton,
            ShieldSkeleton, EliteSkeleton,
        }
           
        private const string FloorImageName = "Image";
        private static List<List<ImageUnit>> _dataSet;
        private static int _objectInRow;
        private static int _objectInCol;

        static MapObjectFactory()
        {
            ImageUnit img = new ImageUnit(FloorImageName);
            _objectInCol = img.Width / Floor.ObjectSize;
            _objectInRow = img.Height / Floor.ObjectSize;
            _dataSet = new List<List<ImageUnit>>();
            for (int row = 0; row < _objectInRow; row++)
            {
                if (row <= 7) // Item
                {
                    for (int col = 0; col < _objectInCol; col++)
                    {
                        Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                        _dataSet.Add(new List<ImageUnit>() { img.GetSubImage(rect) });
                    }
                }
                else if (row == 8) // Shop
                {
                    List<ImageUnit> frames = new List<ImageUnit>();
                    for (int col = 0; col < 2; col++)
                    {
                        Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                        frames.Add(img.GetSubImage(rect));
                    }
                    // TODO: Set Shop
                    _dataSet.Add(frames);
                    for (int col = 2; col < 4; col++)
                    {
                        Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                        _dataSet.Add(new List<ImageUnit>() { img.GetSubImage(rect) });
                    }
                }
                else // NPC & Monster
                {
                    List<ImageUnit> frames = new List<ImageUnit>();
                    for (int col = 0; col < _objectInCol - 1; col++)
                    {
                        Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                        frames.Add(img.GetSubImage(rect));
                    }
                    _dataSet.Add(frames);
                }
            }
        }

        public static MapObject CreateMapObject(MapObjectType type)
        {
            switch (type)
            {
                // Item Zone
                case MapObjectType.Floor1:
                    return new Item(_dataSet[(int)MapObjectImage.Floor1], type);
                case MapObjectType.Block1:
                    return new Item(_dataSet[(int)MapObjectImage.Block1], type);
                case MapObjectType.UpStair1:
                    return new Item(_dataSet[(int)MapObjectImage.UpStair1], type);
                case MapObjectType.UpStair2:
                    return new Item(_dataSet[(int)MapObjectImage.UpStair2], type);
                case MapObjectType.YellowKey:
                    return new Item(_dataSet[(int)MapObjectImage.YellowKey], type);
                case MapObjectType.BlueKey:
                    return new Item(_dataSet[(int)MapObjectImage.BlueKey], type);
                case MapObjectType.RedKey:
                    return new Item(_dataSet[(int)MapObjectImage.RedKey], type);
                case MapObjectType.MonsterBook:
                    return null;
                case MapObjectType.DownStair1:
                    return new Item(_dataSet[(int)MapObjectImage.DownStair1], type);
                case MapObjectType.DownStair2:
                    return new Item(_dataSet[(int)MapObjectImage.DownStair2], type);
                case MapObjectType.RedPotion:
                    return new Item(_dataSet[(int)MapObjectImage.RedPotion], type);
                case MapObjectType.BluePotion:
                    return new Item(_dataSet[(int)MapObjectImage.BluePotion], type);
                case MapObjectType.RedCrystal:
                    return new Item(_dataSet[(int)MapObjectImage.RedCrystal], type);
                case MapObjectType.BlueCrystal:
                    return new Item(_dataSet[(int)MapObjectImage.BlueCrystal], type);

                case MapObjectType.YellowDoor:
                    return new Item(_dataSet[(int)MapObjectImage.YellowDoor], type);
                case MapObjectType.BlueDoor:
                    return new Item(_dataSet[(int)MapObjectImage.BlueDoor], type);
                case MapObjectType.RedDoor:
                    return new Item(_dataSet[(int)MapObjectImage.RedDoor], type);
                case MapObjectType.TeleportStaf:
                    return new Item(_dataSet[(int)MapObjectImage.TeleportStaf], type);

                case MapObjectType.Sword1:
                    return new Item(_dataSet[(int)MapObjectImage.Sword1], type);
                case MapObjectType.Sword2:
                    return new Item(_dataSet[(int)MapObjectImage.Sword2], type);
                case MapObjectType.Sword3:
                    return new Item(_dataSet[(int)MapObjectImage.Sword3], type);
                case MapObjectType.Sword4:
                    return new Item(_dataSet[(int)MapObjectImage.Sword4], type);
                case MapObjectType.Sword5:
                    return new Item(_dataSet[(int)MapObjectImage.Sword5], type);
                case MapObjectType.Shield1:
                    return new Item(_dataSet[(int)MapObjectImage.Shield1], type);
                case MapObjectType.Shield2:
                    return new Item(_dataSet[(int)MapObjectImage.Shield2], type);
                case MapObjectType.Shield3:
                    return new Item(_dataSet[(int)MapObjectImage.Shield3], type);
                case MapObjectType.Shield4:
                    return new Item(_dataSet[(int)MapObjectImage.Shield4], type);
                case MapObjectType.Shield5:
                    return new Item(_dataSet[(int)MapObjectImage.Shield5], type);

                // Shop Zone
                case MapObjectType.GoldShop:
                    return new Shop(_dataSet[(int)MapObjectImage.Shop], type);
                case MapObjectType.ShopBody1:
                    return new Shop(_dataSet[(int)MapObjectImage.ShopBody1], type);
                case MapObjectType.ShopBody2:
                    return new Shop(_dataSet[(int)MapObjectImage.ShopBody2], type);
                case MapObjectType.ExpShop:
                    return new Shop(_dataSet[(int)MapObjectImage.NPC1], type);

                // Monster Zone
                case MapObjectType.GreenSlime:
                    return new Monster(_dataSet[(int)MapObjectImage.GreenSlime], type);
                case MapObjectType.BlueSlime:
                    return new Monster(_dataSet[(int)MapObjectImage.BlueSlime], type);
                case MapObjectType.RedSlime:
                    return new Monster(_dataSet[(int)MapObjectImage.RedSlime], type);
                case MapObjectType.LittleBat:
                    return new Monster(_dataSet[(int)MapObjectImage.LittleBat], type);
                case MapObjectType.BigBat:
                    return new Monster(_dataSet[(int)MapObjectImage.BigBat], type);
                case MapObjectType.RedBat:
                    return new Monster(_dataSet[(int)MapObjectImage.RedBat], type);
                case MapObjectType.Wizard:
                    return new Monster(_dataSet[(int)MapObjectImage.Wizard], type);
                case MapObjectType.YellowWizard:
                    return new Monster(_dataSet[(int)MapObjectImage.YellowWizard], type);
                case MapObjectType.RedWizard:
                    return new Monster(_dataSet[(int)MapObjectImage.RedWizard], type);
                case MapObjectType.Skeleton:
                    return new Monster(_dataSet[(int)MapObjectImage.Skeleton], type);
                case MapObjectType.ShieldSkeleton:
                    return new Monster(_dataSet[(int)MapObjectImage.ShieldSkeleton], type);
                case MapObjectType.EliteSkeleton:
                    return new Monster(_dataSet[(int)MapObjectImage.EliteSkeleton], type);

            }
            throw new Exception("Not expected MapObject type " + (int)type + ".");
        }
        
    }
}
