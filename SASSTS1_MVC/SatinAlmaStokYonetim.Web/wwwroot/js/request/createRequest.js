﻿let selectedProductId = 0;
let selectedProductName = "";
let selectedProductImage = "";
let selectedUnitName = "";
let basket = [];
let selectedUnitQuantity = 0.1;
let products = [];
function editProduct(productId) {
    let product = basket.find(basket => basket.ProductId === productId);
    if (product != undefined) {
        let inputName = "inputProductQuantity" + productId;
        let quantity = document.getElementById(inputName).value;
        product.ProductQuantity = parseInt(quantity);
    }
}
function productUnitQuantity(productId) {
    let product = basket.find(basket => basket.ProductId === productId);
    if (product != undefined) {
        let inputName = "inputProductUnitQuantity" + productId;
        let quantity = document.getElementById(inputName).value;
        product.ProductUnitQuantity = parseFloat(quantity);
    }
}
function basketProductList() {
    if (basket.length > 0) {
        var html = `<table class="table table-sm table-center"><tr>`;
        html += `<th>${let_productImage}</th>`;
        html += `<th>${let_productName}</th>`;
        html += `<th>${let_quantityUnit}</th>`;
        html += `<th>${let_quantity}</th>`;
        html += `<th>${let_transaction}</th>`;
        html += `</tr>`;
        for (var i = 0; i < basket.length; i++) {
            html += `<tr>`;
            html += `<td><img src="${basket[i].ProductImage}" alt="product-img" title="product-img" class="rounded me-2" height="32"></td>`
            html += `<td>${basket[i].ProductName}</td>`;
            if (basket[i].ProductUnitId != 1) {

                html += `<td><div class="input-group" style="width:150px;">`;
                html += `    <input type="number" id="inputProductUnitQuantity${basket[i].ProductId}" class="form-control form-control-sm" onchange="productUnitQuantity(${basket[i].ProductId})" min="0" value="1" placeholder="0" style="width: 30px;">`;
                html += `    <label class="input-group-text">${basket[i].UnitName}</label>`;
                html += `</div></td>`;
            }
            else {
                html += `<td></td>`;
            }
            html += `<td><input type="number" id="inputProductQuantity${basket[i].ProductId}" onchange="editProduct(${basket[i].ProductId})" min="1" value="${parseInt(basket[i].ProductQuantity)}" class="form-control form-control-sm" placeholder="0" style="width: 90px;"></td>`
            html += `<td><button type="button" onclick="deleteProduct(${basket[i].ProductId})" class="btn btn-sm btn-danger me-1">
            <span><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3" viewBox="0 0 16 16">
             <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5ZM11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H2.506a.58.58 0 0 0-.01 0H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1h-.995a.59.59 0 0 0-.01 0H11Zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5h9.916Zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47ZM8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5Z"/>
            </svg></span>  ${let_remove} </button></td>`;
            html += `</tr>`
        }
        html += `</table> `;
        $("#divProductList").html(html);
        document.getElementById("openModalButton").disabled = false;
    }
    else {
        $("#divProductList").html("");
        document.getElementById("openModalButton").disabled = true;
    }
}
function shortTable() {
    if (basket.length > 0) {

        var html = `<table class="table table-hover table-centered mt-0"><tr>`;
        html += `<th>${let_order}</th>`;
        html += `<th>${let_productName}</th>`;
        html += `<th>${let_quantity}</th>`;
        html += `<th>${let_unit}</th>`;
        html += `<th>${let_piece}</th>`;
        html += `</tr>`;
        for (var i = 0; i < basket.length; i++) {
            html += `<tr>`;
            html += `<td>${i + 1}</td>`;
            html += `<td>${basket[i].ProductName}</td>`;
            html += `<td>${basket[i].ProductUnitQuantity}</td>`;
            html += `<td>${basket[i].UnitName}</td>`;
            html += `<td>${basket[i].ProductQuantity}</td>`;
            html += `</tr>`
        }
        html += `</table> `;
        $("#divShortProducts").html(html);
    }
}
function createRequest() {
    shortTable();
    let pass = true;
    for (var i = 0; i < basket.length; i++) {
        if (basket[i].ProductQuantity < 1) {
            pass = false;
        }
    }
    if (pass) {
        basket.map(function (basket) {
            delete basket.ProductImage;
            delete basket.UnitName;
            delete basket.ProductName;
            return basket;
        });
        var data = {
            UserId: parseInt(UID),
            Basket: basket,
            CompanyId: parseInt(CID),
            DepartmentId: parseInt(DID),
        };
        Post('Basket/Create', data, (data) => {

            if (data.isSuccess) {
                $("#createRequestModal").modal("hide");
                basket.splice(0, basket.length);
                $("#divProductList").html("");
                document.getElementById("openModalButton").disabled = true;
                $.NotificationApp.send(let_request, let_requestCreate, "bottom-right", "rgba(0,0,0,0.2)", "success");
            }
            else {
                $.NotificationApp.send(let_error, let_processError, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
        });
    }
    else {
        $.NotificationApp.send(let_error, let_productNotZero, "bottom-right", "rgba(0,0,0,0.2)", "warning");
    }
}
function deleteProduct(productId) {
    let index = basket.findIndex(basket => basket.ProductId === productId);
    basket.splice(index, 1);
    basketProductList();
}
function addProduct() {
    let product;
    if (basket.length > 0) {
        product = basket.find(basket => basket.ProductId === selectedProductId);
        if (product !== undefined) {
            let selectedProductQuantity = parseInt(product.ProductQuantity);
            product.ProductQuantity = selectedProductQuantity + 1;
        }
        else {
            addNewProduct();
        }
    }
    else {
        addNewProduct();
    }
    refreshModal();
    basketProductList();
}
function addNewProduct() {
    var newProduct = {
        ProductId: selectedProductId,
        ProductName: selectedProductName,
        ProductImage: selectedProductImage,
        ProductQuantity: 1,
        ProductUnitId: selectedProductUnitId,
        UnitName: selectedUnitName,
        ProductUnitQuantity: 1.0,
    };
    basket.push(newProduct);
}
function refreshModal() {
    $("#productImage").attr("src", "/assets/images/null.png");
    document.getElementById("addProductButton").disabled = true;
    $("#addProductModal").modal("hide");
    $('#categoriyOption option:first').prop('selected', true);
    $('#productOption option:first').prop('selected', true);
}
function productChange() {
    selectedProductId = parseInt(document.getElementById("productOption").value);
    product = products.find(products => products.id === selectedProductId);

    if (product !== null) {
        selectedProductImage = product.productImage;
        $("#productImage").attr("src", selectedProductImage);
        selectedProductName = product.productName;
        selectedProductUnitId = product.unitId;
        selectedUnitName = product.unitName;
        document.getElementById("addProductButton").disabled = false;
    }
}
function categoryChange() {
    let categoryId = document.getElementById("categoriyOption").value;

    Post('Product/GetCategory', categoryId, (data) => {
        products = data;
        var html = `<option selected disabled value="0">${let_selectProduct}</option>`;
        for (var i = 0; i < data.length; i++) {
            html += `<option value="${data[i].id}">${data[i].productName}</option>`;
        }
        $("#productImage").attr("src", "/assets/images/null.png");
        $("#productOption").html(html);
    });
}
function getAllCategories() {
    Get('Category/GetAllCategory', (data) => {
        var html = `<option selected disabled value="0">${let_selectCategory}</option>`;
        for (var i = 0; i < data.length; i++) {
            html += `<option value="${data[i].id}">${data[i].categoryName}</option>`;
        }
        $("#categoriyOption").html(html);
        document.getElementById("addProductButton").disabled = true;
    });
}
$(document).ready(function () {
    getAllCategories();
});