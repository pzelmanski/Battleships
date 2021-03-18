using System.Collections.Generic;
using System.Linq;
using Battleships;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class GameTests
    {
        [Fact]
        public void GivenLengthsArray_ItShouldGenerateCorrectAmountOfShips()
        {
            var game = new Game(new ShipFactory());
            game.Init(new[] {4, 4, 5});
            game.Ships.Count.Should().Be(3, game.GameDetailsToString());
        }
        
        [Fact]
        public void GivenLengthsArray_ItShouldGenerateShipsWithUniqueIds()
        {
            var game = new Game(new ShipFactory());
            game.Init(new[] {4, 4, 5});
            game.Ships.Select(x => x.ShipId).Distinct().Should().HaveCount(3, game.GameDetailsToString());
        }
        
        [Fact]
        public void GivenAnotherLengthsArray_ItShouldGenerateCorrectAmountOfShips()
        {
            var game = new Game(new ShipFactory());
             game.Init(new[] {1, 1, 1, 1, 1, 1, 1});
            game.Ships.Count.Should().Be(7);
        }

        // TODO: Its a perfect case for property - based testing
        [Fact]
        public void GivenLengthsArray_ShipsShouldNotCollide()
        {
            for (int i = 0; i < 100; i++)
            {
                var game = new Game(new ShipFactory());
                game.Init(new[] {4, 4, 5});
                var r = game.Ships.SelectMany(x => x.Segments.Select(y => y.Coordinates)).ToList();
                r.Distinct().Count().Should().Be(r.Count(), game.GameDetailsToString());
            }
        }
    }

    public class TestShipsFactory : IShipFactory
    {
        private readonly List<Ship?> _toReturn;
        private int _returnCounter = 0;
        public TestShipsFactory(List<Ship?> toReturn)
        {
            _toReturn = toReturn;
        }
        
        public Ship? TryCreateShip(Coordinates initialPosition, GridDirection direction, int shipLength, List<Ship> otherShips,
            int shipIdsCounter)
        {
            var returned = _toReturn[_returnCounter];
            _returnCounter++;
            return returned;
        }
    }

    public static class GameTestsExtensions
    {
        public static string GameDetailsToString(this Game g)
        {
            var result = "\n";
            var allShipCoordinates = g.Ships.SelectMany(x => x.Segments.Select(y => (y.Coordinates, x.ShipId))).ToList();
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    var coords = allShipCoordinates.SingleOrDefault(x => x.Coordinates.Equals(Coordinates.CreateOrThrow(i, j)));
                    if(coords.Equals(default))
                        result += "O ";
                    else
                        result += $"{coords.ShipId} ";
                }

                result += "\n";
            }

            return result;
        }
    }
}