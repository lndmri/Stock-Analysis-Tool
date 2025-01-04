using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Input
{
    /// <summary>
    /// Represents a single candlestick entry in stock data, containing price and volume details
    /// for a specific time period.
    /// </summary>
    public class aCandlestick
    {
        // Properties for a candlestick

        // Date and time of the candlestick data
        public DateTime Date { get; set; }
        // Opening price for the candlestick's time period
        public decimal Open { get; set; }
        // Highest price during the candlestick's time period
        public decimal High { get; set; }
        // Lowest price during the candlestick's time period
        public decimal Low { get; set; }
        // Closing price at the end of the candlestick's time period
        public decimal Close { get; set; }
        // Trading volume during the candlestick's time period
        public ulong Volume { get; set; }



        // Constructors
        // Default constructor initializing an empty candlestick object
        public aCandlestick()
        {

        }

        /// <summary>
        /// Constructor initializing a candlestick with specified values for date, prices, and volume.
        /// </summary>
        /// <param name="date">Timestamp for the candlestick data.</param>
        /// <param name="open">Opening price.</param>
        /// <param name="high">Highest price.</param>
        /// <param name="low">Lowest price.</param>
        /// <param name="close">Closing price.</param>
        /// <param name="volume">Trading volume.</param>
        public aCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        /// <summary>
        /// Returns a string representation of the candlestick data, summarizing key values.
        /// </summary>
        /// <returns>Formatted string with date, open, high, low, close, and volume information.</returns>
        public override string ToString()
        {
            return $"Timestamp: {Date}, Open: {Open}, High: {High}, Low: {Low}, Close: {Close}, Volume: {Volume}";
        }
    }
}

