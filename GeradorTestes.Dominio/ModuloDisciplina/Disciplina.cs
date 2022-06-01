using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public Disciplina()
        {
            Materias = new List<Materia>();
        }

        public List<Questao> TodasQuestoes
        {
            get
            {
                var todasQuestoes = new List<Questao>();

                if (Materias.Any())
                    foreach (var m in Materias)
                        todasQuestoes.AddRange(m.Questoes);

                return todasQuestoes;
            }
        }

        public List<Materia> Materias { get; set; }

        public string Nome { get; set; }

        public List<Materia> ObterMateriasPorSerie(SerieMateriaEnum serie)
        {
            if (Materias.Any())
                return Materias.Where(x => x.Serie == serie).ToList();

            return new List<Materia>();
        }

        public bool AdicionarMateria(Materia materia)
        {
            if (Materias.Contains(materia))
                return false;

            Materias.Add(materia);

            materia.ConfigurarDisciplina(this);

            return true;
        }

        public override string ToString()
        {
            return Nome;
        }

        public override void Atualizar(Disciplina disciplina)
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Disciplina disciplina &&
                   Numero == disciplina.Numero &&
                   Nome == disciplina.Nome;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numero, Materias, Nome);
        }

        public Disciplina Clone()
        {
            return MemberwiseClone() as Disciplina;
        }
    }
}