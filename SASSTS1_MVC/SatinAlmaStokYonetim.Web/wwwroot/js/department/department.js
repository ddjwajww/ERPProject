let departments = [];
let selectedDepartmentId = 0;
function getAllDepartment() {
    Get('Admin/GetAllDepartment', (data) => {
        departments = data;
        createTable(departments);
    });
}

function saveDepartment() {

    let departmentName = document.getElementById("inputDepartmentName").value;
    let departmentNo = document.getElementById("inputDepartmentNo").value;

    if (selectedDepartmentId > 0) {
        var data = {
            Id: selectedDepartmentId,
            DepartmentName: departmentName,
            DepartmentNo: departmentNo,
            IsDeleted: false,
        }
        Post('Department/UpdateDepartment', data, (resp) => {
            if (resp.isSuccess) {
                $.NotificationApp.send(let_department, let_updateSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
                getAllDepartment();
                resetModal();
            }
            else {
                $.NotificationApp.send(let_department, let_updateFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
        });
    }
    else {
        var data = {
            DepartmentName: departmentName,
            DepartmentNo: departmentNo
        };
        Post(`Department/InsertDepartment`, data, (resp) => {
            if (resp.isSuccess) {
                getAllDepartment();
                resetModal();
                $.NotificationApp.send(let_department, let_regisSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
            }
            else {
                $.NotificationApp.send(let_department, let_regisFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }

        });
    }
}

function newDepartment() {
    selectedDepartmentId = 0;
}
function deleteDepartment(id) {
    Post(`Department/DeleteDepartment`, parseInt(id), (resp) => {
        if (resp.isSuccess) {
            getAllDepartment();
            $.NotificationApp.send(let_department, let_deleteSuccess, "bottom-right", "rgba(0,0,0,0.2)", "warning");
        }
        else {
            $.NotificationApp.send(let_department, let_deleteFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
        }
    });
}

function edit(id) {
    selectedDepartmentId = id;
    $("#addDepartmentModal").modal("show");
    var dep = departments.find(department => department.id === id);
    document.getElementById("inputDepartmentName").value = dep.departmentName;
    document.getElementById("inputDepartmentNo").value = dep.departmentNo;
}
function resetModal() {
    document.getElementById("inputDepartmentName").value = "";
    document.getElementById("inputDepartmentNo").value = "";
    $("#addDepartmentModal").modal("hide");
}
function createTable(data) {

    if (data != null) {
        html = ``;
        html += ``;
        html += `<div class="table-responsive">`;
        html += `    <table class="table table-bordered table-sm w-100 dt-responsive nowrap" id="customDatatable">`;
        html += `        <thead>`;
        html += `           <tr>`;
        html += `               <th class="text-center">${let_queue}</th>`;
        html += `               <th>${let_departmentName}</th>`;
        html += `               <th>${let_departmentNo}</th>`;
        html += `               <th>${let_edit}</th>`;
        html += `               <th>${let_delete}</th>`;
        html += `            </tr>`;
        html += `       </thead>`;
        html += `       <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `           <tr>`;
            html += `               <td class="text-center">#${i + 1}</td>`;
            html += `               <td>${data[i].departmentName}</td>`;
            html += `               <td>${data[i].departmentNo}</td>`;
            html += `     <td class="text-center">`;
            html += `       <a onclick="edit(${data[i].id})" style="cursor:pointer;" class="action-icon">`;
            html += `         <i style="color:rgb(157, 173, 40);" class="ms-1 mdi mdi-lead-pencil"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `     <td class="text-center">`;
            html += `       <a onclick = "deleteDepartment(${data[i].id})" style="cursor:pointer;" class="action-icon">`;
            html += `         <i style="color:rgb(180, 93, 81);" class="ms-1 mdi mdi-delete-outline"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `            </tr > `;
        }
        html += `       </tbody > `;
        html += `   </table > `;
        document.getElementById("divDepartments").innerHTML = html;
    }
    else {
        document.getElementById("divDepartments").innerHTML = "";

    }
}

$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = departments.filter(department => department.departmentName.toLowerCase().includes(find));
    createTable(result);
});
$(document).ready(function () {
    getAllDepartment();
});