using System.Web;
using System.Web.Optimization;

namespace Mshop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                          "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/themecss").Include(
                   "~/vendor/css/bootstrap.min.css",
                          "~/vendor/css/font-awesome.min.css",
                          "~/vendor/css/ionicons.min.css",
                          "~/vendor/css/morris/morris.css",
                          "~/vendor/css/jvectormap/jquery-jvectormap-1.2.2.css",
                          "~/vendor/css/datepicker/datepicker3.css",
                          "~/vendor/css/daterangepicker/daterangepicker-bs3.css",
                          "~/vendor/css/iCheck/all.css",
                          "~/vendor/css/Test.css",
                          "~/vendor/css/style.css",
                           "~/vendor/DataTables/media/css/dataTables.bootstrap.css",
                          "~/vendor/DataTables/extensions/Buttons/css/buttons.bootstrap.min.css",
                           //"~/vendor/DataTables/extensions/Select/css/select.bootstrap.css",
                           "~/Content/Loading.css",
                             "~/vendor/select2/select2.min.css",
                                "~/vendor/SweetAlert/css/sweetalert.css",
                                 "~/vendor/datetimepicker/css/bootstrap-datetimepicker.min.css"
                           ));


            bundles.Add(new ScriptBundle("~/bundles/themeJS").Include(
                  "~/vendor/js/jquery.min.js",
                    "~/vendor/js/bootstrap.min.js",
                     //"~/Scripts/jquery.unobtrusive-ajax.js",
                     "~/vendor/js/plugins/chart.js",
                 "~/vendor/js/plugins/iCheck/icheck.min.js",
                 "~/vendor/js/plugins/fullcalendar/fullcalendar.js",
                 "~/vendor/js/Director/app.js",
                 "~/vendor/DataTables/media/js/jquery.dataTables.js",
                     "~/vendor/DataTables/media/js/dataTables.bootstrap.js",
                          "~/vendor/DataTables/extensions/Buttons/js/dataTables.buttons.js",
                     "~/vendor/DataTables/extensions/Buttons/js/buttons.bootstrap.js",
                      "~/Scripts/jszip.js",
                      "~/vendor/DataTables/extensions/Buttons/js/buttons.html5.js",
                    "~/vendor/DataTables/extensions/Buttons/js/buttons.print.min.js",
                  "~/vendor/DataTables/extensions/Buttons/js/buttons.colVis.min.js",
                  "~/vendor/DataTables/extensions/Buttons/js/jszip.min.js",
                  //"~/vendor/DataTables/extensions/Select/js/dataTables.select.js",
                  "~/vendor/select2/select2.min.js",
                  "~/vendor/select2/select2-tab-fix.js",
                  "~/vendor/js/moment.js",
                    "~/vendor/datetimepicker/js/bootstrap-datetimepicker.js",
                     "~/vendor/SweetAlert/js/sweetalert.min.js"
                  )
                 );
        }
    }
}
