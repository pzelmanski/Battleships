using System;
using System.Collections.Generic;

namespace Battleships
{
    public enum HitStatus
    {
        Miss = 0,
        Hit = 1,
        Sink = 2
    }
    
    public class Ship
    {
        public ShipCoordinates StartCoordinates { get; }
        public ShipCoordinates EndCoordinates { get; }

        public HitStatus GetHitStatus(Coordinates c)
        {
            return HitStatus.Miss;
        }
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