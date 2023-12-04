let sentMessages = [];

function createTable(data) {
    if (data !== null) {
        html = ``;
        html += ``;
        html += `<div class="table-responsive">`;
        html += `    <table class="table table-bordered table-sm w-100 dt-responsive nowrap">`;
        html += `        <thead>`;
        html += `           <tr>`;
        html += `               <th class="text-center numberPageTitle">${let_queue}</th>`;
        html += `               <th>${let_title}</th>`;
        html += `               <th>${let_content}</th>`;
        html += `               <th>${let_date}</th>`;
        html += `            </tr>`;
        html += `       </thead>`;
        html += `       <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `           <tr>`;
            html += `               <td class="text-center numberPageTitle" >#${i + 1}</td>`;
            html += `               <td>${data[i].subject}</td>`;
            html += `               <td>${data[i].text}</td>`;
            html += `               <td>${formatTarih(data[i].messageTime)}</td >`;
            html += `            </tr > `;
        }
        html += `       </tbody > `;
        html += `   </table > `;
        document.getElementById("divMessages").innerHTML = html;
    }

    else {
        document.getElementById("divMessages").innerHTML = "";
    }

}
function sendMessageList() {

    Get(`Message/GetAllMessages`, (resp) => {
        sentMessages = resp;
        createTable(sentMessages);
    });
}
function sendMessage() {

    var data = {
        Request: document.getElementById("checkRequest").checked,
        Success: document.getElementById("checkSuccess").checked,
        Buying: document.getElementById("checkBuying").checked,
        Management: document.getElementById("checkManagement").checked,
        Committee: document.getElementById("checkCommittee").checked,
        Accounting: document.getElementById("checkAccounting").checked,
        Subject: document.getElementById("txtSubject").value,
        Text: document.getElementById("txtMessage").value,
    };

    Post(`Message/CreateMessage`, data, (resp) => {
        $("#createMessageModal").modal("hide");
        resetModal();
        if (resp.isSuccess) {
            $.NotificationApp.send(let_createMessage, let_messageSended, "bottom-right", "rgba(0,0,0,0.2)", "success");
        }

        else {
            $.NotificationApp.send(let_createMessage, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "error");
        }
    });
}

function newMessage() {
    document.getElementById("checkRequest").checked = false;
    document.getElementById("checkSuccess").checked = false;
    document.getElementById("checkBuying").checked = false;
    document.getElementById("checkManagement").checked = false;
    document.getElementById("checkCommittee").checked = false;
    document.getElementById("checkAccounting").checked = false;
    document.getElementById("txtSubject").value = "";
    document.getElementById("txtMessage").value = "";
}
function resetModal() {
    sendMessageList();
    newMessage();
}
$(document).ready(function () {
    sendMessageList();
});