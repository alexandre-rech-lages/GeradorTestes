using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Dominio.ModuloQuestao
{
    public static class CharExtension
    {
        public static char Next(this char instancia)
        {
            int numero = instancia;

            numero++;

            return (char)numero;
        }
    }


    public class Questao : EntidadeBase<Questao>
    {
        private readonly List<Alternativa> _alternativas;

        public Questao()
        {
            _alternativas = new List<Alternativa>();
            Materia = new Materia();
        }

        public Questao(int n, int qtdAlternativas, Materia m)
        {
            Numero = n;
            _alternativas = new List<Alternativa>(qtdAlternativas);
        }

        public List<Alternativa> Alternativas
        {
            get { return _alternativas; }
        }

        public string Enunciado { get; set; }

        public Disciplina Disciplina { get; set; }

        public Materia Materia { get; set; }

        public int Numero { get; set; }


        //public void AdicionaUltimaAlternativa()
        //{
        //    char letra = _alternativas.Last().Letra.Next();

        //    AdicionaAlternativa(new Alternativa { Letra = letra });
        //}

        public void AdicionarAlternativa(Alternativa alternativa)
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

            alternativa.Questao = this;

            alternativa.Letra = ObterProximaLetra();

            _alternativas.Add(alternativa);
        }

        public char ObterProximaLetra()
        {
            char letra = 'a';

            if (Alternativas.Count == 0)
                return letra;

            char ultimaLetra = Alternativas.Select(x => x.Letra).Last();

            int numeroUltimaLetra = ++ultimaLetra;

            return (char)numeroUltimaLetra;
        }



        public Alternativa ObtemAlternativaCorreta()
        {
            return Alternativas.First(x => x.Correta);
        }


        //public bool ExisteAlternativaCorreta()
        //{
        //    return Alternativas
        //        .ToList()
        //        .Exists(x => x.Correta);
        //}

        //public void RemoveAlternativa(Alternativa alternativaSelecionada)
        //{
        //    Alternativas.Remove(alternativaSelecionada);
        //}

        //public override string ToString()
        //{
        //    return string.Format("Número: {0}, {1}", Numero, Enunciado);
        //}



        //private bool EhUltimaPosicao()
        //{
        //    return ObtemQuantidadeAlternativaCadastrada() == Alternativas.Count - 1;
        //}

        //private int ObtemQuantidadeAlternativaCadastrada()
        //{
        //    return Alternativas.Count();
        //}

        //private int ObtemQuantidadeAlternativaCorreta()
        //{
        //    return Alternativas
        //        .Count(x => x.Correta);
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj is Questao == false)
        //        return false;

        //    var questao = (Questao)obj;
        //    if (questao.Numero == Numero)
        //        return true;

        //    return false;
        //}

        //public void ReorganizarLetrasAlternativas()
        //{
        //    int letra = 'A';

        //    var alternativasNaoRemovidas = Alternativas.ToList();

        //    foreach (Alternativa alternativa in alternativasNaoRemovidas)
        //    {
        //        alternativa.Letra = (char)letra++;
        //    }
        //}

        public override void Atualizar(Questao questao)
        {
        }



    }
}