/// <reference path="/Scripts/jquery-2.0.1.js" />
/// <reference path="/Scripts/jquery-2.0.1-vsdoc.js" />
/// <reference path="/Scripts/jquery.validate.js" />
/// <reference path="/Scripts/jquery.validate.unobtrusive.js" />
/// <reference path="/Scripts/knockout-2.1.0.debug.js" />
/// <reference path="/Scripts/modernizr-2.5.3.js" />
/// <reference path="/Scripts/highcharts.js" />
var Theograph = Theograph || {
    Utils: {}
};

function isDef(param) {
    "use strict";
    return param !== undefined;
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

Theograph.Utils = (function () {
    "use strict";

    var formatDataReplace = function theoRUfDr(data, formatTooltipString) {
        var match = formatTooltipString.match(/\{data\.([A-Za-z0-9]+)(\|[A-Za-z0-9]+)?\}/),
            r, i, len;

        while (match !== null) {
            r = '';

            if (isDef(match[2])) {
                match[2] = match[2].substring(1);
            }

            for (i = 0, len = data.length; i < len; i = i + 1) {
                if (data[i].k === match[1]) {
                    r = isDef(match[2]) && match[2] === 'money'
                        ? '\u00a3' + parseFloat(data[i].v).moneyToString()
                        : data[i].v;
                }
            }

            formatTooltipString = formatTooltipString.replace(/\{data\.([A-Za-z0-9\|]+)\}/, r);
            match = formatTooltipString.match(/\{data\.([A-Za-z0-9]+)(\|[A-Za-z0-9]+)?\}/);
        }

        return formatTooltipString;
    },
    ajaxError = function theoRUaE(jqXHR) {
        if (isDef(jqXHR) && isDef(jqXHR.status) && jqXHR.status === 500) {
            return false;
        }
    };

    return {
        getValueByKey: function theoRUgVbK(data, key) {
            if (isDef(data) && isDef(key)) {
                var value = null, i, len;

                for (i = 0, len = data.length; i < len; i = i + 1) {
                    if (!!data[i].k && data[i].k === key) {
                        value = data[i].v;
                        break;
                    }
                }

                return value;
            }
            return null;
        },
        executeFunctionByName: function theoRUeFbN(functionName, context/*, args */) {
            var args = Array.prototype.slice.call(arguments).splice(2, arguments.length - 2),
                namespaces = functionName.split('.'),
                func = namespaces.pop(),
                i = 0;

            for (i = 0; i < namespaces.length; i = i + 1) {
                context = context[namespaces[i]];
            }

            return context[func].apply(this, args);
        },
        formatSummary: function theoRUfS(that, summaryString) {
            // replace data properties
            if (!!that.customData) {
                summaryString = formatDataReplace(that.customData, summaryString);
            }

            // replace series-related properties
            return '<p><small>' + summaryString
                .replace(/\|\|\|/g, '</small><br><small>')
                .replace(/\{dataTotal\}/g, that.getDataTotal().moneyToString())
                + '</small></p>';
        },
        formatTooltip: function theoRUfTt(that, formatTooltipString) {
            // replace data properties
            if (!!that.point && !!that.point.data) {
                formatTooltipString = formatDataReplace(that.point.data, formatTooltipString);
            }

            // replace series-related properties
            return formatTooltipString
                .replace(/\[(#[0-9A-F]{6})\|/gi, '<span style="color:$1">')
                .replace(/\[/g, '<span>')
                .replace(/\]/g, '</span>')
                .replace(/\|\|\|/g, '<br>')
                .replace(/\{xTick\}/g, that.x)
                .replace(/\{seriesName\}/g, that.series.name)
                .replace(/\{pointName\}/g, that.point.name)
                .replace(/\{val\}/g, that.y)
                .replace(/\{val\|money\}/g, '\u00a3' + that.y.moneyToString())
                .replace(/\{totalVal\}/g, that.point.stackTotal)
                .replace(/\{totalVal\|money\}/g, '\u00a3' + (isDef(that.point.stackTotal) ? that.point.stackTotal.moneyToString() : '0'));
        },
        formatAxis: function theoRUfA(that, formatAxisString) {
            return formatAxisString
                .replace(/\[(#[0-9A-F]{6})\|/gi, '<span style="color:$1">')
                .replace(/\[/g, '<span>')
                .replace(/\]/g, '</span>')
                .replace(/\|\|\|/g, '<br>')
                .replace(/\{val\}/g, that.value)
                .replace(/\{val\|money\}/g, '\u00a3' + that.value.moneyToString())
                .replace(/\{val\|moneyshort\}/g, '\u00a3' + that.value.moneyToString({ isShort: true }));
        },
        cleanProp: function theoRUcP(text) {
            return text.replace(/^(.+)(\s\([^\)]+\))$/, "$1");
        },
    };
} ());

Theograph.ChartData = function theoData(name, categories, seriesData, seriesColors, axes, tooltip, dataLabels, legend, headerSummaryText, footerSummaryText, customData, events) {
    "use strict";

    if (!(this instanceof Theograph.ChartData)) {
        return new Theograph.ChartData(name, categories, seriesData, seriesColors, axes, tooltip, dataLabels, legend, headerSummaryText, footerSummaryText, events);
    }
    
    name = isDef(name) ? name : 'Unnamed Chart';
    categories = isDef(categories) ? categories : null;
    seriesData = isDef(seriesData) ? seriesData : {};
    seriesColors = isDef(seriesColors) && seriesColors !== null ? seriesColors : [];
    axes = isDef(axes) && axes !== null ? axes : [];
    tooltip = isDef(tooltip) && tooltip !== null ? tooltip : [];
    dataLabels = isDef(dataLabels) && dataLabels !== null ? dataLabels : null;
    legend = isDef(legend) && legend !== null ? legend : null;
    headerSummaryText = isDef(headerSummaryText) ? headerSummaryText : null;
    footerSummaryText = isDef(footerSummaryText) ? footerSummaryText : null;
    customData = isDef(customData) ? customData : null;
    events = isDef(events) ? events : [];
    
    return {
        customData: customData,
        getName: function () {
            return name;
        },
        setName: function (newName) {
            name = newName;
        },

        getCategories: function () {
            return categories;
        },
        setCategories: function (newCategories) {
            categories = newCategories;
        },

        getSeriesData: function () {
            return seriesData;
        },
        setSeriesData: function (newSeriesData) {
            seriesData = newSeriesData;
        },

        getSeriesColors: function () {
            return seriesColors;
        },
        setSeriesColors: function (newSeriesColors) {
            seriesColors = newSeriesColors;
        },

        getAxes: function () {
            return axes;
        },
        setAxes: function (newAxes) {
            axes = newAxes;
        },

        getTooltip: function () {
            return tooltip;
        },
        setTooltip: function (newTooltip) {
            tooltip = newTooltip;
        },

        getDataLabels: function () {
            return dataLabels;
        },
        setDataLabels: function (newDataLabels) {
            dataLabels = newDataLabels;
        },

        getLegend: function () {
            return legend;
        },
        setLegend: function (newLegend) {
            legend = newLegend;
        },

        getHeaderSummaryText: function () {
            return headerSummaryText;
        },
        setHeaderSummaryText: function (newHeaderSummaryText) {
            headerSummaryText = newHeaderSummaryText;
        },

        getFooterSummaryText: function () {
            return footerSummaryText;
        },
        setFooterSummaryText: function (newFooterSummaryText) {
            footerSummaryText = newFooterSummaryText;
        },

        getEvents: function () {
            return events || [];
        },
        setEvents: function (newEvents) {
            events = newEvents || [];
        },

        getAutoPointInterval: function (width) {
            return categories !== null ? Math.floor((categories.length * 35 / width) + 1) : 1;
        },

        getDataTotal: function (dataSeriesName) {
            var total = 0, v = 0, w = 0, lenV = 0, lenW = 0, dataVal = null;
            dataSeriesName = isDef(dataSeriesName) ? dataSeriesName : '';

            if (seriesData === null || seriesData.length === 0) {
                return null;
            }

            for (v = 0, lenV = seriesData.length; v < lenV; v = v + 1) {
                if (seriesData[v] === null) {
                    break;
                }

                if (seriesData[v].hasOwnProperty("data") && (dataSeriesName === '' || (seriesData[v].hasOwnProperty("name") && seriesData[v].name === dataSeriesName))) {

                    for (w = 0, lenW = seriesData[v].data.length; w < lenW; w = w + 1) {
                        dataVal = seriesData[v].data[w];
                        if (Array.isArray(dataVal)) {
                            total += dataVal[1];
                        } else if (typeof dataVal === "object") {
                            total += dataVal.y;
                        } else {
                            total += dataVal;
                        }
                    }
                }
            }

            return total;
        }
    };
};

Theograph.Chart = function theoChart(chartContainer, chartOptions) {
    "use strict";

    if (!(this instanceof Theograph.Chart)) {
        return new Theograph.Chart(chartContainer, chartOptions);
    }
    
    var i = 0, v = 0, xaxis = null, yaxis = null, yAxisLabelFormatter = null,
        showXaxisLabels = true, //// chartOptions.getCategories() !== null,
        axes = chartOptions.getAxes(),
        tooltipFormatter = typeof chartOptions.getTooltip() === 'string'
            ? function () { return Theograph.Utils.formatTooltip(this, chartOptions.getTooltip()); }
            : function () { return '<b>' + (this.point.name || this.x) + '</b><br/>' + this.y; };
    
    return new Highcharts.Chart({
        chart: {
            renderTo: chartContainer,
            defaultSeriesType: 'line',
            zoomType: 'x'
        },
        title: {
            text: null
        },
        xAxis: {
            type: 'datetime',
            min: Date.UTC(2012, 5, 5),
            max: Date.UTC(2013, 5, 5),
            minRange: 1000 * 3600 * 24, // max zoom of 1 hr,
            dateTimeLabelFormats: {
                month: '%b %y',
                day: '%e %b'
            }
        },
        yAxis: {
            min: 0,
            plotBands: [{
                from: 0,
                to: 100,
                color: 'rgba(68, 170, 213, 0.1)',
                label: {
                    text: 'A & E',
                    style: {
                        color: '#606060'
                    }
                }
            }, {
                from: 100,
                to: 200,
                label: {
                    text: 'Inpatient',
                    style: {
                        color: '#606060'
                    }
                }
            }, {
                from: 200,
                to: 300,
                color: 'rgba(68, 170, 213, 0.1)',
                label: {
                    text: 'Outpatient',
                    style: {
                        color: '#606060'
                    }
                }
            }],
            tickInterval: 100,
            type: 'category',
            max: 300,
            minRange: 300,
            labels: {
                enabled: false
            }
        },
        tooltip: { 
            formatter: function() {
                return '<b>' + new Date(this.point.x).toString() + '</b><br/>' + this.point.series.name + (this.point.org !== undefined ? ' at ' + this.point.org : '');
            } 
        },
        series: chartOptions.getSeriesData()
    });
};