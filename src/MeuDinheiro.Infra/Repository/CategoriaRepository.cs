using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using MeuDinheiro.Bussiness.Models.Categorias;
using MeuDinheiro.Infra.Data.Context;

namespace MeuDinheiro.Infra.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(MeuDinheiroDBContext context) : base(context){}

        public async Task<IEnumerable<Categoria>> ObterCategoriasAtivas()
        {
            return await Buscar(c => c.Ativo == true);
        }

        public override async Task Remover(Guid id)
        {
            var categoria = await ObterPorId(id);
            categoria.Ativo = false;

            await Atualizar(categoria);
        }

        public async Task<Categoria> ObterCategoriaLancamentos(Guid id)
        {
            return await Db.Categorias.AsNoTracking()
                                 .Include(c => c.Lancamentos)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
