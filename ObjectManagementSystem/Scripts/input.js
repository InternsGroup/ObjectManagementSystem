//only numbers allowed
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

//only characters allowed
function isNumericKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return true;
    return false;
}

//clearing search field after search action

function ClearFields() {
    document.getElementById("searchInput").value = "";
}


//auto hiding alert box effect
window.setTimeout(function () {
    $(".alert").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 5000);

// A function to format text to look like a phone number
function phoneFormat(input) {//returns (###) ###-####
    input = input.replace(/\D/g, '');
    var size = input.length;
    if (size > 0) { input = "(" + input }
    if (size > 3) { input = input.slice(0, 4) + ") " + input.slice(4, 11) }
    if (size > 6) { input = input.slice(0, 9) + "-" + input.slice(9) }
    return input;
}

function trim(input) {
    input = input.trim();
    return input;
}
