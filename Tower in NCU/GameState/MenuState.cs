using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_in_NCU.GameState
{
    class MenuState : GameState
    {
        private Image.ImageUnit _background;
        private int _currentChoice;
        private string[] _options = { "Start", "Help", "Quit" };

        private Font menuFont;

        public MenuState(GameStateManager gsm) : base(gsm)
        {
            _currentChoice = 0;
            try
            {
                _background = new Image.ImageUnit("Title");
                _background.SetPosition(0, 0);
                menuFont = new Font("Arial", 16);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Select()
        {
            switch (_currentChoice)
            {
                case 0:
                    gsm.SetState(GameStateManager.TOWERSTATE);
                    break;
                case 1:
                    gsm.SetState(GameStateManager.HELPSTATE);
                    break;
                case 2:
                    Application.Exit();
                    break;
                default:
                    break;
            }
        }

        public override void Draw(Graphics g)
        {
            _background.Draw(g);
            for (int i = 0; i < _options.Length; i++)
            {
                if (i == _currentChoice)
                {
                    g.DrawString(_options[i], menuFont, new SolidBrush(Color.Red), new PointF(450, 310 + i * 30));
                }
                else
                {
                    g.DrawString(_options[i], menuFont, new SolidBrush(Color.Black), new PointF(450, 310 + i * 30));
                }
            }
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (--_currentChoice < 0)
                        _currentChoice = _options.Length - 1;
                    break;
                case Keys.Down:
                    if (++_currentChoice >= _options.Length)
                        _currentChoice = 0;
                    break;
                case Keys.Enter:
                    Select();
                    break;
                default:
                    break;
            }
        }
        
    }
}
