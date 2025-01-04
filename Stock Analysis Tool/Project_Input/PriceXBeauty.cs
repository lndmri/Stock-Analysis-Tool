using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Input
{
    // Represents a data point containing a price and its associated beauty value
    public class PriceXBeauty
    {
        // The price value associated with this data point
        public decimal Price { get; set; }

        // The beauty score associated with this price
        public int Beauty { get; set; }

        // Default constructor, initializes an empty instance of the class
        public PriceXBeauty() { }

        // Parameterized constructor that creates a PriceXBeauty instance from a Wave object
        public PriceXBeauty(Wave wave) 
        {
            // Assign the price based on whether the wave is downward or upward
            // For a downward wave, use the LowestPrice; for an upward wave, use the HighestPrice.
            // Truncate the price to 2 decimal places to avoid rounding issues.
            Price = wave.IsDownward
                    ? Math.Truncate(wave.LowestPrice * 100) / 100
                    : Math.Truncate(wave.HighestPrice * 100) / 100;

            // Assign the beauty score of the wave to the Beauty property
            Beauty = wave.WaveBeauty;
        }
    }
}
