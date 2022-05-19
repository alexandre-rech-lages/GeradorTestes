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
        public override bool Equals(object obj)
        {
            if (obj is Alternativa == false)
                return false;

            var alternativa = (Alternativa)obj;

            return (Letra == alternativa.Letra && Resposta == alternativa.Resposta && Correta == alternativa.Correta);
        }
        public override string ToString()
        {
            if (Correta)
                return string.Format($"({Letra}) -> {Resposta} [Correta]");

            return string.Format($"({Letra}) -> {Resposta}");
        }


    }
}