
$(document).ready(function () {
    $('#tblCaches').on('click', 'tr', function () {
        var $this = $(this);
        var id = $this.attr('id');
        $.ajax({
            url: "/Geocache/GetGeocacheById",
            ContentType: "application/html; charset=utf-8",
            type: "GET",
            data: { id: id },
            success: function (response, status, xhr) {
                $('#divGeocache').html(response);
            },
            error: function (response, status, xhr) {
                $('#divGeocache').html(response);
            }


        });


    });

});