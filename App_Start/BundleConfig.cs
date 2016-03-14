using System.Web;
using System.Web.Optimization;

namespace tech4mUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Bundles
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
             "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajaxform").Include(
            "~/Scripts/jquery.form.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/FacebookPhotoScripts").Include(
            "~/Scripts/FacebookPhoto/InitializeAjaxForms.js",
            "~/Scripts/FacebookPhoto/ViewBehaviors.js",
            "~/Scripts/FacebookPhoto/DocumentReady.js"));

            bundles.Add(new ScriptBundle("~/bundles/CommonFunctions").Include(
            "~/Scripts/CommonFunctions.js"));

            bundles.Add(new ScriptBundle("~/bundles/FacebookSubscriptionScripts").Include(
                "~/Scripts/FacebookSubscription/ViewBehaviors.js",
                "~/Scripts/FacebookSubscription/DocumentReady.js"));

            bundles.Add(new ScriptBundle("~/bundles/SignalRScripts").Include(
                "~/Scripts/jquery.signalR-2.1.2.min.js"));
            #endregion

            bundles.Add(new ScriptBundle("~/bundles/FacebookPaymentScripts").Include(
                "~/Scripts/FacebookPayment/DocumentReady.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // application script bundle
            var layoutJsBundle = new ScriptBundle("~/js").Include("~/Scripts/app.js");
            bundles.Add(layoutJsBundle);

            // css bundle
            var layoutCssBundle = new StyleBundle("~/Content/themes/simple/css").Include("~/Content/themes/simple/style.css");
            bundles.Add(layoutCssBundle);

            // login page bundles
            var loginCssBundle = new StyleBundle("~/Content/themes/simple/admin").Include("~/Content/themes/simple/admin.css");
            bundles.Add(loginCssBundle);


            var manageCssBundle = new StyleBundle("~/Scripts/jqgrid/css/bundle").Include("~/Scripts/jqgrid/css/ui.jqgrid.css");
            bundles.Add(manageCssBundle);

            var jqueryUICssBundle =
new StyleBundle("~/Content/themes/simple/jqueryuicustom/css/sunny/bundle").Include("~/Content/themes/simple/jqueryuicustom/css/sunny/jquery-ui-1.9.2.custom.css");
            bundles.Add(jqueryUICssBundle);

            var tinyMceBundle = new ScriptBundle("~/Scripts/tiny_mce/js").Include("~/Scripts/tiny_mce/tiny_mce.js");
            bundles.Add(tinyMceBundle);

            var manageJsBundle = new ScriptBundle("~/manage/js").Include("~/Scripts/jqgrid/js/jquery.jqGrid.js").Include("~/Scripts/jqgrid/js/i18n/grid.locale-en.js").Include("~/Scripts/admin.js");
            bundles.Add(manageJsBundle);

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
