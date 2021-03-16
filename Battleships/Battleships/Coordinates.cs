using System;

namespace Battleships
{
    public class Coordinates
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
                if (IsSingleValueValid(row) && IsSingleValueValid(column))
                    return new Coordinates(row, column);
            return null;
        }

        public static Coordinates CreateOrThrow(int row, int column)
        {
            if(IsSingleValueValid(row) && IsSingleValueValid(column))
                return new Coordinates(row, column);
            throw new InvalidOperationException($"Trying to create invalid coordinates (row, col): ({row}, {column})");
        }

        public Coordinates? TryCreateNext(GridDirection direction)
        {
            var c = direction switch
            {
                GridDirection.up => (RowIndex, ColumnIndex - 1),
                GridDirection.down => (RowIndex, ColumnIndex + 1),
                GridDirection.left => (RowIndex - 1, ColumnIndex),
                GridDirection.right => (RowIndex + 1, ColumnIndex),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            if (IsSingleValueValid(c.Item1) && IsSingleValueValid(c.Item2))
                return new Coordinates(c.Item1, c.Item2);
            return null;
        }
        
        public static bool AreCoordinatesValid(int row, int column) => IsSingleValueValid(row) && IsSingleValueValid(column);

        private static bool IsSingleValueValid(int value) => value < 11 && value > 0;
    }
}