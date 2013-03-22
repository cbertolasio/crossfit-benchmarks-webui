/// <reference path="crossfitbenchmarks.site.js" />

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

        self.deleteItem = function (data, event) {
            console.log("delete: " + data.WorkoutLogId());
            var workoutLogId = data.WorkoutLogId();
            CFBM.Site.post("/Logger/DeleteLogEntry",
                { id: workoutLogId },
                function (data, textStatus) {
                    console.log("success:" + data.result);
                    if (data.result) {
                        var itemToRemove = ko.utils.arrayFirst(self.value(), function (item) {
                            return item.WorkoutLogId() === workoutLogId;
                        });

                        self.value.remove(itemToRemove);

                        refreshHistory();
                    }
                },
                function (jqXHR, statusText) {
                    console.log("Status: " + jqXHR.status + " " + jqXHR.statusText);
                },
                function (jqXHR, statusText) {
                    console.log("Status: " + jqXHR.status + " " + jqXHR.statusText);
                });
        };
    }

    function refreshHistory() {
        $.get("/Dashboard/History", null, function (data) {
            console.log(data);
            var history = data;
            ko.mapping.fromJS($.parseJSON(history), viewModel);
        }, "json");
    };

    function onSaveSuccessful(data) {
        console.log("save successful...", data);
        refreshHistory();
        
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
    var dashboardModule = CFBM.Dashboard;

    if ($("#summary-content").length) {
        dashboardModule.ready($("#summary-content"));
    }
});
