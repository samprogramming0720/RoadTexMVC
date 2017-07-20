$(function () {
    $('#CreateBtn').on('click', function () {
        var div = $('#AppendPartial');
        div.load("/Permission/Create", function () {
            div.dialog({
                modal: true,
                title: "Create New Permission"
            });
        });
    });
    $('.edit-mode').hide();
    $('.edit-permission').on('click', function () {
        var tr = $(this).parents('tr:first');
        tr.find('.edit-mode, .display-mode').toggle();
    });
    $('.cancel').on('click', function () {
        var tr = $(this).parents('tr:first');
        tr.find('.display-mode, .edit-mode').toggle();
        $('.edit-mode').hide();
    });
    $('.update-permission').on('click', function () {
        var tr = $(this).parents('tr:first');
        var permId = $(this).prop('id');
        var permName = tr.find('#perm_textbox').val();
        var PermissionDescription = permName;
        $.ajax({
            type: "POST",
            url: "/Permission/Edit",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "permId": permId, "perminfo": PermissionDescription }),
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
    
    $('.delete-permission').on('click', function () {
        var result = confirm('Are you sure you want to delete this permission?');
        if (result) {
            var tr = $(this).parents('tr:first');
            id = $(this).prop('id');
            $.ajax({
                type: "POST",
                url: "/Permission/Delete/" + id,
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