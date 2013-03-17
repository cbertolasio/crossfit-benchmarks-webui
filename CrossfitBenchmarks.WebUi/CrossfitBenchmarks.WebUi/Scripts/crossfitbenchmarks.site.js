if (!window.console) {
    console = { log: function () { } }
};

var CFBM = CFBM || {};

CFBM.namespace = function (ns_string) {
    var parts = ns_string.split("."),
        parent = CFBM,
        i;

    if (parts[0] === "CFBM") {
        parts = parts.slice(1);
    }

    for (i = 0; i < parts.length; i += 1) {
        if (typeof parent[parts[i]] === "undefined") {
            parent[parts[i]] = {};
        }
        parent = parent[parts[i]];
    }
    return parent;
};

CFBM.clientTimeZone = function () {
    var offsetInMinutes = moment().zone();

    console.log(offsetInMinutes);
    return offsetInMinutes;
};

CFBM.registerToolTips = function () {
};

CFBM.Site = (function () {

    function applyHoverStyle() {
        $(".thumbnail", $("ul#wodItems")).on("hover", function (event) {
            $(this).toggleClass("hover");
        });
    };

    function registerPopvers() {
        $("[rel='popover']").popover({
            html: true,
            content: function () {
                return $($(this).attr("data-id")).html();
            }
        });
    }

    function registerToolTips() {
        $("[rel='tooltip']").tooltip({
            html: true,
            title: function () {
                return $($(this).attr("data-id")).html();
            }
        });
    }

    function post(url, data, successCallback, errorCallback, completeCallback) {
        //var json = $.parseJSON(data);
        $.ajax({
            url: url,
            data: data,
            //contentType: "application/json",
            error: errorCallback,
            success: successCallback,
            complete: completeCallback,
            type: "POST"
        });
    };

    function init() {
        var $modalContainer = $("#addLogEntry-modal");

        applyHoverStyle();
        registerToolTips();
        registerPopvers();

        $("#dp3", $modalContainer).datepicker();
        $(".timepicker", $modalContainer).timepicker(
            {
                minuteStep: 1
            });
    }

    return {
        init: init,
        post: post
    };
})();