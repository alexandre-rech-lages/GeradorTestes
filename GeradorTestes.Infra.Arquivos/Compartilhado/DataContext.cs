using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Infra.Arquivos
{
    [Serializable]
    public class DataContext //Container
    {
        private readonly ISerializador serializador;

        public DataContext()
        {
            Disciplinas = new List<Disciplina>();

            Materias = new List<Materia>();

            Testes = new List<Teste>();

            Questoes = new List<Questao>();
        }

        public int ObterUltimoNumeroDisciplina()
        {
            if (Disciplinas.Any())
                return Disciplinas.Max(x => x.Numero);

            return 0;
        }

        public int ObterUltimoNumeroMateria()
        {
            if (Materias.Any())
                return Materias.Max(x => x.Numero);
            return 0;
        }

        public int ObterUltimoNumeroQuestao()
        {
            if (Questoes.Any())
                return Questoes.Max(x => x.Numero);
            return 0;
        }

        public int ObterUltimoNumeroTeste()
        {
            if (Testes.Any())
                return Testes.Max(x => x.Numero);
            return 0;
        }

        public DataContext(ISerializador serializador) : this()
        {
            this.serializador = serializador;

            CarregarDados();
        }

        public List<Materia> Materias { get; set; }

        public List<Disciplina> Disciplinas { get; set; }

        public List<Teste> Testes { get; set; }

        public List<Questao> Questoes { get; set; }

        public void GravarDados()
        {
            serializador.GravarDadosEmArquivo(this);
        }

        private void CarregarDados()
        {
            var ctx = serializador.CarregarDadosDoArquivo();

            if (ctx.Disciplinas.Any())
                this.Disciplinas.AddRange(ctx.Disciplinas);

            if (ctx.Materias.Any())
                this.Materias.AddRange(ctx.Materias);

            if (ctx.Questoes.Any())
                this.Questoes.AddRange(ctx.Questoes);

            if (ctx.Testes.Any())
                this.Testes.AddRange(ctx.Testes);
        }
    }
}
