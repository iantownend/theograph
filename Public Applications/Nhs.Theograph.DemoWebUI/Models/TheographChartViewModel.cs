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

    public class TheographChartViewModel
    {
        public TheographChartViewModel(PatientEpsiodes patientEpisodes)
        {
            var episodeGroups = patientEpisodes.Episodes.GroupBy(x => x.EpisodeType.Value);

            this.Series = new List<TheographChartSeries>();

            int yvalue = 50;

            foreach (var episodeGroup in episodeGroups)
            {
                var series = new TheographChartSeries
                {
                    Name = episodeGroup.Key
                };

                var orderedEpsiodes = episodeGroup.OrderBy(x => x.StartTime);

                foreach (var orderedEpsiode in orderedEpsiodes)
                {
                    var dataPoint = new TheographChartSeriesData
                    {
                        // * 1000 to conform with the Highcharts way of handling dates
                        EpisodeStartTimestamp = orderedEpsiode.StartTime.ToUnixTime()* 1000,
                        Y = yvalue
                    };

                    series.Data.Add(dataPoint);

                    if (orderedEpsiode.EndTime.HasValue)
                    {
                        dataPoint = new TheographChartSeriesData
                        {
                            // * 1000 to conform with the Highcharts way of handling dates
                            EpisodeStartTimestamp = orderedEpsiode.EndTime.Value.ToUnixTime() * 1000,
                            Y = yvalue
                        };
                        series.Data.Add(dataPoint);
                    }


                    // add null-value spacer to disconnect the points
                    var dataNullPoint = new TheographChartSeriesData
                    {
                        EpisodeStartTimestamp = dataPoint.EpisodeStartTimestamp + 1,
                        Y = null
                    };

                    series.Data.Add(dataNullPoint);
                }

                this.Series.Add(series);
                yvalue += 100;
            }
        }

        [JsonProperty(PropertyName = "series")]
        public IList<TheographChartSeries> Series { get; set; }
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
            //// this.Data = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the episode start timestamp.
        /// </summary>
        [JsonProperty(PropertyName = "x")]
        public long EpisodeStartTimestamp { get; set; }

        [JsonProperty(PropertyName = "y")]
        public int? Y { get; set; }

        //// [JsonProperty(PropertyName = "data")]
        //// public IDictionary<string, string> Data { get; private set; }
    }
}