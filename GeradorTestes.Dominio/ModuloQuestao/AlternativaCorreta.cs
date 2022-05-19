namespace GeradorTestes.Dominio.ModuloQuestao
{
    public class AlternativaCorreta
    {
        public AlternativaCorreta(int n, char l)
        {
            numero = n;
            letra = l;
        }

        public int numero;
        public char letra;

        public override string ToString()
        {
            return string.Format("Letra: {0} Número: {1}", numero, letra);
        }
    }
}