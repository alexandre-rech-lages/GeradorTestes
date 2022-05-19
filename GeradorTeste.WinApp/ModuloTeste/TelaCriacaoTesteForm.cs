using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloTeste
{
    public partial class TelaCriacaoTesteForm : Form
    {
        public TelaCriacaoTesteForm()
        {
            InitializeComponent();
            this.ConfigurarTela();
        }

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }

       
    }
}
