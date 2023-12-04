let stocks = [];
let stocksFilter = [];
let selectedStock;
let selectedStockOpr;
let selectedProductName;
function createStockTable(data) {
    if (data.length > 0) {
        html = ``;
        html += `<table class="table table-sm table-centered border" id="customDatatable">`;
        html += `  <thead class="table-light">`;
        html += `    <tr>`;
        html += `      <th class="text-center">${let_queue}</th>`;
        html += `      <th class="text-center">${let_productName}</th>`;
        html += `      <th class="text-center">${let_quantity}</th>`;
        html += `      <th class="text-center">${let_deStock}</th>`;
        html += `    </tr>`;
        html += `  </thead>`;
        html += `  <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `    <tr>`;
            html += `      <td  class="text-center">${i + 1}</td>`;
            html += `      <td  class="text-center">${data[i].productName}</td>`;
            html += `      <td  class="text-center">${data[i].quantity}</td>`;
            html += `     <td  class="text-center">`;
            html += `       <a href="javascript:stockOut(${data[i].id});" class="action-icon">`;
            html += `         <i style="color:rgb(153, 173, 61);" class="ms-1 mdi mdi-login"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `    </tr>`;
        }
        html += `  </tbody>`;
        html += `</table>`;
        $("#divStocks").html(html);
        createProductOption(data);
    }
    else {
        $("#divStocks").html("");
    }
}
function createStockOperationTable(data) {
    if (data.length > 0) {
        html = ``;
        html += `<table class="table table-sm table-centered border" id="customDatatable">`;
        html += `  <thead class="table-light">`;
        html += `    <tr>`;
        html += `      <th class="text-center">${let_queue}</th>`;
        html += `      <th class="text-center">${let_productName}</th>`;
        html += `      <th class="text-center">${let_quantity}</th>`;
        html += `      <th class="text-center">${let_date}</th>`;
        html += `      <th class="text-center">${let_stockInOut}</th>`;
        html += `    </tr>`;
        html += `  </thead>`;
        html += `  <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `    <tr>`;
            html += `      <td  class="text-center">${i + 1}</td>`;
            html += `      <td  class="text-center">${selectedProductName}</td>`;
            html += `      <td  class="text-center">${data[i].quantity}</td>`;
            html += `      <td  class="text-center">${formatTarih(data[i].operationTime)}</td>`;
            if (data[i].status) {
                html += `      <td  class="text-center"><span class="font-13 badge badge-success-lighten rounded-pill"><i class="mdi mdi-tray-plus"></i> ${let_stockInDone}.</span></td>`;
            }
            else {
                html += `      <td  class="text-center"><span class="font-13 badge badge-danger-lighten rounded-pill"><i class="mdi mdi-tray-minus"></i>  ${let_deStockDone}.</span></td>`;
            }
            html += `    </tr>`;
        }
        html += `  </tbody>`;
        html += `</table>`;
        $("#divStockOperation").html(html);
    }
    else {
        $("#divStockOperation").html("");

    }
}
function createProductOption(data) {
    var html = `<option selected disabled value="0">${let_selectProduct}</option>`;
    for (var i = 0; i < data.length; i++) {
        html += `<option value="${data[i].id}">${data[i].productName}</option>`;
    }
    $("#productOption").html(html);
}
function stockOut(id) {
    selectedStock = stocks.find(stocks => stocks.id === id);
    document.getElementById("productQuantity").value = selectedStock.quantity;
    $("#productImage").attr("src", `${selectedStock.productImage}`);
    $("#inputQuantity").attr("max", `${selectedStock.quantity}`);
    $("#stockModal").modal("show");
}
function productChange() {
    let sel = document.getElementById("productOption");
    selectedProductName = sel.options[sel.selectedIndex].text;
    selectedStockOpr = stocks.find(stock => stock.productName === selectedProductName);
    let stockId = selectedStockOpr.id;
    Post('StockProcess/GetallStockOperationbyStockId', stockId, (data) => {
        createStockOperationTable(data);
    });
}
function stockOutAction() {
    let outQuantity = parseInt(document.getElementById("inputQuantity").value);
    var data = {
        Quantity: outQuantity,
        EmployeeId: parseInt(UID),
        StockId: selectedStock.id
    };
    Post('StockProcess/LogOutStock', data, (data) => {
        $("#stockModal").modal("hide");
        getAllStocks();
    });
}
function getAllStocks() {
    Post('StockProcess/GetallStockbyCompanyId', parseInt(CID), (data) => {
        stocks = data;
        createStockTable(stocks);
    });
}
function categoryChange() {
    let categoryId = parseInt(document.getElementById("categoriyOption").value);
    if (categoryId === 0) {
        createStockTable(stocks);
    }
    else {
        stocksFilter = stocks;
        let data = stocksFilter.filter(stock => stock.categoryId === categoryId);
        createStockTable(data);
    }
}

$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = stocks.filter(stock => stock.productName.toLowerCase().includes(find));
    createStockTable(result);
});

$(document).ready(function () {
    getAllStocks();
});