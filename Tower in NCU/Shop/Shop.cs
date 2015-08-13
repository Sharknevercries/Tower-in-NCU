using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tower_in_NCU.Image;
using Tower_in_NCU.Tower;
using Tower_in_NCU.Applet;

namespace Tower_in_NCU.Shop
{
    abstract class Shop
    {
        private const string BackgroundImageName = "dialogueBackground";
        private const string ChoiceBackgroundImageName = "choiceBackground";
        protected Applet.Shop _shop;
        protected Player _player;
        protected ImageUnit _background;
        protected ImageUnit _choiceBackground;
        protected int _currentChoice;
        protected string _titleText;
        protected string[] _menuText;

        public Shop(string titleText, string[] menuText, Player player, Applet.Shop shop)
        {
            _shop = shop;
            _player = player;
            _menuText = menuText;
            _titleText = titleText;
            _background = new ImageUnit(BackgroundImageName);
            _choiceBackground = new ImageUnit(ChoiceBackgroundImageName);
            _currentChoice = 0;
        }

        public void Initialzzie()
        {
            _currentChoice = 0;
        }

        abstract protected void Select();

        public void Draw(Graphics g)
        {
            int paddingLeft = Floor.Edge / 4 * Floor.ObjectSize;
            int paddingTop = Floor.Edge / 2 * Floor.ObjectSize - _menuText.Length / 2 * Floor.ObjectSize;
            int titleOffset = 2;
            int width = Floor.Edge / 2 * Floor.ObjectSize;
            int height = (titleOffset + _menuText.Length) * Floor.ObjectSize;

            _background.SetPosition(Floor.StartX + paddingLeft, paddingTop);
            _background.Draw(g, width, height);
            _choiceBackground.SetPosition(Floor.StartX + paddingLeft, paddingTop + (_currentChoice + titleOffset) * Floor.ObjectSize);
            _choiceBackground.Draw(g, width, Floor.ObjectSize);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < _menuText.Length; i++)
            {
                g.DrawString(_menuText[i], new Font("Arial", 14), Brushes.White,
                    new RectangleF(Floor.StartX + paddingLeft, paddingTop + (i + titleOffset) * Floor.ObjectSize, width, Floor.ObjectSize), sf);
            }
        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (--_currentChoice < 0)
                        _currentChoice = _menuText.Length - 1;
                    break;
                case Keys.Down:
                    if (++_currentChoice >= _menuText.Length)
                        _currentChoice = 0;
                    break;
                case Keys.Enter:
                    Select();
                    break;
            }
        }
    }
}
