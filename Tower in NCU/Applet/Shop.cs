using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_in_NCU.Applet
{
    class Shop : Applet
    {
        private static Shop _shop = new Shop();
        private List<Tower_in_NCU.Shop.Shop> _shops;
        private Player _player;
        private int _currentShop;

        private Shop() { }

        public override void Initialize()
        {
            _player = Player.GetInstance();
            _shops = new List<Tower_in_NCU.Shop.Shop>();
            // TODO: Set Text
            _shops.Add(new Tower_in_NCU.Shop.GoldShop("迷途的旅行者啊\n我這可以消費金幣來強化你的能力", new string[] { "HP + 500", "ATk + 3", "Def + 3" , "Exit" }, _player, this));
            _currentShop = 0;
        }

        public static Shop GetInstance() => _shop;

        public override void Draw(Graphics g)
        {
            if (!_active)
                return;

            _shops[_currentShop].Draw(g);
        }

        public override void Excute()
        {
            if (!_active)
                return;
        }

        public override void KeyDown(KeyEventArgs e)
        {
            if (!_active)
                return;

            _shops[_currentShop].KeyDown(e);
        }

        public override void KeyUp(KeyEventArgs e)
        {
            if (!_active)
                return;
        }

        public void SetShop(int id)
        {
            _currentShop = id;
            _shops[_currentShop].Initialzzie();
        }
    }
}
