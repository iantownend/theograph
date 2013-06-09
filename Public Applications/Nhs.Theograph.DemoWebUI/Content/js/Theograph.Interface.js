/// <reference path="/Scripts/jquery-1.8.3-vsdoc.js" type="text/javascript" />
/// <reference path="/Scripts/jquery-1.8.3.js" type="text/javascript" />
/// <reference path="/Scripts/Common.js" type="text/javascript" />
/// <reference path="/Scripts/Common-vsdoc.js" type="text/javascript" />
/// <reference path="/Content/js/Theograph.js" type="text/javascript" />
var Theograph = Theograph || {
    Utils: {}
};

Theograph.Interface = (function () {
    "use strict";

    return {
        hideGraphStatus: function (container) {
            var jqChartLoader = container.getChartLoader();
            if (jqChartLoader.length > 0) {
                jqChartLoader.children('.graphLoader').remove();
                jqChartLoader.children().css('display', '');
            }
        },
        showGraphStatus: function (container, text, icon) {
            if (!container.hasOwnProperty('getChartLoader') || typeof container.getChartLoader !== 'function') {
                return;
            }

            text = isDef(text) ? text : 'Loading...';
            icon = isDef(icon) ? icon : 'graph-ajax-loader.gif';

            var jqChartLoader = container.getChartLoader();
            jqChartLoader.children('.graphLoader').remove();
            jqChartLoader.children().css('display', 'none');
            jqChartLoader.append('<div class="graphLoader"><img src="/content/images/radar/' + icon + '" /><p>' + text + '</p></div>');
        },
        drawChart: function dC(callback) {
            $.get('/Theograph/getData?nhsNumber=' + getParameterByName("nhsNumber"), null, function dCrA(chartOptionsProxy) {
                if (chartOptionsProxy === null) {
                    return;
                }

                var categories = null, series = null, colors = null, axes = null, tooltip = null, dataLabels = null, legend = null, hdSummary = null, ftSummary = null, events = null, chartData, template, nav = { count: 0, startAt: 0, maxItems: 10 }, customData = null;
                if (chartOptionsProxy.hasOwnProperty('categories') && Array.isArray(chartOptionsProxy.categories)) {
                    categories = chartOptionsProxy.categories;
                }

                if (chartOptionsProxy.hasOwnProperty('series') && Array.isArray(chartOptionsProxy.series)) {
                    series = chartOptionsProxy.series;
                }

                if (chartOptionsProxy.hasOwnProperty('colors') && Array.isArray(chartOptionsProxy.colors)) {
                    colors = chartOptionsProxy.colors;
                }

                if (chartOptionsProxy.hasOwnProperty('axes') && Array.isArray(chartOptionsProxy.axes)) {
                    axes = chartOptionsProxy.axes;
                }

                if (chartOptionsProxy.hasOwnProperty('events') && Array.isArray(chartOptionsProxy.events)) {
                    events = chartOptionsProxy.events;
                }

                tooltip = typeof chartOptionsProxy.tooltip === 'string' ? chartOptionsProxy.tooltip : null;
                dataLabels = typeof chartOptionsProxy.dataLabels === 'object' ? chartOptionsProxy.dataLabels : null;
                legend = typeof chartOptionsProxy.legend === 'object' ? chartOptionsProxy.legend : null;
                hdSummary = typeof chartOptionsProxy.hdSummary === 'string' ? chartOptionsProxy.hdSummary : null;
                ftSummary = typeof chartOptionsProxy.ftSummary === 'string' ? chartOptionsProxy.ftSummary : null;
                customData = typeof chartOptionsProxy.customData === 'object' ? chartOptionsProxy.customData : null;

                chartData = new Theograph.ChartData('Theograph', categories, series, colors, axes, tooltip, dataLabels, legend, hdSummary, ftSummary, customData, events);

                new Theograph.Chart('theographChart', chartData);
            });
        },
        loadChart: function (callback) {
            Theograph.Interface.drawChart(callback);
        }
    };
} ());