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
        public List<Ship> Ships { get; }
        private int _shipIdsCounter = 1;

        public Game(IEnumerable<int> shipLengths)
        {
            var ships = new List<Ship>();
            foreach (var l in shipLengths)
            {
                ships.Add(GenerateShip(l, ships));
            }
            Ships = ships;
        }

        private Ship GenerateShip(int length, List<Ship> currentShips)
        {
            var r = new Random();

            Ship? newShip = null;
            while (newShip is null)
            {
                var row = r.Next(1, 10);
                var column = r.Next(1, 10);
                var direction = (GridDirection) r.Next(1, 4);
                newShip = ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(row, column), direction, length, currentShips, _shipIdsCounter);
            }

            _shipIdsCounter++;
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