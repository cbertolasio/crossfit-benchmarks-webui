

CFBM.namespace("CFBM.Calculator");
CFBM.Calculator = (function () {
    var viewModel = null,
        $container = $("#calculator-section");

    function onReady() {
        viewModel = ko.mapping.fromJS(calculatorViewModel);

        viewModel.calculate = function click() {
            var self = this,
                base = .05,
                weight = self.weight(),
                value = 0,
                roundingValue = $("input[name=roundingRadio]:checked").val();

            self.percentages.removeAll();
            
            while (base < 1.05)
            {
                if (roundingValue === "toNearest5") {
                    value = Math.round(parseInt((weight * base) / 5) * 5);
                }
                else if(roundingValue === "dontRound") {
                    value = Math.round(weight * base);
                }
                
                
                self.percentages.push({ label: Math.round(base * 100) + "%", value: value + " #" });

                base = base + .05;
            }
        };

        viewModel.valueIsRounded = function () {
            return ($("input[name=roundingRadio]:checked").val() === "toNearest5");
        };

        viewModel.clear = function clear() {
            var self = this;
            self.percentages.removeAll();
        };

        ko.applyBindings(viewModel, $container[0]);
    };

    return {
        ready: onReady
    };
}());

$(document).ready(function () {
    var module = CFBM.Calculator;
    if ($("#calculator-section").length) {
        module.ready();
    }
});