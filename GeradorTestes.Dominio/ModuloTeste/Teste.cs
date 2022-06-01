using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Dominio.ModuloTeste
{

    public class Teste : EntidadeBase<Teste>
    {
        public Teste()
        {
            DataGeracao = DateTime.Now;
            Questoes = new List<Questao>();
        }

        public string Titulo { get; set; }

        public List<Questao> Questoes { get; set; }

        public bool Provao { get; set; }

        public DateTime DataGeracao { get; set; }

        public Disciplina Disciplina { get; set; }

        public Materia Materia { get; set; }

        public int QuantidadeQuestoes { get; set; }


        public Gabarito ObterGabarito()
        {
            Gabarito gabarito = new Gabarito();

            gabarito.QuestoesCorretas = new List<AlternativaCorreta>(QuantidadeQuestoes);

            foreach (var questao in Questoes)
            {
                Alternativa alternativa = questao.ObtemAlternativaCorreta();

                gabarito.AdicionaQuestaoCorreta(questao.Numero, alternativa.Letra);
            }

            return gabarito;
        }

        public void SortearQuestoes()
        {
            if (Provao)
                Questoes = Disciplina.TodasQuestoes.Randomize(QuantidadeQuestoes).ToList();
            else
                Questoes = Materia.Questoes.Randomize(QuantidadeQuestoes).ToList();
        }

        public override void Atualizar(Teste teste)
        {
        }

        public Teste Clone()
        {
            return MemberwiseClone() as Teste;
        }

        public void RemoverQuestoes()
        {
            Questoes.Clear();
        }

        public void ConfigurarMateria(Materia materia)
        {
            Materia = materia;
        }
    }
}