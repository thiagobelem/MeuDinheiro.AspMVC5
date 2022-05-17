using System;
using System.Threading.Tasks;

namespace MeuDinheiro.Bussiness.Models.Categorias.Services
{
    public interface ICategoriaService : IDisposable
    {
        Task Adicionar(Categoria categoria);

        Task Atualizar(Categoria categoria);

        Task Remover(Guid id);
    }
}
