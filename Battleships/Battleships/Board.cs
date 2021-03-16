using System;
using System.Collections.Generic;

namespace Battleships
{
    public class ShipCoordinates
    {
        public readonly int Row;
        public readonly int Column;

        private ShipCoordinates(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static ShipCoordinates CreateOrThrow(int row, int column)
        {
            if(row > 0 && row < 11 && column > 0 && column < 11)
                return new ShipCoordinates(row, column);
            throw new InvalidOperationException($"Trying to create invalid coordinates (row, col): ({row}, {column})");
        }
    }
    
    public class Ship
    {
        public ShipCoordinates StartCoordinates { get; }
        public ShipCoordinates EndCoordinates { get; }
    }
    
    public class Board
    {
        private List<Ship> _ships;
        
        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}