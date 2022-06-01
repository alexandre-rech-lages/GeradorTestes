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
    public partial class TelaVisualizacaoTesteForm : Form
    {
        private readonly Teste teste;

        public TelaVisualizacaoTesteForm(Teste teste)
        {
            InitializeComponent();
            this.ConfigurarTela();
            this.teste = teste;

            ConfigurarTela(teste);
        }

        private void ConfigurarTela(Teste teste)
        {
            labelTitulo.Text = teste.Titulo;
            labelDisciplina.Text = teste.Disciplina.Nome;
            if (teste.Provao)
                labelMateria.Text = "Todas as Matérias";
            else
                labelMateria.Text = teste.Materia.Nome;

            listQuestoes.Items.Clear();

            foreach (var item in teste.Questoes)
            {
                listQuestoes.Items.Add(item);
            }
        }
    }
}
