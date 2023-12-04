let request1 = [];
let request2 = [];
let request3 = [];
let requests = [];
function createTable(data, element, status) {
    document.getElementById(element).innerHTML = "";
    html = ``;
    html += `<table class="table table-centered table-sm border mb-0">`;
    html += `    <thead>`;
    html += `        <tr>`;
    html += `           <th>#</th>`;
    html += `            <th>${let_requestNo}</th>`;
    html += `           <th>${let_company}</th>`;
    html += `           <th class="text-center">${let_status}</th>`;
    html += `        </tr>`;
    html += `    </thead>`;
    html += `    <tbody>`;
    html += `        <tr>`;
    for (var i = 0; i < data.length; i++) {
        html += `            <td>${i + 1}</td>`;
        html += `            <td>${data[i].requestNo}</td>`;
        html += `            <td>${CNA}</td>`;
        if (status === 1) {
            html += `   <td class="text-center mdi mdi-circle text-success"></td>`;
        }
        else if (status === 2) {
            html += `     <td class="text-center mdi mdi-circle text-warning"></td>`;
        }
        else {
            html += `  <td class="text-center mdi mdi-circle text-danger"></td>`;
        }
        html += `       </tr>`;
    }
    html += `   </tbody>`;
    html += `</table>`;
    document.getElementById(element).innerHTML = html;
}
function getAllRequest() {
    let companyId = parseInt(CID);
    Post('Request/GetAllNoStatus', companyId, (data) => {
        requests = data;
        request1 = requests.filter(request => request.requestStatus === 1);
        request2 = requests.filter(request => request.requestStatus === 0);
        request3 = requests.filter(request => request.requestStatus === 2);
        createTable(request1, "divRequest1", 1);
        createTable(request2, "divRequest2", 2);
        createTable(request3, "divRequest3", 3);
    });
}
$(document).ready(function () {
    getAllRequest();
});