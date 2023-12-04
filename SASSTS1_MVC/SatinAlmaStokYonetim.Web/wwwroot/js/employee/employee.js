let employees = [];
let selectedEmployee;
let selectedEmployeId = 0;
let selectedBase64Image;
function createTable(data) {

    if (data !== null) {
        html = ``;
        html += ``;
        html += `<div class="table-responsive">`;
        html += `    <table class="table table-bordered table-sm w-100 dt-responsive nowrap" id="customDatatable">`;
        html += `        <thead>`;
        html += `           <tr>`;
        html += `                <th class="text-center">${let_queue}</th>`;
        html += `                <th  class="text-center">${let_photo}</th>`;
        html += `                <th>${let_nameSurname}</th>`;
        html += `                <th>${let_phone}</th>`;
        html += `                <th>${let_mail}</th>`;
        html += `                <th class="text-center">${let_edit}</th>`;
        html += `                <th class="text-center">${let_delete}</th>`;
        html += `            </tr>`;
        html += `       </thead>`;
        html += `       <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `           <tr>`;
            html += `               <td class="text-center">${i + 1}</td>`;
            html += `<td class="text-center"><img src="${data[i].image}" alt="product-img" title="employee-img" class="rounded me-2" height="32"></td>`
            html += `               <td>${data[i].employeeName} ${data[i].employeeSurname}</td>`;
            html += `               <td>${data[i].phone}</td>`;
            html += `               <td>${data[i].email}</td>`;
            html += `     <td class="text-center">`;
            html += `       <a onclick="editEmployee(${data[i].id});" data-bs-toggle="modal" data-bs-target="#addPersonelModal" class="action-icon">`;
            html += `         <i style="color:rgb(157, 173, 40);" class="ms-1 mdi mdi-lead-pencil"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `     <td class="text-center">`;
            html += `       <a href="javascript:deleteEmployee(${data[i].id});" class="action-icon">`;
            html += `         <i style="color:rgb(180, 93, 81);" class="ms-1 mdi mdi-delete-outline"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `            </tr > `;
        }
        html += `       </tbody > `;
        html += `   </table > `;
        document.getElementById("employeeTable").innerHTML = html;
    }
    else {
        document.getElementById("employeeTable").innerHTML = "";
    }
}
function deleteEmployee(id) {
    var employeeId = parseInt(id);
    Post('Admin/DeleteEmployee', employeeId, (data) => {
        if (data.isSuccess) {
            getAllEmployee();
            $.NotificationApp.send(let_employee, let_deleteSuccess, "bottom-right", "rgba(0,0,0,0.2)", "warning");
        }
        else {
            $.NotificationApp.send(let_employee, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");
        }
    });
}

function getBaseUrl() {
    let file = document.querySelector('input[type=file]')['files'][0];
    let reader = new FileReader();
    reader.onloadend = function () {
        selectedBase64Image = reader.result;
        document.getElementById("accountImg").src = selectedBase64Image;
    };
    reader.readAsDataURL(file);
}
function editEmployee(id) {
    selectedEmployee = employees.find(employee => employee.id === id);
    selectedEmployeId = selectedEmployee.id;
    document.getElementById("departmentOption").value = selectedEmployee.departmentId;
    document.getElementById("companyOption").value = selectedEmployee.companyId;
    document.getElementById("roleOption").value = selectedEmployee.roleId;
    document.getElementById("authorityOption").value = selectedEmployee.authorityId;
    document.getElementById("inputEmail").value = selectedEmployee.email;
    document.getElementById("inputName").value = selectedEmployee.employeeName;
    document.getElementById("inputSurname").value = selectedEmployee.employeeSurname;
    document.getElementById("inputPhone").value = selectedEmployee.phone;
    document.getElementById("inputIdentityNo").value = selectedEmployee.identityNo;
    selectedBase64Image = selectedEmployee.image;
    document.getElementById("accountImg").src = selectedBase64Image;
    $("#addPersonelModal").modal("show");
}
function getDemartment() {
    Get('Department/GetAllDepartment', (data) => {
        var html = `<option selected disabled value="0">${let_selectDepartment}</option>`;
        for (var i = 0; i < data.length; i++) {
            html += `<option value="${data[i].id}">${data[i].departmentName}</option>`;
        }
        $("#departmentOption").html(html);
    });
}
function getCompany() {
    Get('Company/GetAllCompany', (data) => {
        var html = `<option selected disabled value="0">${let_selectCompany}</option>`;
        for (var i = 0; i < data.length; i++) {
            html += `<option value="${data[i].id}">${data[i].companyName}</option>`;
        }
        $("#companyOption").html(html);
    });
}
function getRole() {
    Get('Role/GetAllRole', (data) => {
        var html = `<option selected disabled value="0">${let_selectRole}</option>`;
        for (var i = 0; i < data.length; i++) {
            html += `<option value="${data[i].id}">${data[i].roleName}</option>`;
        }
        $("#roleOption").html(html);
    });
}
function getAuthority() {
    Get('Authority/GetAllAuthority', (data) => {
        var html = `<option selected disabled value="0">${let_selectAuthority}</option>`;
        for (var i = 0; i < data.length; i++) {
            html += `<option value="${data[i].id}">${data[i].authorityName}</option>`;
        }
        $("#authorityOption").html(html);
    });
}
function getAllEmployee() {
    Get('Employee/GetAllEmployee', (data) => {
        employees = data;
        createTable(employees);
    });
}
function newEmployee() {
    resetModal();
    selectedEmployeId = 0;
    getCompany();
    getAuthority();
    getRole();
    getDemartment();
}
function saveEmployee() {
    if (selectedEmployeId === 0) {
        var accountItem = {
            Id: selectedEmployeId,
            Email: document.getElementById("inputEmail").value,
        }
        var data = {
            RoleId: parseInt(document.getElementById("roleOption").value),
            AuthorityId: parseInt(document.getElementById("authorityOption").value),
            CompanyId: parseInt(document.getElementById("companyOption").value),
            DepartmentId: parseInt(document.getElementById("departmentOption").value),
            EmployeeName: document.getElementById("inputName").value,
            EmployeeSurname: document.getElementById("inputSurname").value,
            Phone: document.getElementById("inputPhone").value,
            Email: document.getElementById("inputEmail").value,
            IdentityNo: document.getElementById("inputIdentityNo").value,
            Image: selectedBase64Image,
            AccountItems: accountItem,
        }
        Post('Employee/InsertEmployee', data, (resp) => {
            if (resp.isSuccess) {
                resetModal();
                $.NotificationApp.send(let_employee, let_regisSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
            }
            else {
                var messages = ``;
                for (var i = 0; i < resp.errors.length; i++) {
                    messages += `${resp.errors[i].errorMessage}<br />`
                }
                messages += `<br />`;
                /*       errorShow("errorFeedback1", messages);*/
                $.NotificationApp.send(let_employee, messages, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
        });
    }
    else {
        var data = {
            Id: selectedEmployeId,
            RoleId: parseInt(document.getElementById("roleOption").value),
            AuthorityId: parseInt(document.getElementById("authorityOption").value),
            CompanyId: parseInt(document.getElementById("companyOption").value),
            DepartmentId: parseInt(document.getElementById("departmentOption").value),
            EmployeeName: document.getElementById("inputName").value,
            EmployeeSurname: document.getElementById("inputSurname").value,
            Phone: document.getElementById("inputPhone").value,
            Email: document.getElementById("inputEmail").value,
            IdentityNo: document.getElementById("inputIdentityNo").value,
            Image: selectedBase64Image,
        }
        Post('Employee/UpdateEmployee', data, (data) => {
            if (data.isSuccess) {
                $.NotificationApp.send(let_employee, let_updateProcessSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
                resetModal();
            }
            else {
                $.NotificationApp.send(let_employee, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
        });
    }
}
function resetModal() {
    document.getElementById("companyOptionFilter").selectedIndex = `0`;
    $("#addPersonelModal").modal("hide");
    document.getElementById("inputEmail").value = "";
    document.getElementById("inputName").value = "";
    document.getElementById("inputSurname").value = "";
    document.getElementById("inputPhone").value = "";
    document.getElementById("inputIdentityNo").value = "";
    selectedBase64Image = '/assets/images/userNull.png';
    document.getElementById("accountImg").src = selectedBase64Image;
    getAllEmployee();
}
function companyOptionFilterChange() {
    let companyId = parseInt(document.getElementById("companyOptionFilter").value);
    if (companyId === 0) {
        getAllEmployee();
    }
    else {
        Post('Employee/GetAllEmployeeByCompanyId', companyId, (data) => {
            if (data !== null) {
                employees = data;
                createTable(employees);
            }
            else {
                document.getElementById("employeeTable").innerHTML = "";
            }
        });
    }
}

$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = employees.filter(employee => employee.employeeName.toLowerCase().includes(find) ||
        employee.employeeSurname.toLowerCase().includes(find));
    createTable(result);
});
function getCompanyFilter() {
    Get('Company/GetAllCompany', (data) => {
        var html = `<option selected value="0">${let_allCompanies}</option>`;
        for (var i = 0; i < data.length; i++) {
            html += `<option value="${data[i].id}">${data[i].companyName}</option>`;
        }
        $("#companyOptionFilter").html(html);
    });
}

$(document).ready(function () {
    newEmployee();
    getCompanyFilter();
    getAllEmployee();
});