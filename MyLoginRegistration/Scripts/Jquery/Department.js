$(document).ready(function () {

    // --- LOGIC FOR GET CREATE DEPARTMENT ---

    $("#btnCreateDeptt").on('click', function () {
        debugger
        $.get('/Department/Create', function (data) {
            $('#modalContent').html(data);
            $('#accountmodal').modal('show');
        });
    });

    // --- LOGIC FOR GET EDIT DEPARTMENT ---
    $(".btnEditDeptt").on('click', function (e) {
        e.preventDefault()
        debugger

        var depttid = $(this).data('id');
        $.get('/Department/Edit/' + depttid, function (data) {
            $('#modalContent').html(data);
            $('#accountmodal').modal('show');
        }).fail(function () {
            alert("Failed to fetch the Edit form.");
        });
    });

    // --- Calling API For Create ---
    $(document).on('submit', "#createdepttForm", function (e) {
        e.preventDefault();
        debugger
        submitForm(
            "#createdepttForm",
            "/api/Deppt/Create",
            function () {
                alert("Department Saved Successfully.");
            });
    });


    // --- Calling API for EDIT ---
    $(document).on('submit', "#editDepttForm", function (e) {
        e.preventDefault();
        debugger

        submitForm(
            "#editDepttForm",
            "/api/Deppt/Edit",
            function () {
                alert("Department Updated Successfully.");
            });
    });
    // Refresh Index
    function loadDepartmentlist() {
        $("#Departmentlist").load("/Department/Index");
    }

    // Ajax call for Delete
    $(document).on("click", ".btndeletedeptt", function (e) {
        var depttid = $(this).data('id');

        if (!confirm("Are you sure you want to delete this department?")) {
            return
        }
        debugger
        $.ajax({
            url: '/api/Deppt/Delete/' + depttid,
            type: 'POST',
            success: function () {
                alert("Department deleted successfully.");
                loadDepartmentlist();
            },
            error: function () {
                alert("Failed to delete the department.");
            }
        });
    });
});