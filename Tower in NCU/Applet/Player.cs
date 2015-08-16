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
using Tower_in_NCU.Image;
using Tower_in_NCU.Audio;

namespace Tower_in_NCU.Applet
{
    class Player : Applet
    {
        private static Player _player = new Player();

        private Tower.Tower _tower;
        private Dialogue _dialogue;
        private AudioPlayer _audioPlayer;

        private List<ImageUnit[]> _frames;
        private const string _characterImageName = "Actor1";
        private int _currentFrame;
        private int _battleFrame;
        private const int RowFrame = 4;
        private const int ColFrame = 3;

        private Point _position;
        private Point _nextPosition;

        private ImageUnit[] _keysImage;
        private int _hp;
        private int _atk;
        private int _atkTime;
        private int _def;
        private int _gold;
        private int _exp;
        private int _currentFloor;
        private int[] _keys;
        private bool _left;
        private bool _right;
        private bool _down;
        private bool _up;

        public enum Face { Down, Left, Right, Up };
        
        private Face _finalFace;
        
        private Player()
        {
            _frames = new List<ImageUnit[]>();
            _keysImage = new ImageUnit[3];
            try
            {
                ImageUnit character = new ImageUnit(_characterImageName);
                for (int row = 0; row < RowFrame; row++)
                {
                    ImageUnit[] tmp = new ImageUnit[ColFrame];
                    for (int col = 0; col < ColFrame; col++)
                    {
                        Rectangle rect = new Rectangle(col * Floor.ObjectSize, row * Floor.ObjectSize, Floor.ObjectSize, Floor.ObjectSize);
                        tmp[col] = character.GetSubImage(rect);
                    }
                    _frames.Add(tmp);
                }
                _keysImage[0] = MapObjectFactory.CreateMapObject(MapObjectType.YellowKey).Frames[0];
                _keysImage[1] = MapObjectFactory.CreateMapObject(MapObjectType.BlueKey).Frames[0];
                _keysImage[2] = MapObjectFactory.CreateMapObject(MapObjectType.RedKey).Frames[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static Player GetInstance() => _player;

        public override void Initialize()
        {
            _finalFace = Face.Down;
            _hp = 1000;
            _atk = 100;
            _atkTime = 1;
            _def = 100;
            _gold = 0;
            _exp = 0;
            _keys = new int[] { 55, 55, 0 };
            _currentFloor = 3;
            _battleFrame = 0;
            _currentFrame = 0;
            _active = true;
            _position = new Point(6, 11);
            _tower = Tower.Tower.GetInstance();
            _dialogue = Dialogue.GetInstance();
            _audioPlayer = AudioPlayer.GetInstance();
        }

        public override void Excute()
        {
            if (!_active)
            {
                StopMove();
                return;
            }

            if (_dialogue.HasMessage())
                return;

            if (_left) _finalFace = Face.Left;
            if (_right) _finalFace = Face.Right;
            if (_down) _finalFace = Face.Down;
            if (_up) _finalFace = Face.Up;
            if (_left || _right || _up || _down)
            {
                _audioPlayer.Play(AudioPlayer.SoundEffect.Move);
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

            if (_dialogue.HasMessage())
            {
                StopMove();
                return;
            }

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
            {
                StopMove();
                return;
            }

            if (_dialogue.HasMessage())
            {
                StopMove();
                return;
            }

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
            {
                StopMove();
                return;
            }

            if (_dialogue.HasMessage())
            {
                StopMove();
                return;
            }

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
            get { return _keys[0]; }
            set { _keys[0] = value; }
        }

        public int BlueKey
        {
            get { return _keys[1]; }
            set { _keys[1] = value; }
        }

        public int RedKey
        {
            get { return _keys[2]; }
            set { _keys[2] = value; }
        }

        public int AttackTime
        {
            get { return _atkTime; }
            set { AttackTime = value; }
        }

        private void DrawStatus(Graphics g)
        {
            string currentFloorString = string.Format("第 {0} 層", _currentFloor + 1 >= 10 ? "" + (_currentFloor + 1) : "0" + (_currentFloor + 1));
            int[] width = { Floor.ObjectSize * 2, Floor.ObjectSize * 3 };
            int[] sumWidth = { Floor.ObjectSize * 2, Floor.ObjectSize * 5 };
            Font font = new Font("Arial", 16);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.FillRectangle(Brushes.Black, new Rectangle(0, 0, Floor.StartX, GameWindow.GameHeight));
            g.DrawString("Tower In NCU", font, Brushes.White, new RectangleF(0, 6, Floor.ObjectSize * 5, Floor.ObjectSize), sf);
            g.DrawString(currentFloorString, font, Brushes.White, new PointF(35, 40));
            font = new Font("Arial", 14);
            g.DrawString("生命", font, Brushes.White, new RectangleF(0, 3 * Floor.ObjectSize, width[0], Floor.ObjectSize), sf);
            g.DrawString("" + _hp, font, Brushes.White,
                new RectangleF(width[0], 3 * Floor.ObjectSize, width[1], Floor.ObjectSize), sf);
            g.DrawString("攻擊", font, Brushes.White, new RectangleF(0, 4 * Floor.ObjectSize, width[0], Floor.ObjectSize), sf);
            g.DrawString("" + _atk, font, Brushes.White,
                new RectangleF(width[0], 4 * Floor.ObjectSize, width[1], Floor.ObjectSize), sf);
            g.DrawString("防禦", font, Brushes.White, new RectangleF(0, 5 * Floor.ObjectSize, width[0], Floor.ObjectSize), sf);
            g.DrawString("" + _def, font, Brushes.White,
                new RectangleF(width[0], 5 * Floor.ObjectSize, width[1], Floor.ObjectSize), sf);
            g.DrawString("金幣", font, Brushes.White, new RectangleF(0, 6 * Floor.ObjectSize, width[0], Floor.ObjectSize), sf);
            g.DrawString("" + _gold, font, Brushes.White,
                new RectangleF(width[0], 6 * Floor.ObjectSize, width[1], Floor.ObjectSize), sf);
            g.DrawString("經驗", font, Brushes.White, new RectangleF(0, 7 * Floor.ObjectSize, width[0], Floor.ObjectSize), sf);
            g.DrawString("" + _exp, font, Brushes.White,
                new RectangleF(width[0], 7 * Floor.ObjectSize, width[1], Floor.ObjectSize), sf);

            for (int i = 0; i < 3; i++)
            {
                _keysImage[i].SetPosition(Floor.ObjectSize, (8 + i) * Floor.ObjectSize);
                _keysImage[i].Draw(g, Floor.ObjectSize, Floor.ObjectSize);
                g.DrawString("X", font, Brushes.White, 2.2F * Floor.ObjectSize, (8.2F + i) * Floor.ObjectSize);
                g.DrawString("" + _keys[i], font, Brushes.White,
                    new RectangleF(3 * Floor.ObjectSize, (8.1F + i) * Floor.ObjectSize, 1 * Floor.ObjectSize, Floor.ObjectSize), sf);
            }
            
        }

        public void DrawPlayer(Graphics g, Point position, Face face)
        {
            _frames[(int)face][_battleFrame / 3].SetPosition(position);
            _frames[(int)face][_battleFrame++ / 3].Draw(g);
            if (_battleFrame >= _frames[(int)face].Length * 3)
                _battleFrame = 0;
        }

        public void StopMove()
        {
            _left = _right = _up = _down = false;
        }
        
    }
}
