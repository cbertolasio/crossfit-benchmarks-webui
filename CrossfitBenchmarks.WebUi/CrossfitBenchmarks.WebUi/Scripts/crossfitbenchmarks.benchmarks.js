CFBM.namespace("CFBM.Benchmarks");

CFBM.Benchmarks = (function () {
    var viewModel = null,
        addNewViewModel = null,
        $container = null,
        $modalContainer=$("#addLogEntry-modal")[0],
        logEntryType = null;

    function toggleAddNew() {
        $($container).toggle(500);
        $($modalContainer).toggle(500);
    };
    function onReady(jsModel, rootContainer, logEntryType) {
        $container = rootContainer;
        viewModel = ko.mapping.fromJS(jsModel);
        addNewViewModel = ko.mapping.fromJS(workoutLogEntryViewModel);
        addNewViewModel.logEntryType(logEntryType);
        this.logEntryType = logEntryType;

        var now = moment();
        $.each(viewModel.wodList(), function (index, item) {
            item.lastPrDateHumanized = ko.computed(function () {
                return moment(item.lastPersonalRecordDate()).fromNow();
            });

            item.lastAttemptDateHumanized = ko.computed(function () {
                return moment(item.lastAttemptDate()).fromNow();
            });

            item.isInPast = function(comparator) {
                  return moment(comparator).isBefore(now);
            };
        });

        viewModel.selectedHeader = ko.observable("");
        viewModel.setHeader = function (data) {
            viewModel.selectedHeader(data.name);
            toggleAddNew();
        };
        viewModel.historyUri = function (data) {
            return "/Workout/History?id=" + data.workoutId();
        };

        addNewViewModel.cancelAddNew = function () {
            toggleAddNew();
        };
        addNewViewModel.clientTimeZone = ko.computed(function () {
            var clientTimeZone = CFBM.clientTimeZone();
            return clientTimeZone;
        });
        

        ko.editable(addNewViewModel);
        ko.applyBindings(viewModel, $container[0]);
        ko.applyBindings(addNewViewModel, $modalContainer);
        
        $(".cancel-button", $(".form-actions")).click(function () {
            addNewViewModel.rollback();
            addNewViewModel.score("");
            addNewViewModel.dateOfWod(moment().format("MM/DD/YYYY"));
            addNewViewModel.note("");
            addNewViewModel.isaPersonalRecord(false);
        });

        $(".addNew-button", $container).click(function () {
            addNewViewModel.beginEdit();
            addNewViewModel.selectedWorkoutName($(this).closest("div.thumbnail").find(".workout-name").text());
            addNewViewModel.selectedWorkoutId($(this).closest("div.thumbnail").attr("id"));
            addNewViewModel.userId(3); //note this needs to come out soon...
            addNewViewModel.logEntryType(logEntryType); //not sure why i need this exactly... but i will get it worked out
        });

        CFBM.Site.init();
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

        addNewViewModel.commit();
        addNewViewModel.score("");
        addNewViewModel.dateOfWod(moment().format("MM/DD/YYYY"));
        addNewViewModel.note("");
        addNewViewModel.isaPersonalRecord(false);

        
        //$("#addLogEntry-modal").modal("hide");
        toggleAddNew();
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

    if ($("#theHeroes-content").length) {
        //theHeroesModule.ready();
        module.ready(theHeroesViewModel, $("#theHeroes-content"), "H");
    }
});