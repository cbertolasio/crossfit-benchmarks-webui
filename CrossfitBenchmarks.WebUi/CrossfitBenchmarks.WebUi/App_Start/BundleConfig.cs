using System.Web.Optimization;

namespace CrossfitBenchmarks.WebUi
{

    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-timepicker.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/knockout-2.2.0.js",
                        "~/Scripts/knockout.mapping-latest.js",
                        "~/Scripts/ko.editables.js",
                        "~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/crossfitbenchmarks").Include(
                        "~/Scripts/crossfitbenchmarks.site.js",
                        "~/Scripts/crossfitbenchmarks.calculator.js",
                        "~/Scripts/crossfitbenchmarks.benchmarks.js"
                        ));

            
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-responsive.css",
                        "~/Content/bootstrap-timepicker.css",
                        "~/Content/datepicker.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
        }
    }
}