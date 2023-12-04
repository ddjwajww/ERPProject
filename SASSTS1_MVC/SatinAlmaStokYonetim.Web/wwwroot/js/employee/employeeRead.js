let selectedEmployee;
let selectedImage;


Webcam.set({
    width: 420,
    height: 340,
    image_format: 'jpeg',
    jpeg_quality: 120,
    visibility: false
});
function start_camera() {
    Webcam.attach('#my_camera');
}
function take_snapshot() {
    Webcam.snap(function (data_uri) {
        selectedImage = data_uri;
        document.getElementById("inputCameraImage").src = selectedImage;
    });
}
function getBaseUrl() {
    let file = document.querySelector('input[type=file]')['files'][0];
    let reader = new FileReader();
    reader.onloadend = function () {
        selectedBase64Image = reader.result;
        document.getElementById("accountImg").src = selectedBase64Image;
        selectedImage = selectedBase64Image;
    };
    reader.readAsDataURL(file);
}

function saveCameraImage() {
    var data = {
        Image: selectedImage,
    };
    Post(`Image/ChangeImage`, data, (resp) => {
        if (resp.isSuccess) {
            $("#cameraModal").modal("hide");
            reload();
        }
    });

}

function saveAccountandEmployee() {
    var dataForm = {
        id: parseInt(UID),
        image: selectedImage,
        phone: document.getElementById("inputPhone").value,
        email: document.getElementById("inputEmail").value,
        userName: document.getElementById("inputUsername").value,
        userPassword: document.getElementById("inputPassword").value
    };
    Post('Employee/PostUpdateAccount', dataForm, (resp) => {
        if (resp.isSuccess) {
            getAccount();
            $("#accountModal").modal("hide");
            reload();
        }
        else {
        }
        Webcam.reset('#my_camera');
    });
}
function getAccount() {
    let id = parseInt(UID);
    Post('Employee/GetAccountbyAccountingId', id, (data) => {
        selectedEmployee = data;
        if (selectedEmployee !== null) {
            document.getElementById("inputUsername").value = selectedEmployee.userName;
            document.getElementById("inputPassword").value = selectedEmployee.userPassword;
            document.getElementById("inputEmail").value = selectedEmployee.email;
            document.getElementById("inputPhone").value = selectedEmployee.phone;
            let element = document.getElementById("baseAvatar");
            selectedImage = element.getAttribute('src');
        }
        else {
            document.getElementById("accountPhone").innerHTML = "";
            document.getElementById("accountEmail").innerHTML = "";
        }
    });
}

function reload() {
    window.location = BASE_URI + "/Employee/Home/Account";
}
$(document).ready(function () {
    getAccount();
});