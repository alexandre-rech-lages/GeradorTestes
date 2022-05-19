using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloQuestao
{
    public partial class TelaCadastroQuestoesForm : Form
    {
        private Questao questao;

        public TelaCadastroQuestoesForm(List<Disciplina> disciplinas, List<Materia> materias)
        {
            InitializeComponent();
            this.ConfigurarTela();
            CarregarDisciplinas(disciplinas);
            CarregarMaterias(materias);
        }

        public Func<Questao, ValidationResult> GravarRegistro { get; set; }

        public Questao Questao
        {
            get => questao;
            set
            {
                questao = value;
                txtNumero.Text = Questao.Numero.ToString();
                txtEnunciado.Text = Questao.Enunciado;
                cmbMaterias.SelectedItem = Questao.Materia;
                cmbDisciplinas.SelectedItem = Questao.Disciplina;
            }
        }

        private void CarregarMaterias(List<Materia> materias)
        {
            cmbMaterias.Items.Clear();

            foreach (var item in materias)
            {
                cmbMaterias.Items.Add(item);
            }
        }

        private void CarregarDisciplinas(List<Disciplina> disciplinas)
        {
            cmbDisciplinas.Items.Clear();

            foreach (var item in disciplinas)
            {
                cmbDisciplinas.Items.Add(item);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Questao.Enunciado = txtEnunciado.Text;
            Questao.Materia = (Materia)cmbMaterias.SelectedItem;
            Questao.Disciplina = (Disciplina)cmbDisciplinas.SelectedItem;

            var resultadoValidacao = GravarRegistro(questao);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Alternativa alternativa = new Alternativa();

            if (chkAlternativaCorreta.Checked)
                alternativa.Correta = true;

            alternativa.Resposta = txtResposta.Text;

            questao.AdicionarAlternativa(alternativa);

            listAlternativas.Items.Add(alternativa);
        }
    }
}
