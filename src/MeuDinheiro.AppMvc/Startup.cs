using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MeuDinheiro.AppMvc.App_Start;

[assembly: OwinStartupAttribute(typeof(MeuDinheiro.AppMvc.Startup))]
namespace MeuDinheiro.AppMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            DependencyInjectionConfig.RegisterDIContainer();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CultureConfig.RegisterCulture();
        }
    }
}
