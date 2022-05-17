using MeuDinheiro.AppMvc.Extensions;
using MeuDinheiro.Bussiness.Models.Lancamentos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeuDinheiro.AppMvc.ViewModels
{
    public class LancamentoViewModel
    {
        public LancamentoViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Moeda]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TipoLancamento Tipo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Categoria")]
        public Guid CategoriaId { get; set; }

        public CategoriaViewModel Categoria { get; set; }

        public IEnumerable<CategoriaViewModel> Categorias { get; set; }
    }
}