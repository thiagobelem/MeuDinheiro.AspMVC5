using System.Reflection;
using System.Linq;
using AutoMapper;
using MeuDinheiro.Bussiness.Models.Categorias;
using MeuDinheiro.Bussiness.Models.Lancamentos;
using MeuDinheiro.AppMvc.ViewModels;
using System;

namespace MeuDinheiro.AppMvc.App_Start
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var profiles = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .Where(x => typeof(Profile).IsAssignableFrom(x));

            return new MapperConfiguration(cfg => 
            {
                foreach(var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            });
        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<Lancamento, LancamentoViewModel>().ReverseMap();
        }
    }
}