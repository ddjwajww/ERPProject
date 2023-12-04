﻿let requests = [];
function requestNo(id) {
    Post('Request/Delete', id, (data) => {
        if (data.isSuccess) {
            getAllRequest();
            $("#divBasketDetail").html("");
            $("#infoRequestNo").html("");
            $.NotificationApp.send(let_request, let_requestDenied, "bottom-right", "rgba(0,0,0,0.2)", "warning");
        }
        else {
            $.NotificationApp.send(let_error, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");
        }
    });
}
function requestOk(id) {
    Post('Request/SuccessRequest', id, (data) => {
        if (data.isSuccess) {
            getAllRequest();
            $("#divBasketDetail").html("");
            $("#infoRequestNo").html("");
            $.NotificationApp.send(let_request, let_requestApproved, "bottom-right", "rgba(0,0,0,0.2)", "success");
        }
        else {
            $.NotificationApp.send(let_error, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");
        }
    });
}
function getAllRequest() {
    var data = {
        CompanyId: parseInt(CID),
        DepartmentId: parseInt(DID)
    };
    Post('Request/GetbyCompanyIdAndDepartmanId', data, (data) => {
        requests = data;
        createRequestsTable(requests);
    });
}
function readRequest(id, requestNo, requestId) {
    Post('Basket/GetbyBasketId', id, (data) => {
        let basket = data;
        if (basket.length > 0) {
            createBasketDetailTable(basket, requestId);
            htmlInfo = `<h5>${let_requestNo} : ${requestNo}</h5>`;
            $("#infoRequestNo").html(htmlInfo);
        }
        else {
            $("#divBasketDetail").html("");
            $("#infoRequestNo").html("");
        }
    });
}
function createBasketDetailTable(data, requestId) {
    html = ``;
    html += `<table class="table table-sm table-centered mb-0">`;
    html += `  <thead>`;
    html += `   <tr>`;
    html += `     <th>${let_queue}</th>`;
    html += `     <th>${let_productName}</th>`;
    html += `     <th>${let_quantity}</th>`;
    html += `     <th>${let_unit}</th>`;
    html += `     <th>${let_quantity}</th>`;
    html += `     </tr>`;
    html += ` </thead>`;
    html += ` <tbody>`;
    for (var i = 0; i < data.length; i++) {
        html += `  <tr>`;
        html += `  <td>${i + 1}</td>`;
        html += `  <td>${data[i].productName}</td>`;
        html += `  <td>${data[i].unitQuantity}</td>`;
        html += `  <td>-</td>`;
        html += `  <td>${data[i].quantity}</td>`;
        html += `  </tr>`;
    }
    html += `</tbody>`;
    html += `</table>`;
    html += `<div class="row mb-2 mt-4">`;
    html += `   <div class="col-sm-6"> ${let_requestCreationDate}: ${formatTarih(data[0].createdTime)}`;
    html += `   </div>`;
    html += `   <div class="col-sm-6">`;
    html += `       <button type="button" class="btn ms-2 btn-sm btn-outline-danger float-end" onclick="requestNo(${requestId})"><i class="mdi dripicons-to-do me-1"></i>${let_denyRequest}</button>`;
    html += `       <button type="button" class="btn ms-2 btn-sm btn-outline-success float-end" onclick="requestOk(${requestId})"><i class="mdi dripicons-to-do me-1"></i>${let_acceptRequest}</button>`;

    html += `   </div>`;
    html += ` </div>`;
    $("#divBasketDetail").html(html);
}
function createRequestsTable(data) {
    html = ``;
    for (var i = 0; i < data.length; i++) {
        html += `<a href="javascript:void(readRequest(${data[i].basketId},'${data[i].requestNo}', ${data[i].id}));" class="text-body">`;
        html += `<div class="d-flex mt-1 p-1">`;
        html += `<div class="avatar-sm d-table">`;
        html += `<span class="avatar-title bg-info-lighten rounded-circle text-info">`;
        html += `<i class="uil uil-receipt-alt font-24"></i>`;
        html += `</span>`;
        html += `</div>`;
        html += `<div class="ms-2">`;
        html += `<h5 class="mt-0 mb-0">${CNA}</h5>`;
        html += `<span class="badge badge-danger-lighten ms-1">+ ${let_waitingForApproval}</span>`;
        html += `<p class="mb-0 text-muted font-12">${let_requestNo}:</p><p class="mb-0 text-muted font-12"> ${data[i].requestNo}</p>`;
        html += `</div>`;
        html += `</div>`;
        html += `</a>`;
    }
    $("#divRequests").html(html);
}

$("#searchRequest").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = requests.filter(request => request.requestNo.toLowerCase().includes(find));
    createRequestsTable(result);
});

$(document).ready(function () {
    getAllRequest();
});