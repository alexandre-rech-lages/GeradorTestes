using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;

namespace GeradorTestes.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {

        public Disciplina Disciplina { get; set; }

        public List<Questao> Questoes { get; private set; }

        public string Nome { get; set; }

        public SerieMateriaEnum Serie { get; set; }

        public Materia()
        {
            Disciplina = new Disciplina();
            Questoes = new List<Questao>();
        }

        public Materia(string n)
        {
            Nome = n;
        }

        public Materia(string n, Disciplina d, SerieMateriaEnum s)
        {
            Nome = n;
            Disciplina = d;
            Serie = s;
        }

        public void AdicionaQuestao(Questao questao)
        {
            Questoes.Add(questao);
        }

        public override string ToString()
        {
            return string.Format("{0} - Série: {1}", Nome, Serie);
        }       

        public override bool Equals(object obj)
        {
            if (obj is Materia == false)
                return false;

            Materia materia = (Materia)obj;
            if (materia.Numero == this.Numero)
                return true;

            return false;
        }

        public override void Atualizar(Materia materia)
        {

        }
    }
}