using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Battleships
    {
        public int _shipLength;
        public string _id;
        public int[] _columnPosition, _rowPosition;
        public int _hitsTaken;
        public bool _shipActive;

        public int[] ColumnPosition 
        { 
            get 
            { 
                return _columnPosition; 
            } 
            set 
            { 
                _columnPosition = value; 
            } 
        }
        public int[] RowPosition 
        {
            get 
            {
                return _rowPosition;
            } 
            set
            {
                _rowPosition = value;
            }
        }
    }
}
