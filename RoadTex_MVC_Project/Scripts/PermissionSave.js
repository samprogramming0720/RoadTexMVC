$(function () {
    $('#SavePermBtn').on('click', function () {
        var perm = $('#Perm_DESC').val();
        var model = {
            PermissionDescription: perm
        };
        $.ajax({
            type: "POST",
            url: "/Permission/Create",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "Model": model }),
            dataType: "json",
            success: function (data) {
                if (data.success) {
                    alert('New permission added');
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