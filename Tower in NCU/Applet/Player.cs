using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Tower_in_NCU.Tower;
using Tower_in_NCU.Main;
using Tower_in_NCU.MapObject;
using System.Windows.Forms;

namespace Tower_in_NCU.Applet
{
    class Player : Applet
    {

        private Tower.Tower _tower;

        private static List<Image.ImageUnit[]> _frames;
        private const string _characterImageName = "Actor1";
        private int _currentFrame;
        private int _battleFrame;
        private const int RowFrame = 4;
        private const int ColFrame = 3;

        private Point _position;
        private Point _nextPosition;

        private static Image.ImageUnit[] _keys;
        private int _hp;
        private int _atk;
        private int _def;
        private int _gold;
        private int _exp;
        private int _currentFloor;
        private int _yellowKey;
        private int _blueKey;
        private int _redKey;
        private bool _left;
        private bool _right;
        private bool _down;
        private bool _up;

        private static Player _player = new Player();

        public enum Face { Down, Left, Right, Up };
        
        private Face _finalFace;

        static Player()
        {
            _player = new Player();
            _frames = new List<Image.ImageUnit[]>();
            _keys = new Image.ImageUnit[3];
            try
            {
                Image.ImageUnit character = new Image.ImageUnit(_characterImageName);
                for(int row= 0; row < RowFrame; row++)
                {
                    Image.ImageUnit[] tmp = new Image.ImageUnit[ColFrame];
                    for(int col = 0; col < ColFrame; col++)
                    {
                        Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                        tmp[col] = character.GetSubImage(rect);
                    }
                    _frames.Add(tmp);
                }
                _keys[0] = MapObjectFactory.CreateMapObject(MapObjectType.YellowKey).Frames[0];
                _keys[1] = MapObjectFactory.CreateMapObject(MapObjectType.BlueKey).Frames[0];
                _keys[2] = MapObjectFactory.CreateMapObject(MapObjectType.RedKey).Frames[0];
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private Player() { }

        public static Player GetInstance() => _player;

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
            _battleFrame = 0;
            _currentFrame = 0;
            _active = true;
            _position = new Point(6, 11);
            _tower = Tower.Tower.GetInstance();
        }

        public override void Excute()
        {
            if (!_active)
                return;

            if (_left) _finalFace = Face.Left;
            if (_right) _finalFace = Face.Right;
            if (_down) _finalFace = Face.Down;
            if (_up) _finalFace = Face.Up;
            if (_left || _right || _up || _down)
            {
                _nextPosition = _position;
                switch (_finalFace)
                {
                    case Face.Down:
                        _nextPosition.Y++;
                        break;
                    case Face.Up:
                        _nextPosition.Y--;
                        break;
                    case Face.Left:
                        _nextPosition.X--;
                        break;
                    case Face.Right:
                        _nextPosition.X++;
                        break;
                    default:
                        throw new Exception("Not expected player's direction.");
                }
                if(_tower.Event(this)) 
                {
                    _position = _nextPosition;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            DrawStatus(g);
            _frames[(int)_finalFace][_currentFrame].SetPosition(Floor.StartX + _position.X * Floor.ObjectSize, _position.Y * Floor.ObjectSize);
            _frames[(int)_finalFace][_currentFrame].Draw(g);

            if (!_active)
                return;

            if (_left || _right || _up || _down)
                _currentFrame++;
            else
                _currentFrame = 1;
            if (_currentFrame >= _frames[(int)_finalFace].Length)
                _currentFrame = 0;
        }

        public override void KeyDown(KeyEventArgs e)
        {
            if (!_active)
                return;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    _left = true;
                    break;
                case Keys.Right:
                    _right = true;
                    break;
                case Keys.Down:
                    _down = true;
                    break;
                case Keys.Up:
                    _up = true;
                    break;
            }
        }

        public override void KeyUp(KeyEventArgs e)
        {
            if (!_active)
                return;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    _left = false;
                    break;
                case Keys.Right:
                    _right = false;
                    break;
                case Keys.Down:
                    _down = false;
                    break;
                case Keys.Up:
                    _up = false;
                    break;
            }
        }

        public Face FinalFace
        {
            get { return _finalFace; }
            set { _finalFace = value; }
        }

        public bool Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public bool Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public bool Up
        {
            get { return _up; }
            set { _up = value; }
        }

        public bool Down
        {
            get { return _down; }
            set { _down = value; }
        }

        public int X
        {
            get { return _position.X; }
            set { _position = new Point(value, _position.Y); }
        }

        public int Y
        {
            get { return _position.Y; }
            set { _position = new Point(_position.Y, value); }
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Point NextPosition
        {
            get { return _nextPosition; }
            set { _nextPosition = value; }
        }

        public int CurrentFloor
        {
            get { return _currentFloor; }
            set { _currentFloor = value; }
        }

        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public int Atk
        {
            get { return _atk; }
            set { _atk = value; }
        }

        public int Def
        {
            get { return _def; }
            set { _def = value; }
        }

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }

        public int Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }

        public int YellowKey
        {
            get { return _yellowKey; }
            set { _yellowKey = value; }
        }

        public int BlueKey
        {
            get { return _blueKey; }
            set { _blueKey = value; }
        }

        public int RedKey
        {
            get { return _redKey; }
            set { _redKey = value; }
        }

        private void DrawStatus(Graphics g)
        {
            Font font = new Font("Arial", 16);
            SolidBrush brush = new SolidBrush(Color.White);
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, Floor.StartX, GameWindow.GameHeight));
            string currentFloorString = string.Format("第 {0} 層", _currentFloor + 1 >= 10 ? "" + (_currentFloor + 1) : "0" + (_currentFloor + 1));
            g.DrawString("Tower In NCU", font, brush, new PointF(10, 10));
            g.DrawString(currentFloorString, font, brush, new PointF(35, 40));
            font = new Font("微軟正黑體", 14);
            g.DrawString("生命", font, brush, new PointF(15, 80));
            g.DrawString("" + _hp, font, brush, new PointF(80, 80));
            g.DrawString("攻擊", font, brush, new PointF(15, 110));
            g.DrawString("" + _atk, font, brush, new PointF(80, 110));
            g.DrawString("防禦", font, brush, new PointF(15, 140));
            g.DrawString("" + _def, font, brush, new PointF(80, 140));
            g.DrawString("金幣", font, brush, new PointF(15, 170));
            g.DrawString("" + _gold, font, brush, new PointF(80, 170));
            g.DrawString("經驗", font, brush, new PointF(15, 200));
            g.DrawString("" + _exp, font, brush, new PointF(80, 200));
            for(int i = 0; i < 3; i++)
            {
                _keys[i].SetPosition(new Point(15, 240 + 30 * i));
                _keys[i].Draw(g, Floor.ObjectSize, Floor.ObjectSize);
            }
            g.DrawString("X   " + _yellowKey, font, brush, new PointF(70, 245));
            g.DrawString("X   " + _blueKey, font, brush, new PointF(70, 275));
            g.DrawString("X   " + _redKey, font, brush, new PointF(70, 305));
        }

        public void DrawPlayer(Graphics g, Point position, Face face)
        {
            _frames[(int)face][_battleFrame / 3].SetPosition(position);
            _frames[(int)face][_battleFrame++ / 3].Draw(g);
            if (_battleFrame >= _frames[(int)_finalFace].Length)
                _battleFrame = 0;
        }

        public void StopMove()
        {
            _left = _right = _up = _down = false;
        }
        
    }
}
