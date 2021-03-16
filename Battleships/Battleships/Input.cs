using System;

namespace Battleships
{
    public class Coordinates
    {
        public int RowNumber { get; private set; }
        public int ColumnNumber { get; private set; }
        
        private Coordinates(int rowNumber, int columnNumber)
        {
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
        }
        
        public static Coordinates TryCreate(string inputValue)
        {
            var columnIndex = char.ToUpper(inputValue[0]) - 64;

            if (columnIndex < 1 || columnIndex > 10) 
                throw new ArgumentException($"Column out of index. Input: {inputValue}");
            if (inputValue.Length == 2)
            {
                if (Int32.TryParse(inputValue[1].ToString(), out var rowIndex))
                    if (rowIndex == 0)
                        throw new ArgumentException("Row number is zero");
                    else
                        return new Coordinates(rowIndex, columnIndex);
                throw new ArgumentException($"There was a problem with parsing row. Input: {inputValue}");
            }

            if (inputValue.Length == 3)
            {
                if (Int32.TryParse(inputValue.Substring(1), out var rowIndex))
                    if(rowIndex == 10)
                    {
                        return new Coordinates(rowIndex, columnIndex);
                    }
                    else
                    {
                        throw new ArgumentException($"Incorrect row index. Input: {inputValue}");
                    }
                throw new ArgumentException($"There was a problem with parsing row. Input: {inputValue}");
            }    
                
            throw new ArgumentException($"Incorrect input length. Input: {inputValue}");
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