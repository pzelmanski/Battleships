using System;

namespace Battleships
{
    public class Coordinates
    {
        public int RowIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        
        private Coordinates(int rowIndex, int columnIndex)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }
        
        public static Coordinates? TryCreate(string inputValue)
        {
            var columnIndex = char.ToUpper(inputValue[0]) - 64;
            
            if(Int32.TryParse(inputValue.Substring(1), out var rowIndex))
                if (rowIndex < 11 && rowIndex > 0 && columnIndex < 11 && columnIndex > 0)
                    return new Coordinates(rowIndex, columnIndex);
            return null;
        }
    }
    
    public class Input
    {
        // public Coordinates TryGet(string inputValue)
        // {
        //     
        // }
    }
}