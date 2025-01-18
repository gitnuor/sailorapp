

var colors = ['#FFFFFF', '#F0F0F0', '#F8F8F8', '#D4E4FE', '#DFF0D8', '#FFF3CD', '#007bff', '#6c757d', '#28a745',
    '#D4E4FE', '#DFF0D8', '#FFF3CD', '#FFF3CD', '#FFFFFF'];


function BindTabEventsForChart() {

    $('#tab_precosting_chart li').click(function (e) {

        e.preventDefault();

        var current_index = $(this).attr("tab_index");

        var next_tab = $(this).parent().find("li[tab_index=" + current_index + "]");

        if (next_tab.length > 0) {

            $('#tab_precosting_chart li').removeClass("active");

            $(this).addClass("active");

            $(this).parent().parent().find(".tab-pane").removeClass("active");

            $(this).parent().parent().find(".tab-pane[tabpane_index=" + current_index + "]").addClass("active");

            if (current_index == 1) {
                drawSeasonWisePieChart(1, $("#rpt_fiscal_year_id").val());
            }
            else if (current_index == 2) {
                drawMonthWisePieChart(2, $("#rpt_fiscal_year_id").val());
            }
            else if (current_index == 3) {
                drawOutletWisePieChart(3, $("#rpt_fiscal_year_id").val());
            }
            else if (current_index == 4) {
                drawDistrictWisePieChart(4, $("#rpt_fiscal_year_id").val());
            }
            else if (current_index == 5) {
                drawOutletWiseChart(5, $("#rpt_fiscal_year_id").val());
            }
            else if (current_index == 6) {
                $("#dvMonthCompare").css("display", "block");
                MonthWiseCompareChart();
            }//
            else if (current_index == 7) {
                drawMonthWiseOutletChart(7, $("#rpt_fiscal_year_id").val());
            }
        }

    });

}
function cleanOutletName(outletName) {
    return outletName.replace(/\n/g, '').trim();
}
function drawOutletWiseChart(dvchartid, fiscal_year_id) {
    var container = $("#tab_bar_style .tab-pane[tabpane_index=" + dvchartid + "]").find(".dvchart")[0];

    var dataTableArray = [];

    ajaxGetHandler("/Dashboard/GetOutlateData?fiscal_year_id=" + fiscal_year_id, null, function (data) {
        data.forEach(function (item) {
            var outletName = cleanOutletName(item.outlet_name);
            var randomIndex = Math.floor(Math.random() * colors.length); //colors[randomIndex]
            dataTableArray.push([outletName, item.total_sales, colors[randomIndex]]);
        });

        dataTableArray.unshift(["Element", "Outlet", { role: "style" }]);
        var data = google.visualization.arrayToDataTable(dataTableArray);

        var view = new google.visualization.DataView(data);

        view.setColumns([0, 1,
            {
                calc: "stringify",
                sourceColumn: 1,
                type: "string",
                role: "annotation"
            },
            2]);

        var options = {
            title: "Outlet Wise Business Plan",
            //width: 1500,

            height: 500,
            bar: { groupWidth: "70%" },
            legend: { position: "none" },
        };
        var chart = new google.visualization.ColumnChart(container);
        chart.draw(view, options);

    });

}
function ViewDataAnalysis(btn) {

    try {
        //setTimeout(function () {
        showLoader("Loading..........");
        //}, 100);

        ajaxGetHandler("/BusinessPlan/ViewAnalysisData", null, function (data) {
            hideLoader();

            $('#modalcontent-common').html(data);
            $('#modalcontainer_common').modal({ backdrop: 'static', keyboard: false });
            $('#modalcontainer_common').modal("show");


            google.charts.load('current', { 'packages': ['line', 'corechart', 'timeline', 'bar'] });

            $("#rpt_compare_with_fiscal_year_id").change(function () {
                MonthWiseCompareChart("dvchart", $("#rpt_fiscal_year_id").val(), $("#rpt_compare_with_fiscal_year_id").val());
            });
            $("#rpt_fiscal_year_id").change(function () {
                LoadDataChart();
            });


            BindTabEventsForChart();

            setTimeout(function () {
                LoadDataChart();
            }, 500);

        }, true);


    } catch (e) {
        hideLoader();
    }
}

