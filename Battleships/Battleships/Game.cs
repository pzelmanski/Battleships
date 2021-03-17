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
                newShip = ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(row, column), direction, length, currentShips);
            }

            return newShip;
        }

        public void PrintBoard()
        {
            var allShipCoordinates = Ships.SelectMany(x => x.Segments.Select(y => (y.Coordinates, x.ShipId))).ToList();
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    var coords = allShipCoordinates.SingleOrDefault(x => x.Coordinates.Equals(Coordinates.CreateOrThrow(i, j)));
                    if(coords.Equals(default))
                        Console.Write("O ");
                    else
                        Console.Write($"{coords.ShipId} ");
                }
                Console.WriteLine();
            }
        }
    }
}