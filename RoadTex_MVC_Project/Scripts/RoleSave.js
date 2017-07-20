$(function () {
    $('#SaveRoleBtn').on('click', function () {
        var role = $('#Role_DESC').val();
        var model = {
            RoleDescription: role
        };
        $.ajax({
            type: "POST",
            url: "/Role/Create",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "Model": model }),
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    alert('New role added');
                    window.location.href = data.url;
                }
                else {
                    $('#Error-message').text(data.result);
                }
            },
            error: function (err) {
                alert('error');
            }
        });
    });
});