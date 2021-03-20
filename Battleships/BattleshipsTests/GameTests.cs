using System;
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
            result.Ships.Count.Should().Be(3, result.ToString());
        }

        [Fact]
        public void GivenLengthsArray_WhenItsImpossibleToGenerateShips_ItShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() => new Game(new [] {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9}));
        }
        
        [Fact]
        public void GivenLengthsArray_ItShouldGenerateShipsWithUniqueIds()
        {
            var result = new Game(new[] {4, 4, 5});
            result.Ships.Select(x => x.ShipId).Distinct().Should().HaveCount(3, result.ToString());
        }
        
        [Fact]
        public void GivenAnotherLengthsArray_ItShouldGenerateCorrectAmountOfShips()
        {
            var result = new Game(new[] {1, 1, 1, 1, 1, 1, 1});
            result.Ships.Count.Should().Be(7);
        }

        // TODO: Its a perfect case for property - based testing
        [Fact]
        public void GivenLengthsArray_ShipsShouldNotCollide()
        {
            for (int i = 0; i < 100; i++)
            {
                var result = new Game(new[] {4, 4, 5});
                var r = result.Ships.SelectMany(x => x.Segments.Select(y => y.Coordinates)).ToList();
                r.Distinct().Count().Should().Be(r.Count, result.ToString());
            }
        }

        [Fact]
        public void GivenGameWithSingleShip_WhenHitAndSink_ItShouldReturnCorrectHitStatus()
        {
            // Arrange
            var game = new Game(new[] {2});
            var coordinates = game.Ships.Single().Segments.Select(x => x.Coordinates).ToList();

            // Act && Assert
            game.NextRound(coordinates[0]).Should().Be(HitStatus.Hit, game.ToString());
            game.IsFinished().Should().BeFalse(game.ToString());

            game.NextRound(coordinates[1]).Should().Be(HitStatus.Sink, game.ToString());
            game.IsFinished().Should().BeTrue(game.ToString());
        }

        [Fact]
        public void GivenGame_WhenGettingToString_ItShouldNotThrow()
        {
            var game = new Game(new[] {5, 6, 3});
            game.ToString().Should().NotBeEmpty();
        }
    }
}