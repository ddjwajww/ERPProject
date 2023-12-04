let categories = [];
let products = [];
let selectedId = 0;
let selectedProduct;
let units = [];
let selectedBase64Image;
function createProductTable(data) {
    if (data.length > 0) {
        html = ` <div class="table-responsive">`;
        html += `<table class="table table-sm table-bordered" id="productTable">`;
        html += ` <thead>`;
        html += `<tr>`;
        html += `<th class="text-center">${let_queue}</th>`;
        html += `<th class="text-center" style="max-width:60px;">${let_productImage}</th>`;
        html += `<th>${let_productName}</th>`;
        html += `<th>${let_unit}</th>`;
        html += `<th>${let_category}</th>`;
        html += `<th class="text-center">${let_edit}</th>`;
        html += `<th class="text-center">${let_delete}</th>`;
        html += `</tr>`;
        html += `</thead>`;
        html += `<tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `<tr>`;
            html += `<td class="text-center">#${i + 1}</td >`;
            html += `<td class="text-center" style="max-width:60px;"><img src="${data[i].productImage}" alt="product-img" title="product-img" class="rounded me-2" height="32"></td>`
            html += `<td>${data[i].productName}</td >`;
            if (data[i].unitName === "0") {
                html += `<td>${let_piece}</td >`;
            }
            else {
                html += `<td>${data[i].unitName}</td >`;
            }
            html += `<td>${getCategoryName(data[i].categoryId)}</td >`;
            html += `     <td class="text-center">`;
            html += `       <a onclick="editView(${data[i].id});" data-bs-toggle="modal" data-bs-target="#addPersonelModal" class="action-icon">`;
            html += `         <i style="color:rgb(157, 173, 40);" class="ms-1 mdi mdi-lead-pencil"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `     <td class="text-center">`;
            html += `       <a href="javascript:deleteProduct(${data[i].id});" class="action-icon">`;
            html += `         <i style="color:rgb(180, 93, 81);" class="ms-1 mdi mdi-delete-outline"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `</tr>`;
        }
        html += ` </tbody>`;
        html += ` </table>`;
        html += ` </div>`;
        $("#divProducts").html(html);
    }
    else {
        $("#divProducts").html("");
    }
}
function deleteProduct(id) {

    Post(`Product/DeleteProduct`, parseInt(id), (resp) => {
        if (resp.isSuccess) {
            $.NotificationApp.send(let_product, let_deleteSuccess, "bottom-right", "rgba(0,0,0,0.2)", "warning");
            getAllProducts();
        }
        else {
            $.NotificationApp.send(let_product, let_regisFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
        }
    });
}
function getBaseUrl() {
    let file = document.querySelector('input[type=file]')['files'][0];
    let reader = new FileReader();
    reader.onloadend = function () {
        selectedBase64Image = reader.result;
        document.getElementById("productImage").src = selectedBase64Image;
    };
    reader.readAsDataURL(file);
}
function getCategoryName(id) {
    let category = categories.find(category => category.id == id);
    if (category !== null) {
        return category.categoryName;
    }
    else {
        return id;
    }
}
function editView(id) {
    selectedId = id;
    if (selectedId > 0) {
        selectedProduct = products.find(products => products.id === selectedId);
        if (selectedProduct !== null) {
            document.getElementById("fileinput").value = "";
            document.getElementById("inputProductName").value = selectedProduct.productName;
            document.getElementById("modalCategoryOption").selectedIndex = `${selectedProduct.categoryId}`;
            document.getElementById("unitOption").selectedIndex = `${selectedProduct.unitId}`;
            selectedBase64Image = selectedProduct.productImage;
            document.getElementById("productImage").src = selectedBase64Image;
            $("#addProductModal").modal("show");
        }
    }
}
function newProduct() {
    selectedId = 0;
    $("#productModal").modal("show");
}
function saveProduct() {
    if (selectedId === 0) {
        var data = {
            UnitId: parseInt(document.getElementById("unitOption").value),
            ProductName: document.getElementById("inputProductName").value,
            CategoryId: parseInt(document.getElementById("modalCategoryOption").value),
            ProductImage: selectedBase64Image,
            IsDeleted: false,

        };
        Post(`Product/InsertProduct`, data, (resp) => {
            if (resp.isSuccess) {
                $.NotificationApp.send(let_product, let_regisSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
                getAllProducts();
            }
            else {
                $.NotificationApp.send(let_product, let_regisFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
            $("#addProductModal").modal("hide");
        });
    }
    else {
        var data = {
            Id: selectedId,
            UnitId: parseInt(document.getElementById("unitOption").value),
            ProductName: document.getElementById("inputProductName").value,
            CategoryId: parseInt(document.getElementById("modalCategoryOption").value),
            ProductImage: selectedBase64Image,
            IsDeleted: false,
        };
        Post(`Product/UpdateProduct`, data, (resp) => {
            if (resp.isSuccess) {
                $.NotificationApp.send(let_product, let_updateProcessSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
                getAllProducts();
            }
            else {
                $.NotificationApp.send(let_product, let_updateProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
            $("#addProductModal").modal("hide");
        });
    }
}
function getAllProducts() {
    Get(`Product/GetAllProduct`, (data) => {
        if (data.length > 0) {
            products = data;
            createProductTable(data);
        }
    });
}
function categoryOptionChange() {
    let selectId = parseInt(document.getElementById("categoryOption").value);
    if (selectId > 0) {
        const result = products.filter(product => product.categoryId === selectId);
        createProductTable(result);
    }
    else {
        createProductTable(products);
    }
}
function getAllCategories() {
    Get(`Category/GetAllCategory`, (data) => {
        categories = data;
        var html = `<option selected value="0">${let_allCategories}</option>`;
        var modalhtml = `<option selected disabled value="0">${let_selectCategory}</option>`
        for (var i = 0; i < categories.length; i++) {
            html += `<option value="${categories[i].id}">${categories[i].categoryName}</option>`;
            modalhtml += `<option value="${categories[i].id}">${categories[i].categoryName}</option>`;
        }
        $("#categoryOption").html(html);
        $("#modalCategoryOption").html(modalhtml);
        getAllProducts();
    });
}
function getAllUnits() {
    Get(`Unit/GetAllUnit`, (data) => {
        units = data;
        var html = `<option selected disabled value="0">${let_selectUnit}</option>`;
        for (var i = 0; i < units.length; i++) {
            html += `<option value="${units[i].id}">${units[i].unitName}</option>`;
        }
        $("#unitOption").html(html);
    });
}
$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = products.filter(product => product.productName.toLowerCase().includes(find));
    document.getElementById("categoryOption").selectedIndex = '0';
    createProductTable(result);
});
$(document).ready(function () {
    getAllCategories();
    getAllUnits();
});