function LoadDataChart() {

    drawSeasonWisePieChart(1, $("#rpt_fiscal_year_id").val());

}

function drawMonthWiseOutletChart(dvchartid, fiscal_year_id) {

    var dataTableArray = [];

    ajaxGetHandler("/Dashboard/GetMonthWiseOutletSummaryData?fiscal_year_filter=" + fiscal_year_id, null, function (result) {

        var header_array = ['Month', { role: "style" }];

        for (let i = 1; i <= result[0].allOutletList.length; i++) {
            let objoutlet = result[0].allOutletList[i - 1];
            header_array.splice(i, 0, objoutlet.outlet_name);
        }
        dataTableArray.push(header_array);
        result.forEach(function (item) {

            var randomIndex = Math.floor(Math.random() * colors.length); //colors[randomIndex]

            var data_row = [item.month_name, colors[randomIndex]];

            for (let i = 1; i <= result[0].allOutletList.length; i++) {

                let objsingleoutlet = result[0].allOutletList[i - 1];

                var gross_total = 0;

                if (item.outletList.filter(p => p.outlet_id == objsingleoutlet.outlet_id).length > 0) {
                    gross_total = item.outletList.filter(p => p.outlet_id == objsingleoutlet.outlet_id)[0].outlet_gross_sales;
                }

                data_row.splice(i, 0, gross_total);
            }

            dataTableArray.push(data_row);

        });

        var data = google.visualization.arrayToDataTable(dataTableArray);

        var options = {
            title: '',
            is3D: true,
            legend: { position: 'top', maxLines: 3 },
            bar: { groupWidth: '75%' },
            isStacked: true,
        };

        var container = $("#tab_bar_style .tab-pane[tabpane_index=" + dvchartid + "]").find(".dvchart")[0];
        var chart = new google.visualization.ColumnChart(container);
        chart.draw(data, options);

    });

}
function drawDistrictWisePieChart(dvchartid, fiscal_year_id) {

    var dataTableArray = [];

    ajaxGetHandler("/Dashboard/GetDistrictWiseSummaryData?fiscal_year_filter=" + fiscal_year_id, null, function (result) {

        // dataTableArray.push(['District', "Sales", { role: "style" }]);

        var header_array = ['District', { role: "style" }];

        for (let i = 1; i <= result[0].allOutletList.length; i++) {
            let objoutlet = result[0].allOutletList[i - 1];
            header_array.splice(i, 0, objoutlet.outlet_name);
        }
        dataTableArray.push(header_array);
        result.forEach(function (item) {
            var randomIndex = Math.floor(Math.random() * colors.length); //colors[randomIndex]

            var data_row = [item.name, colors[randomIndex]];

            for (let i = 1; i <= result[0].allOutletList.length; i++) {

                let objsingleoutlet = result[0].allOutletList[i - 1];

                var gross_total = 0;

                if (item.outletList.filter(p => p.outlet_id == objsingleoutlet.outlet_id).length > 0) {
                    gross_total = item.outletList.filter(p => p.outlet_id == objsingleoutlet.outlet_id)[0].outlet_gross_sales;
                }

                data_row.splice(i, 0, gross_total);
            }

            dataTableArray.push(data_row);

        });

        var data = google.visualization.arrayToDataTable(dataTableArray);

        var options = {
            title: '',
            is3D: true,
            legend: { position: 'top', maxLines: 3 },
            bar: { groupWidth: '75%' },
            isStacked: true,
        };

        var container = $("#tab_bar_style .tab-pane[tabpane_index=" + dvchartid + "]").find(".dvchart")[0];
        var chart = new google.visualization.ColumnChart(container);
        chart.draw(data, options);

    });

}
function formatDate(dateString) {
    var parts = dateString.split('T')[0].split('-');
    return new Date(parts[0], parts[1] - 1, parts[2]);
}
function drawSeasonWisePieChart(dvchartid, fiscal_yr_id) {

    var container = $("#tab_bar_style .tab-pane[tabpane_index=" + dvchartid + "]").find(".dvchart")[0];
    var chart = new google.visualization.Timeline(container);
    var dataTable = new google.visualization.DataTable();

    dataTable.addColumn({ type: 'string', id: 'Term' });
    dataTable.addColumn({ type: 'string', id: 'Name' });
    dataTable.addColumn({ type: 'date', id: 'Start' });
    dataTable.addColumn({ type: 'date', id: 'End' });

    ajaxGetHandler("/Dashboard/GetCalenderData?fiscal_year_id=" + fiscal_yr_id, null, function (data) {

        var dataarr = [];
        data.forEach(function (item) {
            var eventTitle = item.event_title.replace(/\n/g, ' (').replace('(', ' (').replace(')', ')');
            var formattedStartDate = formatDate(item.start_date);
            var formattedEndDate = formatDate(item.end_date);
            var item = [item.row_number.toString(), eventTitle, formattedStartDate, formattedEndDate]

            dataarr.push(item);

        });

        dataTable.addRows(dataarr);
        chart.draw(dataTable);

    });


}
function MonthWiseCompareChart() {

    var fiscal_yr_id = $("#rpt_fiscal_year_id").val();
    var compare_with_fiscal_year_id = $("#rpt_compare_with_fiscal_year_id").val();

    var dataTableArray = [];

    ajaxGetHandler("/Dashboard/GetMonthWiseCompareData?fiscal_year_filter=" + fiscal_yr_id + "&compare_with_fiscal_year_id=" + compare_with_fiscal_year_id, null, function (response) {

        google.charts.setOnLoadCallback(function () {
            drawMonthWiseCompareChart(response);
        });
    });

}
function drawMonthWiseCompareChart(response) {

    var dvchartid = "6";

    var container = $("#tab_bar_style .tab-pane[tabpane_index=" + dvchartid + "]").find(".dvchart")[0];

    if (response.length < 24) {
        $(container).html('');
    }

    var dataTableArray = [];

    var result = response;

    var uniqueYears = {};

    response.forEach(obj => {
        uniqueYears[obj.year_name] = true;
    });

    var header_array = [
        'Month',
        Object.keys(uniqueYears)[0],
        { type: 'string', role: 'annotation' },
        Object.keys(uniqueYears)[1],
        { type: 'string', role: 'annotation' }
    ];

    dataTableArray.push(header_array);

    var randomIndex = Math.floor(Math.random() * colors.length); //colors[randomIndex]

    for (let i = 1; i <= 12; i++) {

        var val1 = 0, val2 = 0; var monthname = "";

        if (result.filter(p => p.month_id == i && p.year_name == Object.keys(uniqueYears)[0]).length > 0) {
            val1 = result.filter(p => p.month_id == i && p.year_name == Object.keys(uniqueYears)[0])[0].total_gross;
            monthname = result.filter(p => p.month_id == i && p.year_name == Object.keys(uniqueYears)[0])[0].month_name;
        }

        if (result.filter(p => p.month_id == i && p.year_name == Object.keys(uniqueYears)[1]).length > 0) {
            val2 = result.filter(p => p.month_id == i && p.year_name == Object.keys(uniqueYears)[1])[0].total_gross;
            monthname = result.filter(p => p.month_id == i && p.year_name == Object.keys(uniqueYears)[1])[0].month_name;
        }

        var data_row = [monthname, val1, (val1 / 100000000).toFixed(2) + ' Cr', val2, (val2 / 100000000).toFixed(2) + ' Cr'];

        dataTableArray.push(data_row);
    }

    var data = google.visualization.arrayToDataTable(dataTableArray);

    var options = {
        //width: 500,
        //legend: { position: 'none' },
        annotations: {
            alwaysOutside: true
        },
        height: 500,
        chart: {
            title: 'Company Sales Comparison',
            subtitle: 'Year ' + Object.keys(uniqueYears)[0] + ' - ' + Object.keys(uniqueYears)[1]
        },
      
        chartArea: {
            backgroundColor: {
                //fill: '#FF0000',
                //fillOpacity: 0.1
            },
        },

        backgroundColor: {
            //fill: '#FF0000',
            //fillOpacity: 0.8
        },
    }

    var classicChart = new google.visualization.BarChart(container);
    classicChart.draw(data, options);
    
}
function drawMonthWisePieChart(dvchartid, fiscal_year_id) {

    var dataTableArray = [];
    ajaxGetHandler("/Dashboard/GetMonthlyWiseSummaryData?fiscal_year_filter=" + fiscal_year_id, null, function (data) {

        dataTableArray.push(['Month', "Sales"]);
        data.forEach(function (item) {

            dataTableArray.push([item.month_name, parseFloat(item.total_gross)]);
        });

        var data = google.visualization.arrayToDataTable(dataTableArray);

        var options = {
            title: '',
            is3D: true,
        };
        var container = $("#tab_bar_style .tab-pane[tabpane_index=" + dvchartid + "]").find(".dvchart")[0];
        var chart = new google.visualization.PieChart(container);

        chart.draw(data, options);

    });

}
function drawOutletWisePieChart(dvchartid, fiscal_year_id) {

    var dataTableArray = [];
    ajaxGetHandler("/Dashboard/GetOutletWiseSummaryData?fiscal_year_filter=" + fiscal_year_id, null, function (data) {

        dataTableArray.push(['Outlet', "Sales"]);
        data.forEach(function (item) {
            dataTableArray.push([item.outlet_name, parseFloat(item.total_gross)]);
        });

        var data = google.visualization.arrayToDataTable(dataTableArray);

        var options = {
            title: '',
            is3D: true,
        };

        var container = $("#tab_bar_style .tab-pane[tabpane_index=" + dvchartid + "]").find(".dvchart")[0];
        var chart = new google.visualization.PieChart(container);
        chart.draw(data, options);

    });

}
function ViewMonthWiseData(btn) {

    try {
        setTimeout(function () {
            showLoader("Loading..........");
        }, 100);

        ajaxGetHandler("/BusinessPlan/ViewMonthWiseData?fiscal_year_filter=" + $("#fiscal_year_id").val(), null, function (data) {
            hideLoader();


            $('#modalcontent-common').html(data);
            $('#modalcontainer_common').modal({ backdrop: 'static', keyboard: false });
            $('#modalcontainer_common').modal("show");

            ConvertNumberToCurrency();

            drawMonthWisePieChart("dvmonthwisechart", $("#fiscal_year_id").val());

        }, true);


    } catch (e) {
        hideLoader();
    }
}
function ViewOutletWiseData(btn) {

    try {
        setTimeout(function () {
            showLoader("Loading..........");
        }, 100);

        ajaxGetHandler("/BusinessPlan/ViewOutletWiseData?fiscal_year_filter=" + $("#fiscal_year_id").val(), null, function (data) {
            hideLoader();

            $('#modalcontent-common').html(data);
            $('#modalcontainer_common').modal({ backdrop: 'static', keyboard: false });
            $('#modalcontainer_common').modal("show");

            ConvertNumberToCurrency();

            drawOutletWisePieChart("dvoutletwisechart", $("#fiscal_year_id").val());

        }, true);


    } catch (e) {
        hideLoader();
    }
}
function closePopup() {
    $('#modalcontent-common').html("");

    $('#modalcontainer_common').modal("hide");
}
