$(document).ready(function () {
    // --- LOGIC FOR GET CREATE USER ---
    $("#btnCreateUser").on('click', function () {
        debugger
        $.get('/Account/Create', function (data) {
            $('#modalContent').html(data);
            $('#accountmodal').modal('show');
        });
    });

    // --- LOGIC FOR GET EDIT USER ---
    $(".btneditUser").on('click', function (e) {
        e.preventDefault();
        debugger

        var userId = $(this).data('id');

        $.get('/Account/Edit/' + userId, function (data) {
            $('#modalContent').html(data);
            $('#accountmodal').modal('show');
        }).fail(function () {
            alert("Failed to fetch the Edit form.");
        });
    });

    // --- Calling API For Create ---
    $("#createUserForm").submit( function (e) {
        e.preventDefault();
        debugger

        submitForm(
            "#createUserForm",
            "/api/Account/Create",

            function () {
                alert("User Saved Successfully.");
            });
    });

    // --- Calling API For EDIT ---

    $(document).on('submit', "#editUserForm", function (e) {
        e.preventDefault();
        debugger

        submitForm(
            "#editUserForm",
            "/api/Account/Edit",

            function () {
                alert("User Updated Successfully.");
            });
    });

    // Refresh Index
    function loadUserList() {
        $("#UserList").load("/Account/Index");
    }

    // AJAX Function to delete user
    $(document).on("click", ".btndeleteuser", function () {
        debugger
        var userId = $(this).data('id');

        if (!confirm("Are you sure you want to delete this User")) {
            return
        }
        debugger
        $.ajax({
            url: '/api/Account/Delete/' + userId,
            type: 'POST',
            success: function () {
                alert("User Deleted Successfully.");
                loadUserList();
            },
            error: function () {
                alert("Error deleting User");
            }
        });

    });
});