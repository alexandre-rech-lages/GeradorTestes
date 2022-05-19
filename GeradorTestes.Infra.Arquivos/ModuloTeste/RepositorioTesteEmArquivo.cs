using FluentValidation;
using GeradorTestes.Dominio.ModuloTeste;
using System.Collections.Generic;

namespace eAgenda.Infra.Arquivos.ModuloTeste
{
    public class RepositorioTesteEmArquivo : RepositorioEmArquivoBase<Teste>, IRepositorioTeste
    {
        public RepositorioTesteEmArquivo(DataContext context) : base(context)
        {
        }

        public override void AtualizarContador()
        {
            contador = dataContext.ObterUltimoNumeroTeste();
        }

        public override List<Teste> ObterRegistros()
        {
            return dataContext.Testes;
        }

        public override AbstractValidator<Teste> ObterValidador()
        {
            return new ValidadorTeste();
        }
    }
}
