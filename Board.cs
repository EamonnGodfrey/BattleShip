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
                string entry = Convert.ToString(value);
                while( !int.TryParse(entry, out _gridSize) || Convert.ToInt32(value) < 5 || Convert.ToInt32(value) > 26)
                {
                    Console.Write("Please enter a valid grid size (5-26): ");
                    entry = Console.ReadLine();
                    int.TryParse(entry, out value);
                    ++value;
                    _gridSize = value;
                }
                ++value;
                _gridSize = value;
            } 
        }        

        

    }
}
