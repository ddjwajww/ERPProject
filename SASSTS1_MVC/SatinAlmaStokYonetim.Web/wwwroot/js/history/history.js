let histories = [];
function createTable(data) {
    if (data.length > 0) {
        html = ``;
        html += `<table class="table table-sm table-centered border" id="customDatatable">`;
        html += `    <thead class="table-light">`;
        html += `       <tr>`;
        html += `           <th class="text-center" style="max-width:80px;">${let_queue}</th>`;
        html += `           <th class="text-center">${let_transactionTime}</th>`;
        html += `           <th class="text-center">${let_transactionType}</th>`;
        html += `           <th class="text-center">${let_description}</th>`;
        html += `    </thead>`;
        html += `    <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `       <tr>`;
            html += `<td class="text-center" style="max-width:0px;">${i + 1}</td>`;
            html += `<td class="text-center">${formatTarih(data[i].processTime)}</td >`;
            html += `<td class="text-center">${data[i].processType}</td>`;
            html += `<td class="text-center">${data[i].processDetail}</td>`;
            html += `       </tr>`;
        }
        html += ``;
        document.getElementById("divHistory").innerHTML = html;
    }
    else {
        document.getElementById("divHistory").innerHTML = "";
    }
}
function getAllHistories() {
    var employeeId = parseInt(UID);
    Post('ProcessHistory/GetHistories', employeeId, (data) => {
        histories = data;
        createTable(histories);
    });
}

$(document).ready(function () {
    getAllHistories();
});