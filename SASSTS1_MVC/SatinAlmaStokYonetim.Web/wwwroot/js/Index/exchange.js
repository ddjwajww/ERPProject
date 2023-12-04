function getERW() {

    Get('DailyExchangeRate/Run', (data) => {
    });
}

$(document).ready(function () {
    getERW();
    // setInterval(getERW, 20000); 
});