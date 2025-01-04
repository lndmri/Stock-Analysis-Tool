using Project_Input.Project_Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Project_Input
{
    /// <summary>
    /// Class representing a display form that shows stock candlestick data in a grid and chart format.
    /// This form will hold all the candlestick related infromation to containerize the candlestick data.
    /// This form in invoked by Form_Input forms.
    /// </summary>

    public partial class Form_display : Form
    {
        // Variables needed for rubber banding, selecting an area in the chart, and defining a wave
        private bool isSelecting = false;   // Flag to control the dragging status during rubber banding
        private Rectangle selectionRect;    // Rectangle that will be shaped while the user drags.
        private Wave wave;                  // Wave object that will be formed once the user make a valid selction
        private Point startPoint;           // Start point of the user selection
        private Point endPoint;             // Ending pint of the user selection

        // List to hold all candlesticks read from the CSV file
        List<SmartCandlestick> allCandlesticks;

        // List to hold bound candlesticks in the chart
        BindingList<SmartCandlestick> boundList;

        /// <summary>
        /// Default constructor for Form_display, initializes the form's components.
        /// </summary>
        public Form_display() 
        {
            InitializeComponent(); // Initialize form components.
            
            // Attach event handlers to chart_candlesticks
            chart_candlesticks.MouseDown += chart_candlesticks_MouseDown;
            chart_candlesticks.MouseMove += chart_candlesticks_MouseMove;
            chart_candlesticks.MouseUp += chart_candlesticks_MouseUp;
            chart_candlesticks.Paint += chart_candlesticks_Paint;
        }

        /// <summary>
        /// Overloaded constructor for Form_display, setting it up as a child form of Form_Input.
        /// </summary>
        /// <param name="data_source_path">Path to the CSV file containing candlestick data.</param>
        /// <param name="startDate">Start date filter for the candlesticks.</param>
        /// <param name="endDate">End date filter for the candlesticks.</param>
        public Form_display(String data_source_path, DateTime startDate, DateTime endDate)
        {
            InitializeComponent(); // Initialize form components.

            // Attach event handlers to chart_candlesticks
            chart_candlesticks.MouseDown += chart_candlesticks_MouseDown;
            chart_candlesticks.MouseMove += chart_candlesticks_MouseMove;
            chart_candlesticks.MouseUp += chart_candlesticks_MouseUp;
            chart_candlesticks.Paint += chart_candlesticks_Paint;

            ResumeLayout(false); // Suspend layout logic to do some changes in the form.

            // Extract just the file name from the full path for display.
            String filename = Path.GetFileName(data_source_path);

            // Set the form's title to the filename for easy identification.
            Text = filename;
            dateTimePicker_startDate.Value = startDate; // Set the start date filter.
            dateTimePicker_endDate.Value = endDate; // Set the end date filter.

            // Load all candlesticks from the specified CSV file.
            allCandlesticks = ACandlestickLoader.LoadFromCsv(data_source_path);

            // Check if the data is already ordered from recent to oldest by comparing the dates of the first two items.
            if (allCandlesticks.Count > 1 && allCandlesticks[0].Date > allCandlesticks[1].Date)
            {
                // If it is in descending order (recent to oldest), reverse it to display oldest to recent.
                allCandlesticks.Reverse();
            }

            // Update the form’s contents based on the specified date range passed to the constructor.
            // This method resumes the layout and shows the form.
            // This method was implemented for reusability as the same code executes when a user changes the dates
            // for displaying a form in the current form
            updateContent(startDate, endDate);
        }



        /// <summary>
        /// Event handler for the update button click event to refresh displayed content.
        /// </summary>
        private void button_update_Click(object sender, EventArgs e)
        {
            // Update the content based on the current date filter range of this from based on the date pickers
            updateContent((DateTime)dateTimePicker_startDate.Value, (DateTime)dateTimePicker_endDate.Value);
        }

        /// <summary>
        /// Event handler for the Load Stock button click event. Opens a file dialog
        /// to allow the user to select a CSV file containing stock data.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">Event data containing event-specific information.</param>
        private void button_load_Click(object sender, EventArgs e)
        {
            // Displaying the file dialog as show dialog so it is the window with permament focus until terminated.
            DialogResult r = openFileDialog_load.ShowDialog();
        }

        /// <summary>
        /// Event handler triggered after a file is selected in the open file dialog. 
        /// Loads the stock data in the current form for the selected file
        /// Opens new display form to show candlestick data from the other files selected if multiple.
        /// </summary>
        /// <param name="sender">The file dialog that triggered the event.</param>
        /// <param name="e">Provides event metadata and status.</param>
        private void openFileDialog_load_FileOk(object sender, CancelEventArgs e)
        {
            // Retrieve the file path of the selected file
            String fullPath = openFileDialog_load.FileName;

            // Retrieve the date range from date pickers to filter candlestick data
            // Gets the start date desired
            DateTime startDate = dateTimePicker_startDate.Value;
            // Gets the end date desired
            DateTime endDate = dateTimePicker_endDate.Value;

            // Setting the name of the current form to the text file name. Denoting MAIN as it is the principal form
            Text = "MAIN FORM: " +Path.GetFileName(fullPath);
            // Load all candlesticks from the specified CSV file.
            allCandlesticks = ACandlestickLoader.LoadFromCsv(fullPath);

            // Check if the data is already ordered from recent to oldest by comparing the dates of the first two items.
            if (allCandlesticks.Count > 1 && allCandlesticks[0].Date > allCandlesticks[1].Date)
            {
                // If it is in descending order (recent to oldest), reverse it to display oldest to recent.
                allCandlesticks.Reverse();
            }

            // Update the form’s contents based on the specified date range.
            // This method resumes the layout and shows the form.
            // In this case the method is called for displaying the data in the current form
            updateContent(startDate, endDate);


            // instantiate forms for the different filenames selected 
            for (int i = 1; i < openFileDialog_load.FileNames.Length; i++)
            {
                // creating the new form with parameters. We are invoking the overloaded contructor
                Form_display newForm = new Form_display(openFileDialog_load.FileNames[i], startDate, endDate);
                newForm.Show(); // show the form
            }
        }

        /// <summary>
        /// This function filters the list of candlesticks passes as argument by returning a filtered list
        /// containting only the candlesticks within the startingDate and endingDate datetimes. 
        /// </summary>
        /// <param name="candlesticks">List of Candlesticks</param>
        /// <param name="startingDate">Start Date</param>
        /// <param name="endingDate">End Date</param>
        /// <returns></returns>
        public static List<SmartCandlestick> filterCandlesticks(List<SmartCandlestick> candlesticks, DateTime startingDate, DateTime endingDate)
        {
            // Create a list to hold candlesticks within the date range.
            var filteredCandlesticks = new List<SmartCandlestick>();

            // Initialize an index counter.
            int index = 0;

            // Iterate through each candlestick in the list.
            foreach (var candlestick in candlesticks)
            {
                // If the candlestick's date is within the range, add it to the filtered list.
                if (candlestick.Date >= startingDate && candlestick.Date <= endingDate)
                {
                    // Assign the current index to the candlestick.
                    candlestick.Index = index++;
                    // Adding the candlestick to the filtered list
                    filteredCandlesticks.Add(candlestick);
                    
                }
            }
            // Returning the filtered list
            return filteredCandlesticks;
        }

        /// <summary>
        /// Adjusts the chart's minimum and maximum Y-axis values based on the candlestick data range.
        /// Receives a boundlist of Candlesticks
        /// </summary>
        private void normalizeChart(BindingList<SmartCandlestick> boundList)
        {
            // if no there are no candlesticks in the boundlist then we return to avoid any running time issue.
            if (boundList.Count == 0) 
            {
                return;
            }

            // Get the minimum and maximum candlestick prices for Y - axis scaling.
            // We use arrow functuoins and the .Min, .Max methods from boudnlist

            double min = (double)boundList.Min(cs => cs.Low); // gets the minimum 
            double max = (double)boundList.Max(cs => cs.High); // gets the maximum

            // Adding some margin to the data so the candlestics Graphical representation does not start right at the y axis
            min = Math.Floor(0.98 * min); // removing 2% from the minimum
            max = Math.Ceiling(1.02 * max); // adding 2% to the maximum

            // Set the chart's Y-axis minimum and maximum values
            chart_candlesticks.ChartAreas["ChartArea_OHLC"].AxisY.Minimum = min; // set the minimum
            chart_candlesticks.ChartAreas["ChartArea_OHLC"].AxisY.Maximum = max; // set the maximum

            // Calculate and set the interval based on the range for better visibility
            chart_candlesticks.ChartAreas["ChartArea_OHLC"].AxisY.Interval = Math.Ceiling((max - min) / 10); // Divide by 10 for a smoother interval
        }

        /// <summary>
        /// Updates the content displayed in the chart based on the specified date range.
        /// </summary>
        private void updateContent(DateTime startingDate, DateTime endingDate)
        {

            // Filter the candlesticks to match the selected date range.
            List<SmartCandlestick> filteredCandlesticks = filterCandlesticks(allCandlesticks, startingDate, endingDate);

            // Creating a binding list using the filtered candlesticks. This is will be the datasource
            boundList = new BindingList<SmartCandlestick>(filteredCandlesticks);

            // Set the datasource of the chart in the form display to our boundlist of candlesticks
            chart_candlesticks.DataSource = boundList;

            // Divide chart range by 10 splits for a smoother interval
            normalizeChart(boundList);

            // Clear previous annotations to avoid duplicating them
            chart_candlesticks.Annotations.Clear();

            // Enabling the Show Peaks and Show Valley buttons
            button_showPeaks.Enabled = true;
            button_showValleys.Enabled = true;

            // Setting the names of the buttons Show Peaks and Show Valleys correctly
            button_showPeaks.Text = "Show Peaks";
            button_showValleys.Text = "Show Valleys";

            // Disabling button_recalculateWave and resetting label_currentWaveBeauty
            label_currentWaveBeauty.Text = "Current Wave Beauty: ";
            button_recalculateWave.Enabled = false;

            // Clearing previous Waves
            wave = null;

            // identify peaks and valleys but set them to be hidden
            identify_peaks_and_valleys(boundList);

            // Resume Layout of the form as we have done all changes. If Layout was already Resumed this command does not affect.
            ResumeLayout(true);

            // Show the form
            this.Show();
        }
        /// <summary>
        /// Identifies peaks and valleys in the candlestick data and adds annotations to the chart.
        /// </summary>
        /// <param name="filteredCandlesticks">boundlist (filtered)</param>
        private void identify_peaks_and_valleys(BindingList<SmartCandlestick> filteredCandlesticks) 
        {
            // Loop through each candlestick in the list, starting from the second element
            // and ending before the last element (to allow comparison with neighboring elements)
            for (int i = 1; i < filteredCandlesticks.Count - 1; i++)
            {
                // Set 'current' to the candlestick at index i, which is the one being evaluated.
                var current = filteredCandlesticks[i];
                // Set 'previous' to the candlestick before the current one, for comparison purposes.
                var previous = filteredCandlesticks[i - 1];
                // Set 'next' to the candlestick after the current one, for comparison purposes.
                var next = filteredCandlesticks[i + 1];

                // Check if the current candlestick represents a peak by comparing its high value
                // with the high values of the previous and next candlesticks.
                if (current.High > previous.High && current.High > next.High)
                {   
                    // Set the IsPeak property of the candlestick to true
                    current.IsPeak = true;

                    // Create a hidden annotation for a peak
                    addTextAnnotation(chart_candlesticks, "Peak", current.High, i, Color.Green);

                    // Draw a horizontal red line at the valley (hidden)
                    addLineAnnotation(chart_candlesticks, "Peak", current.High, i, Color.Green);
                }
                // Check for a valley: Current low is less than both previous and next lows
                else if (current.Low < previous.Low && current.Low < next.Low)
                {
                    // Set the IsValley property of the candlestick to true
                    current.IsValley = true;

                    // Create a hidden annotation for a valley
                    addTextAnnotation(chart_candlesticks, "Valley", current.Low, i, Color.Red);

                    // Draw a horizontal red line at the valley (hidden)
                    addLineAnnotation(chart_candlesticks, "Valley", current.Low, i, Color.Red);
                }
                // If it is not peak nor valley we need to set so accordingly so all candlesticks have a denomination
                else
                {
                    current.IsPeak = false;
                    current.IsValley = false;
                }

            }
        }


        /// <summary>
        /// Adds a hidden text annotation to the chart for marking peaks and valleys.
        /// </summary>
        /// <param name="chart">The chart to which the annotation will be added.</param>
        /// <param name="text">The annotation text, such as "Peak" or "Valley".</param>
        /// <param name="price">The price level at which to place the annotation.</param>
        /// <param name="index">The x-axis index position for the annotation.</param>
        /// <param name="color">The color of the annotation text.</param>>
        private void addTextAnnotation(Chart chart, string text, decimal price, int index, Color color)
        {
            // Create a new text annotation for marking a peak or valley point.
            var newAnnotation = new TextAnnotation
            {
                Text = text,            // Set the annotation text (e.g., "Peak" or "Valley").
                ForeColor = color,      // Set the color of the annotation text.
                Font = new Font("Arial", 8),    // Set the font style and size for the annotation text.
                TextStyle = TextStyle.Frame,    // Define the annotation style as framed text.
                Alignment = System.Drawing.ContentAlignment.TopCenter,  // Set the alignment of the annotation text to be centered at the top.
                AxisX = chart_candlesticks.ChartAreas["ChartArea_OHLC"].AxisX, // Link the annotation to the X-axis of the specified chart area.
                AxisY = chart_candlesticks.ChartAreas["ChartArea_OHLC"].AxisY, // Link the annotation to the Y-axis of the specified chart area.
                AnchorX = index + 1, // Set the anchor position on the X-axis slightly offset to align with the candlestick.
                Y = (double)price, // Set the position on the Y-axis to the specified price level.
                Visible = false // Make the annotation hidden initially, to be shown when needed.

            };

            // Adjust the position slightly above the price for peak annotations.
            if (text == "Peak")
            {
                // show the annotation to a third of the current chart interval above the actual peak
                newAnnotation.Y += (double) chart.ChartAreas["ChartArea_OHLC"].AxisY.Interval;
            }
            // Adjust the position slightly below the price for valley annotations.
            else if (text == "Valley")
            {
                // show the annotation 2% below the actual peak
                //newAnnotation.Y -= chart.ChartAreas["ChartArea_OHLC"].AxisY.Interval; 
            }
            // Add the configured annotation to the chart's annotations collection.
            chart_candlesticks.Annotations.Add(newAnnotation);
        }

        /// <summary>
        /// Adds a hidden horizontal line annotation to the chart to mark a specific price level,
        /// such as the price of a peak or valley.
        /// </summary>
        /// <param name="chart">The chart where the annotation will be added.</param>
        /// <param name="price">The price level at which to place the line.</param>
        /// <param name="index">The x-axis index position for the annotation.</param>
        /// <param name="color">The color of the line annotation.</param>
        private void addLineAnnotation(Chart chart, string purpose, decimal price, int index, Color color) 
        {
            HorizontalLineAnnotation newLineAnnotation = null;

            if (purpose == "Peak" || purpose == "Valley")
            {
                // Create a new horizontal line annotation to mark the price level.
                newLineAnnotation = new HorizontalLineAnnotation
                {
                    AxisX = chart_candlesticks.ChartAreas["ChartArea_OHLC"].AxisX, // Link the line annotation to the X-axis of the specified chart area.
                    AxisY = chart_candlesticks.ChartAreas["ChartArea_OHLC"].AxisY, // Link the line annotation to the Y-axis of the specified chart area.
                    ClipToChartArea = "ChartArea_OHLC",     // Restrict the annotation to be drawn within the specified chart area.
                    Y = (double)price,                      // Set the Y position of the line to the specified price level.
                    LineColor = color,                      // Set the color of the line annotation.
                    LineWidth = 1,                          // Set the width of the line.
                    IsInfinitive = true,                    // Make the line infinite, stretching horizontally across the entire chart.
                    Visible = false                         // Start with the annotation hidden; it can be shown when necessary.
                };
            }

            // Add the configured line annotation to the chart's annotations collection.
            if (newLineAnnotation != null) 
            {
                chart_candlesticks.Annotations.Add(newLineAnnotation);
            }
        }

        /// <summary>
        /// Toggles the visibility of annotations that mark peaks on the chart.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">Event data.</param>
        private void button_showPeaks_Click(object sender, EventArgs e)
        {
            // Toggle the text of the button when it is showing and when not so it is descriptive to the user
            button_showPeaks.Text = button_showPeaks.Text == "Show Peaks" ? "Hide Peaks" : "Show Peaks";
            // Iterate through each annotation in the chart's annotation collection
            foreach (var annotation in chart_candlesticks.Annotations)
            {
                // Check if the annotation is a peak by its color or label
                if (annotation is TextAnnotation textAnnotation && textAnnotation.Text == "Peak")
                {
                    textAnnotation.Visible = !textAnnotation.Visible; // Toggle visibility
                }
                // Check if the annotation is a peak line annotation by its color
                else if (annotation is LineAnnotation lineAnnotation && lineAnnotation.LineColor == Color.Green)
                {
                    lineAnnotation.Visible = !lineAnnotation.Visible; // Toggle visibility
                }
            }
        }

        /// <summary>
        /// Toggles the visibility of annotations that mark valleys on the chart.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">Event data.</param>
        private void button_showValleys_Click(object sender, EventArgs e)
        {
            // Toggle the text of the button when it is showing and when not so it is descriptive to the user
            button_showValleys.Text = button_showValleys.Text == "Show Valleys" ? "Hide Valleys" : "Show Valleys";
            // Iterate through each annotation in the chart's annotation collection
            foreach (var annotation in chart_candlesticks.Annotations)
            {
                // Check if the annotation is a valley text annotation by verifying the text label
                if (annotation is TextAnnotation textAnnotation && textAnnotation.Text == "Valley")
                {
                    textAnnotation.Visible = !textAnnotation.Visible; // Toggle visibility
                }
                // Check if the annotation is a valley line annotation by its color
                else if (annotation is LineAnnotation lineAnnotation && lineAnnotation.LineColor == Color.Red)
                {
                    // Toggle the visibility of the valley line annotation
                    lineAnnotation.Visible = !lineAnnotation.Visible; // Toggle visibility
                   
                }
            }
        }

        /// <summary>
        /// This function handles the custom drawing of the candlestick chart, including selection rectangles, wave highlight rectangles,
        /// Fibonacci levels, and confirmation points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Provides event metadata and status.</param>
        private void chart_candlesticks_Paint(object sender, PaintEventArgs e)
        {
            // Draw the selection rectangle while dragging
            if (isSelecting)
            {
                using (Pen pen = new Pen(Color.Blue, 1)) // Create a blue dashed pen
                using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Blue))) // Semi-transparent
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; // Set dashed line style
                    e.Graphics.FillRectangle(brush, selectionRect); // Fill selection rectangle
                    e.Graphics.DrawRectangle(pen, selectionRect);   // Draw rectangle outline
                }
            }

            // Draw the final rectangle for the selected wave (after selection is finalized)
            if (wave != null)
            {
                var chartArea = chart_candlesticks.ChartAreas["ChartArea_OHLC"]; // Access the chart area for OHLC data

                // Convert wave bounds to pixel positions dynamically
                double xStartPixel = chartArea.AxisX.ValueToPixelPosition(wave.StartIndex); // x position of the wave first candlestick
                double xEndPixel = chartArea.AxisX.ValueToPixelPosition(wave.EndIndex); // x position of the wave last candlestick
                double yTopPixel = chartArea.AxisY.ValueToPixelPosition((double)wave.HighestPrice); // highest price in the wave
                double yBottomPixel = chartArea.AxisY.ValueToPixelPosition((double)wave.LowestPrice); // lowest price in the wave


                // Draw the rectangle
                using (Pen pen = new Pen(Color.Orange, 2)) // Create an orange solid pen
                using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Orange))) // Semi-transparent
                {
                    // create the recangle for the wave drawing. We draw the rectangle using the top leftmost pixel x, y coordinate and the height and width of the rectangle
                    var rect = new Rectangle(
                        (int)xStartPixel,       // Left coordinate
                        (int)Math.Min(yTopPixel, yBottomPixel), // Top coordinate
                        (int)(xEndPixel - xStartPixel), // Width of the rectangle
                        (int)Math.Abs(yTopPixel - yBottomPixel) // Height of the rectangle
                    );

                    e.Graphics.FillRectangle(brush, rect);  // Fill rectangle
                    e.Graphics.DrawRectangle(pen, rect);    // Draw rectangle outline

                }

                // Draw Fibonacci lines
                using (Pen pen = new Pen(Color.Purple, 2))  //  Purple pen to draw
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; // Set dashed style
                    List<string> percentage = new List<String> { "0 %", "23.6 %", "38.2 %", "50 %", "62.8 %", "76.4 %", "100 %" }; // list of fibonacci levels labels

                    // for each calculated fibonacci level
                    for (int i = 0; i < wave.Fibonacci_lvl.Count; i++)
                    {   
                        // we get the level and store it in the level variable
                        var level = wave.Fibonacci_lvl[i];  
                        double yPixel = chartArea.AxisY.ValueToPixelPosition((double) level); // converting the calculated number for a given level to y coordinate

                        // Draw the line
                        e.Graphics.DrawLine(pen, (float)xStartPixel, (float)yPixel, (float)xEndPixel, (float)yPixel);

                        // if is the wave is downward 
                        if (wave.IsDownward) 
                        {
                            percentage.Reverse(); //we display 100 % at the top and 0 % at the bottom so we reverse the percentage list
                        }

                        // Add text labels near the Fibonacci lines
                        string label = $"${wave.Fibonacci_lvl[i]:F2}    {percentage[i]}";   // label that goes on each fibonacci level, I decided to include the price asociated
                                                                                            // with the fibonacci along with the fibonacci in percentage
                        e.Graphics.DrawString(     // We draw it as a string
                            label,                 // The previously label 
                            new Font("Arial", 8),  // Set font
                            Brushes.Black,         // Set the inner color (in this case font color)
                            (float)xEndPixel + 5,  // Slight offset to the right
                            (float)yPixel - 10    // Align vertically with the line
                        );
                    }
                }

                // Draw all confirmation points
                foreach (var (index, price) in wave.Confirmation_Points) // for each point in the list of confirmation points of the wave
                {
                    double xPixel = chartArea.AxisX.ValueToPixelPosition((double) index+1); // get the x pixel coordinate 
                    double yPixel = chartArea.AxisY.ValueToPixelPosition((double) price);   // get the y pixel coordinate

                    // Draw a small circle for each confirmation point
                    using (Brush brush = new SolidBrush(Color.Navy))
                    {
                        e.Graphics.FillEllipse(brush, (float)xPixel - 3, (float)yPixel - 3, 6, 6);  // change the offset so it comes in the center of the given coordinate
                    }
                }
            }
        }

        /// <summary>
        /// Handles the MouseDown event to start selection of a wave.
        /// </summary>
        /// <param name="sender">The control (in this case the chart) that triggered the MouseDown Event</param>
        /// <param name="e">Metadata about the mouse event, such as the cursor's position</param>
        private void chart_candlesticks_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  // Check for left mouse button
            {
                var hit = chart_candlesticks.HitTest(e.X, e.Y); // Check where the mouse was clicked
                if (hit.ChartArea != null && hit.ChartArea.Name == "ChartArea_OHLC")  // Ensure it is within the relevant chart area
                { 
                    // Reset wave if there is one as we will be selecting a new one.
                    if (wave != null)
                    {
                        wave = null;
                    } 

                    isSelecting = true; // Start selection
                    startPoint = e.Location; // Store the start point of the selection
                    selectionRect = new Rectangle(e.Location, new Size(0,0)); // Initialize selection rectangle
                    label_currentWaveBeauty.Text = "Current Wave Beauty: "; // Reset label
                    button_recalculateWave.Enabled = false; // Disable recalculation button
                    chart_candlesticks.Invalidate();    // Force chart redraw
                }
            }
        }

        /// <summary>
        /// Handles the MouseMove event to update the selection rectangle while the mouse is being dragged.
        /// </summary>
        /// <param name="sender">The control (in this case the chart) that triggered the MouseMove Event</param>
        /// <param name="e">Metadata about the mouse event, such as the cursor's position</param>
        private void chart_candlesticks_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting)    // If a selection (dragging) operation is in progress
            {
                endPoint = e.Location;
                selectionRect = new Rectangle(
                    Math.Min(startPoint.X, endPoint.X), //  Left coordinate
                    Math.Min(startPoint.Y, endPoint.Y), //  Top coordinate
                    Math.Abs(startPoint.X - endPoint.X), // Width of the rectangle
                    Math.Abs(startPoint.Y - endPoint.Y) // Height of the rectangle
                );
                chart_candlesticks.Invalidate(); // Redraw chart to update selection rectangle
            }
        }
        /// <summary>
        /// Handles the MouseUp event to finalize the selection rectangle and create a wave if the selection is valid.
        /// </summary>
        /// <param name="sender">The control (in this case the chart) that triggered the MouseUp Event</param>
        /// <param name="e"> Metadata about the mouse event, such as the cursor's position</param>
        private void chart_candlesticks_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSelecting) //If a selection operation was in progress
            {
                isSelecting = false; // End the selection operation

                var chartArea = chart_candlesticks.ChartAreas["ChartArea_OHLC"]; // save the chartarea instance into a variable to avoid typing clutter in future references

                // Translate selectionRect to chart coordinates
                double xMin = chartArea.AxisX.PixelPositionToValue(selectionRect.Left);     // selection's x leftmost coordinate
                double xMax = chartArea.AxisX.PixelPositionToValue(selectionRect.Right);    // selections x rightmost coordinate

                // Find candlesticks in the selected range and add them in a List
                // This works as our chart of candlesticks is indexed in the x axis. So there is a one to one correspondence between the canldestick index and the x value
                // We are creating the list of Selected candlesticks whose indexes are between xMin and xMax
                var selectedCandlesticks = boundList                    
                    .Where(c => c.Index  >= xMin - 1 && c.Index <= xMax - 1)
                    .ToList();
                
                // Ensure at least two candlesticks are selected
                if (selectedCandlesticks.Count < 2)
                {
                    MessageBox.Show("Select at least two candlesticks to form a wave!");
                    return;
                }

                // if valid move we proceed to create the wave
                if (ValidateMove(selectedCandlesticks) == true)
                {
                    decimal lowestPrice = selectedCandlesticks.Min(c => c.Low);     // Find the lowest price
                    decimal highestPrice = selectedCandlesticks.Max(c => c.High);   // Find the highest price
                    CreateWave(selectedCandlesticks, lowestPrice, highestPrice);    // Create the wave 

                }
            }
        }
        /// <summary>
        /// Validates whether the selected candlesticks form a valid wave.
        /// It is a valid wave if:
        /// 1. First candlestick must be a peak or a valley
        /// 2. all Candlesticks in the Selected Candlesticks are within the bounds of the first candlestick high/low value 
        /// and last candlestick high/low value depending on the type of wave (downward/upward). 
        /// </summary>
        /// <param name="selectedCandlesticks"></param>
        /// <returns>True if wave is valid, False otherwise</returns>
        private bool ValidateMove(List<SmartCandlestick> selectedCandlesticks)
        {
            var first = selectedCandlesticks.First();   // getting the first candlestick in the list
            var second = selectedCandlesticks.Last();   // getting the last canldesitck in the list

            // Check if the first candlestick is a peak or valley. 
            // If it is no peak, nor valley then send a message to the user 
            if (!first.IsPeak && !first.IsValley)   
            {
                MessageBox.Show("The first candlestick must be a peak or a valley!");
                return false;       // returns false
            }

            // Ensure proper order making sure that the first candlestick is before the last in the selection based on index
            if (first.Index >= second.Index)
            {
                MessageBox.Show("The wave selection is invalid. Ensure proper order.");
                return false;
            }

            // Validating Wave depending if first is Peak or Valley
            // if Peak we should expect a downward move. 
            // No candlestick can have a Higher than the Peak's high nor Lower than the Last candlestick selected low in the selectedCandlestick List.
            if (first.IsPeak) 
            {   
                // Itarating over the canldesticks in the selectedCanldesticks List
                for (int i = 0; i <= second.Index - first.Index; i++) 
                {
                    if (selectedCandlesticks[i].High > first.High)
                    {
                        MessageBox.Show("Invalid Wave. For downward move, no inner candlestick can have higer price than your first selected candlestick");
                        return false;
                    }
                    if (selectedCandlesticks[i].Low < second.Low)
                    {
                        MessageBox.Show("Invalid Wave. For downward move, no inner candlestick can have lower price than your last selected candlestick");
                        return false;
                    }
                }
            }

            // if Valley we should expect an upward move
            // No candlestick can have a Lower than the Valleys's low nor Higher than the Last candlestick selected High in the selectedCandlestick List.
            if (first.IsValley)
            {
                // Itarating over the canldesticks in the selectedCanldesticks List
                for (int i = 0; i <= second.Index - first.Index; i++)
                {
                    if (selectedCandlesticks[i].Low < first.Low)
                    {
                        MessageBox.Show("Invalid Wave. For upward move, no inner candlestick can have lower price than your first selected candlestick");
                        return false;

                    }
                    if (selectedCandlesticks[i].High > second.High)
                    {
                        MessageBox.Show("Invalid Wave. For upward move, no inner candlestick can have higer price than your last selected candlestick");
                        return false;
                    }
                }
            }

            // if we passed all of the above filters and got here, then it means that the canldestick is Valid.
            return true;
        }

        /// <summary>
        /// Creates a wave using the selected candlesticks and the desired price range for the wave.
        /// </summary>
        /// <param name="selectedCandlesticks">List of candlesticks</param>
        /// <param name="lowestPrice">Minimum price for the wave</param>
        /// <param name="highestPrice">Maximum price for the wave</param>
        /// I decided to pass the lowest and highest price as parameters as it will allow me to create waves with higher/lower prices than the once enclosed in the selectedCanldesticks list
        private void CreateWave(List<SmartCandlestick> selectedCandlesticks, decimal lowestPrice, decimal highestPrice)
        {

            wave = new Wave(selectedCandlesticks, lowestPrice, highestPrice);   // Create a new wave instance
            wave.fibonacci_threshold = numericUpDown_fibonacciThreshold.Value;  // Set Fibonacci threshold (we get it from the numericUpDown control defined in the form)
            label_currentWaveBeauty.Text = $"Current Wave Beauty: {wave.WaveBeauty}"; // Update wave beauty label with the current wave Beauty
            button_recalculateWave.Enabled = true;  // Enable recalculate button in case the user wants to get more wave intervals for Beauty Comparison.
            var waveIntervals = GenerateWaveIntervals(wave, (int) numericUpDown_numberOfWaves.Value);   // Generate wave intervals for Beauty Comparison. This function returns a list of waves with modified HighestPrice and LowestPrice to this way obtain different beauties based on the Fibonacci levels resultant of the new Range of the wave.
            PlotWaveBeauty(waveIntervals);  // Plot the wave beauty chart
            chart_candlesticks.Invalidate(); // Trigger redraw of the candlestick chart
        }
        /// <summary>
        /// Saves to a list new waves that have go up (if Upward) or down (if Downward) on increments/decrements of 25% of the range divided by the interval number.
        /// </summary>
        /// <param name="original">wave object from where we will derive the altered waves</param>
        /// <param name="intervals">This is defined by the user (by default it is 10. It refers to the number of new waves taht will be generated based of the original wave)</param>
        /// <returns>A list of waves generated, with new interval shift</returns>
        private List<Wave> GenerateWaveIntervals(Wave original, int intervals) 
        {
            List<Wave> waves = new List<Wave>();    // List to hold generated wave intervals

            // Caluclate the interval size
            // Take the 25% of the wave range and divide it by the number of intervals the user wants
            decimal interval_size = (original.Range * (decimal) 0.25) / intervals;
            
            // We save the Original Wave into the List so we can comapare it with then other intervals 
            waves.Add(original);

            // If the original wave is Downward we do n interval decrements (1 decrement at the time, done in the loop) from the Lowest Price of the orignal wave.
            // Each time we do a decrement we save the new wave to our list of waves
            if (original.IsDownward) 
            {
                for (int i = 1; i <= intervals; i++)
                {
                    // Here we add a new wave to the list based on the newly calculated Lowest Price
                    waves.Add(new Wave(original.WaveCandlesticks, original.LowestPrice - (i * interval_size), original.HighestPrice));
                }
                waves.Reverse(); // Reverse to maintain order (smallar prices go first)
            }


            // If the original wave is Upward we do n interval inclrements (1 increment at the time, done in the loop) from the Highest Price of the orignal wave.
            // Each time we do an increment we save the new wave to our list of waves
            if (original.IsUpward)
            {
                for (int i = 1; i <= intervals; i++)
                {
                    // Here we add a new wave to the list based on the newly calculated Highest Price
                    waves.Add(new Wave(original.WaveCandlesticks, original.LowestPrice, original.HighestPrice + (i * interval_size)));
                }
            }

            // we return the list of waves generated, with new interval shift
            return waves;
        }

        /// <summary>
        /// Plots the wave beauty data on a the Beauty chart for visualization.
        /// </summary>
        /// <param name="waveIntervals">The wave intervals to be plotted. We extract the price and Beauty from each wave</param>
        private void PlotWaveBeauty(List<Wave> waveIntervals)
        {
            List<PriceXBeauty> pxb = new List<PriceXBeauty>(); // List of price vs beauty data points

            // Convert each wave into a PriceXBeauty object
            foreach (Wave w in waveIntervals) 
            {
                pxb.Add(new PriceXBeauty(w));
            }
            BindingList<PriceXBeauty> priceXBeauties = new BindingList<PriceXBeauty>(pxb); // creating Binding List that will be then set as Binding Source
            chart_beauty.DataSource = priceXBeauties; // Set chart data source
            chart_beauty.DataBind();    // Bind data to chart
            chart_beauty.ResumeLayout(true);    // Resume Layout for visualization
        }

        /// <summary>
        /// Recalculates the current wave by reusing existing candlesticks and price data. 
        /// This is used when the user for example changes the number of intervals desired or the Fibonacci Accuracy controls in the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_recalculateWave_Click(object sender, EventArgs e)
        {
            CreateWave(wave.WaveCandlesticks, wave.LowestPrice, wave.HighestPrice);
        }

        private void Form_display_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView_candlesticks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker_startDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void aCandlestickBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void chart_beauty_Click(object sender, EventArgs e)
        {

        }

    }
}
