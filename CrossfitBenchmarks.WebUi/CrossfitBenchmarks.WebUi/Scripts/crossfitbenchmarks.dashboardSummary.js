CFBM.namespace("CFBM.Dashboard");
CFBM.Dashboard = (function () {
    var viewModel = null,
    $container = null;


    function applyHoverStyle() {
        $(".thumbnail", $("ul#wodItems")).on("hover", function (event) {
            $(this).toggleClass("hover");
        });

        $("[rel='tooltip']").tooltip();
        $("[rel='popover']").popover();
    };

    function onReady(model, rootContainer) {
        $container = rootContainer;
        viewModel = ko.mapping.fromJS(model);

        ko.applyBindings(viewModel, $container[0]);

        applyHoverStyle();
    };

    return {
        ready:onReady
    };
}());

$(document).ready(function () {
    var module = CFBM.Dashboard;

    if ($("#summary-content").length) {
        module.ready(summaryData, $("#summary-content"));
    }
});
