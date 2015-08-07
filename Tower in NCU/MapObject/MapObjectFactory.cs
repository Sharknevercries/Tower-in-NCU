using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower_in_NCU.Tower;

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
                _objectInCol = img.Width / Tower.Floor.ObjectSize;
                _objectInRow = img.Height / Tower.Floor.ObjectSize;
                for(int row = 0; row < _objectInRow; row++)
                {
                    if(row <= 5)
                    {
                        for(int col = 0; col < _objectInCol; col++)
                        {
                            Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                            int type = row * _objectInCol + col;
                            _dataSet.Add(new Item(img.GetSubImage(rect), type));
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static MapObject CreateMapObject(int type)
        {
            // UNDONE: Sort the MapObject by type
            switch (type)
            {
                case 0: return _dataSet[type];
            }
            return _dataSet[0];
        }

    }
}
