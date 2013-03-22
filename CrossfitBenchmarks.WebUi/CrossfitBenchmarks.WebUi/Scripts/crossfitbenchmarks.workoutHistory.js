/// <reference path="crossfitbenchmarks.site.js" />

CFBM.namespace("CFBM.WorkoutHistory");
CFBM.WorkoutHistory = (function () {
    var $container;

    function WorkoutHistoryViewModel(){
        var self = this;
        ko.mapping.fromJS(workoutHistoryViewModel, {}, this);

        self.workoutName = ko.computed(function () {
            if (self.value().length > 0) {
                return self.value()[0].WorkoutName();
            }

            return "";

        }, this);

        self.workoutNameForHeader = ko.computed(function () {
            if (self.value().length > 0)
            {
                return " for '" + self.workoutName() + "'";
            }
            
            return "";

        }, this);

        self.deleteItem = function(data,event) {
            console.log("delete: " + data.WorkoutLogId());
            var workoutLogId = data.WorkoutLogId();
            CFBM.Site.post("/Logger/DeleteLogEntry",
                { id: workoutLogId },
                function (data, textStatus) {
                    console.log("success:" + data.result);
                    if (data.result)
                    {
                        var itemToRemove = ko.utils.arrayFirst(self.value(), function (item) {
                            return item.WorkoutLogId() === workoutLogId;
                        });

                        self.value.remove(itemToRemove);
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

    function ready(rootContainer){
        $container = rootContainer;

        ko.applyBindings(new WorkoutHistoryViewModel(), $container[0]);

        CFBM.Site.init();
    };

    return {
        ready:ready
    }
})();

$(document).ready(function () {
    var workoutHistoryModule = CFBM.WorkoutHistory;

    if ($(".workoutHistory-container").length) {
        workoutHistoryModule.ready($(".workoutHistory-container"));
    }
});
