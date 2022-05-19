using GeradorTestes.Dominio.ModuloQuestao;
using System.Collections.Generic;

namespace GeradorTestes.Dominio.ModuloTeste
{
    public class Gabarito
    {
        public List<AlternativaCorreta> QuestoesCorretas;

        public void AdicionaQuestaoCorreta(int numero, char letra)
        {
            QuestoesCorretas.Add(new AlternativaCorreta(numero, letra));
        }

    }
}
