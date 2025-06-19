using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace GS_Digital_Recruitment {

    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            var scriptBundle = new ScriptBundle("~/Scripts/bundle").Include(
                "~/Content/assets/js/jquery.min.js",
                "~/Content/assets/js/bootstrap.bundle.min.js",
                "~/Content/assets/js/popper.min.js"
            );

            var styleBundle = new StyleBundle("~/Content/bundle").Include(
                "~/Content/assets/css/bootstrap.min.css"
            );

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

            BundleTable.EnableOptimizations = true;
        }
    }
}