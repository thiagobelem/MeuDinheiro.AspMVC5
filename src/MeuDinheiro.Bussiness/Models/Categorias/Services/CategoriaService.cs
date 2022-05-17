using System;
using System.Linq;
using System.Threading.Tasks;
using MeuDinheiro.Bussiness.Core.Services;
using MeuDinheiro.Bussiness.Models.Categorias.Validations;
using MeuDinheiro.Bussiness.Core.Notifications;

namespace MeuDinheiro.Bussiness.Models.Categorias.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository, INotificador notificador)
               :base(notificador)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task Adicionar(Categoria categoria)
        {
            if(!ExecutarValidacao(new CategoriaValidation(), categoria)) return;

            if (await CategoriaExistente(categoria)) return;

            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;

            if (await CategoriaExistente(categoria)) return;

            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Remover(Guid id)
        {
            var categoria = await _categoriaRepository.ObterCategoriaLancamentos(id);

            if (categoria.Lancamentos.Any())
            {
                Notificar("Não é possível desativar uma Categoria com Lançamentos");
                return;
            };

            await _categoriaRepository.Remover(id);
        }

        private async Task<bool> CategoriaExistente(Categoria categoria)
        {
            var categoriaAtual = await _categoriaRepository
                                        .Buscar(f => f.Descricao == categoria.Descricao && f.Id != categoria.Id);

            if(!categoriaAtual.Any())
            { 
                return false; 
            }
            else
            {
                Notificar("Já existe uma Categoria com os dados informados");
                return true;
            }
        }

        public void Dispose()
        {
            _categoriaRepository?.Dispose();
        }
    }
}
