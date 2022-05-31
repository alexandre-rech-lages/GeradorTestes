using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;

namespace GeradorTestes.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {
        public Materia()
        {
            Disciplina = new Disciplina();
            Questoes = new List<Questao>();
        }

        public Materia(string n, Disciplina d, SerieMateriaEnum s) : this()
        {
            Nome = n;
            Disciplina = d;
            Serie = s;
        }

        public string Nome { get; set; }

        public SerieMateriaEnum Serie { get; set; }

        public Disciplina Disciplina { get; set; }

        public List<Questao> Questoes { get; set; }

        public void AdicionaQuestao(Questao questao)
        {
            if (Questoes.Contains(questao))
                return;

            Questoes.Add(questao);

            questao.Materia = this;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Nome, Serie);
        }

        public void ConfigurarDisciplina(Disciplina disciplina)
        {
            if (Disciplina.Equals(disciplina) == false)
            {
                Disciplina = disciplina;
                Disciplina.AdicionarMateria(this);
            }
        }

        public Materia Clone()
        {
            return MemberwiseClone() as Materia;
        }

        public override void Atualizar(Materia materia)
        {
            Nome = materia.Nome;
            Disciplina = materia.Disciplina;
            Serie = materia.Serie;
        }

        public override bool Equals(object obj)
        {
            return obj is Materia materia &&
                   Numero == materia.Numero &&
                   Nome == materia.Nome &&
                   Serie == materia.Serie;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numero, Nome, Serie, Disciplina, Questoes);
        }
    }
}