using System;
using System.Collections.Generic;
using Battleships;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class ShipHitTests
    {
        [Fact]
        public void GivenShip_WhenGettingHits_ItShouldReturnCorrectHitStatus()
        {
            // Arrange
            var shipSegments = new List<Ship.SingleSegmentSingleSegment>
            {
                new(Coordinates.CreateOrThrow(1, 1)),
                new(Coordinates.CreateOrThrow(1, 2))
            };
            var ship = new Ship(shipSegments, 1);
            var missCoordinates = Coordinates.CreateOrThrow(2, 2);
            var hitCoordinates = Coordinates.CreateOrThrow(1, 1);
            var sinkCoordinates = Coordinates.CreateOrThrow(1, 2);

            // Act
            var miss = ship.GetHitStatus(missCoordinates);
            var hit = ship.GetHitStatus(hitCoordinates);
            var sink = ship.GetHitStatus(sinkCoordinates);

            // Assert
            miss.Should().Be(HitStatus.Miss);
            hit.Should().Be(HitStatus.Hit);
            sink.Should().Be(HitStatus.Sink);

            ship.IsShipSunk().Should().BeTrue();
        }

        [Fact]
        public void GivenSegment_WhenTryingToHitWithDifferentCoordinates_ItShouldThrow()
        {
            var segment = new Ship.SingleSegmentSingleSegment(Coordinates.CreateOrThrow(1, 1));
            var hitCoordinates = Coordinates.CreateOrThrow(2, 2);

            Assert.Throws<ArgumentException>(() => segment.HitSegment(hitCoordinates));
        }
    }
}