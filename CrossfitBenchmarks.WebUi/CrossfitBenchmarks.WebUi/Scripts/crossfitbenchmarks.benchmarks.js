CFBM.namespace("CFBM.Benchmarks");
CFBM.Benchmarks = (function () {
    var viewModel = null,
        tempViewModel = null,
        $container = null;

    function onReady(jsModel, rootContainer, logEntryType) {
        $container = rootContainer;
        viewModel = ko.mapping.fromJS(jsModel);

        tempViewModel = ko.mapping.fromJS(workoutLogEntryViewModel);
        viewModel.addNewViewModel = ko.observable(tempViewModel);
        viewModel.addNewViewModel().selectedWorkoutName = ko.observable("");
        viewModel.addNewViewModel().selectedWorkoutId = ko.observable(0);
        viewModel.addNewViewModel().userId = ko.observable(0);
        viewModel.addNewViewModel().logEntryType = ko.observable(logEntryType);

        $.each(viewModel.wodList(), function (index, item) {
            item.lastPrDateHumanized = ko.computed(function () {
                return moment(item.lastPersonalRecordDate()).fromNow();
            });

            item.lastAttemptDateHumanized = ko.computed(function () {
                return moment(item.lastAttemptDate()).fromNow();
            });
        });

        viewModel.selectedHeader = ko.observable("");
        viewModel.setHeader = function (data) {
            viewModel.selectedHeader(data.name);
        };
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
            viewModel.addNewViewModel().logEntryType(logEntryType); //not sure why i need this exactly... but i will get it worked out
        });
    };


    function onAddLogEntrySuccess(data) {
        var updatedItem = ko.utils.arrayFirst(viewModel.wodList(), function (item) {
            return item.id() == data.id;
        });

        if (updatedItem != null)
        {
            updatedItem.lastScore(data.lastScore);
            updatedItem.personalRecordScore(data.personalRecordScore);
            updatedItem.lastAttemptDate(data.lastAttemptDate);
            updatedItem.lastPersonalRecordDate(data.lastPersonalRecordDate);
        }
        $("#addLogEntry-modal").modal("hide");
    };

    return {
        ready: onReady,
        onAddLogEntrySuccess: onAddLogEntrySuccess
    };
}());

$(document).ready(function () {
    var module = CFBM.Benchmarks;

    if ($("#benchmarks-content").length) {
        module.ready(benchmarksViewModel, $("#benchmarks-content"), "B");
    }

    if ($("#theGirls-content").length) {
        //theGirlsModule.ready();
        module.ready(theGirlsViewModel, $("#theGirls-content"), "G");

    }

    if ($("#theHeros-content").length) {
        //theHerosModule.ready();
        module.ready(theHerosViewModel, $("#theHeros-content"), "H");
    }
});