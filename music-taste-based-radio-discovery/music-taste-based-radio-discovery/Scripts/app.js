(function ($, foundation) {
    var debug = true;

    function log(message) {
        if (debug) console.log(message);
    }

    log("init foundation..");
    $(document).foundation();
    log("OK");
})(jQuery, Foundation);