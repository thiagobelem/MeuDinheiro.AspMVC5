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
            var receitaMes = FormataDicionario(await _lancamentoRepository.ObterReceitaPorMes(DateTime.Now.AddMonths(-9)));

            return PartialView("_Grafico_ReceitaMes", FormataSaida(receitaMes));
        }

        public async Task<ActionResult> GraficoDespesaPorMes()
        {
            var despesaMes = FormataDicionario(await _lancamentoRepository.ObterDespesaPorMes(DateTime.Now.AddMonths(-9)));

            return PartialView("_Grafico_DespesaMes", FormataSaida(despesaMes));
        }

        public async Task<ActionResult> GraficoReceitaPorCategoria()
        {
            var receitasCategoria = FormataDicionario(await _lancamentoRepository.ObterReceitaPorCategoria(DateTime.Now.AddMonths(-9)));

            return PartialView("_Grafico_ReceitaCategoria", FormataSaida(receitasCategoria));
        }


        public async Task<ActionResult> GraficoDespesaPorCategoria()
        {
            var despesasCategoria = FormataDicionario(await _lancamentoRepository.ObterDespesaPorCategoria(DateTime.Now.AddMonths(-9)));

            return PartialView("_Grafico_DespesaCategoria", FormataSaida(despesasCategoria));
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

        private DashboardViewModel FormataSaida(Dictionary<string,string> keyValues)
        {
            DashboardViewModel model = new DashboardViewModel();

            model.Label = string.Join(",", keyValues.Keys);
            model.Data = string.Join(",", keyValues.Values);

            return model;
        }

    }
}