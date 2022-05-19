using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloMateria
{
    public partial class TelaCadastroMateriasForm : Form
    {
        private Materia materia;

        public TelaCadastroMateriasForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            this.ConfigurarTela();
            CarregarDisciplinas(disciplinas);
            CarregarSeries();
        }

        public Materia Materia
        {
            get { return materia; }

            set
            {
                materia = value;
                txtNumero.Text = materia.Numero.ToString();
                txtNome.Text = materia.Nome;
                cmbDisciplinas.SelectedItem = materia.Disciplina;
                cmbSeries.SelectedItem = materia.Serie;
            }
        }

        public Func<Materia, ValidationResult> GravarRegistro { get; set; }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            materia.Nome = txtNome.Text;
            materia.Serie = (SerieMateriaEnum)cmbSeries.SelectedItem;
            materia.Disciplina = (Disciplina)cmbDisciplinas.SelectedItem;

            var resultadoValidacao = GravarRegistro(materia);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void CarregarSeries()
        {
            var series = Enum.GetValues(typeof(SerieMateriaEnum));

            cmbSeries.Items.Clear();

            foreach (var item in series)
            {
                cmbSeries.Items.Add(item);
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

    }
}
