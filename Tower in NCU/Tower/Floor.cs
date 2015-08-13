using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Tower_in_NCU.MapObject;
using Tower_in_NCU.Applet;

namespace Tower_in_NCU.Tower
{
    class Floor
    {
        public static readonly int Edge = 13;
        public static readonly int ObjectSize = 32;
        public static readonly int StartX = Main.GameWindow.GameWidth - Edge * ObjectSize;

        private MapObject.MapObject[,] _map;
        private MapObject.MapObject _background;
        
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
                        // Neglect \r\t 
                        int id = row * (Edge + 1) + col;
                        MapObjectType type = (MapObjectType)int.Parse(numbers[id]);
                        SetMapObject(row, col, type);
                    }
                }
                _background = MapObjectFactory.CreateMapObject(MapObjectType.Floor1);
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
                    _background.SetPosition(StartX + col * ObjectSize, row * ObjectSize);
                    _background.Draw(g);
                    _map[row, col].SetPosition(StartX + col * ObjectSize, row * ObjectSize);
                    _map[row, col].Draw(g);
                }
            }
        }

        public void SetMapObject(int row, int col, MapObjectType type) => _map[row, col] = MapObjectFactory.CreateMapObject(type);

        public void SetMapObject(Point position, MapObjectType type) => SetMapObject(position.Y, position.X, type);

        public bool Event(Player player) => _map[player.NextPosition.Y, player.NextPosition.X].Event(player, this);
        
    }
}
