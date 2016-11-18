(function ($, foundation) {
    var debug = true;

    function log(message) {
        if (debug) console.log(message);
    }

    log("init foundation..");
    $(document).foundation();
    log("OK");

    var $elem = $("#what");
    var elem = new foundation.Magellan($elem, {});

    log(elem);
})(jQuery, Foundation);