using Project_Input.Project_Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Input
{
    /// <summary>
    /// This class reads stock data from a CSV file and loads
    /// it into a List of aCandlestick objects, each representing a candlestick data entry.
    /// </summary>
    public class ACandlestickLoader
    {
        /// <summary>
        /// Reads candlestick data from a specified CSV file and returns a list of aCandlestick objects.
        /// </summary>
        /// <param name="filePath">Path to the CSV file containing candlestick data.</param>
        /// <returns>List of aCandlestick objects populated with data from the CSV file.</returns>
        static public List<SmartCandlestick> LoadFromCsv(string filePath)
        {
            // List to store each candlestick entry from the CSV file
            var candlesticks = new List<SmartCandlestick>();

            // try catch block for reading the file
            try
            {
                // Declaring a streamREader object to read the csv file
                using (var reader = new StreamReader(filePath))
                {
                    // Read the header line (assumes there's a header)
                    reader.ReadLine();

                    //Setting delimeters
                    char[] delimiters = {',', '\\', '"'};

                    // Read each line from the file until end-of-file
                    while (!reader.EndOfStream)
                    {
                        // reading the line
                        var line = reader.ReadLine();
                        // if the line is not empty or null
                        if (!string.IsNullOrEmpty(line))
                        {
                            // Split the line into an array of values based on delimiters
                            var values = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                            // Validate column count; expects exactly 6 columns
                            // Assuming the CSV has 6 columns: Time, Open, High, Low, Close, Volume
                            if (values.Length != 6)     
                                throw new FormatException("Unexpected Number of Columns in CSV file");

                            // Populate aCandlestick object with parsed values, rounding decimal fields to two decimal places
                            var candlestick = new SmartCandlestick
                            {
                                // Parse the date string from the first column (values[0]) into a DateTime object, using "yyyy-MM-dd" format
                                Date = DateTime.ParseExact(values[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                // Parse the open price from the second column, convert it to decimal, and round it to two decimal places
                                Open = Math.Round(100 * decimal.Parse(values[1], CultureInfo.InvariantCulture))/100,
                                // Parse the high price from the third column, convert it to decimal, and round it to two decimal places
                                High = Math.Round(100 * decimal.Parse(values[2], CultureInfo.InvariantCulture)) / 100,
                                // Parse the low price from the fourth column, convert it to decimal, and round it to two decimal places
                                Low = Math.Round(100 * decimal.Parse(values[3], CultureInfo.InvariantCulture))/100,
                                // Parse the close price from the fifth column, convert it to decimal, and round it to two decimal places
                                Close = Math.Round(100 * decimal.Parse(values[4], CultureInfo.InvariantCulture))/ 100,
                                // Parse the volume from the sixth column and convert it to an unsigned long integer
                                Volume = ulong.Parse(values[5], CultureInfo.InvariantCulture)
                            };

                            // Add the parsed candlestick data to the list
                            candlesticks.Add(candlestick);
              
                        }
                    }
                }
            }
            // catching exception
            catch (Exception ex)
            {
                // Display an error message if any issues arise during file reading or parsing
                MessageBox.Show($"An error occurred while loading candlesticks: {ex.Message}");
            }
            // Return the populated list of candlestick objects
            return candlesticks;
        }
    }
}
