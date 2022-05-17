using System;
using System.Reflection;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using MeuDinheiro.Infra.Repository;
using MeuDinheiro.Bussiness.Models;
using MeuDinheiro.Bussiness.Models.Categorias;
using MeuDinheiro.Bussiness.Models.Lancamentos;
using MeuDinheiro.Bussiness.Models.Categorias.Services;
using MeuDinheiro.Bussiness.Models.Lancamentos.Services;
using MeuDinheiro.Bussiness.Core.Notifications;
using MeuDinheiro.Infra.Data.Context;

namespace MeuDinheiro.AppMvc.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void RegisterDIContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.RegisterSingleton(() => AutoMapperConfig.GetMapperConfiguration().CreateMapper(container.GetInstance));

            container.Register<MeuDinheiroDBContext>(Lifestyle.Scoped);

            container.Register<ICategoriaRepository, CategoriaRepository>(Lifestyle.Scoped);
            container.Register<ICategoriaService, CategoriaService>(Lifestyle.Scoped);

            container.Register<ILancamentoRepository, LancamentoRepository>(Lifestyle.Scoped);
            container.Register<ILancamentoService, LancamentoService>(Lifestyle.Scoped);

            container.Register<INotificador, Notificador>(Lifestyle.Scoped);
        }
    }
}