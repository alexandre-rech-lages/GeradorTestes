using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using System;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloDisciplina
{
    public partial class TelaCadastroDisciplinasForm : Form
    {
        private Disciplina disciplina;

        public TelaCadastroDisciplinasForm()
        {
            InitializeComponent();
            this.ConfigurarTela();
        }

        public Func<Disciplina, ValidationResult> GravarRegistro { get; set; }

        public Disciplina Disciplina
        {
            get => disciplina;

            set
            {
                disciplina = value;

                txtNumero.Text = disciplina.Numero.ToString();
                txtNome.Text = disciplina.Nome;
            }
        }

        private void btnGravar_Click(object sender, System.EventArgs e)
        {
            disciplina.Nome = txtNome.Text;

            var resultadoValidacao = GravarRegistro(Disciplina);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }
    }
}
