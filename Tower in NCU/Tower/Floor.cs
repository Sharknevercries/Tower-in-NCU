using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Tower_in_NCU.Tower
{
    class Floor
    {
        public static readonly int Edge = 13;
        public static readonly int ObjectSize = 32;
        public static readonly int StartX = Main.GameWindow.GameWidth - Edge * ObjectSize;

        private MapObject.MapObject[,] _map;
        
        public Floor()
        {
            _map = new MapObject.MapObject[Edge, Edge];
        }

        public void Initilize(string s)
        {
            try
            {
                string lines = (string)Properties.Resources.ResourceManager.GetObject(s);
                string[] numbers = lines.Split(new char[] { ' ', '\t', '\n', '\r' });
                for (int row = 0; row < Edge; row++)
                {
                    for (int col = 0; col < Edge; col++)
                    {
                        int id = row * (Edge + 1) + col;
                        int type = int.Parse(numbers[id]);
                        SetMapObject(row, col, type);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Draw(Graphics g)
        {
            for (int row = 0; row < Edge; row++)
            {
                for (int col = 0; col < Edge; col++)
                {
                    _map[row, col].SetPosition(StartX + col * ObjectSize, row * ObjectSize);
                    _map[row, col].Draw(g);
                }
            }
        }

        public void SetMapObject(int row, int col, int type)
        {
            _map[row, col] = MapObject.MapObjectFactory.CreateMapObject(type);
        }

        public int GetType(int row,int col)
        {
            return _map[row, col].Type;
        }

    }
}
