using System.Web.Optimization;

namespace MeuDinheiro.AppMvc
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css/base").Include(
                      "~/Content/css/argon-dashboard.min.css",
                      "~/Content/css/nucleo-icons.css",
                      "~/Content/css/nucleo-svg.css",
                      "~/Content/css/sweetalert.min.css"));

            bundles.Add(new Bundle("~/Content/scripts/base").Include(
              "~/Content/scripts/jquery.min.js",
              "~/Content/scripts/popper.min.js",
               "~/Content/scripts/bootstrap.min.js",
               "~/Content/scripts/perfect-scrollbar.min.js",
               "~/Content/scripts/sweetalert.min.js",
               "~/Content/scripts/argon-dashboard.min.js"));


            bundles.Add(new ScriptBundle("~/Content/scripts/js_val").Include(
                        "~/Content/scripts/jquery.validate.min.js",
                        "~/Content/scripts/jquery.validate.unobtrusive.min.js",
                        "~/Content/scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/Content/scripts/datepicker").Include(
                "~/Content/scripts/flatpickr.min.js"));

            bundles.Add(new ScriptBundle("~/Content/scripts/mask_js").Include(
               "~/Content/scripts/jquery.mask.min.js"));

            bundles.Add(new ScriptBundle("~/Content/scripts/chart").Include(
              "~/Content/scripts/chartjs.min.js",
              "~/Content/scripts/graficos-dashboard.js"));


            BundleTable.EnableOptimizations = false;
        }
    }
}
