let offers = [];
let requests = [];
let selectedSupplierId = 0;
let selectedRequestId = 0;
let basket = [];
let postBasket = [];
let currency = "Türk Lirası"
function closeOfferEntry() {
    Post('Request/StatusTwo', selectedRequestId, (data) => {
        $("#closeOfferEntryModal").modal("hide");
        htmlInfo = ` <h5 class="mb-4 bg-light p-2"><i class="mdi mdi-office-building me-1"></i>${let_requestNo}: - </h5>`;
        $("#infoRequestNo").html(htmlInfo);
        $("#divOfferDetail").html("");
        getAllRequest();
    });
}
function readRequest(id, requestNo, requestId) {
    document.getElementById("closeOfferEntryButton").style.display = "inline-block";
    $("#divOfferDetail").html("");
    getAllSupplier();
    document.getElementById("offerDetailButton").style.display = "inline-block";
    selectedBasketId = id;
    selectedRequestId = requestId;
    if (selectedBasketId > 0) {
        Post('Basket/GetbyBasketId', selectedBasketId, (data) => {
            basket = data;
            if (basket.length > 0) {
                basket.map(function (basket) {
                    delete basket.unitQuantity;
                    delete basket.detailStatus;
                    delete basket.createdTime;
                    return basket;
                });
                basket.forEach((obj) => {
                    obj.unitPrice = 1;
                });
                basket.forEach((obj) => {
                    obj.currency = "Lira";
                });
                postBasket = basket;
                createProductTable(basket);
            }
            else {
            }
        });
    }
    htmlInfo = ` <h5 class="mb-4 bg-light p-2">
                        <i class="mdi mdi-office-building me-1"></i>${let_requestNo} : ${requestNo}
                    </h5>`;
    $("#infoRequestNo").html(htmlInfo);
}
function createProductTable(data) {
    html = ``;
    html += `<div class="table-responsive">`;
    html += `<table class="table mt-1 table-bordered table-sm table-centered mb-0">`;
    html += `   <thead class="table-light">`;
    html += `        <tr>`;
    html += `            <th>${let_queue}</th>`;
    html += `            <th>${let_productName}</th>`;
    html += `            <th>${let_quantity}</th>`;
    html += `            <th>${let_unitPrice}</th>`;
    html += `        </tr>`;
    html += `   </thead>`;
    html += `   <tbody>`;
    for (var i = 0; i < data.length; i++) {
        html += `        <tr>`;
        html += `            <th>${i + 1}</th>`;
        html += `            <th>${data[i].productName}</th>`;
        html += `            <th><span class="badge badge-info-lighten p-1">${data[i].quantity}</span></th>`;
        html += `            <th><div class="input-group input-group-sm" style="min-width:180px;">`;
        html += `            <input type="number" id="inputProductPrice${i + 1}" class="form-control form-control-sm" oninput="offerProductPrice(${data[i].productId},'inputProductPrice${i + 1}')" min="0" value="1" placeholder="0" style="width: 30px;">`;
        html += `            <label class="input-group-text input-group-text-sm">${let_price}</label>`;
        html += `            </div></th></tr>`;
    }
    html += `   </tbody>`;
    html += `</table>`
    html += `</div>`;
    html += `</div>`;
    html += `<table class="table mt-2 table-bordered table-sm table-centered mb-0"><tbody><tr></tr>`;
    html += ` <th id="priceInfo">${let_totalPrice}: ${priceCalculator()} ₺</th>`;
    html += ` <th id="priceInfo2">${let_totalTLPrice}: ${priceCalculator()} ₺</th>`;
    html += `<th>`;
    html += `  <select class="form-select" id="optionCurrency" onchange="currencyChange()" style="min-width:80px;">`;
    html += `    <option selected value="1">₺ (${let_turkishLiras})</option>`;
    html += `    <option value="2">$ (${let_dolar})</option>`;
    html += `    <option value="3">€ (${let_euro})</option>`;
    html += `    <option value="4">£ (${let_sterlin})</option>`;
    html += `   </select>`;
    html += `</th>`;

    html += ` </tr></tbody></table>`;
    html += `<table class="table mt-2 table-bordered table-sm table-centered mb-0" ><tr><th><span class=" float-end ms-3" id="supplierWarning" style="display:inline; color:#cc9778;"> ! ${let_supplierNotSelected}</span><button type="button" data-bs-toggle="modal" data-bs-target="#addOfferModal" onclick="offerInfo()" class="btn btn-sm btn-primary float-end"  id="createOfferButton" style="display:none;">`;
    html += `    <i class="mdi mdi-chevron-triple-down me-1"></i>${let_createOffer} `;
    html += `</button></th></tr></table>`;
    $("#divOfferDetail").html(html);
}
function offerInfo() {
    $("#divOfferDetail2").html("");
    html = ``;
    html += `<table class="table mb-3 table-bordered table-sm table-centered mb-0">`;
    html += `   <thead class="table-light">`;
    html += `        <tr>`;
    html += `            <th>${let_queue}</th>`;
    html += `            <th>${let_productName}</th>`;
    html += `            <th>${let_quantity}</th>`;
    html += `            <th>${let_unitPrice}</th>`;
    html += `        </tr>`;
    html += `   </thead>`;
    html += `   <tbody>`;
    for (var i = 0; i < postBasket.length; i++) {
        html += `        <tr>`;
        html += `            <th>${i + 1}</th>`;
        html += `            <th>${postBasket[i].productName}</th>`;
        html += `            <th><span class="badge badge-info-lighten p-1">${postBasket[i].quantity}</span></th>`;
        html += `            <th><span class="badge badge-success-lighten p-1">${postBasket[i].unitPrice}${getIcon()}</span></th>`;
        html += `        </tr>`;
    }
    html += `   </tbody>`;
    html += `</table>`
    $("#divOfferDetail2").html(html);
}
function offerProductPrice(id, elementName) {
    let product = postBasket.find(postBasket => postBasket.productId === id);
    let price = document.getElementById(elementName).value;
    product.unitPrice = parseFloat(price);
    $("#priceInfo").html(`${let_totalPrice} : ${priceCalculator()} ${getIcon()}`);
    $("#priceInfo2").html(`${let_totalTLPrice} : ${exChange()}₺`);
}
function currencyChange() {
    let val = document.getElementById("optionCurrency").value
    switch (val) {
        case "1": currency = "Türk Lirası"; break;
        case "2": currency = "Dolar"; break;
        case "3": currency = "Euro"; break;
        case "4": currency = "Sterlin"; break;
        default: currency = "Türk Lirası"; break;
    }
    $("#priceInfo").html(`${let_totalPrice}: ${priceCalculator()} ${getIcon()}`);
    $("#priceInfo2").html(`${let_totalTLPrice}: ${exChange()}₺`);
}
function exChange() {
    let dolarStr = R_Dolar.replace(",", ".");
    let dolar = parseFloat(dolarStr);
    let euroStr = R_Euro.replace(",", ".");
    let euro = parseFloat(euroStr);
    let sterlinStr = R_Sterlin.replace(",", ".");
    let sterlin = parseFloat(sterlinStr);
    let result = 0;
    switch (currency) {
        case "Türk Lirası": result = (priceCalculator() * 1); break;
        case "Dolar": result = (priceCalculator() * dolar); break;
        case "Euro": result = (priceCalculator() * euro); break;
        case "Sterlin": result = (priceCalculator() * sterlin); break;
        default: result = priceCalculator(); break;
    }
    var resulttotalPrice = result.toFixed(2);
    return resulttotalPrice;
}
function createOffer() {
    postBasket.map(function (postBasket) {
        delete postBasket.productId;
        return postBasket;
    });
    const myJSON = JSON.stringify(postBasket);
    var data = {
        CompanyId: parseInt(CID),
        SupplierId: selectedSupplierId,
        RequestId: selectedRequestId,
        OfferDescription: myJSON,
        OfferPrice: priceCalculator(),
        PriceCurrency: currency,
    };
    Post('Offer/CreateOffer', data, (data) => {
        if (data.isSuccess) {
            $("#addOfferModal").modal("hide");
            $.NotificationApp.send(let_offer, let_offerTook, "bottom-right", "rgba(0,0,0,0.2)", "success");
            resetView();
        }
        else {
            $.NotificationApp.send(let_error, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");
        }
    });
}
function resetView() {
    Get('Supplier/GetAllSupplier', (data) => {
        var html = `<option selected disabled value="0">${let_selectSupplierCompany}</option>`;
        for (var i = 0; i < data.length; i++) {
            html += `<option value="${data[i].id}">${data[i].companyName}</option>`;
        }
        $("#suppliersOption").html(html);
        document.getElementById("createOfferButton").style.display = "none";
        document.getElementById("supplierWarning").style.display = "block";
        for (var i = 0; i < basket.length; i++) {
            basket[i].unitPrice = 1;
        }
        createProductTable(basket);
    });
}
function priceCalculator() {
    let totalPrice = 0;
    for (var i = 0; i < postBasket.length; i++) {
        totalPrice += (postBasket[i].quantity * postBasket[i].unitPrice);
    }
    var resulttotalPrice = totalPrice.toFixed(2);
    return resulttotalPrice;
}
function getAllRequest() {
    resetOfferView();
    var data = {
        CompanyId: parseInt(CID),
        DepartmentId: parseInt(DID)
    };
    Post('Request/GetbySA', data, (data) => {
        requests = data;
        createRequestsTable(requests);
    });
}
function getAllSupplier() {
    Get('Supplier/GetAllSupplier', (data) => {
        var html = `<option selected disabled value="0">${let_selectSupplierCompany}</option>`;
        for (var i = 0; i < data.length; i++) {
            html += `<option value="${data[i].id}">${data[i].companyName}</option>`;
        }
        $("#suppliersOption").html(html);
    });
}
function supplierChange() {
    selectedSupplierId = parseInt(document.getElementById("suppliersOption").value)
    document.getElementById("createOfferButton").style.display = "block";
    document.getElementById("supplierWarning").style.display = "none";
}
function resetOfferView() {
    $("#divOfferList").html("");
    document.getElementById("offerDetailButton").style.display = "none";
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
        html += `<span class="badge badge-warning-lighten ms-1"> ${let_offersTaking}</span>`;
        html += `<p class="mb-0 text-muted font-12">${let_requestNo}:</p><p class="mb-0 text-muted font-12"> ${data[i].requestNo}</p>`;
        html += `</div>`;
        html += `</div>`;
        html += `</a>`;
    }
    $("#divRequests").html(html);
}
function getIcon() {
    let icon = "₺"
    switch (currency) {
        case "Türk Lirası": icon = "₺"; break;
        case "Dolar": icon = "$"; break;
        case "Euro": icon = "€"; break;
        case "Sterlin": icon = "£"; break;
        default: icon = "₺"; break;
    }
    return icon;
}
$("#searchRequest").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = requests.filter(request => request.requestNo.toLowerCase().includes(find));
    createRequestsTable(result);
});
$(document).ready(function () {
    getAllRequest();
    getAllSupplier();
});