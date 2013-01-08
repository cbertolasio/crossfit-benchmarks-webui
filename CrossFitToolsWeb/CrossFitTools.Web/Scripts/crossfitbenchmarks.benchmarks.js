

CFBM.namespace("CFBM.Benchmarks");
CFBM.Benchmarks = (function () {
    var viewModel = null,
        $container = $("#benchmarks-content");

    function onReady() {
        viewModel = ko.mapping.fromJS(benchmarksViewModel);
        
        $.each(viewModel.benchmarks(), function (index, benchmark) {
            benchmark.lastPrDateHumanized = ko.computed(function () {
                return moment(benchmark.lastPersonalRecordDate()).fromNow();
            });

            benchmark.lastAttemptDateHumanized = ko.computed(function () {
                return moment(benchmark.lastAttemptDate()).fromNow();
            });
        });

        ko.applyBindings(viewModel, $container[0]);
    };

    return {
        ready: onReady
    };
}());

$(document).ready(function () {
    var module = CFBM.Benchmarks;
    if ($("#benchmarks-content").length) {
        module.ready();
    }
});