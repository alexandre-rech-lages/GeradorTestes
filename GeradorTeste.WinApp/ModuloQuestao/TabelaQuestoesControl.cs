using GeradorTestes.Dominio.ModuloQuestao;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloQuestao
{
    public partial class TabelaQuestoesControl : UserControl
    {
        public TabelaQuestoesControl()
        {
            InitializeComponent();
            grid.ConfigurarGridZebrado();
            grid.ConfigurarGridSomenteLeitura();
            grid.Columns.AddRange(ObterColunas());
        }

        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número", FillWeight=15F },

                new DataGridViewTextBoxColumn { DataPropertyName = "Nome", HeaderText = "Nome", FillWeight=35F },

                new DataGridViewTextBoxColumn { DataPropertyName = "Materia.Nome", HeaderText = "Matéria", FillWeight=25F },

                new DataGridViewTextBoxColumn { DataPropertyName = "Disciplina.Nome", HeaderText = "Disciplina", FillWeight=25F }

            };

            return colunas;
        }

        public void AtualizarRegistros(List<Questao> questoes)
        {
            grid.Rows.Clear();

            foreach (var questao in questoes)
            {
                grid.Rows.Add(questao.Numero, questao.Enunciado, questao?.Materia?.Nome, questao.Materia?.Disciplina?.Nome);
            }
        }

        internal int ObtemNumeroQuestaoSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }
    }
}
