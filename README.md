# Stock Analysis Application

## Overview
This Windows Forms application provides tools for visualizing and analyzing stock market data. Built in C#, it allows users to load, display, and interact with stock data in candlestick chart format while supporting advanced features such as pattern detection, Fibonacci level analysis, and prediction of future price trends.

## Features

### Data Visualization
- **Candlestick Charts**: Visualize stock data (Open, High, Low, Close) in candlestick format, with green for upward and red for downward movements.
- **User Interaction**:
  - Load data from `.csv` files stored in the `Stock Data` folder (Daily, Weekly, Monthly intervals).
  - Specify stock symbol, time range, and period (daily, weekly, or monthly).
  - Dynamic date filtering and axis scaling for improved visualization.

### Enhanced Analysis
- **SmartCandlestick Class**: Extends the basic candlestick structure with:
  - Pattern identification (e.g., Doji, Hammer, Marubozu).
  - Calculations of ranges, upper/lower tails, and body range.
- **Annotations**:
  - Identify peaks (green markers) and valleys (red markers).
  - Draw horizontal lines across the chart at these critical points.

### Predictive Tools
- **Wave Selection**:
  - Interactive selection of candlestick waves using rubber banding.
  - Validation of selected waves for accuracy.
- **Fibonacci Levels**: Automatically compute and overlay Fibonacci levels for selected waves.
- **Beauty Function**:
  - Quantify the "beauty" of a wave by calculating the alignment of candlesticks with Fibonacci levels.
  - Display beauty as a function of price to aid in predicting future highs and lows.

## How It Works
1. **Loading Data**: Import stock data from Yahoo Finance or other sources as `.csv` files with columns: Date, Open, High, Low, Close, Volume.
2. **Visualizing Data**: Use normalized charts to ensure gaps (e.g., weekends, holidays) are handled seamlessly.
3. **Advanced Analysis**:
   - Identify candlestick patterns.
   - Highlight key points like peaks and valleys.
   - Compute Fibonacci levels and beauty scores for trend prediction.

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/username/stock-analysis-app.git
2. Open the solution in Visual Studio.
3. Build and run the project.

## Usage
1. Place .csv files in the Stock Data Samples folder.
2. Launch the application.
3. Load a stock dataset and specify the desired date range and charting interval.
4. Use the chart tools to analyze patterns, identify key levels, and compute predictions.

## Requirements
1. Development Environment: Visual Studio 2022+
2. Framework: .NET Framework (Windows .NET Forms)

## Future Enhancements
1. Expand pattern detection to include more advanced algorithms.
2. Incorporate machine learning for predictive analytics.
3. Support real-time data streaming.


