$(document).ready(function () {

    $("#LoginForm").submit(function (e) {
        debugger
        console.log("Submit event fired");

        e.preventDefault();

        $.ajax({
            url: '/api/Account/Login',
            type: 'POST',
            data: $("#LoginForm").serialize(),

            success: function (response) {

                document.cookie = "jwtToken=" + encodeURIComponent(response.Token) + "; path=/";
                window.location.href = '/Account/LoggedIn';
            },
            error: function () {

            }
        });

    });
});