Apex.grid = { padding: { right: 0, left: 0 } };
Apex.dataLabels = { enabled: false };

var randomizeArray = function (array) {
    for (var i, temp, index = array.slice(), length = index.length; 0 !== length;) {
        i = Math.floor(Math.random() * length);
        temp = index[--length];
        index[length] = index[i];
        index[i] = temp;
    }
    return index;
};

$(document).ready(function () {
    "use strict";

    var e = [47, 45, 54, 38, 56, 24, 65, 31, 37, 39, 62, 51, 35, 41, 35, 27, 93, 53, 61, 27, 54, 43, 19, 46],
        r = [],
        t = 1;

    for (t; t <= 24; t++) {
        r.push("2018-09-" + t);
    }

    var a = ["#3688fc"];
    var o = $("#sales-spark").data("colors");
    if (o) {
        a = o.split(",");
    }

    var salesOptions = {
        chart: {
            type: "area",
            height: 172,
            sparkline: { enabled: true },
        },
        stroke: { width: 2, curve: "straight" },
        fill: { opacity: 0.2 },
        series: [{ name: "Hyper Sales", data: randomizeArray(e) }],
        xaxis: { type: "datetime" },
        yaxis: { min: 0 },
        colors: a,
        labels: r,
        title: {
            text: "$424,652",
            offsetX: 20,
            offsetY: 20,
            style: { fontSize: "24px" },
        },
        subtitle: {
            text: "Sales",
            offsetX: 20,
            offsetY: 55,
            style: { fontSize: "14px" },
        },
    };

    new ApexCharts(document.querySelector("#sales-spark"), salesOptions).render();

    var b = ["#0acf97"];
    var p = $("#profit-spark").data("colors");
    if (p) {
        b = p.split(",");
    }

    var profitOptions = {
        chart: {
            type: "bar",
            height: 172,
            sparkline: { enabled: true },
        },
        stroke: { width: 0, curve: "straight" },
        fill: { opacity: 0.5 },
        series: [{ name: "Net Profits", data: randomizeArray(e) }],
        xaxis: { crosshairs: { width: 1 } },
        yaxis: { min: 0 },
        colors: b,
        title: {
            text: "$135,965",
            offsetX: 20,
            offsetY: 20,
            style: { fontSize: "24px" },
        },
        subtitle: {
            text: "Profits",
            offsetX: 20,
            offsetY: 55,
            style: { fontSize: "14px" },
        },
    };

    new ApexCharts(document.querySelector("#profit-spark"), profitOptions).render();

    var spark1Colors = ["#734CEA"];
    var spark1DataColors = $("#spark1").data("colors");
    if (spark1DataColors) {
        spark1Colors = spark1DataColors.split(",");
    }

    var spark1Options = {
        chart: {
            type: "line",
            height: 100,
            sparkline: { enabled: true },
        },
        series: [{ data: [25, 66, 41, 59, 25, 44, 12, 36, 9, 21] }],
        stroke: { width: 4, curve: "smooth" },
        markers: { size: 0 },
        colors: spark1Colors,
    };

    var spark2Colors = ["#34bfa3"];
    var spark2DataColors = $("#spark2").data("colors");
    if (spark2DataColors) {
        spark2Colors = spark2DataColors.split(",");
    }

    var spark2Options = {
        chart: {
            type: "bar",
            height: 100,
            sparkline: { enabled: true },
        },
        series: [{ data: [12, 14, 2, 47, 32, 44, 14, 55, 41, 69] }],
        stroke: { width: 3, curve: "smooth" },
        markers: { size: 0 },
        colors: spark2Colors,
    };

    var spark3Colors = ["#f4516c"];
    var spark3DataColors = $("#spark3").data("colors");
    if (spark3DataColors) {
        spark3Colors = spark3DataColors.split(",");
    }

    var spark3Options = {
        chart: {
            type: "line",
            height: 100,
            sparkline: { enabled: true },
        },
        series: [{ data: [47, 45, 74, 32, 56, 31, 44, 33, 45, 19] }],
        stroke: { width: 4, curve: "smooth" },
        markers: { size: 0 },
        colors: spark3Colors,
    };

    var spark4Colors = ["#00c5dc"];
    var spark4DataColors = $("#spark4").data("colors");
    if (spark4DataColors) {
        spark4Colors = spark4DataColors.split(",");
    }

    var spark4Options = {
        chart: {
            type: "bar",
            height: 100,
            sparkline: { enabled: true },
        },
        plotOptions: {
            bar: {
                horizontal: false,
                endingShape: "rounded",
                columnWidth: "55%",
            },
        },
        series: [{ data: [15, 75, 47, 65, 14, 32, 19, 54, 44, 61] }],
        stroke: { width: 3, curve: "smooth" },
        markers: { size: 0 },
        colors: spark4Colors,
    };

    new ApexCharts(document.querySelector("#spark1"), spark1Options).render();
    new ApexCharts(document.querySelector("#spark2"), spark2Options).render();
    new ApexCharts(document.querySelector("#spark3"), spark3Options).render();
    new ApexCharts(document.querySelector("#spark4"), spark4Options).render();

    var campaignColors = ["#727cf5"];
    var campaignDataColors = $("#campaign-sent-chart").data("colors");
    if (campaignDataColors) {
        campaignColors = campaignDataColors.split(",");
    }

    var campaignOptions = {
        chart: {
            type: "bar",
            height: 60,
            sparkline: { enabled: true },
        },
        plotOptions: { bar: { columnWidth: "60%" } },
        colors: campaignColors,
        series: [{ data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54] }],
        labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
        xaxis: { crosshairs: { width: 1 } },
        tooltip: {
            fixed: { enabled: false },
            x: { show: false },
            y: {
                title: {
                    formatter: function (e) {
                        return "";
                    },
                },
            },
            marker: { show: false },
        },
    };

    new ApexCharts(document.querySelector("#campaign-sent-chart"), campaignOptions).render();

    var leadsColors = ["#727cf5"];
    var leadsDataColors = $("#new-leads-chart").data("colors");
    if (leadsDataColors) {
        leadsColors = leadsDataColors.split(",");
    }

    var leadsOptions = {
        chart: {
            type: "line",
            height: 60,
            sparkline: { enabled: true },
        },
        series: [{ data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54] }],
        stroke: { width: 2, curve: "smooth" },
        markers: { size: 0 },
        colors: leadsColors,
        tooltip: {
            fixed: { enabled: false },
            x: { show: false },
            y: {
                title: {
                    formatter: function (e) {
                        return "";
                    },
                },
            },
            marker: { show: false },
        },
    };

    new ApexCharts(document.querySelector("#new-leads-chart"), leadsOptions).render();

    var dealsColors = ["#727cf5"];
    var dealsDataColors = $("#deals-chart").data("colors");
    if (dealsDataColors) {
        dealsColors = dealsDataColors.split(",");
    }

    var dealsOptions = {
        chart: {
            type: "bar",
            height: 60,
            sparkline: { enabled: true },
        },
        plotOptions: { bar: { columnWidth: "60%" } },
        colors: dealsColors,
        series: [{ data: [12, 14, 2, 47, 42, 15, 47, 75, 65, 19, 14] }],
        labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
        xaxis: { crosshairs: { width: 1 } },
        tooltip: {
            fixed: { enabled: false },
            x: { show: false },
            y: {
                title: {
                    formatter: function (e) {
                        return "";
                    },
                },
            },
            marker: { show: false },
        },
    };

    new ApexCharts(document.querySelector("#deals-chart"), dealsOptions).render();

    var bookedRevenueColors = ["#727cf5"];
    var bookedRevenueDataColors = $("#booked-revenue-chart").data("colors");
    if (bookedRevenueDataColors) {
        bookedRevenueColors = bookedRevenueDataColors.split(",");
    }

    var bookedRevenueOptions = {
        chart: {
            type: "bar",
            height: 60,
            sparkline: { enabled: true },
        },
        plotOptions: { bar: { columnWidth: "60%" } },
        colors: bookedRevenueColors,
        series: [{ data: [47, 45, 74, 14, 56, 74, 14, 11, 7, 39, 82] }],
        labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
        xaxis: { crosshairs: { width: 1 } },
        tooltip: {
            fixed: { enabled: false },
            x: { show: false },
            y: {
                title: {
                    formatter: function (e) {
                        return "";
                    },
                },
            },
            marker: { show: false },
        },
    };

    new ApexCharts(document.querySelector("#booked-revenue-chart"), bookedRevenueOptions).render();
});
