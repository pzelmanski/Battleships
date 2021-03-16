using System;
using System.Collections.Generic;

namespace Battleships
{
    public class ShipCoordinates
    {
        public Coordinates StartCoordinates { get; }
        public Coordinates EndCoordinates { get; }
        private ShipCoordinates(Coordinates startCoordinates, Coordinates endCoordinates)
        {
            StartCoordinates = startCoordinates;
            EndCoordinates = endCoordinates;
        }

        public ShipCoordinates CreateCoordinates(int shipLength, List<ShipCoordinates> otherShipCoordinates)
        {
            throw new NotImplementedException();
        }
    }
    
    
    public class Ship
    {
        private ShipCoordinates Coordinates { get; }
        public HitStatus GetHitStatus(Coordinates c)
        {
            return HitStatus.Miss;
        }
    }
}