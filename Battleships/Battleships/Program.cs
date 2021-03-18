using System;
using System.Linq;

namespace Battleships
{
    /*
        The program should create a 10x10 grid, 
        and place several ships on the grid at random with the following sizes:
            1x Battleship (5 squares)
            2x Destroyers (4 squares)
        The player enters or selects coordinates of the form “A5”,
        where “A” is the column and “5” is the row, 
        to specify a square to target. 
        Shots result in hits, misses or sinks. 
        The game ends when all ships are sunk.
     */
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(new[] {4, 4, 5});
            PrintBoard(game);
            
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

                var hitStatus = game.NextRound(hitCoordinates);
                
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
        
        public static void PrintBoard(Game game)
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