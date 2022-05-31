﻿using GeradorTestes.Dominio.ModuloTeste;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloTeste
{
    public partial class TabelaTestesControl : UserControl
    {
        public TabelaTestesControl()
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

                new DataGridViewTextBoxColumn { DataPropertyName = "Titulo", HeaderText = "Título", FillWeight=15F },

                new DataGridViewTextBoxColumn { DataPropertyName = "Disciplina.Nome", HeaderText = "Disciplina", FillWeight=20F },

                new DataGridViewTextBoxColumn { DataPropertyName = "Recuperacao", HeaderText = "Recuperação", FillWeight=25F },

                new DataGridViewTextBoxColumn { DataPropertyName = "Materia.Nome", HeaderText = "Matéria", FillWeight=25F },


            };

            return colunas;
        }

        public int ObtemNumeroTarefaSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Teste> testes)
        {
            grid.Rows.Clear();

            foreach (var teste in testes)
            {
                string disciplina = teste.Recuperacao ? teste.Disciplina.Nome : teste.Materia.Disciplina.Nome;

                grid.Rows.Add(teste.Numero, teste.Titulo, disciplina,
                    teste.Recuperacao ? "Provão" : "", teste.Materia?.Nome);
            }
        }
    }
}
