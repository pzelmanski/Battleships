using System;
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
            var result = new Game(new[] {4, 4, 5});
            result.Ships.Count.Should().Be(3, result.ToString());
        }

        [Fact]
        public void GivenLengthsArray_WhenItsImpossibleToGenerateShips_ItShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() => new Game(new[] {9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9}));
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
        public void GivenGameWithSingleShip_WhenMissHitAndSink_ItShouldReturnCorrectHitStatus()
        {
            // Arrange
            var game = new Game(new[] {2});
            var hitCoordinates = game.Ships.Single().Segments.Select(x => x.Coordinates).ToList();

            var missCoordinates = Coordinates.CreateOrThrow(1, 1);
            if (hitCoordinates.Contains(missCoordinates))
                missCoordinates = Coordinates.CreateOrThrow(5, 5);

            // Act && Assert
            game.NextRound(missCoordinates).Should().Be(HitStatus.Miss, game.ToString());
            game.IsFinished().Should().BeFalse(game.ToString());

            game.NextRound(hitCoordinates[0]).Should().Be(HitStatus.Hit, game.ToString());
            game.IsFinished().Should().BeFalse(game.ToString());

            game.NextRound(hitCoordinates[1]).Should().Be(HitStatus.Sink, game.ToString());
            game.IsFinished().Should().BeTrue(game.ToString());
        }

        [Fact]
        public void GivenGameWithThreeShips_WhenPlayingFromBeginningToEnd_ItShouldActProperly()
        {
            for (int i = 0; i < 100; i++)
            {
                // Arrange
                var game = new Game(new[] {4, 4, 5});

                var ship1Coordinates = game.Ships[0].Segments.Select(x => x.Coordinates).ToList();
                var ship2Coordinates = game.Ships[1].Segments.Select(x => x.Coordinates).ToList();
                var ship3Coordinates = game.Ships[2].Segments.Select(x => x.Coordinates).ToList();

                // Act & Assert
                HitAndAssertHitStatus(game, ship1Coordinates);
                HitAndAssertHitStatus(game, ship2Coordinates);
                HitAndAssertHitStatus(game, ship3Coordinates);
                game.IsFinished().Should().BeTrue();
            }

            static void HitAndAssertHitStatus(Game g, List<Coordinates> shipCoordinates)
            {
                for (var i = 0; i < shipCoordinates.Count; i++)
                {
                    var hitStatus = g.NextRound(shipCoordinates[i]);
                    var expectedHitStatus = i == shipCoordinates.Count - 1
                        ? HitStatus.Sink
                        : HitStatus.Hit;
                    hitStatus.Should().Be(expectedHitStatus,
                        $"Coordinates of hit: ({shipCoordinates[i].ColumnIndex}, {shipCoordinates[i].RowIndex})\n" + g);
                }
            }
        }

        [Fact]
        public void GivenGame_WhenGettingToString_ItShouldNotThrow()
        {
            var game = new Game(new[] {5, 6, 3});
            game.ToString().Should().NotBeEmpty();
        }
    }
}