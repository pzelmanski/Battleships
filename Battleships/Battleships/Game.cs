using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
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

        private Ship GenerateShip(int length, List<Ship> currentShips)
        {
            var r = new Random();

            Ship? newShip = null;
            for (int i = 0; i < 700; i++)
            {
                var row = r.Next(1, 10);
                var column = r.Next(1, 10);
                var direction = (GridDirection) r.Next(1, 4);
                newShip = ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(row, column), direction, length, currentShips, _nextShipId);
                if (newShip is { })
                    break;
            }

            if (newShip is null)
                throw new InvalidOperationException("Unable to generate a ship");

            _nextShipId++;
            return newShip;
        }

        public HitStatus NextRound(Coordinates hitCoordinates)
        {
            var hits = Ships.Select(x => x.GetHitStatus(hitCoordinates)).Where(x => x != HitStatus.Miss).ToList();

            return !hits.Any() ? HitStatus.Miss : hits.Single();
        }

        public bool IsFinished() => Ships.All(x => x.IsShipSunk());
    }
}