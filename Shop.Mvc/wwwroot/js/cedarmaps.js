function contactMap() {
    // Map options
    var cm_options = { "center": { "lat": 35.7479198, "lng": 51.5474939 }, "maptype": "light", "scrollWheelZoom": false, "zoomControl": true, "zoom": 15, "minZoom": 6, "maxZoom": 17, "legendControl": false, "attributionControl": false }
    // Initialized CedarMap
    var map = window.L.cedarmaps.map('map_09kgdf', 'https://api.cedarmaps.com/v1/tiles/cedarmaps.streets.json?access_token=98e76563900bde072e3e71b2e62a31bc4334cf66', cm_options);
    // Markers options
    var markers = [{ "popupContent": "ایمن فروش", "center": { "lat": 35.7479198, "lng": 51.5474939 }, "iconOpts": { "iconUrl": "https://api.cedarmaps.com/v1/markers/marker-default.png", "iconRetinaUrl": "https://api.cedarmaps.com/v1/markers/marker-default@2x.png", "iconSize": [82, 98] } }];
    var markersLeaflet = [];
    var _marker = null;

    map.setView(cm_options.center, cm_options.zoom);
    // Add Markers on Map
    if (markers.length === 0) return;
    markers.map(function (marker) {
        var iconOpts = {
            iconUrl: marker.iconOpts.iconUrl,
            iconRetinaUrl: marker.iconOpts.iconRetinaUrl,
            iconSize: marker.iconOpts.iconSize,
            popupAnchor: [0, -49]
        };

        const markerIcon = {
            icon: window.L.icon(iconOpts)
        };

        _marker = new window.L.marker(marker.center, markerIcon);
        markersLeaflet.push(_marker);
        if (marker.popupContent) {
            _marker.bindPopup(marker.popupContent);
        }
        _marker.addTo(map);
    });
    // Bounding Map to Markers
    if (markers.length > 1) {
        var group = new window.L.featureGroup(markersLeaflet);
        map.fitBounds(group.getBounds(), { padding: [30, 30] });
    }
};

(function (c, e, d, a) {
    var p = c.createElement(e);
    p.async = 1; p.src = d;
    p.onload = a;
    c.body.appendChild(p);
})(document, 'script', 'https://api.cedarmaps.com/cedarmaps.js/v1.8.0/cedarmaps.js', contactMap);