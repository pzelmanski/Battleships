using System.Collections.Generic;
using Battleships;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class ShipCreationTests
    {
        [Fact]
        public void GivenShipLength_ItShouldCreate()
        {
            var result = Ship.CreateShip(4, new List<Ship>());
            result.Segments.Should().NotBeEmpty();
        }
    }
}