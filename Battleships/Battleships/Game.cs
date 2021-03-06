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

    public enum HitStatus
    {
        Miss = 0,
        Hit = 1,
        Sink = 2
    }

    public class Game
    {
        public List<Ship> Ships { get; } = new();
        private int _nextShipId = 1;

        public Game(IEnumerable<int> shipLengths)
        {
            foreach (var l in shipLengths) 
                Ships.Add(GenerateShip(l, Ships));
        }

        public HitStatus NextRound(Coordinates hitCoordinates)
        {
            var hits = Ships
                .Select(x => x.HitAndGetStatus(hitCoordinates))
                .Where(x => x != HitStatus.Miss)
                .ToList();

            return !hits.Any() ? HitStatus.Miss : hits.Single();
        }

        public bool IsFinished() => Ships.All(x => x.IsShipSunk());

        private Ship GenerateShip(int length, List<Ship> currentShips)
        {
            var r = new Random();

            Ship? newShip = null;
            for (int i = 0; i < 700; i++)
            {
                var row = r.Next(1, 10);
                var column = r.Next(1, 10);
                var direction = (GridDirection) r.Next(1, 4);
                newShip = Ship.TryCreate(Coordinates.CreateOrThrow(row, column), direction, length, currentShips, _nextShipId);
                if (newShip is { })
                    break;
            }

            if (newShip is null)
                throw new InvalidOperationException("Unable to generate a ship");

            _nextShipId++;
            return newShip;
        }

        public override string ToString()
        {
            var result = "  A B C D E F G H I J \n1 ";
            var rowSign = new[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "X"};
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    var isSegment = false;
                    foreach (var ship in Ships)
                    {
                        var segmentHit =
                            ship.Segments.SingleOrDefault(x => x.Coordinates.Equals(Coordinates.CreateOrThrow(i, j)));
                        if (segmentHit is null)
                            continue;
                        isSegment = true;
                        var symbol = segmentHit.IsHit ? "X" : ship.ShipId.ToString();
                        result += $"{symbol} ";
                    }

                    if (!isSegment)
                        result += "O ";
                }

                result += "\n";
                result += i == 10 ? "" : $"{rowSign[i]} ";
            }

            return result;
        }
    }
}