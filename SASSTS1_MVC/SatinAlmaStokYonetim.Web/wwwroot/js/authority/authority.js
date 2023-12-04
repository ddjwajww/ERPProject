let authorities = [];
let selectedAuhorityId = 0;
function createTable(data) {
    if (data != null) {
        html = ``;
        html += `<div class="table-responsive">`;
        html += `    <table class="table table-bordered table-sm w-100 dt-responsive nowrap" id="customDatatable">`;
        html += `        <thead>`;
        html += `           <tr>`;
        html += `               <th class="text-center">${let_queue}</th>`;
        html += `               <th>${let_authorityName}</th>`;
        html += `               <th class="text-center">${let_edit}</th>`;
        html += `               <th class="text-center">${let_delete}</th>`;
        html += `            </tr>`;
        html += `       </thead>`;
        html += `       <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `           <tr>`;
            html += `               <td class="text-center">${i + 1}</td>`;
            html += `               <td>${data[i].authorityName}</td>`;
            html += `     <td class="text-center">`;
            html += `       <a onclick="editAuthority(${data[i].id})"  style="cursor: pointer;" class="action-icon" data-bs-toggle="modal" data-bs-target="#addCompanyModal" >`;
            html += `         <i style="color:rgb(157, 173, 40);" class="ms-1 mdi mdi-lead-pencil"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `     <td class="text-center">`;
            html += `       <a onclick="deleteAuthority(${data[i].id})" style="cursor: pointer;" class="action-icon">`;
            html += `         <i style="color:rgb(180, 93, 81);" class="ms-1 mdi mdi-delete-outline"></i>`;
            html += `       </a>`;
            html += `            </tr > `;
        }
        html += `       </tbody > `;
        html += `   </table > `;
        document.getElementById("divAuhtorites").innerHTML = html;
    }
    else {
        document.getElementById("divAuhtorites").innerHTML = "";
    }
}


function newAuthority() {
    selectedAuhorityId = 0;
    document.getElementById("inputAuthorityName").value = "";
}

function saveData() {

    let authorityName = document.getElementById("inputAuthorityName").value;
    if (selectedAuhorityId > 0) {
        var data = {
            Id: selectedAuhorityId,
            AuthorityName: authorityName,
            IsDeleted: false,
        };
        Post(`Authority/UpdateAuthority`, data, (resp) => {
            if (resp.isSuccess) {
                document.getElementById("inputAuthorityName").value = "";
                $("#addAuthorityModal").modal("hide");
                $.NotificationApp.send(let_authority, let_updateProcessSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
            }
            else {
                $.NotificationApp.send(let_authority, let_updateProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
            }
            getAllAuthority();
        });
    }

    else {
        var data = {
            AuthorityName: authorityName,
            IsDeleted: false,
        };
        Post(`Authority/InsertAuthority`, data, (resp) => {
            if (resp.isSuccess) {
                document.getElementById("inputAuthorityName").value = "";
                $("#addAuthorityModal").modal("hide");
                $.NotificationApp.send(let_authority, let_registrationSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
            }
            else {
                $.NotificationApp.send(let_authority, let_registrationFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
                var messages = ``;
                for (var i = 0; i < resp.errors.length; i++) {
                    messages += `${resp.errors[i].errorMessage}<br />`
                }
                messages += `<br />`;
                errorShow("errorFeedback1", messages);
            }
            getAllAuthority();
        });
    }


}
function editAuthority(id) {
    selectedAuhorityId = id;
    let selectedAuhority = authorities.find(authority => authority.id === selectedAuhorityId);
    document.getElementById("inputAuthorityName").value = selectedAuhority.authorityName;
    $("#addAuthorityModal").modal("show");
}

function deleteAuthority(id) {

    Post('Authority/DeleteAuthority', parseInt(id), (resp) => {
        if (resp.isSuccess) {
            $.NotificationApp.send(let_authority, let_deleteSuccess, "bottom-right", "rgba(0,0,0,0.2)", "warning");
        }
        else {
            $.NotificationApp.send(let_authority, let_deleteFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
        }
        getAllAuthority();
    });
}
function getAllAuthority() {
    Get('Authority/GetAllAuthority', (data) => {
        authorities = data;
        createTable(authorities);
    });
}
$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = authorities.filter(authority => authority.authorityName.toLowerCase().includes(find));
    createTable(result);
});

$(document).ready(function () {
    getAllAuthority();
});