using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryeasing").Include(
                      "~/Scripts/jquery.easing.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                      "~/Scripts/admin.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/fonts/fontawesome/css/all.min.css",
                      "~/Content/ui.css",
                      "~/Content/responsive.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Admin/Content/css").Include(
                      "~/Content/fonts/fontawesome/css/all.min.css",
                      "~/Content/admin.min.css",
                      "~/Content/AdminSite.css"));
        }
    }
}