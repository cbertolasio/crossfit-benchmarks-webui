/// <reference path="crossfitbenchmarks.site.js" />

CFBM.namespace("CFBM.WorkoutHistory");
CFBM.WorkoutHistory = (function () {
    var $container;

    function WorkoutHistoryViewModel(){
        var self = this;
        ko.mapping.fromJS(workoutHistoryViewModel, {}, this);

        self.workoutName = ko.computed(function(){
            return self.value()[0].WorkoutName();
        }, this);
    }

    function ready(rootContainer){
        $container = rootContainer;

        ko.applyBindings(new WorkoutHistoryViewModel(), $container[0]);

        CFBM.Site.init();
    };

    return {
        ready:ready
    }
})();

CFBM.namespace("CFBM.Dashboard");
CFBM.Dashboard = (function () {
    var viewModel = null,
    $container = null,
    addNewViewModel = null,
    toolbarViewModel = null;

    function ToolbarViewModel() {
        var self = this;
        self.addNewBasicWod = function () {
            toggleView();
        };
    };

    function AddNewViewModel() {
        var self = this;
        ko.mapping.fromJS(workoutLogEntryViewModel, {}, this);

        self.cancelAddNew = function () {
            toggleView();
        };


        self.save = function () {
            saveBasicLogEntry();
        }

        self.clientTimeZone = ko.computed(function () {
            var clientTimeZone = CFBM.clientTimeZone();
            return clientTimeZone;
        });
    }

    function SummaryViewModel() {
        var self = this;
        ko.mapping.fromJS(summaryData, {}, this);

        self.getHrefToWorkoutHistory = function(data) {
            return "/Workout/History?id=" + data.WorkoutId();
        };
    }

    function onSaveSuccessful(data) {
        console.log("save successful...", data);
        $.get("/Dashboard/History", null, function (data) {
            console.log(data);
            var history = data;
            ko.mapping.fromJS($.parseJSON(history), viewModel);
        }, "json");

        
        toggleView();
    };

    function saveBasicLogEntry() {
        var data = $("#addNewLogEntry-form").serialize();
        
        CFBM.Site.post("/Logger/AddLogEntry", 
            data, 
            onSaveSuccessful, 
            function(jqXHR, statusText){
                console.log("Status: " + jqXHR.status + " " + jqXHR.statusText);
            }, 
            function(jqXHR, statusText){
                console.log("Status: " + jqXHR.status + " " + jqXHR.statusText);
            });
    };

    function toggleView() {
        $(".addNew-container").toggle();
        $(".summary-container").toggle();
    };

    function onReady(rootContainer) {
        var $modalContainer = $("#addLogEntry-modal");

        $container = rootContainer;
        viewModel = new SummaryViewModel();
        addNewViewModel = new AddNewViewModel();
        toolbarViewModel = new ToolbarViewModel();
        
        ko.applyBindings(viewModel, $container[0]);
        ko.applyBindings(toolbarViewModel, $("#dashboard-toolbar")[0]);
        ko.applyBindings(addNewViewModel, $modalContainer[0]);

        CFBM.Site.init();
    };

    return {
        ready:onReady
    };
}());

$(document).ready(function () {
    var dashboardModule = CFBM.Dashboard,
        workoutHistoryModule = CFBM.WorkoutHistory;

    if ($("#summary-content").length) {
        dashboardModule.ready($("#summary-content"));
    }

    if ($(".workoutHistory-container").length) {
        workoutHistoryModule.ready($(".workoutHistory-container"));
    }
});
