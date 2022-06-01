using FluentValidation;

namespace GeradorTestes.Dominio.ModuloTeste
{
    public class ValidadorTeste : AbstractValidator<Teste>
    {
        public ValidadorTeste()
        {
            RuleFor(x => x.Disciplina)
                .NotNull().NotEmpty();

            RuleFor(x => x.DataGeracao)
                .NotNull().NotEmpty();

            RuleFor(x => x.Titulo)
                .NotNull().NotEmpty();

            When(x => x.Provao == true, () =>
            {
                RuleFor(x => x.Materia).Null();
            }).Otherwise(() =>
            {
                RuleFor(x => x.Materia).NotNull();
            });

            RuleFor(x => x.QuantidadeQuestoes)
                .GreaterThanOrEqualTo(5);
        }
    }
}
