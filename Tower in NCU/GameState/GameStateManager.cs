using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Tower_in_NCU.GameState
{
    class GameStateManager
    {
        private List<GameState> _gameStates;
        private int _currentState;

        public static readonly int MENUSTATE        = 0;
        public static readonly int TOWERSTATE       = 1;
        public static readonly int HELPSTATE        = 2;
        public static readonly int GAMEOVERSTATE    = 3;
        public static readonly int GAMEENDSTATE     = 4;

        public GameStateManager()
        {
            _gameStates = new List<GameState>();
            _currentState = MENUSTATE;
            _gameStates.Add(new MenuState(this));
            _gameStates.Add(new TowerState(this));
            //gameStates.Add(new HelpState(this));
            // gameStates.Add(new GameOverState(this));
            // gameStates.Add(new GameEndState(this));
        }

        public void Draw(Graphics g) => _gameStates[_currentState].Draw(g);

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void KeyDown(KeyEventArgs e) => _gameStates[_currentState].KeyDown(e);

        public void KeyPress(KeyEventArgs e) => _gameStates[_currentState].KeyUp(e);

        public void Excute() => _gameStates[_currentState].Excute();

        public void SetState(int state)
        {
            _gameStates[state].Initialize();
            try
            {
                Thread.Sleep(100);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            _currentState = state;
        }
    }
}
