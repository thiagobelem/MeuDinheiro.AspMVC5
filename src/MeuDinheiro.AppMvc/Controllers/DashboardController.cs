using MeuDinheiro.AppMvc.ViewModels;
using MeuDinheiro.Bussiness.Models.Lancamentos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MeuDinheiro.AppMvc.Controllers
{   
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        public DashboardController(ILancamentoRepository lancamentoRepository)
        {
            _lancamentoRepository = lancamentoRepository;
        }

        [Route("~/")]
        public ActionResult Inicio()
        {
            return View(); 
        }

        public async Task<ActionResult> GraficoReceitaPorMes()
        {
            DashboardViewModel model = new DashboardViewModel();

            var receitaMes = FormataDicionario(await _lancamentoRepository.ObterReceitaPorMes(DateTime.Now.AddMonths(-9)));

            model.Label = string.Join(",", receitaMes.Keys);
            model.Data = string.Join(",", receitaMes.Values);

            return PartialView("_Grafico_ReceitaMes", model);
        }

        public async Task<ActionResult> GraficoDespesaPorMes()
        {
            DashboardViewModel model = new DashboardViewModel();

            var despesaMes = FormataDicionario(await _lancamentoRepository.ObterDespesaPorMes(DateTime.Now.AddMonths(-9)));

            model.Label = string.Join(",", despesaMes.Keys);
            model.Data = string.Join(",", despesaMes.Values);

            return PartialView("_Grafico_DespesaMes", model);
        }

        public async Task<ActionResult> GraficoReceitaPorCategoria()
        {
            DashboardViewModel model = new DashboardViewModel();

            var receitasCategoria = FormataDicionario(await _lancamentoRepository.ObterReceitaPorCategoria(DateTime.Now.AddMonths(-9)));

            model.Label = string.Join(",", receitasCategoria.Keys);
            model.Data = string.Join(",", receitasCategoria.Values);

            return PartialView("_Grafico_ReceitaCategoria", model);
        }


        public async Task<ActionResult> GraficoDespesaPorCategoria()
        {
            DashboardViewModel model = new DashboardViewModel();
            var despesasCategoria = FormataDicionario(await _lancamentoRepository.ObterDespesaPorCategoria(DateTime.Now.AddMonths(-9)));

            model.Label = string.Join(",", despesasCategoria.Keys);
            model.Data = string.Join(",", despesasCategoria.Values);

            return PartialView("_Grafico_DespesaCategoria", model);
        }




        private Dictionary<string, string> FormataDicionario(Dictionary<int, decimal> valores)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            foreach (var item in valores)
            {
                string mes = string.Empty;
                switch (item.Key)
                {
                    case 1: mes = "JAN"; break;
                    case 2: mes = "FEV"; break;
                    case 3: mes = "MAR"; break;
                    case 4: mes = "ABR"; break;
                    case 5: mes = "MAI"; break;
                    case 6: mes = "JUN"; break;
                    case 7: mes = "JUL"; break;
                    case 8: mes = "AGO"; break;
                    case 9: mes = "SET"; break;
                    case 10: mes = "OUT"; break;
                    case 11: mes = "NOV"; break;
                    case 12: mes = "DEZ"; break;
                }
                ret.Add(String.Format("'{0}'", mes), item.Value.ToString().Replace(",","."));
            }

            return ret;
        }


        private Dictionary<string, string> FormataDicionario(Dictionary<string, decimal> valores)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            foreach (var item in valores)
            {
                ret.Add(String.Format("'{0}'", item.Key), item.Value.ToString().Replace(",", "."));
            }

            return ret;
        }
    }
}