using System.Collections.Generic;
using System.Linq;
using Battleships;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class ShipFactoryTests
    {
        [Fact]
        public void GivenCorrectShipPosition_WhenGridDirectionHorizontal_ItShouldReturnShip()
        {
            var result = ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(1, 1), GridDirection.Right, 4,
                new List<Ship>(), 1);

            Assert.NotNull(result);
            result.Segments.Count.Should().Be(4);
            result.Segments.All(x => x.Coordinates.RowIndex == 1).Should().BeTrue();
            result.Segments.All(x => x.IsHit == false).Should().BeTrue();
            for (int i = 1; i < 4; i++)
            {
                result.Segments.Any(x => x.Coordinates.ColumnIndex == i).Should().BeTrue();
            }
        }

        [Fact]
        public void GivenCorrectShipPosition_WhenGridDirectionVertical_ItShouldReturnShip()
        {
            var result = ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(1, 1), GridDirection.Down, 4,
                new List<Ship>(), 1);

            Assert.NotNull(result);
            result.Segments.Count.Should().Be(4);
            result.Segments.All(x => x.Coordinates.ColumnIndex == 1).Should().BeTrue();
            result.Segments.All(x => x.IsHit == false).Should().BeTrue();
            for (int i = 1; i < 4; i++)
            {
                result.Segments.Any(x => x.Coordinates.RowIndex == i).Should().BeTrue();
            }
        }

        [Fact]
        public void GivenIncorrectShipPosition_ItShouldReturnNull()
        {
            var result =
                ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(1, 1), GridDirection.Up, 4, new List<Ship>(), 1);
            result.Should().BeNull();
        }

        [Fact]
        public void GivenCorrectShipPosition_WhenCollidingWithAnotherShip_ItShouldReturnNull()
        {
            var collidingShip = ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(2, 1), GridDirection.Right, 4,
                new List<Ship>(), 1);

            var result = ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(1, 1), GridDirection.Down, 4,
                new List<Ship> {collidingShip}, 1);

            result.Should().BeNull();
        }

        [Fact]
        public void GivenCorrectShipPosition_WhenNotCollidingWithAnotherShip_ItShouldReturnShip()
        {
            var collidingShip = ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(2, 1), GridDirection.Right, 4,
                new List<Ship>(), 1);

            var result = ShipFactory.TryCreateShip(Coordinates.CreateOrThrow(1, 1), GridDirection.Right, 4,
                new List<Ship> {collidingShip}, 1);

            result.Should().NotBeNull();
        }
    }
}