$(document).ready(function () {
    // The ID is 'Username' because that is the name of your property
    
});


$('#username').on('change', function () {
    debugger
    var username = $(this).val();

    // Find the validation span associated with this field
    // MVC 5 generates a span with data-valmsg-for="Username"
    var validationSpan = $('span[data-valmsg-for="Username"]');

    if (username.length < 3) return; // Let built-in validation handle the empty case

    $.ajax({
        url: '/Account/Register',
        type: 'GET',
        data: { username: username },
        success: function (data) {
            if (data.isAvailable) {
                validationSpan.text("Username is available!").css("color", "green");
            } else {
                validationSpan.text("Sorry, this username is taken.").css("color", "red");
            }
        }
    });
});