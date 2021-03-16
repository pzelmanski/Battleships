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

        private static bool IsSingleValueValid(int value)
        {
            return value < 11 && value > 0;
        }
    }
}