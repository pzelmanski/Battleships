using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class ShipSingleSegment
    {
        public Coordinates Coordinates { get; }
        public bool IsHit { get; set; }

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

        private void _markSegmentAsHit(Coordinates hitCoordinates) => Segments.Single(x => x.Coordinates.Equals(hitCoordinates)).IsHit = true;

        private bool _areAllSegmentsHit() => Segments.All(x => x.IsHit);
    }
}