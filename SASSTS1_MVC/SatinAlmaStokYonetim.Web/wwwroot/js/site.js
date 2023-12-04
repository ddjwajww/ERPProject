var BASE_URI = "https://localhost:7172";

function PostRedirect(data, success) {
    $.ajax({
        url: `Login`,
        type: "POST",
        datatype: "json",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        processData: false,
        success: function (response) {
            if (response.result == 'Redirect') {
                Post(`Account/DeleteRandomNumberLogin`, data, (data) => {
                    window.location = response.url;
                });
            }  
            else {
                success(response);
            }
        },
        error: function (response) {
            if (response.status === 500) {
                $("#errorModal").modal("show");
            }
        }
    });
}
function Get(action, success) {
    $.ajax({
        type: "GET",
        url: `${BASE_URI}/${action}`,
      
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            success(response);
        },
        error: function (response) {
            if (response.status == 500) {
                errorShow("errorFeedback1", let_failedToEstablishServerConnection);//çevir
            }
        }
    });
}
function Post(action, data, success) {
    $.ajax({
        type: "POST",
        url: `${BASE_URI}/${action}`,
        async: true,
    
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        success: function (response) {
            success(response);
        },
        error: function (response) {
            if (response.status == 500) {
                errorShow("errorFeedback1", let_failedToEstablishServerConnection);//çevir
            }
        }
    });
}
function formatTarih(tarih) {
    const girilenTarih = new Date(tarih);
    if (isNaN(girilenTarih)) {
        return let_invalidDate;
    }
    const gun = girilenTarih.getDate().toString().padStart(2, '0');
    const ay = (girilenTarih.getMonth() + 1).toString().padStart(2, '0');
    const yil = girilenTarih.getFullYear();
    const saat = girilenTarih.toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' });
    return `${gun}.${ay}.${yil} - ${saat}`;
}

function formatTarih2(tarih) {
    const girilenTarih = new Date(tarih);
    if (isNaN(girilenTarih)) {
        return let_invalidDate;
    }
    const gun = girilenTarih.getDate().toString().padStart(2, '0');
    const ay = (girilenTarih.getMonth() + 1).toString().padStart(2, '0');
    const yil = girilenTarih.getFullYear();
    return `${gun}.${ay}.${yil}`;
}
function errorHide(element) {
    var element = document.getElementById(element);
    if (element !== null) {
        element.style.display = "none";
        element.innerHTML = "";
    }
}
function errorShow(element, messages) {
    var element = document.getElementById(element);
    if (element !== null) {
        element.style.display = "block";
        element.innerHTML = messages;
    }
}