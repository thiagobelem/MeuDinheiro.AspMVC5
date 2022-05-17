using System;
using System.Threading.Tasks;
using MeuDinheiro.Bussiness.Core.Services;
using MeuDinheiro.Bussiness.Models.Lancamentos.Validations;
using MeuDinheiro.Bussiness.Core.Notifications;

namespace MeuDinheiro.Bussiness.Models.Lancamentos.Services
{
    public class LancamentoService : BaseService, ILancamentoService
    {
        private readonly ILancamentoRepository _lancamentoRepository;

        public LancamentoService(ILancamentoRepository lancamentoRepository, INotificador notificador)
               :base(notificador)
        {
            _lancamentoRepository = lancamentoRepository;
        }

        public async Task Adicionar(Lancamento lancamento)
        {
            if (!ExecutarValidacao(new LancamentoValidation(), lancamento)) return;

            await _lancamentoRepository.Adicionar(lancamento);
        }

        public async Task Atualizar(Lancamento lancamento)
        {
            if (!ExecutarValidacao(new LancamentoValidation(), lancamento)) return;

            await _lancamentoRepository.Atualizar(lancamento);
        }

        public async Task Remover(Guid id)
        {
            await _lancamentoRepository.Remover(id);
        }

        public void Dispose()
        {
            _lancamentoRepository?.Dispose();
        }
    }
}
