function submitForm(formSelector, apiUrl, onSuccess) {
    debugger
        var formData = $(formSelector).serialize();

        $.ajax({
            url: apiUrl,
            type: "POST",
            data: formData,

            success: function (response) {

                if (typeof onSuccess === "function") {
                    onSuccess(response);
                }
                // CLOSE MODAL
                $("#accountModal").modal("hide");

                // clear old form content
                $("#modalContent").html("");

                // refresh list
                location.reload();
            },

            error: function () {
                alert("Something went wrong.");
            }
        });
}

function loadDashboard() {

    $.get("/api/Dashboard/GetDashboardInfo", function (responce) {
        if (responce.IsSuccess) {
            $("#totalUsers").text(responce.Data.TotalUsers);
            $("#activeUsers").text(responce.Data.ActiveUsers);
            $("#newUsers").text(responce.Data.NewRegistrations);
            $("#departments").text(responce.Data.TotalDepartments);
            $("#employees").text(responce.Data.TotalEmployees);

           // New chart logic
            renderDashboardChart(responce.Data);
        }
    });
}

function renderDashboardChart(data) {
    const ctx = document.getElementById('dashboardChart').getContext('2d');

    // Destroy previous instance to prevent overlapping charts on refresh
    if (window.myChart instanceof Chart) {
        window.myChart.destroy();
    }

    window.myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Total Users','Employees', 'Departments'],
            datasets: [{
                label: 'Dashboard Stats',
                data: [
                    data.TotalUsers,
                    data.TotalEmployees,
                    data.TotalDepartments
                ],
                backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc']
            }]
        }
    });
}