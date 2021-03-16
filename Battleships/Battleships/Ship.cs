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
    
    public class ShipFactory
    {
        public ShipSingleSegment GetNextOrNull(ShipSingleSegment s, GridDirection direction)
        {
            // get initial coordinates
            // try to generate N next segments, where N is ship length
            // if out of bounds of grid, try again
            // if collision with existing ships, try again
            // return ship
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