$(document).ready(function () {
    var lat = $('#lblLat').text();
    var long = $('#lblLong').text();
    var map = new GMaps({
        div: '#map',
        lat: lat,
        lng: long,
        width: '400px',
        height: '200px'
    });

    map.addMarker({
        lat: lat,
        lng: long,
        title: $(this).html()
    });

});