using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower_in_NCU.Tower;
using Tower_in_NCU.MapObject;

namespace Tower_in_NCU.MapObject
{
    static class MapObjectFactory
    {   
        private const string FloorImageName = "image";
        private static List<MapObject> _dataSet;
        private static int _objectInRow;
        private static int _objectInCol;

        static MapObjectFactory()
        {
            _dataSet = new List<MapObject>();
            try
            {
                Image.ImageUnit img = new Image.ImageUnit(FloorImageName);
                _objectInCol = img.Width / Floor.ObjectSize;
                _objectInRow = img.Height / Floor.ObjectSize;
                for(int row = 0; row < _objectInRow; row++)
                {
                    if(row <= 5)
                    {
                        for(int col = 0; col < _objectInCol; col++)
                        {
                            Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                            _dataSet.Add(new Item(img.GetSubImage(rect), (MapObjectType)_dataSet.Count));
                        }
                    }
                    else if(row >= 14)
                    {
                        List<Image.ImageUnit> frames = new List<Image.ImageUnit>();
                        for(int col = 0; col < _objectInCol - 1; col++)
                        {
                            Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                            frames.Add(img.GetSubImage(rect));
                        }
                        _dataSet.Add(new Monster(frames, (MapObjectType)_dataSet.Count));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static MapObject CreateMapObject(MapObjectType type) => _dataSet[(int)type].GetCopy();

    }
}
