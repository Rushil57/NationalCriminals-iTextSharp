using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace NationalCriminals.App_Start
{
    public class BundleConfig
    {// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/corejquery").Include(
                            "~/Content/global/plugins/jquery.min.js",
                            "~/Content/global/plugins/jquery-migrate.min.js",
                            "~/Content/global/plugins/bootstrap/js/bootstrap.min.js",
                            "~/Content/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                            "~/Content/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                            "~/Content/global/plugins/jquery.blockui.min.js",
                            "~/Content/global/plugins/jquery.cokie.min.js",
                            "~/Content/global/plugins/uniform/jquery.uniform.min.js",
                            "~/Content/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                            "~/Content/global/plugins/jquery-validation/js/jquery.validate.js",
                            "~/Content/global/plugins/datatables/media/js/jquery.dataTables.min.js",
                            "~/Content/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js",
                            "~/Content/global/plugins/datatables/plugins/bootstrap/jquery.dataTables.columnFilter.js",
                            "~/Content/global/plugins/bootstrap-toastr/toastr.min.js",
                            "~/Content/global/scripts/metronic.js",
                            "~/Content/admin/layout/scripts/layout.js",
                            "~/Content/admin/layout/scripts/quick-sidebar.js",
                            "~/Content/admin/layout/scripts/demo.js",
                            "~/Content/global/plugins/amcharts/amcharts/amcharts.js",
                            "~/Content/global/plugins/amcharts/amcharts/serial.js",
                            "~/Content/global/scripts/common.js",
                            "~/Scripts/bootstrap-multiselect.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                            "~/Content/global/plugins/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()).Include(
                            "~/Content/global/plugins/simple-line-icons/simple-line-icons.min.css", new CssRewriteUrlTransform()).Include(
                            "~/Content/global/plugins/font-awesome/css/fonts.googleapis.css",
                            "~/Content/global/plugins/bootstrap/css/bootstrap.min.css",
                            "~/Content/global/plugins/bootstrap/css/bootstrap.min.css").Include(
                            "~/Content/global/plugins/uniform/css/uniform.default.min.css", new CssRewriteUrlTransform()).Include(
                            "~/Content/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css").Include(
                            "~/Content/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css", new CssRewriteUrlTransform()).Include(
                            "~/Content/global/plugins/bootstrap-toastr/toastr.min.css").Include(
                            "~/Content/global/css/components.css", new CssRewriteUrlTransform()).Include(
                            "~/Content/global/css/plugins.css", new CssRewriteUrlTransform()).Include(
                            "~/Content/admin/layout/css/layout.css").Include(
                            "~/Content/admin/layout/css/themes/darkblue.css", new CssRewriteUrlTransform()).Include(
                            "~/Content/admin/layout/css/custom.css").Include(
                            "~/Content/admin/pages/css/error.css",
                            "~/Content/global/css/common.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}