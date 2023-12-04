let selectedBasketId = 0;
let invoiceProducts = [];
let selectedType = let_purchase;
let selectedCurrency = let_turkishlira;
let selectedSupplierId = 0;
let totalKDV = 0;
let productAmount = 0;
let billTotalPrice = 0;
let invoicePostProducts = [];
let discount = 0;
let requests = [];
function getAllRequest() {
    var data = {
        CompanyId: parseInt(CID),
        DepartmentId: parseInt(DID)
    }
    Post('Request/GetStatusFor', data, (data) => {
        requests = data;
        var html = `<option selected disabled value="0">${let_selectRequestNo}</option>`;
        for (var i = 0; i < requests.length; i++) {
            html += `<option value="${requests[i].basketId}">${requests[i].requestNo}</option>`;
        }
        $("#requestsOption").html(html);
    });
}
function createInvoice() {
    invoicePostProducts.map(function (invoicePostProducts) {
        delete invoicePostProducts.quantity;
        return invoicePostProducts;
    });
    let RID = getRequestId(selectedBasketId);
    var data = {
        RequestId: RID,
        SupplierId: selectedSupplierId,
        BillType: selectedType,
        BillDate: getDateTime(),
        BillNumber: document.getElementById("inputInvoiceNo").value,
        Discount: discount,
        ProductAmount: productAmount,
        TotalKDV: totalKDV,
        BillTotalPrice: billTotalPrice,
        Currency: selectedCurrency,
        BillDetailItem: invoicePostProducts,
        EmployeeId: parseInt(UID),
        CompanyId: parseInt(CID),
    };
    if (selectedSupplierId > 0) {
        if (selectedBasketId > 0) {
            Post('Bill/CreateBill', data, (data) => {
                if (data.isSuccess) {
                    $.NotificationApp.send(let_request, let_invoiceRegistrationCompleted, "bottom-right", "rgba(0,0,0,0.2)", "success");
                    inputReset();
                }
                else {
                    $.NotificationApp.send(let_error, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
                }
            });
        }
        else {
            $.NotificationApp.send(let_error, let_requestNotSelected, "bottom-right", "rgba(0,0,0,0.2)", "error");
        }
    }
    else {
        $.NotificationApp.send(let_error, let_supplierCompanyNotSelected, "bottom-right", "rgba(0,0,0,0.2)", "error");
    }
}
function inputReset() {
    getAllRequest();
    getAllSupplier();
    $("#divInvoice").html("");
    document.getElementById("inputInvoiceNo").value = "0";
    document.getElementById("inputDiscount").value = "0";
    document.getElementById("productTotalAmount").innerHTML = "0" + getIcon();
    document.getElementById("kdv").innerHTML = "0" + getIcon();
    document.getElementById("discount").innerHTML = "0" + getIcon();
    document.getElementById("totalAmount").innerHTML = "0" + getIcon();
    selectedSupplierId = 0;
    selectedBasketId = 0;
}
function getDateTime() {
    var date = $('#datapicker1').val();
    var dateObject = new Date(date);
    var day = dateObject.getDate();
    var mounth = dateObject.getMonth() + 1;
    var year = dateObject.getFullYear();
    if (day < 10) {
        day = '0' + day;
    }
    if (mounth < 10) {
        mounth = '0' + mounth;
    }
    var format = year + '-' + mounth + '-' + day;
    return format;
}
function requestNoChange() {
    selectedBasketId = parseInt(document.getElementById("requestsOption").value)
    Post('Basket/GetbyBasketId', selectedBasketId, (data) => {
        invoiceProducts = data;
        invoicePostProducts = invoiceProducts.map(({ productId, quantity }) => ({ productId, quantity }));
        invoicePostProducts.forEach((obj) => {
            obj.ProductId = obj.productId;
            obj.UnitPrice = 0;
            obj.ProductKdv = 0;
            obj.ProductQuantity = obj.quantity;
        });
        invoicePostProducts.map(function (invoicePostProducts) {
            delete invoicePostProducts.productId;
            return invoicePostProducts;
        });

        createInvoceTable(data);
    });
}
function getRequestId(id) {
    let request = requests.find(requests => requests.basketId === id);
    if (request !== undefined) {
        return request.id;
    }
    else {
        return 0;
    }
}
function createInvoceTable(data) {
    html = ` <div class="table-responsive">`;
    html += `<table class="table table-sm table-bordered" id="customDatatable2">`;
    html += ` <thead>`;
    html += `<tr>`;
    html += `<th class="text-center" style="max-width:0px;">${let_queue}</th>`;
    html += `<th>${let_productName}</th>`;
    html += `<th>${let_quantity}</th>`;
    html += `<th>${let_unit}</th>`;
    html += `<th>${let_unitPrice}</th>`;
    html += `<th>${let_vatRate}(%)</th>`;
    html += `</tr>`;
    html += `</thead>`;
    html += `<tbody>`;
    for (var i = 0; i < data.length; i++) {
        html += `<tr>`;
        html += `<td class="text-center">${i + 1}</td >`;
        html += `<td>${data[i].productName}</td >`;
        html += `<td>${data[i].quantity}</td >`;
        html += `<td>${data[i].unitQuantity}</span></td>`;
        html += `<td>
       <input type="number" id="inputProductUnitPrice${data[i].productId}"  class="form-control form-control-sm" oninput="productUnitPrice(${data[i].productId})" min="0" value="1" placeholder="0" >
                 </td>`;
        html += `<td>
        <input type="number" id="inputProductKDV${data[i].productId}"  class="form-control form-control-sm" oninput="ProductKDV(${data[i].productId})" min="0" max="100" value="1" placeholder="0" required>
                 </td>`;
        html += `</tr>`;
    }
    html += ` </tbody>`;
    html += ` </table>`;
    html += ` </div>`;
    $("#divInvoice").html(html);
}
function productUnitPrice(id) {
    let invoiceProduct = invoicePostProducts.find(invoicePostProducts => invoicePostProducts.ProductId === id);
    let element = "inputProductUnitPrice" + id;
    invoiceProduct.UnitPrice = parseFloat(document.getElementById(element).value);
    calculator();
}
function ProductKDV(id) {
    let invoiceProduct2 = invoicePostProducts.find(invoicePostProducts => invoicePostProducts.ProductId === id);
    let element2 = "inputProductKDV" + id;
    invoiceProduct2.ProductKdv = parseInt(document.getElementById(element2).value);
    calculator();
}
function calculator() {
    discount = parseFloat(document.getElementById("inputDiscount").value)
    if (invoicePostProducts !== undefined && invoicePostProducts.length > 0) {
        productAmount = 0;
        totalKDV = 0;
        let temp = 0;
        for (var i = 0; i < invoicePostProducts.length; i++) {
            productAmount += (invoicePostProducts[i].UnitPrice * invoicePostProducts[i].quantity);
        }
        for (var i = 0; i < invoicePostProducts.length; i++) {
            totalKDV += ((invoicePostProducts[i].UnitPrice * invoicePostProducts[i].quantity * invoicePostProducts[i].ProductKdv) / 100);
        }
        temp = productAmount;
        productAmount = parseFloat(temp.toFixed(2));
        temp = totalKDV;
        totalKDV = parseFloat(temp.toFixed(2));
        $("#productTotalAmount").html(productAmount + getIcon());
        $("#kdv").html(totalKDV + getIcon());
        let strDiscount = "-" + discount;
        $("#discount").html(strDiscount + getIcon());
        billTotalPrice = (productAmount + totalKDV - discount);
        $("#totalAmount").html(billTotalPrice + getIcon());
    }
}
function typeOption() {
    let selectedIndex = document.getElementById("typeOption").value;
    if (selectedIndex == "1") {
        selectedType = let_purchase;
    }
    else {
        selectedType = let_refund;
    }
}
function currencyChange() {
    let index = document.getElementById("currencyOption").value;
    switch (index) {
        case "2": selectedCurrency = "Dolar"; break;
        case "3": selectedCurrency = "Euro"; break;
        case "4": selectedCurrency = "Sterlin"; break;
        default: selectedCurrency = "Türk Lirası";
    }
    calculator();
}
function getIcon() {
    let icon = "₺"
    switch (selectedCurrency) {
        case "Dolar": icon = "$"; break;
        case "Euro": icon = "€"; break;
        case "Sterlin": icon = "£"; break;
        default: icon = "₺"; break;
    }
    return icon;
}
function suppliersOptionChange() {
    selectedSupplierId = parseInt(document.getElementById("suppliersOption").value)
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

$(document).ready(function () {
    getAllRequest();
    getAllSupplier();
});