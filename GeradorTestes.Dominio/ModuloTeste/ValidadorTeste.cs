using FluentValidation;

namespace GeradorTestes.Dominio.ModuloTeste
{
    public class ValidadorTeste : AbstractValidator<Teste>
    {
        public ValidadorTeste()
        {

        }
        public void Valida()
        {
            //if (Disciplina == null)
            //    throw new ApplicationException("Disciplina não pode estar em branco!");

            //if (Recuperacao)
            //{
            //    if (Materia != null)
            //        throw new ApplicationException("Materia deve estar em branco");
            //}
            //else
            //{
            //    if (Materia == null)
            //        throw new ApplicationException("Materia deve estar selecionada");
            //}

            //if (QuantidadeQuestoes < 5)
            //{
            //    throw new ApplicationException("Quantidade de questões inválida");
            //}
        }
    }
}
