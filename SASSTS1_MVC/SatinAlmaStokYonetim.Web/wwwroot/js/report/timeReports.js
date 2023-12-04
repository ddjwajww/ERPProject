let reports = [];
let selectedReport;
function createTable(data) {
    if (data.length > 0) {
        html = ``;
        html += `<table class="table  table-sm w-100 dt-responsive nowrap" id="customDatatable">`;
        html += `  <thead>`;
        html += `    <tr>`;
        html += `      <th>${let_queue}</th>`;
        html += `      <th>${let_company}</th>`;
        html += `      <th>${let_requestDate}</th>`;
        html += `      <th class="text-center">${let_readReport}</th>`;
        html += `      <th class="text-center">${let_status}</th>`;
        html += `    </tr>`;
        html += `  </thead>`;
        html += `  <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `    <tr>`;
            html += `      <td>${i + 1}</td>`;
            if (data[i].isRead) {
                html += `      <td><a href="javascript:viewReport(${data[i].requestId});" class="action-icon font-14">${CNA}<i style="color:rgb(140, 144, 140);"  class="ms-1 mdi mdi-octagon-outline"></i></td>`;
            }
            else {
                html += `      <td><a href="javascript:viewReport(${data[i].requestId});" class="action-icon font-14">${CNA}<i style="color:rgb(193, 163, 61);"  class="ms-1 mdi mdi-octagon"></i></td>`;
            }
            html += `      <td>${formatTarih(data[i].requestDate)}</td>`;
            html += `     <td class="text-center">`;
            html += `       <a href="javascript:viewReport(${data[i].requestId});" class="action-icon">`;
            html += `         <i style="color:rgb(0, 173, 161);" class="ms-1 mdi mdi-printer"></i>`;
            html += `     </td>`;
            if (data[i].isRead) {
                html += `      <td class="text-center">${let_readed}</td>`;
            }
            else {
                html += `      <td class="text-center">${let_notReaded}<span style="margin-top:-4px;" class="badge badge-success-lighten font-10 rounded-pill"> ${let_new} </span></td>`;
            }
            html += `    </tr>`;
        }
        html += `  </tbody>`;
        html += `</table>`;
        $("#divReports").html(html);
    }
}
function viewReport(id) {
    Post('timeReport/GetTimeReportbyRequestId', id, (data) => {
        selectedReport = data;
        document.getElementById("requestTime").innerHTML = formatTarih(selectedReport.requestDate);
        document.getElementById("requestApproveTime").innerHTML = formatTarih(selectedReport.successDate);
        document.getElementById("offerTime").innerHTML = formatTarih(selectedReport.requestDate);
        document.getElementById("offerApproveTime").innerHTML = formatTarih(selectedReport.requestDate);
        document.getElementById("invoiceEntryTime").innerHTML = formatTarih(selectedReport.accountDate);
        document.getElementById("stockCreateTime").innerHTML = formatTarih(selectedReport.stockDate);
        if (!selectedReport.isRead) {
            Post('timeReport/ReadisTimeReport', selectedReport.id, (data) => {

            });
        }
        $("#reportModal").modal("show");
    });
}
function getAllReports() {
    let companyId = parseInt(CID);
    Post('TimeReport/GetallTimeReportbyCompanyId', companyId, (data) => {
        reports = data;
        createTable(reports)
    });
}
function printData() {
    printElement(document.getElementById("printPage"));
}
function printElement(elem) {
    var domClone = elem.cloneNode(true);
    var printSection = document.getElementById("printSection");
    if (!printSection) {
        var printSection = document.createElement("div");
        printSection.id = "printSection";
        document.body.appendChild(printSection);
    }
    printSection.innerHTML = "";
    printSection.appendChild(domClone);
    window.print();
}

$(document).ready(function () {
    getAllReports();
    $("#reportModal").on("hide.bs.modal", function () {
        getAllReports();
    });
});