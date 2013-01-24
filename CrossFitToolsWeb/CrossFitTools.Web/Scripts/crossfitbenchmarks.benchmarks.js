

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

    //// Fix the margins when potentally the floating changed
    //$(window).resize(fixThumbnailMargins);

    //fixThumbnailMargins();
});


/**
* Adds 0 left margin to the first thumbnail on each row that don't get it via CSS rules.
* Recall the function when the floating of the elements changed.
*/
function fixThumbnailMargins() {
    $('.row-fluid .thumbnails').each(function () {
        var $thumbnails = $(this).children(),
            previousOffsetLeft = $thumbnails.first().offset().left;
        $thumbnails.removeClass('first-in-row');
        $thumbnails.first().addClass('first-in-row');
        $thumbnails.each(function () {
            var $thumbnail = $(this),
                offsetLeft = $thumbnail.offset().left;
            if (offsetLeft < previousOffsetLeft) {
                $thumbnail.addClass('first-in-row');
            }
            previousOffsetLeft = offsetLeft;
        });
    });
}