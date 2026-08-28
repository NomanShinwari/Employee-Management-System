$(document).ready(function () {
    // --- LOGIC FOR CREATE EMPLOYEE ---
    $('#btnCreate').on('click', function () {
        debugger
        $.get('/Employee/Create', function (data) {
            $('#modalContent').html(data);
            $('#accountmodal').modal('show');
        });
    });

    // --- LOGIC FOR EDIT EMPLOYEE ---
    $('.btnEdit').on('click', function (e) {
        e.preventDefault();
        debugger
        var empId = $(this).data('id');

        $.get('/Employee/Edit/' + empId, function (data) {
            $('#modalContent').html(data);
            $('#accountmodal').modal('show');
        }).fail(function () {
            alert("Failed to fetch the Edit form.");
        });
    });

    // Calling api for Create submission
    $(document).on("submit", "#employeeForm", function (e) {
        e.preventDefault();

        submitForm(
            "#employeeForm",
            "/api/Employee/Create",

            function () {

                alert("Employee Saved Successfully");

            });

    });

    // Calling api for Edit submission
    $(document).on("submit", "#editEmployeeForm", function (e) {
        e.preventDefault();
        debugger

        submitForm(
            "#editEmployeeForm",
            "/api/Employee/Edit",

            function () {
                alert("Employee Updated Successfully");
            });
    });

    //Refresh Index
    function loadEmployeeList() {

        $("#employeeList").load("/Employee/Index");

    }

    // AJAX Function to delete employee
    $(document).on("click", ".btndelete",  function () {
        var empId = $(this).data('id');

        if (!confirm("Are you sure you want to delete this employee?")) {
            return
        }
        debugger
            $.ajax({
                url: '/api/Employee/Delete/' + empId,
                type: 'POST',
                headers: {
                    "Authorization": "Bearer " + token
                },
                success: function () {
                    alert("Employee deleted successfully.");
                    loadEmployeeList();
                },
                error: function () {
                    alert("Error deleting employee.");
                }
            });
    }); 
});