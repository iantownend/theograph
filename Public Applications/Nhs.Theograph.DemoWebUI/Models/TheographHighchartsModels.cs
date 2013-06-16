namespace Nhs.Theograph.DemoWebUI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Newtonsoft.Json;

    public class TheographChartViewModel
    {
        public TheographChartViewModel()
        {
            this.Series = new List<TheographChartSeries>();
            this.Axes = new Dictionary<string, TheographAxisData>();
        }

        [JsonProperty(PropertyName = "series")]
        public IList<TheographChartSeries> Series { get; set; }

        [JsonProperty(PropertyName = "axes")]
        public IDictionary<string, TheographAxisData> Axes { get; set; }
    }

    public class TheographChartSeries
    {
        public TheographChartSeries()
        {
            this.Data = new List<TheographChartSeriesData>();
        }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "data")]
        public IList<TheographChartSeriesData> Data { get; private set; }
    }

    public class TheographChartSeriesData
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TheographChartSeriesData"/> class.
        /// </summary>
        public TheographChartSeriesData()
        {
            this.Data = null;
        }

        /// <summary>
        /// Gets or sets the episode start timestamp.
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public long EpisodeStartTimestamp { get; set; }

        [JsonProperty(PropertyName = "y")]
        public int? Y { get; set; }

        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Data { get; private set; }

        /// <summary>
        /// Adds the data value with the specified key to the data dictionary, initialising
        /// the dictionary if it has not already been initialised.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="overwrite">if set to <c>true</c>, overwrites the data item if it already
        /// exists.</param>
        /// <exception cref="System.ArgumentException">
        /// A data item with the specified key already exists in the list of data items.
        /// <exception>
        public void AddData(string key, string value, bool overwrite = false)
        {
            // initialise if null
            if (this.Data == null)
            {
                this.Data = new Dictionary<string, string>();
            }

            // check for existing key
            if (this.Data.ContainsKey(key) && !overwrite)
            {
                throw new ArgumentException(string.Format(
                    "A data item with key '{0}' already exists in the list of data items.",
                    key));
            }

            if (this.Data.ContainsKey(key))
            {
                this.Data[key] = value;
            }
            else
            {
                this.Data.Add(key, value);
            }
        }
    }

    public class TheographAxisData
    {
        public TheographAxisData()
        {
            this.PlotBands = null;
            this.Min = 0;
            this.Type = "category";
            this.Labels = null;
            this.Title = null;
        }

        [JsonProperty(PropertyName = "plotBands", NullValueHandling = NullValueHandling.Ignore)]
        public IList<TheographPlotBandData> PlotBands { get; set; }

        [JsonProperty(PropertyName = "min")]
        public long Min { get; set; }

        [JsonProperty(PropertyName = "max")]
        public long Max { get; set; }

        [JsonProperty(PropertyName = "tickInterval", NullValueHandling = NullValueHandling.Ignore)]
        public int? TickInterval { get; set; }

        [JsonProperty(PropertyName = "minRange")]
        public int MinRange { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "labels", NullValueHandling = NullValueHandling.Ignore)]
        public TheographAxisLabelData Labels { get; set; }

        [JsonProperty(PropertyName = "dateTimeLabelFormats", NullValueHandling = NullValueHandling.Ignore)]
        public TheographDataTimeLabelFormatData DateTimeLabelFormats { get; set; }

        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public TheographTitleData Title { get; set; }

        public void AddPlotBand(TheographPlotBandData plotBand)
        {
            if (this.PlotBands == null)
            {
                this.PlotBands = new List<TheographPlotBandData>();
            }

            this.PlotBands.Add(plotBand);
        }
    }

    public class TheographDataTimeLabelFormatData
    {
        /// <summary>
        /// '%H:%M:%S.%L'
        /// </summary>
        [JsonProperty(PropertyName = "millisecond", NullValueHandling = NullValueHandling.Ignore)]
        public string Millisecond { get; set; }

        /// <summary>
        /// '%H:%M:%S'
        /// </summary>
        [JsonProperty(PropertyName = "second", NullValueHandling = NullValueHandling.Ignore)]
        public string Second { get; set; }

        /// <summary>
        /// '%H:%M'
        /// </summary>
        [JsonProperty(PropertyName = "minute", NullValueHandling = NullValueHandling.Ignore)]
        public string Minute { get; set; }

        /// <summary>
        /// '%H:%M'
        /// </summary>
        [JsonProperty(PropertyName = "hour", NullValueHandling = NullValueHandling.Ignore)]
        public string Hour { get; set; }

        /// <summary>
        /// '%e. %b'
        /// </summary>
        [JsonProperty(PropertyName = "day", NullValueHandling = NullValueHandling.Ignore)]
        public string Day { get; set; }

        /// <summary>
        /// '%e. %b'
        /// </summary>
        [JsonProperty(PropertyName = "week", NullValueHandling = NullValueHandling.Ignore)]
        public string Week { get; set; }

        /// <summary>
        /// '%b \'%y'
        /// </summary>
        [JsonProperty(PropertyName = "month", NullValueHandling = NullValueHandling.Ignore)]
        public string Month { get; set; }

        /// <summary>
        /// '%Y'
        /// </summary>
        [JsonProperty(PropertyName = "year", NullValueHandling = NullValueHandling.Ignore)]
        public string Year { get; set; }
    }

    public class TheographAxisLabelData
    {
        public TheographAxisLabelData()
        {
            Enabled = false;
        }

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }
    }

    public class TheographTitleData
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TheographTitleData"/> class.
        /// </summary>
        public TheographTitleData()
        {
            this.Text = null;
        }

        /// <summary>
        /// Gets or sets the text. Defaults to <c>null</c>.
        /// </summary>
        [JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Include)]
        public string Text { get; set; }
    }

    public class TheographPlotBandData
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TheographPlotBandData"/> class.
        /// </summary>
        public TheographPlotBandData()
        {
            this.Label = new TheographPlotBandLabelData();
        }

        [JsonProperty(PropertyName = "from")]
        public long From { get; set; }

        [JsonProperty(PropertyName = "to")]
        public long To { get; set; }

        [JsonProperty(PropertyName = "color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "label")]
        public TheographPlotBandLabelData Label { get; set; }
    }

    public class TheographPlotBandLabelData
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}