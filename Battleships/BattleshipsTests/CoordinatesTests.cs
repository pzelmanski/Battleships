using System;
using Battleships;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class CoordinatesTests
    {
        [Theory]
        [InlineData("A1", 1, 1)]
        [InlineData("a1", 1, 1)]
        [InlineData("B1", 2, 1)]
        [InlineData("A2", 1, 2)]
        [InlineData("J1", 10, 1)]
        [InlineData("A10", 1, 10)]
        [InlineData("J10", 10, 10)]
        public void GivenCorrectInput_ItShouldReturnCoordinates(string input, int column, int row)
        {
            var result = Coordinates.TryCreateFromInput(input);
            Assert.NotNull(result);
            result.ColumnIndex.Should().Be(column);
            result.RowIndex.Should().Be(row);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("A")]
        [InlineData("10")]
        [InlineData("AA1")]
        [InlineData("A11")]
        [InlineData("A20")]
        [InlineData("K1")]
        [InlineData("A0")]
        [InlineData("0A")]
        [InlineData("10A")]
        [InlineData("AA")]
        [InlineData("110")]
        public void GivenIncorrectInput_ItShouldReturnNull(string input)
        {
            Coordinates.TryCreateFromInput(input).Should().BeNull();
        }
        
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

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(1, 0, false)]
        [InlineData(0, 1, false)]
        [InlineData(1, 1, true)]
        [InlineData(10, 10, true)]
        [InlineData(10, 11, false)]
        [InlineData(11, 10, false)]
        public void GivenRowAndColumn_ItShouldValidateValues(int row, int column, bool expected)
        {
            Coordinates.AreCoordinatesValid(row, column).Should().Be(expected);
        }

        [Theory]
        [InlineData(2, 2, GridDirection.up)]
        [InlineData(2, 2, GridDirection.left)]
        [InlineData(9, 9, GridDirection.down)]
        [InlineData(9, 9, GridDirection.right)]
        public void GivenCoordinatesAndDirection_WhenNextCoordinateValid_ItShouldReturnCoordinate(int row, int column,
            GridDirection direction)
        {
            var coordinate = Coordinates.CreateOrThrow(row, column);
            var result = coordinate.TryCreateNext(direction);
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData(2, 2, GridDirection.up, 1, 2)]
        [InlineData(2, 2, GridDirection.left, 2, 1)]
        [InlineData(9, 9, GridDirection.down, 10, 9)]
        [InlineData(9, 9, GridDirection.right, 9, 10)]
        public void GivenCoordinatesAndDirection_WhenNextCoordinateValid_NewCoordinateValuesShouldRespectDirection(
            int beforeRow, int beforeColumn, GridDirection direction, int afterRow, int afterColumn)
        {
            var coordinateBefore = Coordinates.CreateOrThrow(beforeRow, beforeColumn);
            var expectedCoordinate = Coordinates.CreateOrThrow(afterRow, afterColumn);

            var result = coordinateBefore.TryCreateNext(direction);
            result.Should().Be(expectedCoordinate);
        }

        [Theory]
        [InlineData(1, 2, GridDirection.up)]
        [InlineData(2, 1, GridDirection.left)]
        [InlineData(10, 9, GridDirection.down)]
        [InlineData(9, 10, GridDirection.right)]
        public void GivenCoordinatesAndDirection_WhenNextCoordinateInvalid_ShouldReturnNull(
            int beforeRow, int beforeColumn, GridDirection direction)
        {
            var coordinateBefore = Coordinates.CreateOrThrow(beforeRow, beforeColumn);

            var result = coordinateBefore.TryCreateNext(direction);
            result.Should().BeNull();
        }

        [Fact]
        public void GivenTwoCoordinates_WhenRowAndColumnEquals_EqualsShouldBeTrue()
        {
            var firstCoordinate = Coordinates.CreateOrThrow(3, 7);
            var secondCoordinate = Coordinates.CreateOrThrow(3, 7);
            firstCoordinate.Should().Be(secondCoordinate);
        }
        
        [Fact]
        public void GivenTwoCoordinates_WhenRowAndColumnDoesNotEquals_EqualsShouldBeTrue()
        {
            var firstCoordinate = Coordinates.CreateOrThrow(3, 7);
            var secondCoordinate = Coordinates.CreateOrThrow(7, 3);
            firstCoordinate.Should().NotBe(secondCoordinate);
        }
    }
}