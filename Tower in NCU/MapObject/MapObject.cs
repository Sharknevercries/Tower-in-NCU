using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Tower_in_NCU.Tower;
using Tower_in_NCU.Applet;
using Tower_in_NCU.Image;

namespace Tower_in_NCU.MapObject
{
    public enum MapObjectType
    {
        // TODO: Fix Name
        Floor1, Block1, UpStair1, UpStair2, DownStair1,
        YellowKey, BlueKey, RedKey, MonsterBook, DownStair2,
        RedPotion, BluePotion, RedCrystal, BlueCrystal, xxxNeedtoFix,
        YellowDoor, BlueDoor, RedDoor, TeleportStaf, s,
        Sword1, Sword2, Sword3, Sword4, Sword5,
        Shield1, Shield2, Shield3, Shield4, Shield5,
        a, b,c,d,e, 
        f,g,h,i,j,
        Shop, ShopBody1, ShopBody2,
        NPC1, NCP2,
        k,
        l,
        m,
        n,
        GreenSlime, BlueSlime, RedSlime, LittleBat, BigBat,
        RedBigBat, Wizard, BlueWizard, RedWizard, Skeleton,
        ShieldSkeleton, EliteSkeleton,
    };

    abstract class MapObject
    {
        private List<ImageUnit> _frames;
        private int _currentFrame;
        protected MapObjectType _type;
        protected Dialogue _dialogue;
        
        public MapObject(List<ImageUnit> frames, MapObjectType type)
        {
            _frames = frames;
            _currentFrame = 0;
            _type = type;
            _dialogue = Dialogue.GetInstance();
        }

        public MapObject(ImageUnit img, MapObjectType type) : this(new List<ImageUnit>() { img }, type) { }

        public MapObject GetCopy()
        {
            MapObject obj = (MapObject)MemberwiseClone();
            obj._frames = new List<ImageUnit>(obj._frames);
            return obj;
        }

        public void SetPosition(int x, int y)
        {
            for(int i = 0; i < _frames.Count; i++)
            {
                _frames[i].SetPosition(x, y);
            }
        }

        public void SetPosition(Point position)
        {
            SetPosition(position.X, position.Y);
        }

        public void Draw(Graphics g)
        {
            _frames[_currentFrame++ / 3].Draw(g);
            _currentFrame = _currentFrame >= 3 * _frames.Count ? 0 : _currentFrame;
        }
        
        public abstract bool Event(Player player, Floor floor);

        public MapObjectType Type
        {
            get { return _type; }
        }

        public List<ImageUnit> Frames
        {
            get { return _frames; }
        }
        
    }
}
