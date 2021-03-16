using System;
using System.Collections.Generic;

namespace Battleships
{
    public class ShipSingleSegment
    {
        public Coordinates Coordinates { get; }
        public bool IsHit { get; private set; }
    }

    public enum GridDirection
    {
        up = 0,
        down = 1,
        left = 2,
        right = 3
    }
    
    public class ShipSingleSegmentFactory
    {
        public ShipSingleSegment GetNextOrNull(ShipSingleSegment s, GridDirection direction)
        {

            var coordinates = direction switch
            {
                GridDirection.up => (s.Coordinates.RowIndex, s.Coordinates.ColumnIndex - 1),
                GridDirection.down => (s.Coordinates.RowIndex, s.Coordinates.ColumnIndex + 1),
                GridDirection.left => (s.Coordinates.RowIndex - 1, s.Coordinates.ColumnIndex),
                GridDirection.right => (s.Coordinates.RowIndex + 1, s.Coordinates.ColumnIndex)
            };
            throw new NotImplementedException();
        }
    }

    public class Ship
    {
        public List<ShipSingleSegment> Segments { get; }
        
        private Ship(List<ShipSingleSegment> segments)
        {
            Segments = segments;
        }

        public static Ship CreateShip(int shipLength, List<Ship> otherShips)
        {
            if (otherShips.Count != 0)
                throw new NotImplementedException();
            throw new NotImplementedException();
        }
    }
}