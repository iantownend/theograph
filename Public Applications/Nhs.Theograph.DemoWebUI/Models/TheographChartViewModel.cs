namespace Nhs.Theograph.DemoWebUI.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using Nhs.Theograph.Core;
    using Nhs.Theograph.DemoWebUI.Helpers;
    using System.Text;

    public class TheographChartViewModel
    {
        public TheographChartViewModel()
        {
            this.Series = new List<TheographChartSeries>();
            this.Axes = new Dictionary<string, TheographAxisData>();
        }

        public TheographChartViewModel(PatientEpsiodes patientEpisodes)
            : this()
        {
            var episodeGroups = patientEpisodes.Episodes.GroupBy(x => x.EpisodeType.DisplayName);

            var yAxis = new TheographAxisData();

            var plotBands = new List<TheographPlotBandData>();

            const int height = 100;
            yAxis.TickInterval = height;
            yAxis.Max = episodeGroups.Count() * height;
            yAxis.MinRange = episodeGroups.Count() * height;

            for (int i = 0; i < episodeGroups.Count(); i++)
            {
                var episodeGroup = episodeGroups.ElementAt(i);

                var series = new TheographChartSeries
                {
                    Name = episodeGroup.Key
                };

                var plotBand = new TheographPlotBandData
                {
                    From = i * height,
                    To = (i + 1) * height,
                    Color = (i % 2 == 0) ? "rgba(68, 170, 213, 0.1)" : null
                };

                plotBand.Label.Text = episodeGroup.Key;

                var orderedEpsiodes = episodeGroup.OrderBy(x => x.StartTime);

                foreach (var orderedEpsiode in orderedEpsiodes)
                {
                    int yPoint = ((i + 1) * height) - (height / 2);
                    string tooltipTitle = string.Format("{0}{1}",
                        episodeGroup.Key,
                        orderedEpsiode.TreatmentSite != null ? " at " + orderedEpsiode.TreatmentSite.Name : string.Empty);
                    StringBuilder sbToolTipText = new StringBuilder();
                    sbToolTipText.AppendFormat(
                        "Start: {0:ddd MMMM} <sup>{1}</sup> {0:yyyy HH:mm}",
                        orderedEpsiode.StartTime,
                        orderedEpsiode.StartTime.GetDayOrdinal());

                    if (orderedEpsiode.EndTime.HasValue)
                    {
                        sbToolTipText.AppendFormat(
                            "<br/>End: {0:ddd MMMM} <sup>{1}</sup> {0:yyyy HH:mm}", 
                            orderedEpsiode.EndTime.Value,
                            orderedEpsiode.EndTime.Value.GetDayOrdinal());
                    }
                    
                    // start point
                    var dataPoint = new TheographChartSeriesData
                    {
                        // * 1000 to conform with the Highcharts way of handling dates
                        EpisodeStartTimestamp = orderedEpsiode.StartTime.ToUnixTime() * 1000,
                        Y = yPoint
                    };

                    dataPoint.AddData("tooltipTitle", tooltipTitle);
                    dataPoint.AddData("tooltipText", sbToolTipText.ToString());

                    series.Data.Add(dataPoint);

                    // end point
                    if (orderedEpsiode.EndTime.HasValue)
                    {
                        dataPoint = new TheographChartSeriesData
                        {
                            // * 1000 to conform with the Highcharts way of handling dates
                            EpisodeStartTimestamp = orderedEpsiode.EndTime.Value.ToUnixTime() * 1000,
                            Y = yPoint
                        };

                        dataPoint.AddData("tooltipTitle", tooltipTitle);
                        dataPoint.AddData("tooltipText", sbToolTipText.ToString());

                        series.Data.Add(dataPoint);
                    }


                    // add null-value point to disconnect separate epsiodes
                    var dataNullPoint = new TheographChartSeriesData
                    {
                        EpisodeStartTimestamp = dataPoint.EpisodeStartTimestamp + 1,
                        Y = null
                    };

                    series.Data.Add(dataNullPoint);
                }

                this.Series.Add(series);
                plotBands.Add(plotBand);
            }

            yAxis.PlotBands = plotBands;
            this.Axes.Add("y", yAxis);
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
            this.PlotBands = new List<TheographPlotBandData>();
            this.Min = 0;
            this.Type = "category";
            this.Labels = new TheographAxisLabelData();
            this.Title = new TheographTitleData();
        }

        [JsonProperty(PropertyName = "plotBands")]
        public IList<TheographPlotBandData> PlotBands { get; set; }

        [JsonProperty(PropertyName = "min")]
        public int Min { get; set; }

        [JsonProperty(PropertyName = "max")]
        public int Max { get; set; }

        [JsonProperty(PropertyName = "tickInterval")]
        public int TickInterval { get; set; }

        [JsonProperty(PropertyName = "minRange")]
        public int MinRange { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public TheographAxisLabelData Labels { get; set; }

        [JsonProperty(PropertyName = "title")]
        public TheographTitleData Title { get; set; }
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
        public int From { get; set; }

        [JsonProperty(PropertyName = "to")]
        public int To { get; set; }

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