$(document).ready(function() {
$(function () {
    $('#select-shipper').click(function (event) {
        if (this.checked) {
            // Iterate each checkbox
            $("input[name = 'ShipperIDs']").each(function () {
                this.checked = true;
            });
        } else {
            $("input[name = 'ShipperIDs']").each(function () {
                this.checked = false;
            });
        }
    });
});
});