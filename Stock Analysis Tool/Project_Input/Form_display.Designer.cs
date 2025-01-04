using System;
using System.Windows.Forms;

namespace Project_Input
{
    partial class Form_display
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_candlesticks = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateTimePicker_endDate = new System.Windows.Forms.DateTimePicker();
            this.label_endDate = new System.Windows.Forms.Label();
            this.dateTimePicker_startDate = new System.Windows.Forms.DateTimePicker();
            this.label_startDate = new System.Windows.Forms.Label();
            this.button_update = new System.Windows.Forms.Button();
            this.aCandlestickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.openFileDialog_load = new System.Windows.Forms.OpenFileDialog();
            this.button_load = new System.Windows.Forms.Button();
            this.button_showPeaks = new System.Windows.Forms.Button();
            this.button_showValleys = new System.Windows.Forms.Button();
            this.chart_beauty = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.numericUpDown_fibonacciThreshold = new System.Windows.Forms.NumericUpDown();
            this.panel_fibonacci = new System.Windows.Forms.Panel();
            this.label_waveAndFibonacci = new System.Windows.Forms.Label();
            this.label_currentWaveBeauty = new System.Windows.Forms.Label();
            this.button_recalculateWave = new System.Windows.Forms.Button();
            this.numericUpDown_numberOfWaves = new System.Windows.Forms.NumericUpDown();
            this.label_fibonacciAccuracy = new System.Windows.Forms.Label();
            this.label_numOfWaves = new System.Windows.Forms.Label();
            this.panel_allControls = new System.Windows.Forms.Panel();
            this.flowLayoutPanel_charts = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesticks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aCandlestickBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_beauty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fibonacciThreshold)).BeginInit();
            this.panel_fibonacci.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_numberOfWaves)).BeginInit();
            this.panel_allControls.SuspendLayout();
            this.flowLayoutPanel_charts.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart_candlesticks
            // 
            chartArea9.AlignWithChartArea = "ChartArea_OHLC";
            chartArea9.Name = "ChartArea_OHLC";
            this.chart_candlesticks.ChartAreas.Add(chartArea9);
            this.chart_candlesticks.Dock = System.Windows.Forms.DockStyle.Top;
            legend9.Enabled = false;
            legend9.Name = "Legend1";
            this.chart_candlesticks.Legends.Add(legend9);
            this.chart_candlesticks.Location = new System.Drawing.Point(3, 3);
            this.chart_candlesticks.Name = "chart_candlesticks";
            series9.ChartArea = "ChartArea_OHLC";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series9.CustomProperties = "PriceDownColor=Red, PriceUpColor=Green";
            series9.IsXValueIndexed = true;
            series9.Legend = "Legend1";
            series9.Name = "Series_OHLC";
            series9.XValueMember = "Date";
            series9.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series9.YValueMembers = "High,Low,Open,Close";
            series9.YValuesPerPoint = 6;
            this.chart_candlesticks.Series.Add(series9);
            this.chart_candlesticks.Size = new System.Drawing.Size(1453, 414);
            this.chart_candlesticks.TabIndex = 3;
            this.chart_candlesticks.Text = "Candlesticks Chart";
            this.chart_candlesticks.Click += new System.EventHandler(this.chart1_Click);
            this.chart_candlesticks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_candlesticks_MouseDown);
            this.chart_candlesticks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart_candlesticks_MouseMove);
            this.chart_candlesticks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart_candlesticks_MouseUp);
            // 
            // dateTimePicker_endDate
            // 
            this.dateTimePicker_endDate.Location = new System.Drawing.Point(617, 44);
            this.dateTimePicker_endDate.MaxDate = new System.DateTime(2100, 10, 29, 0, 0, 0, 0);
            this.dateTimePicker_endDate.Name = "dateTimePicker_endDate";
            this.dateTimePicker_endDate.Size = new System.Drawing.Size(270, 22);
            this.dateTimePicker_endDate.TabIndex = 5;
            this.dateTimePicker_endDate.Value = new System.DateTime(2024, 10, 29, 0, 0, 0, 0);
            // 
            // label_endDate
            // 
            this.label_endDate.AutoSize = true;
            this.label_endDate.Location = new System.Drawing.Point(702, 69);
            this.label_endDate.Name = "label_endDate";
            this.label_endDate.Size = new System.Drawing.Size(81, 16);
            this.label_endDate.TabIndex = 9;
            this.label_endDate.Text = "Ending Date";
            this.label_endDate.Click += new System.EventHandler(this.label2_Click);
            // 
            // dateTimePicker_startDate
            // 
            this.dateTimePicker_startDate.AccessibleName = "";
            this.dateTimePicker_startDate.Location = new System.Drawing.Point(13, 42);
            this.dateTimePicker_startDate.Name = "dateTimePicker_startDate";
            this.dateTimePicker_startDate.Size = new System.Drawing.Size(242, 22);
            this.dateTimePicker_startDate.TabIndex = 4;
            this.dateTimePicker_startDate.Value = new System.DateTime(2024, 1, 1, 15, 35, 0, 0);
            this.dateTimePicker_startDate.ValueChanged += new System.EventHandler(this.dateTimePicker_startDate_ValueChanged);
            // 
            // label_startDate
            // 
            this.label_startDate.AutoSize = true;
            this.label_startDate.Location = new System.Drawing.Point(85, 67);
            this.label_startDate.Name = "label_startDate";
            this.label_startDate.Size = new System.Drawing.Size(84, 16);
            this.label_startDate.TabIndex = 8;
            this.label_startDate.Text = "Starting Date";
            this.label_startDate.Click += new System.EventHandler(this.label1_Click);
            // 
            // button_update
            // 
            this.button_update.Location = new System.Drawing.Point(449, 14);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(139, 39);
            this.button_update.TabIndex = 6;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // aCandlestickBindingSource
            // 
            this.aCandlestickBindingSource.CurrentChanged += new System.EventHandler(this.aCandlestickBindingSource_CurrentChanged);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // openFileDialog_load
            // 
            this.openFileDialog_load.FileName = "IBM-Month.csv";
            this.openFileDialog_load.Filter = "All Files|*.csv|Monthly|-Month.csv";
            this.openFileDialog_load.Multiselect = true;
            this.openFileDialog_load.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_load_FileOk);
            // 
            // button_load
            // 
            this.button_load.Location = new System.Drawing.Point(283, 14);
            this.button_load.Name = "button_load";
            this.button_load.Size = new System.Drawing.Size(139, 39);
            this.button_load.TabIndex = 12;
            this.button_load.Text = "Load Stock";
            this.button_load.UseVisualStyleBackColor = true;
            this.button_load.Click += new System.EventHandler(this.button_load_Click);
            // 
            // button_showPeaks
            // 
            this.button_showPeaks.Enabled = false;
            this.button_showPeaks.Location = new System.Drawing.Point(283, 59);
            this.button_showPeaks.Name = "button_showPeaks";
            this.button_showPeaks.Size = new System.Drawing.Size(139, 39);
            this.button_showPeaks.TabIndex = 13;
            this.button_showPeaks.Text = "Show Peaks";
            this.button_showPeaks.UseVisualStyleBackColor = true;
            this.button_showPeaks.Click += new System.EventHandler(this.button_showPeaks_Click);
            // 
            // button_showValleys
            // 
            this.button_showValleys.Enabled = false;
            this.button_showValleys.Location = new System.Drawing.Point(449, 59);
            this.button_showValleys.Name = "button_showValleys";
            this.button_showValleys.Size = new System.Drawing.Size(139, 39);
            this.button_showValleys.TabIndex = 14;
            this.button_showValleys.Text = "Show Valleys";
            this.button_showValleys.UseVisualStyleBackColor = true;
            this.button_showValleys.Click += new System.EventHandler(this.button_showValleys_Click);
            // 
            // chart_beauty
            // 
            chartArea10.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea10.AxisX.Title = "Price";
            chartArea10.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea10.AxisY.Title = "Wave Beauty";
            chartArea10.Name = "ChartArea_Beauty";
            this.chart_beauty.ChartAreas.Add(chartArea10);
            this.chart_beauty.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend10.Enabled = false;
            legend10.Name = "Legend1";
            legend10.Title = "Pices vs Beauty";
            this.chart_beauty.Legends.Add(legend10);
            this.chart_beauty.Location = new System.Drawing.Point(3, 423);
            this.chart_beauty.Name = "chart_beauty";
            series10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series10.BorderWidth = 4;
            series10.ChartArea = "ChartArea_Beauty";
            series10.Color = System.Drawing.Color.Gold;
            series10.CustomProperties = "LabelStyle=TopLeft";
            series10.IsValueShownAsLabel = true;
            series10.IsVisibleInLegend = false;
            series10.IsXValueIndexed = true;
            series10.Legend = "Legend1";
            series10.MarkerSize = 8;
            series10.Name = "Series_Beauty";
            series10.XValueMember = "Price";
            series10.YValueMembers = "Beauty";
            this.chart_beauty.Series.Add(series10);
            this.chart_beauty.Size = new System.Drawing.Size(1450, 287);
            this.chart_beauty.TabIndex = 15;
            this.chart_beauty.Text = "Chart Beauty";
            this.chart_beauty.Click += new System.EventHandler(this.chart_beauty_Click);
            // 
            // numericUpDown_fibonacciThreshold
            // 
            this.numericUpDown_fibonacciThreshold.DecimalPlaces = 1;
            this.numericUpDown_fibonacciThreshold.Location = new System.Drawing.Point(145, 82);
            this.numericUpDown_fibonacciThreshold.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown_fibonacciThreshold.Name = "numericUpDown_fibonacciThreshold";
            this.numericUpDown_fibonacciThreshold.Size = new System.Drawing.Size(81, 22);
            this.numericUpDown_fibonacciThreshold.TabIndex = 17;
            this.numericUpDown_fibonacciThreshold.Tag = "";
            this.numericUpDown_fibonacciThreshold.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // panel_fibonacci
            // 
            this.panel_fibonacci.Controls.Add(this.label_waveAndFibonacci);
            this.panel_fibonacci.Controls.Add(this.label_currentWaveBeauty);
            this.panel_fibonacci.Controls.Add(this.button_recalculateWave);
            this.panel_fibonacci.Controls.Add(this.numericUpDown_numberOfWaves);
            this.panel_fibonacci.Controls.Add(this.label_fibonacciAccuracy);
            this.panel_fibonacci.Controls.Add(this.label_numOfWaves);
            this.panel_fibonacci.Controls.Add(this.numericUpDown_fibonacciThreshold);
            this.panel_fibonacci.Location = new System.Drawing.Point(893, 0);
            this.panel_fibonacci.Name = "panel_fibonacci";
            this.panel_fibonacci.Size = new System.Drawing.Size(510, 117);
            this.panel_fibonacci.TabIndex = 18;
            // 
            // label_waveAndFibonacci
            // 
            this.label_waveAndFibonacci.Location = new System.Drawing.Point(23, 23);
            this.label_waveAndFibonacci.Name = "label_waveAndFibonacci";
            this.label_waveAndFibonacci.Size = new System.Drawing.Size(100, 78);
            this.label_waveAndFibonacci.TabIndex = 23;
            this.label_waveAndFibonacci.Text = "Wave and FIbonacci Panel ";
            this.label_waveAndFibonacci.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_currentWaveBeauty
            // 
            this.label_currentWaveBeauty.AutoSize = true;
            this.label_currentWaveBeauty.ForeColor = System.Drawing.Color.Blue;
            this.label_currentWaveBeauty.Location = new System.Drawing.Point(345, 85);
            this.label_currentWaveBeauty.Name = "label_currentWaveBeauty";
            this.label_currentWaveBeauty.Size = new System.Drawing.Size(142, 16);
            this.label_currentWaveBeauty.TabIndex = 22;
            this.label_currentWaveBeauty.Text = "Current Wave Beauty:  ";
            // 
            // button_recalculateWave
            // 
            this.button_recalculateWave.Enabled = false;
            this.button_recalculateWave.Location = new System.Drawing.Point(358, 23);
            this.button_recalculateWave.Name = "button_recalculateWave";
            this.button_recalculateWave.Size = new System.Drawing.Size(116, 52);
            this.button_recalculateWave.TabIndex = 21;
            this.button_recalculateWave.Text = "Recalculate Current Wave";
            this.button_recalculateWave.UseVisualStyleBackColor = true;
            this.button_recalculateWave.Click += new System.EventHandler(this.button_recalculateWave_Click);
            // 
            // numericUpDown_numberOfWaves
            // 
            this.numericUpDown_numberOfWaves.Location = new System.Drawing.Point(145, 32);
            this.numericUpDown_numberOfWaves.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown_numberOfWaves.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_numberOfWaves.Name = "numericUpDown_numberOfWaves";
            this.numericUpDown_numberOfWaves.Size = new System.Drawing.Size(78, 22);
            this.numericUpDown_numberOfWaves.TabIndex = 20;
            this.numericUpDown_numberOfWaves.Tag = "";
            this.numericUpDown_numberOfWaves.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label_fibonacciAccuracy
            // 
            this.label_fibonacciAccuracy.AutoSize = true;
            this.label_fibonacciAccuracy.Location = new System.Drawing.Point(120, 63);
            this.label_fibonacciAccuracy.Name = "label_fibonacciAccuracy";
            this.label_fibonacciAccuracy.Size = new System.Drawing.Size(216, 16);
            this.label_fibonacciAccuracy.TabIndex = 19;
            this.label_fibonacciAccuracy.Text = "Fibonacci Accuracy Allowance  (%)";
            this.label_fibonacciAccuracy.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // label_numOfWaves
            // 
            this.label_numOfWaves.AutoSize = true;
            this.label_numOfWaves.Location = new System.Drawing.Point(117, 10);
            this.label_numOfWaves.Name = "label_numOfWaves";
            this.label_numOfWaves.Size = new System.Drawing.Size(173, 16);
            this.label_numOfWaves.TabIndex = 18;
            this.label_numOfWaves.Text = "Number of waves Up/Down ";
            this.label_numOfWaves.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label_numOfWaves.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // panel_allControls
            // 
            this.panel_allControls.AutoSize = true;
            this.panel_allControls.Controls.Add(this.dateTimePicker_startDate);
            this.panel_allControls.Controls.Add(this.panel_fibonacci);
            this.panel_allControls.Controls.Add(this.dateTimePicker_endDate);
            this.panel_allControls.Controls.Add(this.button_update);
            this.panel_allControls.Controls.Add(this.button_showValleys);
            this.panel_allControls.Controls.Add(this.label_startDate);
            this.panel_allControls.Controls.Add(this.button_showPeaks);
            this.panel_allControls.Controls.Add(this.label_endDate);
            this.panel_allControls.Controls.Add(this.button_load);
            this.panel_allControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_allControls.Location = new System.Drawing.Point(0, 841);
            this.panel_allControls.Name = "panel_allControls";
            this.panel_allControls.Size = new System.Drawing.Size(1453, 120);
            this.panel_allControls.TabIndex = 19;
            // 
            // flowLayoutPanel_charts
            // 
            this.flowLayoutPanel_charts.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel_charts.Controls.Add(this.chart_candlesticks);
            this.flowLayoutPanel_charts.Controls.Add(this.chart_beauty);
            this.flowLayoutPanel_charts.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel_charts.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_charts.Name = "flowLayoutPanel_charts";
            this.flowLayoutPanel_charts.Size = new System.Drawing.Size(1453, 714);
            this.flowLayoutPanel_charts.TabIndex = 20;
            // 
            // Form_display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1453, 961);
            this.Controls.Add(this.flowLayoutPanel_charts);
            this.Controls.Add(this.panel_allControls);
            this.Name = "Form_display";
            this.Text = "Form_display";
            this.Load += new System.EventHandler(this.Form_display_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart_candlesticks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aCandlestickBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_beauty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fibonacciThreshold)).EndInit();
            this.panel_fibonacci.ResumeLayout(false);
            this.panel_fibonacci.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_numberOfWaves)).EndInit();
            this.panel_allControls.ResumeLayout(false);
            this.panel_allControls.PerformLayout();
            this.flowLayoutPanel_charts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource aCandlestickBindingSource;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_candlesticks;
        private DateTimePicker dateTimePicker_endDate;
        private Label label_endDate;
        private DateTimePicker dateTimePicker_startDate;
        private Label label_startDate;
        private Button button_update;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private OpenFileDialog openFileDialog_load;
        private Button button_load;
        private Button button_showPeaks;
        private Button button_showValleys;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_beauty;
        private NumericUpDown numericUpDown_fibonacciThreshold;
        private Panel panel_fibonacci;
        private NumericUpDown numericUpDown_numberOfWaves;
        private Label label_fibonacciAccuracy;
        private Label label_numOfWaves;
        private Label label_currentWaveBeauty;
        private Button button_recalculateWave;
        private Label label_waveAndFibonacci;
        private Panel panel_allControls;
        private FlowLayoutPanel flowLayoutPanel_charts;
    }
}