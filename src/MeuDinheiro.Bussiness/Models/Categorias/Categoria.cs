using System.Collections.Generic;
using MeuDinheiro.Bussiness.Models.Lancamentos;
using MeuDinheiro.Bussiness.Core.Models;

namespace MeuDinheiro.Bussiness.Models.Categorias
{
    public class Categoria : Entity
    {
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        /*EF Relations*/
        public ICollection<Lancamento> Lancamentos { get; set; }

    }
}
