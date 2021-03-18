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

        public static Coordinates? TryCreateFromUserInput(string inputValue)
        {
            if (inputValue.Length == 0)
                return null;

            var column = char.ToUpper(inputValue[0]) - 64;

            if (int.TryParse(inputValue.Substring(1), out var row) && IsWithinBounds(row) && IsWithinBounds(column))
                return new Coordinates(row, column);
            
            return null;
        }

        public static Coordinates CreateOrThrow(int row, int column)
        {
            if (IsWithinBounds(row) && IsWithinBounds(column))
                return new Coordinates(row, column);
            
            throw new InvalidOperationException($"Trying to create invalid coordinates (row, col): ({row}, {column})");
        }

        public Coordinates? TryCreateNext(GridDirection direction)
        {
            var next = direction switch
            {
                GridDirection.Up => (RowIndex - 1, ColumnIndex),
                GridDirection.Down => (RowIndex + 1, ColumnIndex),
                GridDirection.Left => (RowIndex, ColumnIndex - 1),
                GridDirection.Right => (RowIndex, ColumnIndex + 1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            
            if (IsWithinBounds(next.Item1) && IsWithinBounds(next.Item2))
                return new Coordinates(next.Item1, next.Item2);
            return null;
        }

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