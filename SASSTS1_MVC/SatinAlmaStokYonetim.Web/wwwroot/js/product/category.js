let categories = [];
let selectedCategory;
function createTable(data) {
    html = ``;
    html += `<div class="table-responsive">`;
    html += `    <table class="table table-bordered table-sm w-100 dt-responsive nowrap" id="customDatatable">`;
    html += `        <thead>`;
    html += `           <tr>`;
    html += `               <th class="text-center">${let_queue}</th>`;
    html += `               <th>${let_categoryName}</th>`;
    html += `                <th>${let_transactions}</th>`;
    html += `            </tr>`;
    html += `       </thead>`;
    html += `       <tbody>`;
    for (var i = 0; i < data.length; i++) {
        html += `           <tr>`;
        html += `               <td class="text-center">${i + 1}</td>`;
        html += `               <td>${data[i].categoryName}</td>`;
        html += `               <td></td>`;
        html += `            </tr > `;
    }
    html += `       </tbody > `;
    html += `   </table > `;
    document.getElementById("divCategories").innerHTML = html;
}
function getAllCategory() {
    Get('Category/GetAllCategory', (data) => {
        categories = data;
        createTable(categories);
    });
}
function createCategory() {
    let categoryName = document.getElementById("inputCategoryName").value;
    let categoryId = parseInt(document.getElementById("inputCategoryId").value);
    var data = {
        Id: categoryId,
        CategoryName: categoryName,
    };
    document.getElementById("inputCategoryName").value = "";
    document.getElementById("inputCategoryId").value = "";

    saveCategory(data);
}
function editCategory(id) {
    selectedCategory = categories.find(category => category.id === id);
    var data = {
        Id: selectedCategory.id,
        CategoryName: selectedCategory.categoryName,
    }
    Post('https://localhost:7172/', data, (resp) => {
        categories = resp;
        createTable(categories);
    });
}
function saveCategory(data) {
    $("#addCategoryModal").modal("hide");
    Post('Admin/CreateCategory', data, (resp) => {
        if (resp.isSuccess) {
            $.NotificationApp.send(let_category, let_regisSuccess, "bottom-right", "rgba(0,0,0,0.2)", "success");
        }
        else {
            /*$.NotificationApp.send(let_category, let_regisFail, "bottom-right", "rgba(0,0,0,0.2)", "error");*/
            var messages = ``;
            for (var i = 0; i < resp.errors.length; i++) {
                messages += `${resp.errors[i].errorMessage}<br />`
            }
            messages += `<br />`;
            errorShow("errorFeedback1", messages);
            $.NotificationApp.send(let_category, messages, "bottom-right", "rgba(0,0,0,0.2)", "error");
        }
        getAllCategory();
    });
}
function deleteCompany(id) {

}
$("#searchText").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = categories.filter(category => category.categoryName.toLowerCase().includes(find));
    createTable(result);
});

$(document).ready(function () {
    getAllCategory();
});