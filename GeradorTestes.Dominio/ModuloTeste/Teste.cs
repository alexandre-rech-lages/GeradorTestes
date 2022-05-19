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
        public Teste(bool recuperacao)
        {
            Recuperacao = recuperacao;

            Gabarito = new Gabarito();
        }

        public List<Questao> Questoes { get; private set; }

        public bool Recuperacao { get; set; }

        public DateTime DataGeracao { get; set; }

        public Disciplina Disciplina { get; set; }

        public Materia Materia { get; set; }

        public Gabarito Gabarito { get; private set; }

        public int QuantidadeQuestoes { get; set; }

        public void AdicionaQuestao(Questao questao)
        {
            Questoes.Add(questao);
        }

        public void GerarTeste()
        {
            if (Recuperacao)
                Questoes = Disciplina.TodasQuestoes.Randomize(QuantidadeQuestoes).ToList();
            else
                Questoes = Materia.Questoes.Randomize(QuantidadeQuestoes).ToList();

            Gabarito.QuestoesCorretas = new List<AlternativaCorreta>(QuantidadeQuestoes);

            foreach (var questao in Questoes)
            {
                //Alternativa alternativa = questao.ObtemAlternativaCorreta();

                //Gabarito.AdicionaQuestaoCorreta(questao.Numero, alternativa.Letra);
            }
        }


        public override void Atualizar(Teste teste)
        {
        }





    }
}