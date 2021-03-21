using Battleships;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace BattleshipsTests
{
    public static class Int1To10Generator
    {
        public static Arbitrary<int> Generate() => Arb.Default.Int32().Filter(i => i >= 1 && i <= 10);
    }
    
    public static class IntGt10Generator
    {
        public static Arbitrary<int> Generate() => Arb.Default.Int32().Filter(i => i > 10);
    }

    public static class AtoJInputGenerator
    {
        public static Arbitrary<char> Generate() =>
            Arb.Default.Char().Filter(c => (c >= 'A' && c <= 'J') || (c >= 'a' && c <= 'j'));
    }

    public static class KtoZInputGenerator
    {
        public static Arbitrary<char> Generate() =>
            Arb.Default.Char().Filter(c => (c >= 'K' && c <= 'Z') || (c >= 'k' && c <= 'z'));
    }

    public class CoordinatesTestsPropertyBased
    {
        [Property(Arbitrary = new[] {typeof(Int1To10Generator), typeof(Int1To10Generator)})]
        public Property NotEmpty(int x, int y)
        {
            var c = Coordinates.CreateOrThrow(x, y);
            return (c.RowIndex == x && c.ColumnIndex == y).ToProperty();
        }

        [Fact]
        public void Test1()
        {
            // Prop.
        }
        
        [Property(Arbitrary = new[] {typeof(AtoJInputGenerator), typeof(Int1To10Generator)})]
        public Property CorrectUserInput(char col, int row)
        {
            var input = col.ToString() + row;
            var result = Coordinates.TryCreateFromUserInput(input);

            return (result is not null
                    && result.ColumnIndex == char.ToUpper(col) - 64
                    && result.RowIndex == row)
                .ToProperty();
        }
        
        [Property(Arbitrary = new[] {typeof(KtoZInputGenerator), typeof(Int1To10Generator)})]
        public Property GivenIncorrectColumnAndCorrectRow_ItShouldReturnNull(char col, int row)
        {
            var input = col.ToString() + row;
            var result = Coordinates.TryCreateFromUserInput(input);

            return (result is null).ToProperty();
        }
        
        [Property(Arbitrary = new[] {typeof(AtoJInputGenerator), typeof(IntGt10Generator)})]
        public Property GivenCorrectColumnAndIncorrectRow_ItShouldReturnNull(char col, int row)
        {
            var input = col.ToString() + row;
            var result = Coordinates.TryCreateFromUserInput(input);

            return (result is null).ToProperty();
        }
        
        [Property(Arbitrary = new[] {typeof(KtoZInputGenerator), typeof(IntGt10Generator)})]
        public Property GivenIncorrectColumnAndIncorrectRow_ItShouldReturnNull(char col, int row)
        {
            var input = col.ToString() + row;
            var result = Coordinates.TryCreateFromUserInput(input);

            return (result is null).ToProperty();
        }
    }
}