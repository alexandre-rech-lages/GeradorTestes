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

        public TelaCadastroQuestoesForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            this.ConfigurarTela();
            CarregarDisciplinas(disciplinas);
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
                cmbDisciplinas.SelectedItem = Questao.Materia?.Disciplina;

                cmbMaterias.SelectedItem = Questao.Materia;

                foreach (var item in questao.Alternativas)
                {
                    listAlternativas.Items.Add(item);
                }
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

            alternativa.Letra = questao.GerarLetraAlternativa();
            alternativa.Resposta = txtResposta.Text;

            Questao.AdicionarAlternativa(alternativa);

            RecarregarAlternativas();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            var alternativa = listAlternativas.SelectedItem as Alternativa;

            if (alternativa != null)
            {
                Questao.RemoverAlternativa(alternativa);

                listAlternativas.Items.Remove(alternativa);

                RecarregarAlternativas();
            }
        }

        private void RecarregarAlternativas()
        {
            listAlternativas.Items.Clear();

            foreach (var item in questao.Alternativas)
            {
                listAlternativas.Items.Add(item);
            }
        }

        private void cmbDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var disciplina = cmbDisciplinas.SelectedItem as Disciplina;

            if (disciplina != null)
                CarregarMaterias(disciplina.Materias);
        }

        private void chkAlternativaCorreta_CheckedChanged(object sender, EventArgs e)
        {
            var alternativa = listAlternativas.SelectedItem as Alternativa;

            if (alternativa == null)
                return;

            if (chkAlternativaCorreta.Checked)
            {
                Questao.AtualizarAlternativaCorreta(alternativa);
                RecarregarAlternativas();
            }

            else
                alternativa.Correta = false;
        }

        private void listAlternativas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var alternativa = listAlternativas.SelectedItem as Alternativa;
            if (alternativa != null && alternativa.Correta)
                chkAlternativaCorreta.Checked = true;
            else
                chkAlternativaCorreta.Checked = false;
        }
    }
}