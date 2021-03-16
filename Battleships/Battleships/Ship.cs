using System;
using System.Collections.Generic;

namespace Battleships
{
    public class ShipSingleSegment
    {
        public Coordinates Coordinates { get; }
        public bool IsHit { get; private set; }
        
    }
    
    public class Ship
    {
        public List<ShipSingleSegment> Segments { get; }
        
        private Ship(List<ShipSingleSegment> segments)
        {
            Segments = segments;
        }

        public Ship CreateShip(int shipLength, List<Ship> otherShips)
        {
            throw new NotImplementedException();
        }
    }
}