using System;

namespace GeradorTestes.Dominio.ModuloQuestao
{
    public class Alternativa
    {
        public Alternativa()
        {
            Questao = new Questao();
        }

        public bool Correta { get; set; }

        public char Letra { get; set; }

        public Questao Questao { get; set; }

        public string Resposta { get; set; }

        public int Numero { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Alternativa alternativa &&
                   Correta == alternativa.Correta &&
                   Letra == alternativa.Letra &&
                   Resposta == alternativa.Resposta &&
                   Numero == alternativa.Numero;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Correta, Letra, Questao, Resposta, Numero);
        }

        public override string ToString()
        {
            return string.Format($"({Letra}) -> {Resposta}");
        }


    }
}