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
    using Nhs.Theograph.Core.Episode;

    public static class TheographHighchartAdaptors
    {
        internal static TheographChartViewModel GetPatientEpsiodesChart(PatientEpisodes patientEpisodes)
        {
            TheographChartViewModel output = new TheographChartViewModel();

            var episodeGroups = patientEpisodes.Episodes.GroupBy(x => x.EpisodeType.DisplayName);
            var xAxis = new TheographAxisData
            {
                Type = "datetime",
                Min = DateTime.Today.AddYears(-1).ToHighchartsTime(),
                Max = DateTime.Today.AddDays(1).ToHighchartsTime(),
                MinRange = 1000 * 60 * 60 * 24, // max zoom of 1 day,
            };

            xAxis.DateTimeLabelFormats = new TheographDataTimeLabelFormatData
            {
                Month = "%b %y",
                Week = "%e %b",
                Day = "%e %b"
            };

            var yAxis = new TheographAxisData();

            const int height = 100;
            yAxis.TickInterval = height;
            yAxis.Max = episodeGroups.Count() * height;
            yAxis.MinRange = episodeGroups.Count() * height;
            yAxis.Labels = new TheographAxisLabelData();
            yAxis.Title = new TheographTitleData();

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
                        EpisodeStartTimestamp = orderedEpsiode.StartTime.ToHighchartsTime(),
                        Y = yPoint
                    };

                    dataPoint.AddData("tooltipTitle", tooltipTitle);
                    dataPoint.AddData("tooltipText", sbToolTipText.ToString());
                    dataPoint.AddData("episodeId", orderedEpsiode.EpisodeId.Value);

                    series.Data.Add(dataPoint);

                    // end point
                    if (orderedEpsiode.EndTime.HasValue)
                    {
                        dataPoint = new TheographChartSeriesData
                        {
                            // * 1000 to conform with the Highcharts way of handling dates
                            EpisodeStartTimestamp = orderedEpsiode.EndTime.Value.ToHighchartsTime(),
                            Y = yPoint
                        };

                        dataPoint.AddData("tooltipTitle", tooltipTitle);
                        dataPoint.AddData("tooltipText", sbToolTipText.ToString());
                        dataPoint.AddData("episodeId", orderedEpsiode.EpisodeId.Value);

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

                output.Series.Add(series);
                yAxis.AddPlotBand(plotBand);
            }

            output.Axes.Add("x", xAxis);
            output.Axes.Add("y", yAxis);

            return output;
        }

        internal static TheographChartViewModel GetPatientEpsiodeEventsChart(PatientEpisode patientEpisode)
        {
            TheographChartViewModel output = new TheographChartViewModel();

            if (patientEpisode.Episode.Events.Count == 0)
            {
                return output;
            }

            var eventGroups = patientEpisode.Episode.Events.GroupBy(x => x.EventType.DisplayName);

            var maxEventEndTime = patientEpisode.Episode.Events.Select(x => x.StartTime)
                .Union(patientEpisode.Episode.Events.Where(x => x.EndTime.HasValue).Select(x => x.EndTime.Value))
                .Max();

            DateTime start = patientEpisode.Episode.StartTime;
            DateTime end = start;

            // set a sensible end time
            if (patientEpisode.Episode.EndTime.HasValue)
            {
                end = patientEpisode.Episode.EndTime.Value;
            }
            else
            {
                end = maxEventEndTime;
            }

            var xAxis = new TheographAxisData
            {
                Type = "datetime",
                Min = start.AddHours(-1).ToHighchartsTime(),
                Max = end.AddHours(1).ToHighchartsTime(),
                TickInterval = 1000 * 60 * 30,
                MinRange = 1000 * 60 * 60, // max zoom of 1 hour
            };

            var yAxis = new TheographAxisData();
            var yPlotBands = new List<TheographPlotBandData>();

            const int height = 100;
            yAxis.TickInterval = height;
            yAxis.Max = eventGroups.Count() * height;
            yAxis.MinRange = eventGroups.Count() * height;
            yAxis.Labels = new TheographAxisLabelData();
            yAxis.Title = new TheographTitleData();

            for (int i = 0; i < eventGroups.Count(); i++)
            {
                var eventGroup = eventGroups.ElementAt(i);

                var series = new TheographChartSeries
                {
                    Name = eventGroup.Key
                };

                var plotBand = new TheographPlotBandData
                {
                    From = i * height,
                    To = (i + 1) * height,
                    Color = (i % 2 == 0) ? "rgba(68, 170, 213, 0.1)" : null
                };

                plotBand.Label.Text = eventGroup.Key;

                var orderedEvents = eventGroup.OrderBy(x => x.StartTime);

                foreach (var orderedEvent in orderedEvents)
                {
                    int yPoint = ((i + 1) * height) - (height / 2);

                    StringBuilder sbToolTipText = new StringBuilder();

                    string tooltipTitle = eventGroup.Key;

                    IResultsEvent resultsEvent = orderedEvent as IResultsEvent;

                    if (resultsEvent != null)
                    {
                        if (resultsEvent.Results.Count > 1)
                        {
                            tooltipTitle = string.Format("{0} : {1}",
                                eventGroup.Key,
                                orderedEvent.Code.DisplayName);

                            foreach (var result in resultsEvent.Results)
                            {
                                sbToolTipText.AppendFormat(
                                    "{0}{1}",
                                    sbToolTipText.Length == 0 ? string.Empty : "<br/>",
                                    result.GetDisplayValue());
                            }
                        }
                        else if (resultsEvent.Results.Count == 1)
                        {
                            sbToolTipText.AppendFormat(
                                "{0} : {1}",
                                orderedEvent.Code.DisplayName,
                                resultsEvent.Results[0].GetDisplayValue());
                        }
                    }
                    
                    sbToolTipText.AppendFormat(
                        "{0}{1:ddd MMMM} <sup>{2}</sup> {1:yyyy HH:mm}",
                        sbToolTipText.Length == 0 ? string.Empty : "<br/>",
                        orderedEvent.StartTime,
                        orderedEvent.StartTime.GetDayOrdinal());

                    if (orderedEvent.EndTime.HasValue)
                    {
                        sbToolTipText.AppendFormat(
                            " to {0:ddd MMMM} <sup>{1}</sup> {0:yyyy HH:mm}",
                            orderedEvent.EndTime.Value,
                            orderedEvent.EndTime.Value.GetDayOrdinal());
                    }

                    // start point
                    var dataPoint = new TheographChartSeriesData
                    {
                        // * 1000 to conform with the Highcharts way of handling dates
                        EpisodeStartTimestamp = orderedEvent.StartTime.ToHighchartsTime(),
                        Y = yPoint
                    };

                    dataPoint.AddData("tooltipTitle", tooltipTitle);
                    dataPoint.AddData("tooltipText", sbToolTipText.ToString());
                    dataPoint.AddData("episodeId", orderedEvent.EpisodeId.Value);

                    series.Data.Add(dataPoint);

                    // end point
                    if (orderedEvent.EndTime.HasValue)
                    {
                        dataPoint = new TheographChartSeriesData
                        {
                            // * 1000 to conform with the Highcharts way of handling dates
                            EpisodeStartTimestamp = orderedEvent.EndTime.Value.ToUnixTime() * 1000,
                            Y = yPoint
                        };

                        dataPoint.AddData("tooltipTitle", tooltipTitle);
                        dataPoint.AddData("tooltipText", sbToolTipText.ToString());
                        dataPoint.AddData("episodeId", orderedEvent.EpisodeId.Value);

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

                output.Series.Add(series);
                yAxis.AddPlotBand(plotBand);
            }

            var xPlotBands = new List<TheographPlotBandData>();
            xAxis.AddPlotBand(new TheographPlotBandData
            {
                From = start.ToHighchartsTime(),
                To = end.ToHighchartsTime(),
                Color = "rgba(190, 220, 240, 0.35)"
            });

            output.Axes.Add("x", xAxis);
            output.Axes.Add("y", yAxis);

            return output;
        }
    }
}