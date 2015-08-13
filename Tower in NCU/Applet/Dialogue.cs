using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tower_in_NCU.Tower;
using Tower_in_NCU.Main;
using Tower_in_NCU.Image;

namespace Tower_in_NCU.Applet
{
    class Dialogue : Applet
    {
        private static Dialogue _dialogue;
        private static Queue<Dialogue> _queue;

        private ImageUnit _background;
        private const string BackgroundImageName = "dialogueBackground";

        public enum DialogueLocation
        {
            Up = 0, Middle = 160, Down = 320,
        };

        public enum FaceLoaction
        {
            Left = 96, Right = 200 , None,
        };

        private ImageUnit _faceImage;
        private DialogueLocation _dialogueLocation;
        private FaceLoaction _faceLoaction;
        private string _nameTitle;
        private string _message;

        static Dialogue()
        {
            _dialogue = new Dialogue();
            _queue = new Queue<Dialogue>();
        }

        private Dialogue()
        {
            _active = true;
        }

        private Dialogue(DialogueLocation dialogueLocation, string nameTitle, string message, FaceLoaction faceLocation, string faceName)
        {
            _nameTitle = nameTitle;
            _message = message;
            _dialogueLocation = dialogueLocation;
            _faceLoaction = faceLocation;
            _faceImage = faceName != null ? new ImageUnit(faceName) : null;
            _background = new ImageUnit(BackgroundImageName);
        }

        public void Initialize()
        {
            _queue.Clear();
        }

        public void AddDialogue(DialogueLocation dialogueLocation, string nameTitle, string message, FaceLoaction faceLocation, string faceName)
        {
            _queue.Enqueue(new Dialogue(dialogueLocation, nameTitle, message, faceLocation, faceName));
        }

        private void Show(Graphics g)
        {
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font messageFont = new Font("Arial", 14);
            StringFormat sf = new StringFormat();

            _background.SetPosition(Floor.StartX, (int)_dialogueLocation);
            _background.Draw(g);

            switch (_faceLoaction)
            {
                case FaceLoaction.Left:
                    sf.Alignment = StringAlignment.Near;
                    _faceImage.SetPosition(new Point(Floor.StartX, (int)_dialogueLocation));
                    _faceImage.Draw(g);
                    g.DrawString(_nameTitle, titleFont, Brushes.White,
                        new RectangleF(Floor.StartX + _faceImage.Width, (int)_dialogueLocation, _background.Width - _faceImage.Width, _background.Height), sf);
                    g.DrawString(_message, messageFont, Brushes.White,
                        new RectangleF(Floor.StartX + _faceImage.Width, (int)_dialogueLocation + 20, _background.Width - _faceImage.Width, _background.Height), sf);
                    break;
                case FaceLoaction.Right:
                    sf.Alignment = StringAlignment.Near;
                    _faceImage.SetPosition(new Point(GameWindow.GameWidth - _faceImage.Width, (int)_dialogueLocation));
                    _faceImage.Draw(g);
                    g.DrawString(_nameTitle, titleFont, Brushes.White,
                        new RectangleF(Floor.StartX, (int)_dialogueLocation, _background.Width - _faceImage.Width, _background.Height), sf);
                    g.DrawString(_message, messageFont, Brushes.White,
                        new RectangleF(Floor.StartX, (int)_dialogueLocation + 30, _background.Width - _faceImage.Width, _background.Height), sf);
                    break;
                case FaceLoaction.None:
                    sf.Alignment = StringAlignment.Center;
                    g.DrawString(_nameTitle, titleFont, Brushes.White,
                       new RectangleF(Floor.StartX, (int)_dialogueLocation, _background.Width, _background.Height), sf);
                    g.DrawString(_message, messageFont, Brushes.White,
                        new RectangleF(Floor.StartX, (int)_dialogueLocation + 30, _background.Width, _background.Height), sf);
                    break;
            }
        }

        public override void Draw(Graphics g)
        {
            if (_queue.Count > 0)
            {  
                _queue.Peek().Show(g);
            }
        }

        public override void Excute()
        {
            
        }

        public override void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if(_queue.Count > 0)
                        _queue.Dequeue();
                    break;
            }
        }

        public override void KeyUp(KeyEventArgs e)
        {
            
        }

        public static Dialogue GetInstance() => _dialogue;

        public bool HasMessage()
        {
            return _queue.Count > 0;
        }
    }
}
