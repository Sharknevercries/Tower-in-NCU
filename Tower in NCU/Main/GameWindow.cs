using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_in_NCU.Main
{
    public partial class GameWindow : Form
    {
        public static readonly int GameWidth = 576;
        public static readonly int GameHeight = 416;

        private GameState.GameStateManager _gsm;
        private System.Drawing.Graphics _g;

        public GameWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _gsm = new GameState.GameStateManager();
            gamePanel.Paint += GamePanel_Paint;
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            _g = e.Graphics;
            _gsm.Draw(_g);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gamePanel.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _gsm.KeyDown(e);
        }
    }
}
