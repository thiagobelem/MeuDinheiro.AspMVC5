using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeuDinheiro.Bussiness.Models.Lancamentos;
using System.Linq;
using System.Data.Entity;
using MeuDinheiro.Infra.Data.Context;

namespace MeuDinheiro.Infra.Repository
{
    public class LancamentoRepository : Repository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(MeuDinheiroDBContext context) : base(context) { }

        public async Task<IEnumerable<Lancamento>> ObterLancamentosPorCategoria(Guid categoriaId)
        {
            return await Db.Lancamentos.AsNoTracking()
                          .Include(l => l.Categoria)
                          .Where(l => l.CategoriaId == categoriaId)
                          .OrderByDescending(l => l.Data).ToListAsync();
        }

        public async Task<IEnumerable<Lancamento>> ObterLancamentosCategorias()
        {
            return await Db.Lancamentos.AsNoTracking()
                           .Include(l => l.Categoria).ToListAsync();
        }

        public async Task<Lancamento> ObterLancamentoCategoria(Guid id)
        {
            return await Db.Lancamentos.AsNoTracking()
                            .Include(l => l.Categoria)
                            .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Lancamento>> ObterLancamentosCategoriaAtivos()
        {
            return await Db.Lancamentos.AsNoTracking()
                            .Include(l => l.Categoria)
                            .Where(l => l.Categoria.Ativo == true)
                            .OrderByDescending(l => l.Data).ToListAsync();
        }

        public async Task<Dictionary<int, decimal>> ObterReceitaPorMes(DateTime referencia)
        {
            return await Db.Lancamentos.AsNoTracking()
                                       .Where(l => l.Tipo == TipoLancamento.Receita && l.Data >= referencia)
                                       .GroupBy(l => l.Data.Month)
                                       .Select(l => new
                                       {
                                           Mes = l.Key,
                                           Valor = l.Sum(s => s.Valor)
                                       }).OrderBy(s => s.Mes).ToDictionaryAsync(s => s.Mes, s => s.Valor);
        }

        public async Task<Dictionary<int, decimal>> ObterDespesaPorMes(DateTime referencia)
        {
            return await Db.Lancamentos.AsNoTracking()
                                       .Where(l => l.Tipo == TipoLancamento.Despesa && l.Data >= referencia)
                                       .GroupBy(l => l.Data.Month)
                                       .Select(l => new
                                       {
                                           Mes = l.Key,
                                           Valor = l.Sum(s => s.Valor)
                                       }).OrderBy(s => s.Mes).ToDictionaryAsync(s => s.Mes, s => s.Valor);
        }

        public async Task<Dictionary<string, decimal>> ObterReceitaPorCategoria(DateTime referencia)
        {
            return await Db.Lancamentos.AsNoTracking()
                                       .Where(l => l.Tipo == TipoLancamento.Receita && l.Data >= referencia)
                                       .GroupBy(l => l.Categoria.Descricao)
                                       .Select(l => new
                                       {
                                           Categoria = l.Key,
                                           Valor = l.Sum(s => s.Valor)
                                       }).OrderBy(s => s.Categoria).ToDictionaryAsync(s => s.Categoria, s => s.Valor);
        }

        public async Task<Dictionary<string, decimal>> ObterDespesaPorCategoria(DateTime referencia)
        {
            return await Db.Lancamentos.AsNoTracking()
                                       .Where(l => l.Tipo == TipoLancamento.Despesa && l.Data >= referencia)
                                       .GroupBy(l => l.Categoria.Descricao)
                                       .Select(l => new
                                       {
                                           Categoria = l.Key,
                                           Valor = l.Sum(s => s.Valor)
                                       }).OrderBy(s => s.Categoria).ToDictionaryAsync(s => s.Categoria, s => s.Valor);
        }
    }
}
