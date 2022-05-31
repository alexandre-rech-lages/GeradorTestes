using FluentValidation;

namespace GeradorTestes.Dominio.ModuloQuestao
{
    public class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            //int qtdAlternativasCorretas = ObtemQuantidadeAlternativaCorreta();

            //if (qtdAlternativasCorretas == 1 && alternativa.Correta)
            //{
            //    throw new ApplicationException("Não pode cadastrar duas alternativas corretas para a questão: " + Numero);
            //}

            //if (qtdAlternativasCorretas == 0 && EhUltimaPosicao() && alternativa.Correta == false)
            //{
            //    throw new ApplicationException(
            //        "Ao menos uma alternativa correta deverá ser informado para a questão: " + Numero);
            //}

        }

        public void Valida()
        {
            //if (string.IsNullOrEmpty(Enunciado))
            //    throw new ApplicationException("O enunciado é obrigatório.");

            //if (string.IsNullOrEmpty(Bimestre.ToString()))
            //    throw new ApplicationException("Selecione um bimestre.");

            //if (Materia == null)
            //    throw new ApplicationException("Selecione uma Matéria");

            //if (Alternativas.Count == 0)
            //    throw new ApplicationException("0 Alternativas na lista.");

            //if (ExisteAlternativaCorreta() == false)
            //    throw new ApplicationException("Não há alternativa correta");

            //foreach (Alternativa alternativa in Alternativas)
            //{
            //    alternativa.Valida();
            //}

            //int qtdAlternativasCorretas = ObtemQuantidadeAlternativaCorreta();

            //if (qtdAlternativasCorretas > 1)
            //{
            //    throw new ApplicationException("Não pode cadastrar duas alternativas corretas para a questão: " + Numero);
            //}

            //if (qtdAlternativasCorretas == 0)
            //{
            //    throw new ApplicationException(
            //        "Ao menos uma alternativa correta deverá ser informado para a questão: " + Numero);
            //}
        }
    }
}
