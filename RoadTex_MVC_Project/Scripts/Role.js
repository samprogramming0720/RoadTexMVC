$(function () {
    $('#CreateBtn').on('click', function () {
        var div = $('#AppendPartial');
        div.load("/Role/Create", function () {
            div.dialog({
                modal: true,
                title: "Create New Role"
            });
        });
    });
    $('.edit-mode').hide();
    $('.edit-role').on('click', function () {
        var tr = $(this).parents('tr:first');
        tr.find('.edit-mode, .display-mode').toggle();
    });
    $('.cancel').on('click', function () {
        var tr = $(this).parents('tr:first');
        tr.find('.display-mode, .edit-mode').toggle();
        $('.edit-mode').hide();
    });
    $('.update-role').on('click', function () {
        var tr = $(this).parents('tr:first');
        var roleId = $(this).prop('id');
        var roleName = tr.find('#role_textbox').val();
        var RoleDescription = roleName;
        $.ajax({
            type: "POST",
            url: "/Role/Edit",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "roleId": roleId, "roleinfo": RoleDescription }),
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

    $('.delete-role').on('click', function () {
        var result = confirm('Are you sure you want to delete this role?');
        if (result) {
            var tr = $(this).parents('tr:first');
            id = $(this).prop('id');
            $.ajax({
                type: "POST",
                url: "/Role/Delete/" + id,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.success) {
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
});