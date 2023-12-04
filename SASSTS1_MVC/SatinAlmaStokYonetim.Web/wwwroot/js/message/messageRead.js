let sentMessages = [];

function createTable(data) {
    if (data !== null) {
        html = ``;
        html += `<div class="table-responsive">`;
        html += `    <ul class="email-list">`;
        for (var i = 0; i < data.length; i++) {
            html += `<li class="unread">`;
            html += `    <div class="email-sender-info">`;
            html += `        <div class="checkbox-wrapper-mail">`;
            html += `            <div class="form-check">`;
            html += `               <input type="checkbox" class="form-check-input" id="mail1">`;
            html += `               <label class="form-check-label" for="mail1"></label>`;
            html += `           </div>`;
            html += `        </div>`;
            html += `        <span class="star-toggle mdi mdi-star-outline text-warning"></span>`;
            html += `        <a href="javascript: void(0);" class="email-title">${data[i].subject}</a>`;
            html += `    </div>`;
            html += `    <div class="email-content">`;
            html += `        <a href="javascript: void(0);" class="email-subject">`;
            html += `            <span>${data[i].text}</span>`;
            html += `       </a>`;
            html += `        <div class="email-date">${formatTarih2(data[i].messageTime)}</div >`;
            html += `    </div>`;
            html += `   <div class="email-action-icons">`;
            html += `       <ul class="list-inline">`;
            html += `            <li class="list-inline-item">`;
            html += `               <a href="javascript: void(0);"><i class="mdi mdi-delete email-action-icons-item"></i></a>`;
            html += `           </li>`;
            html += `           <li class="list-inline-item">`;
            html += `                <a href="javascript: void(0);"><i class="mdi mdi-email-open email-action-icons-item"></i></a>`;
            html += `           </li>`;
            html += `       </ul>`;
            html += `   </div>`;
            html += `</li>`;
        }
        html += `       </ul> `;
        html += `   </div>`;
        document.getElementById("divMessages").innerHTML = html;
    }
    else {
        document.getElementById("divMessages").innerHTML = "";
    }
}
function getAllMessages() {

    Post(`Message/GetAllbyAuthorityId`, parseInt(AID), (resp) => {
        sentMessages = resp;
        createTable(sentMessages);
    });
}

$(document).ready(function () {
    getAllMessages();
});