﻿if (!window.console) {
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
        $(".wodItems-container").on("hover", ".thumbnail", function (event) {
            $(this).toggleClass("hover");
        });
    };

    function registerPopvers() {
        $("[rel='popover'], .po").popover({
            html: true,
            content: function () {
                return $($(this).attr("data-id")).html();
            },
            placement: function (context, source) {
                var position = $(source).position();

                if (position.left > 515) {
                    return "left";
                }

                if (position.left < 515) {
                    return "right";
                }

                if (position.top < 110) {
                    return "bottom";
                }

                return "top";
            }
        });
    }

    function registerToolTips() {
        $("[rel='tooltip'], .tt").tooltip({
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