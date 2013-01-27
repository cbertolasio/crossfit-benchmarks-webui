

CFBM.namespace("CFBM.Benchmarks");
CFBM.Benchmarks = (function () {
    var viewModel = null,
        tempViewModel = null,
        $container = $("#benchmarks-content");

    function onReady() {
        viewModel = ko.mapping.fromJS(benchmarksViewModel);

        tempViewModel = ko.mapping.fromJS(workoutLogEntryViewModel);
        viewModel.addNewViewModel = ko.observable(tempViewModel);
        viewModel.addNewViewModel().selectedWorkoutName = ko.observable("");
        viewModel.addNewViewModel().selectedWorkoutId = ko.observable(0);
        viewModel.addNewViewModel().userId = ko.observable(0);
        viewModel.addNewViewModel().logEntryType = ko.observable("B");

        $.each(viewModel.benchmarks(), function (index, benchmark) {
            benchmark.lastPrDateHumanized = ko.computed(function () {
                return moment(benchmark.lastPersonalRecordDate()).fromNow();
            });

            benchmark.lastAttemptDateHumanized = ko.computed(function () {
                return moment(benchmark.lastAttemptDate()).fromNow();
            });
        });

        ko.editable(viewModel);
        ko.applyBindings(viewModel, $container[0]);
        $("#dp3", $container).datepicker();


        $(".save-button", $(".modal-footer")).click(function () {
            $(".addNewLogEntry-form", $(".modal-body")).submit();
        });

        $(".cancel-button", $(".modal-footer")).click(function () {
            viewModel.rollback();
        });

        $(".addNew-button", $container).click(function () {
            viewModel.beginEdit();
            viewModel.addNewViewModel().selectedWorkoutName($(this).closest("div.thumbnail").find("h4").html());
            viewModel.addNewViewModel().selectedWorkoutId($(this).closest("div.thumbnail").attr("id"));
            viewModel.addNewViewModel().userId(3); //note this needs to come out soon...
            viewModel.addNewViewModel().logEntryType("B"); //not sure why i need this exactly... but i will get it worked out
        });
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