using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_in_NCU.Image
{
    class ImageUnit
    {
        private Bitmap _img;

        private Point _position;

        public ImageUnit(string s)
        {
            try
            {
                object obj = Properties.Resources.ResourceManager.GetObject(s);
                _img = (System.Drawing.Bitmap)obj;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public ImageUnit(Bitmap img)
        {
            _img = img;
        }
        
        public int Width
        {
            get { return _img.Width; }
        }

        public int Height
        {
            get { return _img.Height; }
        }

        public void SetPosition(int x, int y) => _position = new Point(x, y);

        public void Draw(Graphics g) => g.DrawImage(_img, _position);

        public void Draw(Graphics g, int width, int height) => g.DrawImage(_img, new Rectangle(_position.X, _position.Y, width, height));

        public ImageUnit GetSubImage(Rectangle rect) => new ImageUnit(_img.Clone(rect, _img.PixelFormat));
        
    }
}
