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
