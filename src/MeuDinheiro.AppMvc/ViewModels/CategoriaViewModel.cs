using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeuDinheiro.AppMvc.ViewModels
{
    public class CategoriaViewModel
    {
        public CategoriaViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Descrição")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Ativo?")]
        public bool Ativo { get; set; }

        public IEnumerable<LancamentoViewModel> Lancamentos { get; set; }
    }
}