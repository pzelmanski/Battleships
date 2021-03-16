using System;

namespace Battleships
{
    public class Coordinates : IEquatable<Coordinates>
    {
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        
        private Coordinates(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }
        
        public static Coordinates? TryCreateFromInput(string inputValue)
        {
            var column = char.ToUpper(inputValue[0]) - 64;
            
            if(int.TryParse(inputValue.Substring(1), out var row))
                if (IsWithinBounds(row) && IsWithinBounds(column))
                    return new Coordinates(row, column);
            return null;
        }

        public static Coordinates CreateOrThrow(int row, int column)
        {
            if(IsWithinBounds(row) && IsWithinBounds(column))
                return new Coordinates(row, column);
            throw new InvalidOperationException($"Trying to create invalid coordinates (row, col): ({row}, {column})");
        }

        public Coordinates? TryCreateNext(GridDirection direction)
        {
            var c = direction switch
            {
                GridDirection.up => (RowIndex - 1, ColumnIndex),
                GridDirection.down => (RowIndex + 1, ColumnIndex),
                GridDirection.left => (RowIndex, ColumnIndex - 1),
                GridDirection.right => (RowIndex, ColumnIndex + 1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            if (IsWithinBounds(c.Item1) && IsWithinBounds(c.Item2))
                return new Coordinates(c.Item1, c.Item2);
            return null;
        }
        
        public static bool AreCoordinatesWithinBounds(int row, int column) => IsWithinBounds(row) && IsWithinBounds(column);

        private static bool IsWithinBounds(int rowOrColumn) => rowOrColumn < 11 && rowOrColumn > 0;

        public bool Equals(Coordinates? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return RowIndex == other.RowIndex && ColumnIndex == other.ColumnIndex;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coordinates) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowIndex, ColumnIndex);
        }
    }
}