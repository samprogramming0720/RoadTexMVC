$(function () {
    $('.edit-mode').hide();
    $('.edit-user').on('click', function () {
        var tr = $(this).parents('tr:first');
        tr.find('.edit-mode, .display-mode').toggle();
    });
    $('.cancel').on('click', function () {
        var tr = $(this).parents('tr:first');
        tr.find('.display-mode, .edit-mode').toggle();
        $('.edit-mode').hide();
    });
    $('.save-user').on('click', function () {
        var tr = $(this).parents('tr:first');
        var userId = $(this).prop('id');
        var firstName = tr.find('#ED_FirstName').val();
        var lastName = tr.find('#ED_LastName').val();
        var eMail = tr.find("#ED_Email").val();
        var role = tr.find("#ED_Roles_List").val();
        var salesRep = tr.find("#ED_IsSalesRep").val();
        var preparer = tr.find("#ED_IsPreparer").val();
        var userinfo = {
            FirstName: firstName,
            LastName: lastName,
            Email: eMail,
            Role: role,
            IsSalesRep: salesRep,
            IsPreparer: preparer
        };
        $.ajax({
            type: "POST",
            url: "/UserManagement/Edit",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({"userId": userId, "userinfo": userinfo}),
            dataType: "json",
            success: function (data) {
                alert('Updated successfully');
                window.location.href = data;
            },
            error: function (err) {
                alert('error');
            }
        });
    });
    $('.delete-user').on('click', function () {
        var result = confirm('Are you sure you want to delete this user?');
        if (result) {
            var tr = $(this).parents('tr:first');
            id = $(this).prop('id');
            $.ajax({
                type: "POST",
                url: "/UserManagement/Delete/" + id,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.success)
                    {
                        alert('Deleted successfully');
                        window.location.href = data.url;
                    }
                    else {
                        alert('Cannot delete this record!');
                        window.location.href = data.url;
                    }
                },
                error: function () {
                    alert('error occurred while deleting');
                }
            });
        }
    });
    $('#CreateBtn').on('click', function () {
        var div = $('#AppendPartial');
        div.dialog({
            title: 'Add User',
            modal: true,
            open: function () {
                $(this).load('/UserManagement/Create');
            }
        });
    });
});