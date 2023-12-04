﻿let requests = [];
let basket = [];
let offers = [];
let selectedOfferId;
function detail(offerId) {
    selectedOfferId = offerId;
    createOfferDetailTable(offerId);
    $("#detailModal").modal("show");
}
function decline(offerId, requestId) {
    selectedOfferId = offerId;
    Post('Offer/DeleteOffer', parseInt(selectedOfferId), (data) => {
        if (data.isSuccess) {
            getOffers(requestId);
            $.NotificationApp.send(let_offer, let_offerDenied, "bottom-right", "rgba(0,0,0,0.2)", "warning");
        }
        else {
            $.NotificationApp.send(let_error, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");

        }
    });
}
function approve(offerId, requestId) {
    selectedOfferId = offerId;
    var data = {
        RequestId: requestId,
        OfferId: offerId
    };
    Post('Offer/SuccessOffer', data, (data) => {
        if (data.isSuccess) {
            getAllRequest();
            $.NotificationApp.send(let_offer, let_offerApproved, "bottom-right", "rgba(0,0,0,0.2)", "success");
            document.getElementById("divRequestDetail").innerHTML = "";
            $("#requestInfo").html("");
        }
        else {
            $.NotificationApp.send(let_error, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");
        }
    });
}
function createOfferDetailTable(id) {
    offer = offers.find(offers => offers.id === id);
    if (offer !== null) {
        let data2 = JSON.parse(offer.offerDescription);
        html = ``;
        html += `<table class="table table-bordered table-selected table-sm table-centered mb-0">`;
        html += `   <thead>`;
        html += `       <tr>`;
        html += `            <th>${let_queue}</th>`;
        html += `            <th>${let_product}</th>`;
        html += `            <th>${let_quantity}</th>`;
        html += `            <th>${let_unitPrice}</th>`;
        html += `        </tr>`;
        html += `   </thead>`;
        html += `    <tbody>`;
        for (var i = 0; i < data2.length; i++) {
            html += `       <tr>`;
            html += `           <td>${i + 1}</td>`;
            html += `           <td>${data2[i].productName}</td>`;
            html += `           <td>${data2[i].quantity}</td>`;
            html += `           <td>${data2[i].unitPrice}</td>`;
            html += `       </tr>`;
        }
        html += `    </tbody>`;
        html += `</table>`;
        $("#offerDetail").html(html);
    }
    else {
        $("#offerDetail").html("");
    }
}
function createOffersTable(data) {
    html = ``;
    html += `<table class="table table-bordered table-sm table-centered mb-0">`;
    html += `   <thead>`;
    html += `       <tr>`;
    html += `            <th>${let_queue}</th>`;
    html += `            <th>${let_supplierCompany}</th>`;
    html += `            <th>${let_offerDate}</th>`;
    html += `            <th class="text-center">${let_orders}</th>`;
    html += `            <th class="text-center">${let_currency}</th>`;
    html += `            <th>${let_totalPrice}</th>`;
    html += `            <th class="text-center">${let_accept}</th>`;
    html += `            <th class="text-center">${let_deny}</th>`;
    html += `        </tr>`;
    html += `   </thead>`;
    html += `    <tbody>`;
    for (var i = 0; i < data.length; i++) {
        html += `       <tr>`;
        if (data[i].offerStatus == 2) {
            html += `           <td>${i + 1}</td>`;
            html += `           <td><strike>${data[i].companyName}</strike></td>`;
            html += `           <td><strike>${formatTarih(data[i].offerTime)}</strike></td>`;
            html += `           <td class="table-action text-center">`;
            html += `               <a href="javascript: detail(${data[i].id});" class="action-icon"> <i style="color:rgba(255, 188, 0, 0.85)" class="mdi mdi-cart-variant"></i></a>`;
            html += `           </td>`;
            html += `           <td><span class="badge badge-secondary-lighten font-12 "><strike>${data[i].priceCurrency}</strike></span></td>`;
            html += `           <td><span class="badge badge-secondary-lighten font-12 "><strike>${data[i].offerPrice}</strike></span></td>`;
            html += `           <td class="table-action text-center">`;
            html += `           </td>`;
            html += `           <td class="table-action text-center">`;
            html += `           </td>`;
        }
        else {
            html += `           <td>${i + 1}</td>`;
            html += `           <td>${data[i].companyName}</td>`;
            html += `           <td>${formatTarih(data[i].offerTime)}</td>`;
            html += `           <td class="table-action text-center">`;
            html += `               <a href="javascript: detail(${data[i].id});" class="action-icon"> <i style="color:rgba(255, 188, 0, 0.85)" class="mdi mdi-cart-variant"></i></a>`;
            html += `           </td>`;
            html += `           <td><span class="badge badge-secondary-lighten font-12 ">${data[i].priceCurrency}</span></td>`;
            html += `           <td><span class="badge badge-secondary-lighten font-12 ">${data[i].offerPrice}</span></td>`;
            html += `           <td class="table-action text-center">`;
            html += `               <a href="javascript: decline(${data[i].id},${data[i].requestId});" class="action-icon"> <i style="color:rgb(250, 92, 124);" class="mdi mdi-minus-circle-outline"></i></a>`;
            html += `           </td>`;
            html += `           <td class="table-action text-center">`;
            html += `               <a href="javascript: approve(${data[i].id},${data[i].requestId});" class="action-icon"> <i style="color:rgb(10, 207, 151);" class="mdi mdi-check-outline"></i></a>`;
            html += `           </td>`;
        }
        html += `       </tr>`;
    }
    html += `    </tbody>`;
    html += `</table>`;
    $("#divOffers").html(html);
}
function createBasketTable(data) {
    html = ``;
    html += `<table class="table table-bordered table-sm table-centered mb-0">`;
    html += `   <thead>`;
    html += `       <tr>`;
    html += `            <th>${let_queue}</th>`;
    html += `            <th>${let_product}</th>`;
    html += `            <th>${let_unit}/${let_quantity}</th>`;
    html += `            <th>${let_quantity}</th>`;
    html += `        </tr>`;
    html += `   </thead>`;
    html += `    <tbody>`;
    for (var i = 0; i < data.length; i++) {
        html += `       <tr>`;
        html += `           <td>${i + 1}</td>`;
        html += `           <td>${data[i].productName}</td>`;
        html += `           <td>${data[i].unitQuantity}</td>`;
        html += `           <td>${data[i].quantity}</td>`;
        html += `       </tr>`;
    }
    html += `    </tbody>`;
    html += `</table>`;
    $("#divRequestDetail").html(html);
}
function getOffers(id) {
    selectedOfferId = id;
    var data = {
        CompanyId: parseInt(CID),
        RequestId: id
    }
    Post('Offer/GetAllOffers', data, (data) => {
        offers = data;
        if (offers.length > 0) {
            createOffersTable(offers);
        }
        else {
            $("#divOffers").html("");
            $("#requestInfo").html("");
        }
    });
}
function readRequest(requestId, requestNo, basketId) {
    document.getElementById("offerPage").style.display = "block";
    htmlInfo = `${requestNo}`;
    $("#requestInfo").html(htmlInfo);
    Post('Basket/GetbyBasketId', basketId, (data) => {
        basket = data;
        createBasketTable(basket);
        getOffers(requestId);
    });
}
function createRequestsTable(data) {
    html = ``;
    for (var i = 0; i < data.length; i++) {
        html += `<a href="javascript:void(readRequest(${data[i].id},'${data[i].requestNo}',${data[i].basketId}));" class="text-body">`;
        html += `<div class="d-flex mt-1 p-1">`;
        html += `<div class="avatar-sm d-table">`;
        html += `<span class="avatar-title bg-info-lighten rounded-circle text-info">`;
        html += `<i class="uil uil-receipt-alt font-24"></i>`;
        html += `</span>`;
        html += `</div>`;
        html += `<div class="ms-2">`;
        html += `<h5 class="mt-0 mb-0">${CNA}</h5>`;
        html += `<span class="badge badge-info-lighten ms-1">${let_offerApprove}</span>`;
        html += `<p class="mb-0 text-muted font-12">${let_requestNo}:</p><p class="mb-0 text-muted font-12"> ${data[i].requestNo}</p>`;
        html += `</div>`;
        html += `</div>`;
        html += `</a>`;
    }
    $("#divRequests").html(html);
}
function getAllRequest() {
    document.getElementById("offerPage").style.display = "none";
    Post('Request/GetStatusThree', parseInt(CID), (data) => {
        requests = data;
        createRequestsTable(requests);
    });
}
$(document).ready(function () {
    getAllRequest();
});