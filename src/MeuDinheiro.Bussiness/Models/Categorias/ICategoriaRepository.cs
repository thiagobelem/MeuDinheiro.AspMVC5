using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MeuDinheiro.Bussiness.Core.Data;

namespace MeuDinheiro.Bussiness.Models.Categorias
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterCategoriasAtivas();

        Task<Categoria> ObterCategoriaLancamentos(Guid id);
    }
}
