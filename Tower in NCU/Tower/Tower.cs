using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_in_NCU.Tower
{
    class Tower
    {
        private static readonly int _maxFloor = 5;
        private static Tower _tower = new Tower();
        private Floor[] _floors;

        private Tower()
        {
            _floors = new Floor[_maxFloor];
            for (int i = 0; i < _maxFloor; i++)
                _floors[i] = new Floor();
        }

        public static Tower GetInstance() => _tower;

        public Floor GetFloor(int currentFloor) => _floors[currentFloor];

        public void Initialize()
        {
            for (int i = 0; i < _maxFloor; i++)
                _floors[i].Initilize("floor" + (i + 1));
        }

    }
}
