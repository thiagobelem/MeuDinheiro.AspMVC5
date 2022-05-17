using System;
using MeuDinheiro.Bussiness.Core.Models;
using MeuDinheiro.Bussiness.Models.Categorias;

namespace MeuDinheiro.Bussiness.Models.Lancamentos
{
    public class Lancamento : Entity
    {
        public string Descricao { get; set; }

        public DateTime Data { get; set; }

        public decimal Valor { get; set; }

        public TipoLancamento Tipo { get; set; }

        public Guid CategoriaId { get; set; }

        /*EF Relations*/
        public Categoria Categoria { get; set; }
    }
}
