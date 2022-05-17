using System;
using System.Threading.Tasks;

namespace MeuDinheiro.Bussiness.Models.Lancamentos.Services
{
    public interface ILancamentoService : IDisposable
    {
        Task Adicionar(Lancamento lancamento);

        Task Atualizar(Lancamento lancamento);

        Task Remover(Guid id);
    }
}
