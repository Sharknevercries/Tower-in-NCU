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
        private const string FloorImageName = "Image";
        private static List<MapObject> _dataSet;
        private static int _objectInRow;
        private static int _objectInCol;

        static MapObjectFactory()
        {
            _dataSet = new List<MapObject>();
            ImageUnit img = new ImageUnit(FloorImageName);
            _objectInCol = img.Width / Floor.ObjectSize;
            _objectInRow = img.Height / Floor.ObjectSize;
            for (int row = 0; row < _objectInRow; row++)
            {
                if (row <= 7) // Item
                {
                    for (int col = 0; col < _objectInCol; col++)
                    {
                        Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                        _dataSet.Add(new Item(img.GetSubImage(rect), (MapObjectType)_dataSet.Count));
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
                    _dataSet.Add(new Shop(frames, (MapObjectType)_dataSet.Count));
                    for (int col = 2; col < 4; col++)
                    {
                        Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                        _dataSet.Add(new Shop(img.GetSubImage(rect), (MapObjectType)_dataSet.Count));
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
                    _dataSet.Add(new Monster(frames, (MapObjectType)_dataSet.Count));
                }
            }
        }

        public static MapObject CreateMapObject(MapObjectType type)
        {
            switch (type)
            {
                default:
                    return _dataSet[(int)type].GetCopy();
            }
        }
    }
}
