
    $(function () {
        $("#myAlert").bind('closed.bs.alert', function () {
            alert("Alert message box is closed.");
        });
                    });



window.setTimeout(function () {
    $(".alert").fadeTo(5000, 0).slideUp(200, function () {
        $(this).remove();
    });
}, 4000);