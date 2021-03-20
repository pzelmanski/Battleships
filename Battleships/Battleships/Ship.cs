using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class Ship
    {
        public class SingleSegmentSingleSegment
        {
            public Coordinates Coordinates { get; }
            public bool IsHit { get; private set; }

            public SingleSegmentSingleSegment(Coordinates c)
            {
                Coordinates = c;
                IsHit = false;
            }

            public void HitSegment(Coordinates hitCoordinates)
            {
                if (!hitCoordinates.Equals(Coordinates))
                    throw new ArgumentException("Trying to mark wrong segment as hit");
                IsHit = true;
            }
        }
        public List<SingleSegmentSingleSegment> Segments { get; }
        public int ShipId { get; }
        public Ship(List<SingleSegmentSingleSegment> segments, int shipId)
        {
            Segments = segments;
            ShipId = shipId;
        }
        
        public static Ship? TryCreate(Coordinates initialPosition, GridDirection direction, int shipLength, List<Ship> otherShips, int shipIdsCounter)
        {
            var segments = new List<SingleSegmentSingleSegment>();

            var lastCoords = initialPosition;
            segments.Add(new SingleSegmentSingleSegment(lastCoords));
            
            for (int i = 0; i < shipLength - 1; i++)
            {
                var newCoords = lastCoords.TryCreateNext(direction);
                if (newCoords == null)
                    return null;
                lastCoords = newCoords;
                segments.Add(new SingleSegmentSingleSegment(lastCoords));
            }

            var isColliding = segments
                .Select(x => x.Coordinates)
                .Any(s => otherShips
                    .Any(x => x.Segments
                        .Any(y => y.Coordinates.Equals(s))));

            return isColliding 
                ? null 
                : new Ship(segments, shipIdsCounter);
        }

        public HitStatus GetHitStatus(Coordinates hitCoordinates)
        {
            if (!_anySegmentHit(hitCoordinates))
                return HitStatus.Miss;
            
            _markSegmentAsHit(hitCoordinates);
            
            return _areAllSegmentsHit()
                ? HitStatus.Sink
                : HitStatus.Hit;
        }

        public bool IsShipSunk() => _areAllSegmentsHit();
        
        private bool _anySegmentHit(Coordinates hitCoordinates) => Segments.Any(x => x.Coordinates.Equals(hitCoordinates));

        private void _markSegmentAsHit(Coordinates hitCoordinates) => Segments.Single(x => x.Coordinates.Equals(hitCoordinates)).HitSegment(hitCoordinates);

        private bool _areAllSegmentsHit() => Segments.All(x => x.IsHit);
    }
}