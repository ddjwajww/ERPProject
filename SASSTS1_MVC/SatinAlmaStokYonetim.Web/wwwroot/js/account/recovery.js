
let accountId;
let backupCode;
let email="";
let backupData;
function entryData() {
    email = document.getElementById("emailaddress").value;
    var data = {
        UserData: email,
    };

    backupData = data;
    recovery(data);
}
function entryCode() {
    let code1 = document.getElementById("digit-1").value;
    let code2 = document.getElementById("digit-2").value;
    let code3 = document.getElementById("digit-3").value;
    let code4 = document.getElementById("digit-4").value;
    let code5 = document.getElementById("digit-5").value;
    let code6 = document.getElementById("digit-6").value;
    let strCode = "" + code1 + "" + code2 + "" + code3 + "" + code4 + "" + code5 + "" + code6;
    backupCode = parseInt(strCode);
    var data = {
        UserData: email,
        RandomNumberNo: backupCode,
    };
    Post(`Login/RandomNumberConfirm`, data, (data) => {
        if (data.errors != null ) {
            $.NotificationApp.send(let_codeVerification, let_wrongCode, "bottom-right", "rgba(0,0,0,0.2)", "danger");
            var messages = ``;
            for (var i = 0; i < data.errors.length; i++) {
                messages += `${data.errors[i].errorMessage}<br />`
            }
            messages += `<br />`;
            errorShow("errorFeedback2", messages);
        }
        else
        {
            accountId = parseInt(data.jsonData.data);
            document.getElementById("CodeInput").style.display = "none";
            document.getElementById("PasswordInput").style.display = "block";
            $.NotificationApp.send(let_password, let_codeVerificationSuccessful, "bottom-right", "rgba(0,0,0,0.2)", "success");
        }
    });
}
function entryNewPassword() {
    var data = {
        RandomNumberNo: backupCode,
        Id: accountId,
        UserPassword: document.getElementById("inputPassword").value,
    };
    Post(`Login/NewPassword`, data, (resp) => {
        if (resp.isSuccess) {
            Post(`Login/DeleteRandomNumber`, data, (resp2) => {
                window.location = `${BASE_URI}/Account/Login`;
            });
        }
        else {
            $.NotificationApp.send(let_password, let_duringProcessFail, "bottom-right", "rgba(0,0,0,0.2)", "danger");
            var messages = ``;
            for (var i = 0; i < resp.errors.length; i++) {
                messages += `${resp.errors[i].errorMessage}<br />`
            }
            messages += `<br />`;
            errorShow("errorFeedback3", messages);
        }
    });
}
function recovery() {
    email = document.getElementById("emailaddress").value;
    var data = {
        UserData: email,
    };
    Post(`Login/ForgottenPassword`, data, (resp) => {
        if (resp.isSuccess) {
            $.NotificationApp.send(let_forgetPassword, let_verificationCodeSendedMail, "bottom-right", "rgba(0,0,0,0.2)", "success");
            document.getElementById("EmailInput").style.display = "none";
            document.getElementById("CodeInput").style.display = "block";
            zaman = 180;
            geriSay();
        }
        else {
            $.NotificationApp.send(let_forgetPassword, let_mailAddressNotBeVerified, "bottom-right", "rgba(0,0,0,0.2)", "danger");
            var messages = ``;
            for (var i = 0; i < resp.errors.length; i++) {
                messages += `${resp.errors[i].errorMessage}<br />`
            }
            messages += `<br />`;
            errorShow("errorFeedback1", messages);
        }
    });
}

function retryLogin() {
    if (backupData !== null) {
        recovery(backupData);
    }
}


function geriSay() {
    zaman = zaman - 1;
    kalan_dakika = ('0' + Math.floor((zaman % 3600) / 60)).slice(-2);
    kalan_saniye = ('0' + zaman % 60).slice(-2);

    var yeni_zaman = "";

    if (kalan_dakika > 0 || kalan_saniye > 0) {
        yeni_zaman = "<font style='font-size:14px;'>" + let_timeToLeftToEnterTheCode + " : "+ "</font>" + "<font style='font-size:18px;'>" + kalan_dakika + ":" + kalan_saniye + "</font>";
        if (kalan_dakika == 0 && kalan_saniye < 30) { yeni_zaman = "<font style='color:red;'>" + yeni_zaman + "</font>"; }
    }

    document.getElementById('kalan_zaman').innerHTML = yeni_zaman;

    if (zaman > 0) {
        setTimeout("geriSay();", 1000);
        document.getElementById('giris').innerHTML = let_login;
        document.getElementById("giris").style.display = "block";
        document.getElementById("yeni_kod").style.display = "none";
    }
    else {
        document.getElementById('yeni_kod').innerHTML = let_newSendCode;
        document.getElementById("yeni_kod").style.display = "block";
        document.getElementById("giris").style.display = "none";
    }

}
$(document).ready(function () {

    var input = document.getElementById("digit-6");
    input.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            document.getElementById("giris").click();
        }
    });

    var input2 = document.getElementById("emailaddress");
    input2.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            document.getElementById("emailaddress2").click();
        }
    });

    var input3 = document.getElementById("inputPassword");
    input3.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            document.getElementById("inputPassword2").click();
        }
    });
});
