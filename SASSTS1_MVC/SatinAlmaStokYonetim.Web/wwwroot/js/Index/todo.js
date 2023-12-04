let todolist = [];
function createTodo() {
    let text = document.getElementById("inputText").value;
    if (text !== null && text.trim() !== "") {
        var data = {
            EmployeeId: parseInt(UID),
            Text: text
        };
        Post('Todo/InsertTodo', data, (resp) => {
            getAllTodolist();
        });
    }
}
function deleteTodo() {
    var deletelist = [];
    if (todolist !== null) {
        for (var i = 0; i < todolist.length; i++) {
            if (todolist[i].active) {
                deletelist.push(todolist[i].id);
            }
        }
        Post('Todo/DeleteTodo', deletelist, (resp) => {
            getAllTodolist();
        });
    }
}
function checkboxAction(eid, id) {
    var elementName = "chkTodo" + eid;
    let active = false;
    if (document.getElementById(elementName).checked) {
        active = true;
    }
    var data = {
        Id: id,
        Active: active,
    }
    Post('Todo/UpdateTodo', data, (data) => {
        getAllTodolist();
    });
}
function createView(data) {
    if (data !== null) {
        html = ``;
        document.getElementById("divTest").innerHTML = html;
        let count = 0;
        for (var i = 0; i < data.length; i++) {
            html += `<li class="list-group-item border-0 ps-0">`;
            html += `   <div class="form-check mb-0">`;
            if (data[i].active) {
                count++;
                html += `<input type="checkbox" onclick="checkboxAction(${i},${data[i].id})" class="form-check-input todo-done" id="chkTodo${i}" checked>`;
                html += `   <label class="form-check-label" for="chkTodo${i}"><strike>${data[i].text}</strike></label>`;
            }
            else {
                html += `<input type="checkbox" onclick="checkboxAction(${i},${data[i].id})" class="form-check-input todo-done" id="chkTodo${i}">`;
                html += `   <label class="form-check-label" for="chkTodo${i}">${data[i].text}</label>`;
            }

            html += ` </div>`;
            html += `</li>`;
        }
        document.getElementById("todo-remaining").innerHTML = count;
        document.getElementById("todo-total").innerHTML = data.length;
        $("#divTest").html(html);
    }

    else {
        document.getElementById("todo-remaining").innerHTML = "0";
        document.getElementById("todo-total").innerHTML = "0";
        $("#divTest").html("");
    }
}
function getAllTodolist() {
    let Id = parseInt(UID);
    EmployeeId = Id;
    Post('Todo/GetAllTodoByEmployee', EmployeeId, (data) => {
        todolist = data;
        createView(todolist);
    });
}
$(document).ready(function () {
    getAllTodolist();
});