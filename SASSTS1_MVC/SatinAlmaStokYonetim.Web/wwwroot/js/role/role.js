let roles = [];
let selectedRoleId = 0;
function createTable(data) {
    if (data != null) {
        html = ``;
        html += `<div class="table-responsive">`;
        html += `    <table class="table table-bordered table-sm w-100 dt-responsive nowrap" id="customDatatable">`;
        html += `        <thead>`;
        html += `           <tr>`;
        html += `               <th class="text-center">${let_queue}</th>`;
        html += `               <th>${let_roleName}</th>`;
        html += `               <th class="text-center">${let_edit}</th>`;
        html += `               <th class="text-center">${let_delete}</th>`;
        html += `            </tr>`;
        html += `       </thead>`;
        html += `       <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `           <tr>`;
            html += `               <td class="text-center">${i + 1}</td>`;
            html += `               <td>${data[i].roleName}</td>`;
            html += `     <td class="text-center">`;
            html += `       <a onclick="editRole(${data[i].id})"  style="cursor: pointer;" class="action-icon" data-bs-toggle="modal" data-bs-target="#addCompanyModal" >`;
            html += `         <i style="color:rgb(157, 173, 40);" class="ms-1 mdi mdi-lead-pencil"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `     <td class="text-center">`;
            html += `       <a onclick="deleteRole(${data[i].id})" style="cursor: pointer;" class="action-icon">`;
            html += `         <i style="color:rgb(180, 93, 81);" class="ms-1 mdi mdi-delete-outline"></i>`;
            html += `       </a>`;
            html += `            </tr > `;
        }
        html += `       </tbody > `;
        html += `   </table > `;
        document.getElementById("divRoles").innerHTML = html;
    }
    else {
        document.getElementById("divRoles").innerHTML = "";
    }
}


function newRole() {
    selectedRoleId = 0;
    document.getElementById("inputRoleName").value = "";
}

function saveData() {

    let roleName = document.getElementById("inputRoleName").value;
    if (selectedRoleId > 0) {
        var data = {
            Id: selectedRoleId,
            RoleName: roleName,
            IsDeleted: false,
        };
        Post(`Admin/UpdateRole`, data, (resp) => {
            if (resp.isSuccess) {
                document.getElementById("inputRoleName").value = "";
                $("#addRoleModal").modal("hide");
                $.NotificationApp.send(let_role, let_updateSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
            }
            else {
                $.NotificationApp.send(let_role, let_updateFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
            getAllRoles();
        });
    }

    else {
        var data = {
            RoleName: roleName,
            IsDeleted: false,
        };
        Post(`Admin/InsertRole`, data, (resp) => {
            if (resp.isSuccess) {
                document.getElementById("inputRoleName").value = "";
                $("#addRoleModal").modal("hide");
                $.NotificationApp.send(let_role, let_regisSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
            }
            else {
                $.NotificationApp.send(let_role, let_regisFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
            getAllRoles();
        });
    }


}
function editRole(id) {
    selectedRoleId = id;
    let selectedRole = roles.find(role => role.id === selectedRoleId);
    document.getElementById("inputRoleName").value = selectedRole.roleName;
    $("#addRoleModal").modal("show");
}

function deleteRole(id) {

    Post('Admin/DeleteRole', parseInt(id), (resp) => {
        if (resp.isSuccess) {
            $.NotificationApp.send(let_role, let_deleteSuccess, "bottom-right", "rgba(0,0,0,0.2)", "warning");
        }
        else {
            $.NotificationApp.send(let_role, let_deleteFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
        }
        getAllRoles();
    });
}
function getAllRoles() {
    Get('Admin/GetAllRole', (data) => {
        roles = data;
        createTable(roles);
    });
}
$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = roles.filter(role => role.roleName.toLowerCase().includes(find));
    createTable(result);
});

$(document).ready(function () {
    getAllRoles();
});