using eAgenda.Infra.Arquivos;
using GeradorTeste.WinApp.ModuloDisciplina;
using GeradorTeste.WinApp.ModuloMateria;
using GeradorTeste.WinApp.ModuloQuestao;
using GeradorTeste.WinApp.ModuloTeste;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using GeradorTestes.Infra.BancoDados.ModuloDisciplina;
using GeradorTestes.Infra.BancoDados.ModuloMateria;
using GeradorTestes.Infra.BancoDados.ModuloQuestao;
using GeradorTestes.Infra.BancoDados.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext contextoDados;

        public TelaPrincipalForm(DataContext contextoDados)
        {
            InitializeComponent();

            Instancia = this;

            labelRodape.Text = string.Empty;
            labelTipoCadastro.Text = string.Empty;

            this.contextoDados = contextoDados;

            InicializarControladores();
        }

        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }

        public void AtualizarRodape(string mensagem)
        {
            labelRodape.Text = mensagem;
        }

        private void tarefasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void contatosMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void compromissosMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void despesasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void materiasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void questoesMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void testesMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            controlador.Filtrar();
        }

        private void btnGerarPdf_Click(object sender, EventArgs e)
        {
            controlador.GerarPdf();
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            controlador.Duplicar();
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            controlador.Visualizar();
        }

        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnDuplicar.Enabled = configuracao.DuplicarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
            btnFiltrar.Enabled = configuracao.FiltrarHabilitado;
            btnGerarPdf.Enabled = configuracao.GerarPdfHabilitado;
            btnVisualizar.Enabled = configuracao.VisualizarHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnFiltrar.ToolTipText = configuracao.TooltipFiltrar;
            btnGerarPdf.ToolTipText = configuracao.TooltipGerarPdf;
            btnVisualizar.ToolTipText = configuracao.TooltipVisualizar;
        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarToolbox()
        {
            ConfiguracaoToolboxBase configuracao = controlador.ObtemConfiguracaoToolbox();

            if (configuracao != null)
            {
                toolbox.Enabled = true;

                labelTipoCadastro.Text = configuracao.TipoCadastro;

                ConfigurarTooltips(configuracao);

                ConfigurarBotoes(configuracao);
            }
        }

        private void ConfigurarListagem()
        {
            AtualizarRodape("");

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void InicializarControladores()
        {
            IRepositorioDisciplina repositorioDisciplina = new RepositorioDisciplinaEmBancoDados();
            IRepositorioMateria repositorioMateria = new RepositorioMateriaEmBancoDados();
            IRepositorioQuestao repositorioQuestao = new RepositorioQuestaoEmBancoDados();
            IRepositorioTeste repositorioTeste = new RepositorioTesteEmBancoDados();

            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Disciplinas", new ControladorDisciplina(repositorioDisciplina));
            controladores.Add("Matérias", new ControladorMateria(repositorioMateria, repositorioDisciplina));
            controladores.Add("Questões", new ControladorQuestao(repositorioQuestao, repositorioDisciplina));
            controladores.Add("Testes", new ControladorTeste(repositorioTeste, repositorioQuestao, repositorioDisciplina, repositorioMateria));
        }
    }
}