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
            Console.WriteLine(game.ToString());

            // game loop
            while (true)
            {
                var hitCoordinates = Coordinates.TryCreateFromUserInput(Console.ReadLine() ?? "");
                if (hitCoordinates is null)
                {
                    Console.WriteLine("Incorrect user input. Try again");
                    continue;
                }

                Console.WriteLine(game.NextRound(hitCoordinates).ToString());
                Console.WriteLine(game.ToString());
                
                if (game.IsFinished())
                    break;
            }

            Console.WriteLine("END");
            Console.ReadKey();
        }
    }
}