using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public enum GridDirection
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    public interface IShipFactory
    {
        Ship? TryCreateShip(Coordinates initialPosition, GridDirection direction, int shipLength,
            List<Ship> otherShips, int shipIdsCounter);
    }

    public class ShipFactory : IShipFactory
    {
        public Ship? TryCreateShip(Coordinates initialPosition, GridDirection direction, int shipLength, List<Ship> otherShips, int shipIdsCounter)
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

            if (isColliding)
                return null;
            
            return new Ship(segments, shipIdsCounter);
        }
    }
}