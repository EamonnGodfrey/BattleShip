using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Board
    {
        public static string[,] _initialBoardState;

        public static string[,] _boardPositionalMarkers;
        public static int _gridSize;
        public static int GridSize 
        { 
            get
            {
                return _gridSize;
            }
            set 
            {
                ++value;
                _gridSize = value;
            } 
        }
    }
}
