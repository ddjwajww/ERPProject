let invoices = [];
let selectedInvoice;
let InvoiceProducts = [];
function createTable(data) {
    if (data.length > 0) {
        html = ``;
        html += `<table class="table table-sm table-centered border" id="customDatatable">`;
        html += `  <thead class="table-light">`;
        html += `    <tr>`;
        html += `      <th>${let_queue}</th>`;
        html += `      <th>${let_billNo}</th>`;
        html += `      <th>${let_billDate}</th>`;
        html += `      <th>${let_supplierCompany}</th>`;
        html += `      <th>${let_print}</th>`;
        html += `    </tr>`;
        html += `  </thead>`;
        html += `  <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `    <tr>`;
            html += `      <td>${i + 1}</td>`;
            html += `      <td>${data[i].billNumber}</td>`;
            html += `      <td>${formatTarih(data[i].billDate)}</td>`;
            html += `      <td>${data[i].companyName}</td>`;
            html += `     <td>`;
            html += `       <a href="javascript:printView(${data[i].id});" class="action-icon">`;
            html += `         <i style="color:rgb(0, 173, 161);" class="ms-1 mdi mdi-printer"></i>`;
            html += `       </a>`;
            html += `     </td>`;
            html += `    </tr>`;
        }
        html += `  </tbody>`;
        html += `</table>`;
        $("#divInvoices").html(html);
    }

    else {
        $("#divInvoices").html("");
    }
}
function createDetailTable(data) {
    if (data.length > 0) {
        html = ``;
        html += `<table class="table table-sm table-centered" id="customDatatable">`;
        html += `  <thead class="table-light">`;
        html += `    <tr>`;
        html += `      <th>#</th>`;
        html += `      <th>${let_product}</th>`;
        html += `      <th>${let_quantity}</th>`;
        html += `      <th>${let_unitPrice}</th>`;
        html += `    </tr>`;
        html += `  </thead>`;
        html += `  <tbody>`;
        for (var i = 0; i < data.length; i++) {
            html += `    <tr>`;
            html += `      <td>${i + 1}</td>`;
            html += `      <td>${data[i].productName}</td>`;
            html += `      <td>${data[i].productQuantity}</td>`;
            html += `      <td>${data[i].unitPrice}</td>`;
            html += `    </tr>`;
        }
        html += `  </tbody>`;
        html += `</table>`;
        document.getElementById("productTotalAmount").innerHTML = selectedInvoice.productAmount + getIcon();
        document.getElementById("kdv").innerHTML = selectedInvoice.totalKDV + getIcon();
        document.getElementById("discount").innerHTML = selectedInvoice.discount + getIcon();
        document.getElementById("totalAmount").innerHTML = selectedInvoice.billTotalPrice + getIcon();
        document.getElementById("invoiceDate").innerHTML = formatTarih2(selectedInvoice.billDate);
        document.getElementById("invoiceNo").innerHTML = selectedInvoice.billNumber;
        $("#divInvoiceDetail").html(html);
    }
    else {
        $("#divInvoiceDetail").html("");
    }
}
function getIcon() {
    let icon = "₺";
    switch (selectedInvoice.currency) {
        case "Dolar": icon = "$"; break;
        case "Euro": icon = "€"; break;
        case "Sterlin": icon = "£"; break;
        default: icon = "₺"; break;
    }
    return icon;
}
function printView(id) {
    selectedInvoice = invoices.find(invoices => invoices.id === id);
    Post('Bill/GetBillDetailbyBillId', id, (data) => {
        InvoiceProducts = data;
        createDetailTable(InvoiceProducts);
        $("#invoicePrintModal").modal("show");
    });
}
function getAllInvoices() {
    let companyId = parseInt(CID);
    Post('Bill/GetallBillbyCompanyId', companyId, (data) => {
        invoices = data;
        createTable(invoices);
    });
}
function printData() {
    printElement(document.getElementById("printPage"));
}
function printElement(elem) {
    var domClone = elem.cloneNode(true);
    var printSection = document.getElementById("printSection");
    if (!printSection) {
        var printSection = document.createElement("div");
        printSection.id = "printSection";
        document.body.appendChild(printSection);
    }
    printSection.innerHTML = "";
    printSection.appendChild(domClone);
    window.print();
}

$("#searchInvoice").on("keyup", function () {
    const find = $(this).val().toLowerCase();
    const result = invoices.filter(invoice => invoice.billNumber.toLowerCase().includes(find));
    createTable(result);
});

$(document).on("click", ".print", function () {
    const section = $("section");
    const modalBody = $(".modal-body").detach();
    const divInv = $(".divInv").detach();
    section.append(modalBody);
    window.print();
    section.empty();
    section.append(divInv);
    $(".modal-body-wrapper").append(modalBody);
});

$(document).ready(function () {
    getAllInvoices();
});