function SaveSupport() {

    var dataForm = {

        subject: document.getElementById("txtSubjectSupport").value,
        message: document.getElementById("txtMessageSupport").value

    };

    Post(`Support/CreateSupport`, dataForm, (resp) => {

        if (resp.isSuccess) {
            alert("Mesaj gönderildi");
        }
        else {
            alert("Mesaj gönderilemedi");
        }
    });
};