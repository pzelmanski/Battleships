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
            var result = new Game(new[] {4, 4, 5});
            result.Ships.Count.Should().Be(3);
        }
        
        [Fact]
        public void GivenLengthsArray_ItShouldGenerateShipsWithUniqueIds()
        {
            var result = new Game(new[] {4, 4, 5});
            result.Ships.Select(x => x.ShipId).Distinct().Should().HaveCount(3);
        }
        
        [Fact]
        public void GivenAnotherLengthsArray_ItShouldGenerateCorrectAmountOfShips()
        {
            var result = new Game(new[] {1, 1, 1, 1, 1, 1, 1});
            result.Ships.Count.Should().Be(7);
        }

        // TODO: Its a flaky test
        [Fact]
        public void GivenLengthsArray_ShipsShouldNotCollide()
        {
            var result = new Game(new[] {4, 4, 5});
            var r = result.Ships.SelectMany(x => x.Segments.Select(y => y.Coordinates)).ToList();
            r.Distinct().Count().Should().Be(r.Count());
        }
    }
}