CFBM.namespace("CFBM.Benchmarks");
CFBM.Benchmarks = (function () {
    var viewModel = null,
        addNewViewModel = null,
        $container = null,
        $modalContainer=$("#addLogEntry-modal")[0],
        logEntryType = null;

    function onReady(jsModel, rootContainer, logEntryType) {
        $container = rootContainer;
        viewModel = ko.mapping.fromJS(jsModel);
        addNewViewModel = ko.mapping.fromJS(workoutLogEntryViewModel);
        addNewViewModel.logEntryType(logEntryType);
        this.logEntryType = logEntryType;
        
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
        ko.editable(addNewViewModel);
        ko.applyBindings(viewModel, $container[0]);
        ko.applyBindings(addNewViewModel, $modalContainer);

        $("#dp3", $modalContainer).datepicker();


        $(".save-button", $(".modal-footer")).click(function () {

            $(".addNewLogEntry-form", $(".modal-body")).submit();
            
        });

        $(".cancel-button", $(".modal-footer")).click(function () {
            addNewViewModel.rollback();
            addNewViewModel.score("");
            addNewViewModel.dateCreated(moment().format("MM/DD/YYYY"));
            addNewViewModel.note("");
            addNewViewModel.isaPersonalRecord(false);
        });

        $(".addNew-button", $container).click(function () {
            addNewViewModel.beginEdit();
            addNewViewModel.selectedWorkoutName($(this).closest("div.thumbnail").find("h4").html());
            addNewViewModel.selectedWorkoutId($(this).closest("div.thumbnail").attr("id"));
            addNewViewModel.userId(3); //note this needs to come out soon...
            addNewViewModel.logEntryType(logEntryType); //not sure why i need this exactly... but i will get it worked out
        });

        $(".thumbnail", $("ul#wodItems")).on("hover", function (event) {
            $(this).toggleClass("hover");
        });

        $(".timepicker", $modalContainer).timepicker(
        //    {
        //    minuteStep: 5,
        //    secondStep: 5,
        //    showInputs: true,
        //    template: 'modal',
        //    modalBackdrop: true,
        //    showSeconds: false,
        //    showMeridian: true
        //}
        );
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
        addNewViewModel.dateCreated(moment().format("MM/DD/YYYY"));
        addNewViewModel.note("");
        addNewViewModel.isaPersonalRecord(false);

        
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