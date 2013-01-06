

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
                value = 0;

            self.percentages.removeAll();

            while (base < 1.05)
            {
                value = weight * base

                self.percentages.push({ label: parseInt(base * 100) + "%", value: parseInt(value) + " #" });

                base = base + .05;
            }
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