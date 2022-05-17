using FluentValidation;

namespace MeuDinheiro.Bussiness.Models.Lancamentos.Validations
{
    public class LancamentoValidation : AbstractValidator<Lancamento>
    {
        public LancamentoValidation()
        {
            RuleFor(f => f.Descricao).NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(1,100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Tipo).IsInEnum();

            RuleFor(f => f.Valor).GreaterThan(0).WithMessage("O {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }
}
