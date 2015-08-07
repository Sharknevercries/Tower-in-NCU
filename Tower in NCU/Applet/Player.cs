using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Tower_in_NCU.Tower;

namespace Tower_in_NCU.Applet
{
    class Player
    {
        private static List<Image.ImageUnit[]> _frames;
        private static readonly string _characterImageName = "Actor1";
        private int _currentFrame;
        private static readonly int _rowFrame = 4;
        private static readonly int _colFrame = 3;

        private Point _position;

        private static Image.ImageUnit[] keys;
        private int _hp;
        private int _atk;
        private int _def;
        private int _gold;
        private int _exp;
        private int _currentFloor;
        private int _yellowKey;
        private int _blueKey;
        private int _redKey;

        private static Player _player = new Player();

        enum Face { Up, Down, Left, Right };
        
        private Face _finalFace;

        static Player()
        {
            _frames = new List<Image.ImageUnit[]>();
            try
            {
                Image.ImageUnit character = new Image.ImageUnit(_characterImageName);
                for(int row= 0; row < _rowFrame; row++)
                {
                    Image.ImageUnit[] tmp = new Image.ImageUnit[_colFrame];
                    for(int col = 0; col < _colFrame; col++)
                    {
                        Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                        tmp[col] = character.GetSubImage(rect);
                    }
                    _frames.Add(tmp);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private Player() { }

        public void Initialize()
        {
            _finalFace = Face.Down;
            _hp = 1000;
            _atk = 10;
            _def = 10;
            _gold = 0;
            _exp = 0;
            _yellowKey = 0;
            _blueKey = 0;
            _redKey = 0;
            _currentFloor = 0;
            _currentFrame = 0;
            _position = new Point(6, 11);
        }

    }
}
