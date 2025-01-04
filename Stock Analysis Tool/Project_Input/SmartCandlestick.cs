using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Input
{
    /// <summary>
    /// Represents an advanced candlestick entry with additional calculated properties.
    /// </summary>
    using System;
    using System.Reflection;

    namespace Project_Input
    {
        /// <summary>
        /// Represents an advanced candlestick entry with additional properties and pattern detection.
        /// Member variables are implemented as properties. They are properties due to the => noitation
        /// This class inherits from aCandlestick
        /// </summary>
        public class SmartCandlestick : aCandlestick
                
        {
            // Index (what is the candlestick index in the list)
            // As default, set it to -1 as it is a non-valid index
            private int _index = -1;
            public int Index
            {
                get { return _index; }
                set { _index = value; }
            }

            // IsPeak 
            public bool IsPeak { get; set; }

            // IsValley 
            public bool IsValley { get; set; }

            // Beauty (number of confirmation based on Fibonacci Levels of a Wave)
            private int _beauty = 0;
            public int Beauty
            { 
                get { return _beauty; }
                set { _beauty = value; }
            }


            // Range (the difference between High and Low)
            public decimal Range => High - Low;

            // BodyRange (the absolute difference between Open and Close)
            public decimal BodyRange => Math.Abs(Open - Close);

            // TopPrice (the higher of Open and Close)
            public decimal TopPrice => Math.Max(Open, Close);

            // BottomPrice (the lower of Open and Close)
            public decimal BottomPrice => Math.Min(Open, Close);

            // UpperTail (the height of the upper tail, calculated as the difference between High and TopPrice)
            // If the candlestick has an upper shadow, this represents its height.
            public decimal UpperTail => High - TopPrice;

            // LowerTail (the height of the lower tail, calculated as the difference between BottomPrice and Low)
            // If the candlestick has a lower shadow, this represents its height.
            public decimal LowerTail => BottomPrice - Low;

            // Pattern Detection Properties

            // Determines if the candlestick is bullish (closing price is greater than opening price)
            public bool IsBullish => Close > Open;

            // Determines if the candlestick is bearish (closing price is less than opening price)
            public bool IsBearish => Close < Open;

            // Determines if the candlestick is neutral (open and close prices are equal)
            public bool IsNeutral => Close == Open;

            // Determines if the candlestick is a Marubozu (no upper or lower tails)
            public bool IsMarubozu => BodyRange == Range;

            // Determines if the candlestick is a Hammer pattern
            // The way I defined hammer is that the lower tail has to be at least twice larger than
            // the body of the candlestick, the upper tail hast to be less than half of the body and the stock is bullish
            public bool IsHammer => LowerTail > (BodyRange * 2) && UpperTail < (BodyRange * 0.5m) && IsBullish;

            // Determines if the candlestick is a Doji (very small body) Body Range is less than 10% of the whole range
            public bool IsDoji => BodyRange < (Range * 0.1m);

            // Determines if the candlestick is a Dragonfly Doji
            // this is when the open, high and close are very close to each other.
            // in this case I decided that the upper tail needs to be less than 10% of the range of the stock
            // whole the lower tail needs to be greater than 60% of the range
            public bool IsDragonflyDoji => IsDoji && UpperTail < (Range * 0.1m) && LowerTail > (Range * 0.6m);

            // Determines if the candlestick is a Gravestone Doji
            // this is when the low, open and close are very close to each other (is like the inverse of the dragonfly)
            // the uppe rtail hast to be greater than 60% of the range while the lower tail has to be less than 10% of the range
            public bool IsGravestoneDoji => IsDoji && UpperTail > (Range * 0.6m) && LowerTail < (Range * 0.1m);

            // Constructors
            /// <summary>
            ///  Default constructor
            /// Initializes a new instance of the SmartCandlestick class with default values.
            /// This can be useful for creating a placeholder or when values are assigned later
            /// </summary>
            public SmartCandlestick() { }

            /// <summary>
            /// Parameterized constructor
            /// Initializes a new instance of the SmartCandlestick class with specified values.
            /// This constructor forwards the provided arguments to the base class (aCandlestick) constructor.
            /// Parameters:
            /// - date: The date and time of the candlestick data.
            /// - open: The opening price of the candlestick's time period.
            /// - high: The highest price reached during the candlestick's time period.
            /// - low: The lowest price reached during the candlestick's time period.
            /// - close: The closing price at the end of the candlestick's time period.
            /// - volume: The trading volume during the candlestick's time period.
            /// </summary>

            public SmartCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume)
                : base(date, open, high, low, close, volume) { }

            /// <summary>
            /// Returns a string representation of the SmartCandlestick data, summarizing key values.
            /// </summary>
            public override string ToString()
            {
                return base.ToString() +
                       $", Range: {Range}, BodyRange: {BodyRange}, " +
                       $"TopPrice: {TopPrice}, BottomPrice: {BottomPrice}, " +
                       $"UpperTail: {UpperTail}, LowerTail: {LowerTail}";
            }
        }
    }

}
