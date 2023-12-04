let companies = [];
let selectedCompany;
let selectedCompanyId = 0;
function createTable(data) {
    html = ``;
    html += ``;
    html += `<div class="table-responsive">`;
    html += `    <table class="table table-bordered table-sm w-100 dt-responsive nowrap" id="customDatatable">`;
    html += `        <thead>`;
    html += `           <tr>`;
    html += `               <th class="text-center numberPageTitle">${let_queue}Sıra</th>`;
    html += `               <th>${let_companyName}</th>`;
    html += `               <th>${let_companyNo}</th>`;
    html += `               <th>${let_edit}</th>`;
    html += `               <th>${let_delete}</th>`;
    html += `            </tr>`;
    html += `       </thead>`;
    html += `       <tbody>`;
    for (var i = 0; i < data.length; i++) {
        html += `           <tr>`;
        html += `               <td class="text-center numberPageTitle" >#${i + 1}</td>`;
        html += `               <td>${data[i].companyName}</td>`;
        html += `               <td>${data[i].companyNo}</td>`;
        html += `     <td class="text-center">`;
        html += `       <a onclick="edit(${data[i].id})"  style="cursor: pointer;" class="action-icon" data-bs-toggle="modal" data-bs-target="#addCompanyModal" >`;
        html += `         <i style="color:rgb(157, 173, 40);" class="ms-1 mdi mdi-lead-pencil"></i>`;
        html += `       </a>`;
        html += `     </td>`;
        html += `     <td class="text-center">`;
        html += `       <a onclick="deleteCompany(${data[i].id})" style="cursor: pointer;" class="action-icon">`;
        html += `         <i style="color:rgb(180, 93, 81);" class="ms-1 mdi mdi-delete-outline"></i>`;
        html += `       </a>`;
        html += `            </tr > `;
    }
    html += `       </tbody > `;
    html += `   </table > `;
    document.getElementById("divCompanies").innerHTML = html;
}
function getAllCompany() {
    Get('Admin/GetAllCompany', (data) => {
        companies = data;
        createTable(companies);
    });
}
function createCompany() {

    $("#addCompanyModal").modal("hide");
    document.getElementById("inputCompanyName").value = "";
    document.getElementById("inputCompanyNo").value = "";
    saveCompany(data);
}
function edit(id) {
    selectedCompanyId = id;
    selectedCompany = companies.find(company => company.id === selectedCompanyId);
    document.getElementById("inputCompanyName").value = selectedCompany.companyName;
    document.getElementById("inputCompanyNo").value = selectedCompany.companyNo;
}
function newCompany() {
    selectedCompanyId = 0;
}

function resetModal() {
    document.getElementById("inputCompanyName").value = "";
    document.getElementById("inputCompanyNo").value = "";
    $("#addCompanyModal").modal("hide");
}
function saveCompany() {

    let companyName = document.getElementById("inputCompanyName").value;
    let companyNo = document.getElementById("inputCompanyNo").value;

    if (selectedCompanyId > 0) {

        var data = {
            Id: selectedCompanyId,
            CompanyName: companyName,
            CompanyNo: companyNo,
            IsDeleted: false,
        };

        Post('Company/UpdateCompany', data, (resp) => {
            if (resp.isSuccess) {
                $.NotificationApp.send(let_company, let_updateSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
            }
            else {
                $.NotificationApp.send(let_company, let_updateFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
            getAllCompany();
            resetModal();
        });
    }

    else {

        var data = {
            CompanyName: companyName,
            CompanyNo: companyNo
        };
        Post('Company/CreateCompany', data, (resp) => {
            if (resp.isSuccess) {
                $.NotificationApp.send(let_newCompany, let_regisSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
            }
            else {
                $.NotificationApp.send(let_newCompany, let_regisFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
            getAllCompany();
            resetModal();
        });
    }

}
function deleteCompany(id) {

    Post('Company/DeleteCompany', parseInt(id), (data) => {
        if (data.isSuccess) {
            $.NotificationApp.send(let_company, let_deleteSuccess, "bottom-right", "rgba(0,0,0,0.2)", "warning");
        }
        else {
            $.NotificationApp.send(let_company, let_deleteFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
        }
        getAllCompany();
    });
}

$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = companies.filter(company => company.companyName.toLowerCase().includes(find));
    createTable(result);
});

$(document).ready(function () {
    getAllCompany();
});