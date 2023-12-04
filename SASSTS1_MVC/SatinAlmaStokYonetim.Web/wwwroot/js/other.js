
document.addEventListener('DOMContentLoaded', function () {
    var theme = localStorage.getItem('theme');
    if (theme === 'light') {
        document.getElementById('light-style').disabled = true;
        document.getElementById('dark-style').disabled = false;
        element = document.getElementById('lightMode');
        if (element !== null) {
            element.checked = false;
        }
    } else {
        document.getElementById('light-style').disabled = false;
        document.getElementById('dark-style').disabled = true;
        element = document.getElementById('lightMode');
        if (element !== null) {
            element.checked = true;
        }
    }
});
function lightModeTogle() {

    if (document.getElementById("dark-style").disabled) {
        document.getElementById("dark-style").disabled = false;
        document.getElementById("light-style").disabled = true;
        localStorage.setItem('theme', 'light');
    } else {
        document.getElementById("light-style").disabled = false;
        document.getElementById("dark-style").disabled = true;
        localStorage.setItem('theme', 'dark');
    }
}
$(document).ready(function () {
    $("#datapicker1").datepicker({ dateFormat: 'yy-mm-dd' });
});