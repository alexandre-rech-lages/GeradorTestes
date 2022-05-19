using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Dominio.ModuloDisciplina
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> target, int count)
        {
            Random r = new Random();

            return target.OrderBy(x => (r.Next())).Take(count);
        }
    }

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

        public override bool Equals(object obj)
        {
            if (obj is Disciplina == false)
                return false;

            var disciplina = (Disciplina)obj;
            if (disciplina.Numero == this.Numero)
                return true;

            return false;
        }

        public override string ToString()
        {
            return string.Format("{0} - Quantidade de questões: {1}", Nome, TodasQuestoes.Count());
        }


        public override void Atualizar(Disciplina disciplina)
        {
        }
    }
}