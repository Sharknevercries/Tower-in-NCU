using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_in_NCU.MapObject
{
    class Item : MapObject
    {
        public Item(Image.ImageUnit img, int type) : base(img, type) { }

        public Item(List<Image.ImageUnit> frames, int type) : base(frames, type)
        {

        }
    }
}