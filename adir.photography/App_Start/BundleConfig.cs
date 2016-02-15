using System.Web;
using System.Web.Optimization;

namespace adir.photography
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/mfb/css/angular-material.min.css",
                      "~/Scripts/mfb/css/mfb-menu",
                      "~/Scripts/mfb/css/mfb.css",
                      "~/Content/ionicons.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/maximage").Include(
                    "~/Scripts/jQuery.maximage/css/jquery.maximage.css",
                    "~/Scripts/jQuery.maximage/css/screen.css",
                    "~/Content/maximage-gallery.css"));

            
            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/Angular/angular.js",
                      "~/Scripts/Angular/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/mfb-menu").Include(
                        "~/Scripts/mfb/js/mfb-directive.js",
                        "~/Scripts/mfb/js/mfb-menu.js",
                        "~/App/Common/ViewModels/MaxImageDirectiveViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundels/maximage").Include(
                        "~/Scripts/jQuery.cycle/jquery.cycle.all.js",
                        "~/Scripts/jQuery.maximage/js/jquery.maximage.js"));

        }
    }
}