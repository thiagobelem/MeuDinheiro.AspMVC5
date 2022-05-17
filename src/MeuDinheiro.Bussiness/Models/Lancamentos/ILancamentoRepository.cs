using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuDinheiro.Bussiness.Core.Data;

namespace MeuDinheiro.Bussiness.Models.Lancamentos
{
    public interface ILancamentoRepository : IRepository<Lancamento>
    {
        Task<IEnumerable<Lancamento>> ObterLancamentosPorCategoria(Guid categoriaId);

        Task<IEnumerable<Lancamento>> ObterLancamentosCategorias();

        Task<Lancamento> ObterLancamentoCategoria(Guid id);

        Task<IEnumerable<Lancamento>> ObterLancamentosCategoriaAtivos();

        Task<Dictionary<int, decimal>> ObterReceitaPorMes(DateTime referencia);

        Task<Dictionary<int, decimal>> ObterDespesaPorMes(DateTime referencia);

        Task<Dictionary<string, decimal>> ObterReceitaPorCategoria(DateTime referencia);

        Task<Dictionary<string, decimal>> ObterDespesaPorCategoria(DateTime referencia);
    }
}
