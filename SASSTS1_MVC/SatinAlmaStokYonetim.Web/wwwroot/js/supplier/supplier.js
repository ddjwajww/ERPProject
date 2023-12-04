let suppliers = [];
let selectedSupplier;
function createTable(data) {
    if (data.length > 0) {
        html = ``;
        html += `<table class="table table-sm table-centered border" id="customDatatable">`;
        html += `    <thead class="table-light">`;
        html += `       <tr>`;
        html += `           <th class="text-center" style="max-width:0px;">${let_queue}</th>`;
        html += `           <th class="text-center">${let_companyName}</th>`;
        html += `           <th class="text-center">${let_phone}</th>`;
        html += `           <th class="text-center">${let_mail}</th>`;
        //html +=`           <th class="text-center">${let_edit}</th>`;
        //html +=`           <th class="text-center">${let_delete}</th>`;
        html += `    </thead>`;
        html += `    <tbody>`;

        for (var i = 0; i < data.length; i++) {
            html += `       <tr>`;
            html += `<td class="text-center" style="max-width:0px;">${i + 1}</td>`;
            html += `<td class="text-center">${data[i].companyName}</td>`;
            html += `<td class="text-center">${data[i].companyPhone}</td>`;
            html += `<td class="text-center">${data[i].companyMail}</td>`;
            //html += `<td class="text-center">`;
            //html += `   <a onclick="editSupplier(${data[i].id});" data-bs-toggle="modal" data-bs-target="#addSupplierModal" class="action-icon">`;
            //html += `       <i style="color:rgb(157, 173, 40);" class="ms-1 mdi mdi-lead-pencil">`;
            //html += `       </i>`;
            //html += `   </a>`;
            //html += `</td>`;
            //html += `<td class="text-center">`;
            //html += `   <a onclick="deleteSupplier(${data[i].id});" class="action-icon">`;
            //html += `       <i style="color:rgb(180, 93, 81);" class="ms-1 mdi mdi-delete-outline">`;
            //html += `       </i>`;
            //html += `   </a>`;
            //html += `</td>`;
            html += `       </tr>`;
        }
        html += ``;
        document.getElementById("divSuppliers").innerHTML = html;
    }
    else {
        document.getElementById("divSuppliers").innerHTML = "";
    }
}
function createSupplier() {
    var data = {
        CompanyName: document.getElementById("inputCompanyName").value,
        CompanyMail: document.getElementById("inputEmail").value,
        CompanyPhone: document.getElementById("inputPhone").value,
    }
    Post('Supplier/InsertSupplier', data, (data) => {
        if (data.isSuccess) {
            resetModal();
            $.NotificationApp.send(let_supplierCompany, let_regisSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
        }
        else {
            $.NotificationApp.send(let_supplierCompany, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");
        }
    });
}
function resetModal() {
    $("#addSupplierModal").modal("hide");
    document.getElementById("inputCompanyName").value = "";
    document.getElementById("inputEmail").value = "";
    document.getElementById("inputPhone").value = "";
    getAllSuppliers();
}
function deleteSupplier(id) {
    Post('Supplier/GetAllSupplier', id, (data) => {
        if (data.isSuccess) {
            getAllSuppliers();;
            $.NotificationApp.send(let_supplierCompany, let_deleteSuccess, "bottom-right", "rgba(0,0,0,0.2)", "warning");
        }
        else {
            $.NotificationApp.send(let_supplierCompany, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");
        }
    });
}
function newSupplier() {

}
function editSupplier(id) {
    selectedSupplier = suppliers.find(supplier => supplier.id === id);
    Post('Supplier/GetAllSupplier', id, (data) => {
        if (data.isSuccess) {
            getAllSuppliers();;
            $.NotificationApp.send(let_supplierCompany, let_updateSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
        }
        else {
            $.NotificationApp.send(let_supplierCompany, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");
        }
    });
}
function getAllSuppliers() {
    Get('Supplier/GetAllSupplier', (data) => {
        suppliers = data;
        createTable(suppliers);
    });
}

$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = suppliers.filter(supplier => supplier.companyName.toLowerCase().includes(find));
    createTable(result);
});

$(document).ready(function () {
    getAllSuppliers();
});