using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperPracticalTest
{
    /// <summary>
    /// Helps printing data in the form a table
    /// </summary>
    class TableUtil
    {
        // Default Table Width
        const int TABLE_WIDTH = 75;
        
        public int TableWidth { get; set; }

        /// <summary>
        /// Sets the default table width
        /// </summary>
        public TableUtil ()
        {
            TableWidth = TABLE_WIDTH;
        }

        /// <summary>
        /// Sets custom table width
        /// </summary>
        /// <param name="tableWidth">Table Width as an integer</param>
        public TableUtil(int tableWidth)
        {
            TableWidth = tableWidth;
        }

        /// <summary>
        /// Prints table structure
        /// </summary>
        public void PrintLine()
        {
            Console.WriteLine(new string('-', TableWidth));
        }

        /// <summary>
        /// Prints row of a table
        /// </summary>
        /// <param name="columns">Array of strings which needs to displayed in that row</param>
        public void PrintRow(params string[] columns)
        {
            int width = (TableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        /// <summary>
        /// Aligns the text to center of the cell
        /// </summary>
        /// <param name="text">Text that needs to be displayed</param>
        /// <param name="width">Width of the table</param>
        /// <returns>formatted row as a string</returns>
        public string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
