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