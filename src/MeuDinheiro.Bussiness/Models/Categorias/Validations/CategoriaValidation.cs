using FluentValidation;

namespace MeuDinheiro.Bussiness.Models.Categorias.Validations
{
    public class CategoriaValidation : AbstractValidator<Categoria> 
    {
        public CategoriaValidation()
        {
            RuleFor(f => f.Descricao)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(1, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}
