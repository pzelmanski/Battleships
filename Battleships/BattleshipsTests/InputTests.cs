using System;
using Battleships;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class InputTests
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
            var result = Coordinates.TryCreate(input);
            Assert.NotNull(result);
            result.ColumnNumber.Should().Be(column);
            result.RowNumber.Should().Be(row);
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
            Assert.Throws<ArgumentException>(() => Coordinates.TryCreate(input));
        }
    }
}