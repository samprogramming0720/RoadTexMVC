$(function () {
    $('#AddBtn').on('click', function () {
        var firstName = $('#um_firstName_TBox').val();
        var lastName = $('#um_lastName_TBox').val();
        var eMail = $('#um_email_TBox').val();
        var password = $('#um_password_TBox').val();
        var role = $('#um_role_selector').val();
        var salesrep = $('#um_sales_rep_checkbox').prop("checked");
        var preparer = $('#um_preparer_checkbox').prop("checked");
        var userinfo = {
            FirstName: firstName,
            LastName: lastName,
            Password: password,
            Email: eMail,
            Role: role,
            IsSalesRep: salesrep,
            IsPreparer: preparer
        };
        $.ajax({
            type: "POST",
            url: "/UserManagement/Create",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "userinfo": userinfo }),
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    alert('New user added');
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