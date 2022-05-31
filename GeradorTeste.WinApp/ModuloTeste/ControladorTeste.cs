using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloTeste
{
    public class ControladorTeste : ControladorBase
    {
        private IRepositorioDisciplina repositorioDisciplina;
        private IRepositorioMateria repositorioMateria;
        private IRepositorioQuestao repositorioQuestao;
        private IRepositorioTeste repositorioTeste;

        private TabelaTestesControl tabelaTestes;

        public ControladorTeste(IRepositorioTeste repositorioTeste, IRepositorioQuestao repositorioQuestao,
            IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria repositorioMateria)
        {
            this.repositorioQuestao = repositorioQuestao;
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioMateria = repositorioMateria;
            this.repositorioTeste = repositorioTeste;
        }

        public override void Editar()
        {
        }

        public override void Excluir()
        {

        }

        public override void Inserir()
        {
            var disciplinas = repositorioDisciplina.SelecionarTodos();

            var tela = new TelaCriacaoTesteForm(disciplinas);

            tela.Teste = new Teste();

            tela.GravarRegistro = repositorioTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarQuestaos();
            }
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxTeste();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaTestes == null)
                tabelaTestes = new TabelaTestesControl();

            CarregarQuestaos();

            return tabelaTestes;
        }

        private void CarregarQuestaos()
        {
            List<Teste> testes = repositorioTeste.SelecionarTodos();

            tabelaTestes.AtualizarRegistros(testes);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {testes.Count} teste(s)");
        }
    }
}
