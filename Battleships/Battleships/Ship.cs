using System.Collections.Generic;

namespace Battleships
{
    public class ShipSingleSegment
    {
        public Coordinates Coordinates { get; }
        public bool IsHit { get; private set; }

        public ShipSingleSegment(Coordinates c)
        {
            Coordinates = c;
            IsHit = false;
        }
    }

    public class Ship
    {
        public List<ShipSingleSegment> Segments { get; }
        public int ShipId { get; }
        public Ship(List<ShipSingleSegment> segments, int shipId)
        {
            Segments = segments;
            ShipId = shipId;
        }
    }
}