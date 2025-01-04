using Project_Input.Project_Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project_Input
{
    // Represents a wave, which is a series of candlesticks, along with its properties and behavior. 
    public class Wave
    {
        // The lowest price in the wave
        public decimal LowestPrice;

        // The highest price in the wave
        public decimal HighestPrice;

        // The starting index of the wave (in the list of candlesticks)
        public int StartIndex;

        // The ending index of the wave (in the list of candlesticks)
        public int EndIndex;

        // A private list to store confirmation points as tuples of (index, price)
        private List<(decimal, decimal)> _confirmation_points;

        // Property to determine if the wave is upward (starts with a valley candlestick)
        public bool IsUpward { get { return WaveCandlesticks[0].IsValley; } }

        // Property to determine if the wave is downward (starts with a peak candlestick)
        public bool IsDownward { get { return WaveCandlesticks[0].IsPeak; } }

        // The price range of the wave (difference between the highest and lowest price)
        public decimal Range { get { return HighestPrice - LowestPrice; } }

        // Property to generate and retrieve Fibonacci levels for the wave
        public List<decimal> Fibonacci_lvl { get { return GenerateFibonacci(); } }

        // Property to retrieve confirmation points (list of hits of candlesticks points with the fibonacci price levels)
        public List<(decimal, decimal)> Confirmation_Points { get { return _confirmation_points; } set { } }

        // List of candlesticks that make up the wave
        public List<SmartCandlestick> WaveCandlesticks { get; set; }

        // Calculated "beauty" of the wave, based on its candlesticks and Fibonacci alignment
        public int WaveBeauty { get { return CalculateBeauty(); } set { } }

        // Threshold used for Fibonacci calculations (accuracy percentage)
        public decimal fibonacci_threshold { get; set; }

        // Default contructor
        public Wave() { }

        // Constructor that initializes a wave with a list of candlesticks, lowest price, and highest price
        public Wave(List<SmartCandlestick> waveCandlesticks, decimal lowestPrice, decimal highestPrice) 
        {
            // Assign the selected candlesticks to the wave
            WaveCandlesticks = waveCandlesticks;

            // Determine the first and last candlesticks for better analisis
            var first_selection = WaveCandlesticks.First();
            var second_selection = WaveCandlesticks.Last();

            // Determine bounds
            LowestPrice = lowestPrice;
            HighestPrice = highestPrice;
            StartIndex = WaveCandlesticks.First().Index + 1; // Adjusted to 1-based indexing
            EndIndex = WaveCandlesticks.Last().Index + 1;   // Adjusted to 1-based indexing

            // Setting the Threshold to 3% as default (1.5% up and 1.5% down the fibonacci level for confirmation matching)
            fibonacci_threshold = 3;
        }

        // Private method to generate Fibonacci levels based on the wave's range
        private List<decimal> GenerateFibonacci() {
            
            // we will be adding them
            List<decimal> _fibonacci = new List<decimal>(); 
            decimal range = Range; 

            // Add Fibonacci levels (including 0% and 100%)
            _fibonacci.Add(HighestPrice);  // Highest price
            _fibonacci.Add(HighestPrice - (range * (decimal)23.6) / 100); //23.6%
            _fibonacci.Add(HighestPrice - (range * (decimal)38.2) / 100); //38.2%
            _fibonacci.Add(HighestPrice - (range * (decimal)50) / 100);   //50%
            _fibonacci.Add(HighestPrice - (range * (decimal)62.8) / 100); //62.8%
            _fibonacci.Add(HighestPrice - (range * (decimal)76.4) / 100); //76.4%
            _fibonacci.Add(HighestPrice - range); // Lowest price

            return _fibonacci;   // returns the list of fibonacci levels
        }

        // Private method to calculate the "beauty" of the wave based on candlestick alignment with Fibonacci levels
        private int CalculateBeauty() 
        {
            int _waveBeauty = 0; // Initialize the Beauty of the Wave
            // Half threshold for half up, half down to yield the full thrshold for each fibonacci level
            decimal threshold_halved = ((HighestPrice - LowestPrice) * (fibonacci_threshold / 100) / 2);
            _confirmation_points = new List<(decimal, decimal)>();  // Initialize confirmation points list

            // Iterate though each candlestick in the wave
            for (int i = 0; i < WaveCandlesticks.Count(); i++) 
            {
                var candlestick = WaveCandlesticks[i]; 
                int candlestickBeauty = 0;     // initialize the beauty for the current candlestick

                // List of critical points on the candlestick to compare (high, low, open, close)
                var criticalPoints = new List<decimal> { candlestick.High, candlestick.Open, candlestick.Low, candlestick.Close };

                // Check each critical point against the Fibonacci levels
                foreach (var point in criticalPoints)
                {
                    // For each Level
                    foreach (var fibLevel in Fibonacci_lvl) 
                    {
                        // Define the loweer bound and upper bound based on the threshold 
                        decimal lowerBound = fibLevel - threshold_halved;
                        decimal upperBound = fibLevel + threshold_halved;

                        // If the point falls within the Fibonacci level range
                        if (point >= lowerBound && point <= upperBound) 
                        {
                            candlestickBeauty++;    // Increment candlestick beauty score
                            _confirmation_points.Add((candlestick.Index, point));    // Save index and confirmation price
                            break;  // Avoid multiple confirmations for the same point
                        }
                    }
                }
                // Update the Beauty of the Candlestick 
                candlestick.Beauty = candlestickBeauty;
                // Update the Beauty of the Wave
                _waveBeauty += candlestickBeauty;
                Confirmation_Points = _confirmation_points; // Update confirmation points
            }
            return _waveBeauty; // Return the total beauty score of the wave
        }
    }
}
