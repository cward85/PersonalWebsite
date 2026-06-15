using System.Web;
using System.Web.Optimization;

namespace PersonalWebsite
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                     //             "~/Scripts/dist/js/bootstrap.bundle.min.js",
                     "~/Scripts/dist/js/coreui.bundle.min.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Scripts/dist/css/themes/bootstrap/bootstrap.min.css",

                "~/Content/bootstrap-icons-1.13.1/bootstrap-icons.css",
                "~/Scripts/dist/css/coreui.min.css",
                "~/Content/Site.css"
                      //    "~/Content/bootstrap.css",

                      //"~/Content/jquery.ui.css",
                      //"~/Content/jquery.ui.theme.css",
                      //"~/Content/jquery.ui.structure.css"
                      ));
        }
    }
}
