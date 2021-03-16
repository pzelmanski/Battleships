using System;
using Battleships;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class ShipRowCoordinatesTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 10)]
        [InlineData(10, 1)]
        [InlineData(5, 5)]
        public void GivenCorrectRowAndColumn_ItShouldReturnCoordinates(int row, int column)
        {
            var result = Coordinates.CreateOrThrow(row, column);
            result.RowIndex.Should().Be(row);
            result.ColumnIndex.Should().Be(column);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(11, 1)]
        [InlineData(1, 11)]
        public void GivenIncorrectRowOrColumn_ItShouldThrow(int row, int column)
        {
            Assert.Throws<InvalidOperationException>(() => Coordinates.CreateOrThrow(row, column));
        }
    }
}