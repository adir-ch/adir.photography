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
                        "~/Scripts/Angular/angular-route.js",
                        "~/Scripts/Angular/angular-animate.js"));

            bundles.Add(new ScriptBundle("~/bundles/maximage").Include(
                        "~/Scripts/jQuery.cycle/jquery.cycle.all.js",
                        "~/Scripts/jQuery.maximage/js/jquery.maximage.js",
                        "~/Scripts/ngprogress.js",
                        "~/Scripts/image-loader.js"));

            bundles.Add(new StyleBundle("~/Content/gallery").Include(
                        "~/Scripts/jQuery.maximage/css/jquery.maximage.css",
                        "~/Scripts/jQuery.maximage/css/screen.css",
                        "~/Content/maximage-gallery.css",
                        "~/Content/floating-menu.css",
                        "~/Content/albums.css"));

            bundles.Add(new ScriptBundle("~/bundles/photoswipe").Include(
                        "~/Scripts/Photoswipe/photoswipe.js",
                        "~/Scripts/Photoswipe/photoswipe-ui-default.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-libs").Include(
                        "~/Scripts/mfb/js/mfb-directive.js",
                        "~/Scripts/ngDialog.js"));

            bundles.Add(new ScriptBundle("~/bundles/ap-app").Include(
                        "~/App/AppMain.js",
                        "~/App/AppMainViewModel.js",
                        "~/App/Common/Services/AppCommonServices.js",
                        "~/App/Common/Services/WebApiService.js",
                        "~/App/Common/Services/ExceptionHandlerExt.js",
                        "~/App/Common/Services/GlobalConfigurationService.js",
                        "~/App/Common/Directives/AppCommonDirectives.js",
                        "~/App/Common/Directives/LoginDialog/LoginDialogDirectiveViewModel.js",
                        "~/App/Common/Directives/MfbButtonMenu/MfbButtonMenuViewModel.js",
                        "~/App/Common/Directives/MfbButtonMenu/ButtonMenuDirectiveViewModel.js"));

            bundles.Add(new ScriptBundle("~/bundles/gallery").Include(
                        "~/App/Gallery/GalleryApp.js",
                        "~/App/Gallery/ViewModels/GalleryViewModel.js",
                        "~/App/Gallery/ViewModels/AlbumsViewModel.js",
                        "~/App/Gallery/ViewModels/AlbumsWelcomeViewModel.js",
                        "~/App/Gallery/ViewModels/AlbumViewModel.js",
                        "~/App/Gallery/Directives/FloatingSideMenu/FloatingSideMenuDirectiveViewModel.js",
                        "~/App/Gallery/Directives/AlbumsDisplay/AlbumsDisplayDirectiveViewModel.js",
                        "~/App/Gallery/Directives/MaxImage/MaxImageDirectiveViewModel.js",
                        "~/App/Gallery/Directives/MaxImage/MaxImageInnerDirectiveViewModel.js",
                        "~/App/Gallery/Directives/PhotoSwipe/PhotoswipeDirectiveViewModel.js",
                        "~/App/Gallery/Services/GalleryServices.js"));

            bundles.Add(new ScriptBundle("~/bundles/member").Include(
                        "~/App/Member/MemberApp.js",
                        "~/App/Member/ViewModels/ProfileViewModel.js",
                        "~/App/Member/Services/MemberServices.js"));
        }
    }
}