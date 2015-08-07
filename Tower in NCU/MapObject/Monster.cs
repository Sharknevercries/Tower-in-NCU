using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_in_NCU.MapObject
{
    class Monster : MapObject
    {
        public Monster(List<Image.ImageUnit> frames, int type) : base(frames, type) { }
        
    }
}
