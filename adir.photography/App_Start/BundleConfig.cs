using System.Web;
using System.Web.Optimization;

namespace adir.photography
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jQuery/jquery-2.1.4.js"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/mfb/css/angular-material.min.css",
                      "~/Scripts/mfb/css/mfb-menu",
                      "~/Scripts/mfb/css/mfb.css",
                      "~/Content/ionicons.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/Angular/angular.js",
                        "~/Scripts/Angular/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/maximage").Include(
                        "~/Scripts/jQuery.cycle/jquery.cycle.all.js",
                        "~/Scripts/jQuery.maximage/js/jquery.maximage.js"));

            bundles.Add(new ScriptBundle("~/bundles/mfb-menu").Include(
                        "~/Scripts/mfb/js/mfb-directive.js",
                        "~/Scripts/mfb/js/mfb-menu.js"));

            bundles.Add(new ScriptBundle("~/bundles/ap-app").Include(
                        "~/App/AppMain.js",
                        "~/App/AppMainViewModel.js",
                        "~/App/Common/Directives/AppCommonDirectives.js",
                        "~/App/Common/Services/AppCommonServices.js",
                        "~/App/Common/Directives/ButtonMenuDirectiveViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/gallery").Include(
                        "~/App/Common/Services/WebApiService.js",
                        "~/App/Gallery/GalleryApp.js",
                        "~/App/Gallery/ViewModels/GalleryViewModel.js",
                        "~/App/Gallery/ViewModels/AlbumsViewModel.js",
                        "~/App/Gallery/ViewModels/MaxImageDirectiveViewModel.js",
                        "~/App/Gallery/Services/GalleryServices.js"));
        }
    }
}