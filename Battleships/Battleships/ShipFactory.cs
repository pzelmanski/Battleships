using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public static class ShipFactory
    {
        public static Ship? TryCreateShip(Coordinates initialPosition, GridDirection direction, int shipLength, List<Ship> otherShips, int shipIdsCounter)
        {
            var segments = new List<ShipSingleSegment>();

            var lastCoords = initialPosition;
            segments.Add(new ShipSingleSegment(lastCoords));
            
            for (int i = 0; i < shipLength - 1; i++)
            {
                var newCoords = lastCoords.TryCreateNext(direction);
                if (newCoords == null)
                    return null;
                lastCoords = newCoords;
                segments.Add(new ShipSingleSegment(lastCoords));
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
    }
}