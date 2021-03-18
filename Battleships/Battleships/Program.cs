using Autofac;

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
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ShipFactory>().As<IShipFactory>();

            return builder.Build();
        }
        
        static void Main(string[] args)
        {
            CompositionRoot().Resolve<Application>().Run();
        }
    }
}