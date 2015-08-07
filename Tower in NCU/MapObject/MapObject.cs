using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_in_NCU.MapObject
{
    abstract class MapObject
    {
        private List<Image.ImageUnit> _frames;
        private int _currentFrame;
        private int _type;
        
        public MapObject(List<Image.ImageUnit> frames, int type)
        {
            _frames = frames;
            _currentFrame = 0;
            _type = type;
        }

        public MapObject(Image.ImageUnit img, int type) : this(new List<Image.ImageUnit>() { img }, type) { }

        public void SetPosition(int x, int y)
        {
            for(int i = 0; i < _frames.Count; i++)
            {
                _frames[i].SetPosition(x, y);
            }
        }

        public void Draw(Graphics g)
        {
            _frames[_currentFrame++].Draw(g);
            if(_currentFrame >= _frames.Count)
            {
                _currentFrame = 0;
            }
        }

        public int Type
        {
            get { return _type; }
        }
        
    }
}
