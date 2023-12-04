let reports = [];
let selectedReport;
function createTable(data) {
    if (data !== null) {
        if (data.requestNo !== null) {
            selectedReport = data;
            html = ``;
            html += `<table class="table  table-sm w-100 dt-responsive nowrap" id="customDatatable">`;
            html += `  <thead>`;
            html += `    <tr>`;
            html += `      <th>${let_queue}</th>`;
            html += `      <th>${let_requestNo}</th>`;
            html += `      <th>${let_company}</th>`;
            html += `     <th>${let_supplierCompany}</th>`;
            html += `      <th class="text-center">${let_readReport}</th>`;
            html += `      <th class="text-center">${let_status}</th>`;
            html += `    </tr>`;
            html += `  </thead>`;
            html += `  <tbody>`;
            html += `    <tr>`;
            html += `      <td></td>`;
            if (data.isRead) {
                html += `      <td><a href="javascript:viewReport(${data.id});" class="action-icon font-14">${data.requestNo}<i style="color:rgb(140, 144, 140);"  class="ms-1 mdi mdi-octagon-outline"></i></td>`;
            }
            else {
                html += `      <td><a href="javascript:viewReport(${data.id});" class="action-icon font-14">${data.requestNo}<i style="color:rgb(193, 163, 61);"  class="ms-1 mdi mdi-octagon"></i></td>`;
            }
            html += `      <td>${CNA}</td>`;
            html += `      <td>${data.supplierCompanyName}</td>`;
            html += `     <td class="text-center">`;
            html += `       <a href="javascript:viewReport();" class="action-icon">`;
            html += `         <i style="color:rgb(0, 173, 161);" class="ms-1 mdi mdi-printer"></i>`;
            html += `     </td>`;
            if (data.isRead) {
                html += `      <td class="text-center">${let_readed}</td>`;
            }
            else {
                html += `      <td class="text-center">${let_notReaded} <span style="margin-top:-4px;" class="badge badge-success-lighten font-10 rounded-pill"> ${let_new} </span></td>`;
            }
            html += `    </tr>`;
            html += `  </tbody>`;
            html += `</table>`;
            $("#divReports").html(html);
        }

        else {
            $("#divReports").html("");
        }

    }
    else {
        $("#divReports").html("");
    }
}
function viewReport() {
    if (selectedReport !== null) {
        if (!selectedReport.isRead) {
            Post('ManagementReport/ReadisTimeReport', selectedReport.id, (data) => {

            });
        }
        $("#report2Modal").modal("show");
        document.getElementById("infoSurname").innerHTML = selectedReport.employeeSurname;
        document.getElementById("infoName").innerHTML = selectedReport.employeeName;
        document.getElementById("infoCompanyName").innerHTML = selectedReport.employeeCompanyName;
        document.getElementById("infoDepartmentName").innerHTML = selectedReport.employeeDepartmentName;
        document.getElementById("infoSupplierName").innerHTML = selectedReport.supplierCompanyName;
        document.getElementById("infocompanyPhone").innerHTML = selectedReport.companyPhone;

        html = ``;
        html += `   <table class="table mt-4">`;
        html += `    <thead>`;
        html += `    </thead>`;
        html += `   <caption style="text-align:right"><b>${let_product}</b></caption>`;
        html += `    <thead>`;
        html += `       <tr>`;
        html += `       <th>${let_productName}</th>`;
        html += `      <th>${let_quantity}</th>`;
        html += `<th>${let_categoryName}</th>`;
        html += `    </tr>`;
        html += `   </thead>`;

        html += `   <tbody>`;
        for (var i = 0; i < selectedReport.detailProducts.length; i++) {
            html += `  <tr>`;
            html += `      <td>${selectedReport.detailProducts[i].productName}</td>`;
            html += `      <td>${selectedReport.detailProducts[i].productQuantity}</td>`;
            html += `      <td>${selectedReport.detailProducts[i].categoryName}</td>`;


            html += `</tr>`;
        }
        html += `  </tbody>`;
        html += `  </table>`;



        html += `   <table class="table table-sm mt-4">`;
        html += `    <thead>`;
        html += `<tr>`;
        html += `       <th>${let_totalPrice}</th>`;
        html += `       <th>${let_currency}</th>`;
        html += `</tr>`;
        html += `    </thead>`;
        html += `   <tbody>`;
        html += `<tr>`;
        html += `      <td>${selectedReport.price} </td>`;
        html += `      <td> ${selectedReport.priceCurrency}</td>`;
        html += `</tr>`;

        html += `  </tbody>`;
        html += `  </table>`;
        document.getElementById("productProcess").innerHTML = html;
    }
}
function getAllReports() {
    let companyId = parseInt(CID);
    Post('ManagementReport/GetallManagementReportbyCompanyId', companyId, (data) => {
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

$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = reports.filter(report => report.requestNo.toLowerCase().includes(find));
    createTable(result);
});

$(document).ready(function () {
    getAllReports();
    $("#reportModal").on("hide.bs.modal", function () {
        getAllReports();
    });
});