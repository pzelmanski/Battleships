using Battleships;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;

namespace BattleshipsTests
{
    public static class UserInputGenerator
    {
        public static Arbitrary<int> Generate()
        {
            return Arb.Default.Int32().Filter(i => i >= 1 && i <= 10);
        }
    }
    public class CoordinatesTests_PropertyBased
    {
        
        [Property(Arbitrary = new[] { typeof(UserInputGenerator) })]
        public Property NotEmpty(int i)
        {
            var c = Coordinates.CreateOrThrow(i, 1);
            return (c.RowIndex == i && c.ColumnIndex == 1).ToProperty();
        }
    }
}