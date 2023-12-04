let selectedEmail;
let backupData;

function entryData() {
    selectedEmail = document.getElementById("username").value;
    var data = {
        UserData: selectedEmail,
        UserPassword: document.getElementById("password").value,
    }
    backupData = data;
    login(data);
}
function login(data) {
    Post(`Account/Log2`, data, (resp) => {
        if (resp.isSuccess) {
            document.getElementById("loginPage").style.display = "none";
            document.getElementById("loginTwoCodePage").style.display = "block";
            $.NotificationApp.send(let_loginProcess, let_verificationCodeSentToYourEmailAddress, "bottom-right", "rgba(0,0,0,0.2)", "success");
            zaman = 180;
            geriSay();
        }
        else {
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
        login(backupData);
    }
}
function loginTwoCode() {
    let code1 = document.getElementById("digit-1").value;
    let code2 = document.getElementById("digit-2").value;
    let code3 = document.getElementById("digit-3").value;
    let code4 = document.getElementById("digit-4").value;
    let code5 = document.getElementById("digit-5").value;
    let code6 = document.getElementById("digit-6").value;
    let strCode = "" + code1 + "" + code2 + "" + code3 + "" + code4 + "" + code5 + "" + code6;
    let code = 1;
    if (strCode.length > 5) {
        code = parseInt(strCode);
    }
    var data = {
        UserData: selectedEmail,
        RandomNumberNo: code,
    };
    PostRedirect(data, (resp) => {
        if (resp.isSuccess) {
        }
        else {
            var messages = ``;
            for (var i = 0; i < resp.errors.length; i++) {
                messages += `${resp.errors[i].errorMessage}<br />`
            }
            messages += `<br />`;
            errorShow("errorFeedback2", messages);
        }
    });
}
function resetInfo() {
    errorHide("errorFeedback1");
    errorHide("errorFeedback2");
}

$(document).ready(function () {

    var input = document.getElementById("digit-6");
    input.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            document.getElementById("giris").click();
        }
    });

    var input2 = document.getElementById("password");
    input2.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            document.getElementById("login-button").click();
        }
    });

    var input3 = document.getElementById("username");
    input3.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            document.getElementById("login-button").click();
        }
    });
});


function geriSay() {
    zaman = zaman - 1;
    kalan_dakika = ('0' + Math.floor((zaman % 3600) / 60)).slice(-2);
    kalan_saniye = ('0' + zaman % 60).slice(-2);

    var yeni_zaman = "";

    if (kalan_dakika > 0 || kalan_saniye > 0) {
        yeni_zaman = "<font style='font-size:14px;'>" + let_timeToLeftToEnterTheCode + " : " + "</font>" + "<font style='font-size:18px;'>" + kalan_dakika + ":" + kalan_saniye + "</font>";
        if (kalan_dakika == 0 && kalan_saniye < 30) { yeni_zaman = "<font style='color:red;'>" + yeni_zaman + "</font>"; }
    }
    document.getElementById('kalan_zaman').innerHTML = yeni_zaman;
    if (zaman > 0) {
        setTimeout("geriSay();", 1000);
        document.getElementById("giris").innerHTML = let_login;
        document.getElementById("giris").style.display = "block";
        document.getElementById("yeni_kod").style.display = "none";
    }
    else {
        document.getElementById('yeni_kod').innerHTML = let_newSendCode;
        document.getElementById("yeni_kod").style.display = "block";
        document.getElementById("giris").style.display = "none";
    }

}

