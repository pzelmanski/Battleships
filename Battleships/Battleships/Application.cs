using System;
using System.Linq;

namespace Battleships
{
    class Application
    {
        private readonly Game _game;

        public Application(Game game)
        {
            _game = game;
        }

        public void Run()
        {
            _game.Init(new[] {4, 4, 5});
            PrintBoard(_game);
            
            // game loop
            while (true)
            {
                var input = Console.ReadLine();
                var hitCoordinates = Coordinates.TryCreateFromInput(input ?? "");
                if (hitCoordinates is null)
                {
                    Console.WriteLine("Incorrect user input. Try again");
                    continue;
                }

                var hitStatus = _game.NextRound(hitCoordinates);
                
                switch(hitStatus)
                {
                    case HitStatus.Miss:
                        Console.WriteLine("Miss!");
                        break;
                    case HitStatus.Hit:
                        Console.WriteLine("Hit!");
                        break;
                    case HitStatus.Sink:
                        Console.WriteLine("Sink!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            
            Console.ReadKey();
        }

        private static void PrintBoard(Game game)
        {
            var allShipCoordinates = game.Ships.SelectMany(x => x.Segments.Select(y => (y.Coordinates, x.ShipId))).ToList();
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