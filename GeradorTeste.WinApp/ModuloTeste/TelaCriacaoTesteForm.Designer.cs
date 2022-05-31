namespace GeradorTeste.WinApp.ModuloTeste
{
    partial class TelaCriacaoTesteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbMaterias = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDisciplinas = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkRecuperacao = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSortear = new System.Windows.Forms.ToolStripButton();
            this.listQuestoes = new System.Windows.Forms.ListBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.txtQtdQuestoes = new System.Windows.Forms.NumericUpDown();
            this.rdbPrimeiraSerie = new System.Windows.Forms.RadioButton();
            this.rdbSegundaSerie = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtdQuestoes)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbMaterias
            // 
            this.cmbMaterias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterias.FormattingEnabled = true;
            this.cmbMaterias.Location = new System.Drawing.Point(105, 148);
            this.cmbMaterias.Name = "cmbMaterias";
            this.cmbMaterias.Size = new System.Drawing.Size(324, 23);
            this.cmbMaterias.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 31;
            this.label4.Text = "Matéria:";
            // 
            // cmbDisciplinas
            // 
            this.cmbDisciplinas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisciplinas.FormattingEnabled = true;
            this.cmbDisciplinas.Location = new System.Drawing.Point(105, 89);
            this.cmbDisciplinas.Name = "cmbDisciplinas";
            this.cmbDisciplinas.Size = new System.Drawing.Size(167, 23);
            this.cmbDisciplinas.TabIndex = 30;
            this.cmbDisciplinas.SelectedIndexChanged += new System.EventHandler(this.cmbDisciplinas_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "Disciplina:";
            // 
            // txtNumero
            // 
            this.txtNumero.Enabled = false;
            this.txtNumero.Location = new System.Drawing.Point(105, 22);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(84, 23);
            this.txtNumero.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "Qtd. Questões:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 25;
            this.label1.Text = "Número:";
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(105, 55);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(324, 23);
            this.txtTitulo.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "Título:";
            // 
            // chkRecuperacao
            // 
            this.chkRecuperacao.AutoSize = true;
            this.chkRecuperacao.Location = new System.Drawing.Point(281, 120);
            this.chkRecuperacao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRecuperacao.Name = "chkRecuperacao";
            this.chkRecuperacao.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkRecuperacao.Size = new System.Drawing.Size(148, 19);
            this.chkRecuperacao.TabIndex = 38;
            this.chkRecuperacao.Text = "?Prova de Recuperação";
            this.chkRecuperacao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRecuperacao.UseVisualStyleBackColor = true;
            this.chkRecuperacao.CheckedChanged += new System.EventHandler(this.chkRecuperacao_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.listQuestoes);
            this.groupBox1.Location = new System.Drawing.Point(15, 186);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(414, 259);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Questões Selecionadas:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSortear});
            this.toolStrip1.Location = new System.Drawing.Point(3, 18);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(408, 50);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSortear
            // 
            this.btnSortear.Image = global::GeradorTeste.WinApp.Properties.Resources.checklist_FILL0_wght400_GRAD0_opsz24;
            this.btnSortear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSortear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSortear.Margin = new System.Windows.Forms.Padding(5);
            this.btnSortear.Name = "btnSortear";
            this.btnSortear.Padding = new System.Windows.Forms.Padding(5);
            this.btnSortear.Size = new System.Drawing.Size(82, 40);
            this.btnSortear.Text = "Sortear";
            this.btnSortear.Click += new System.EventHandler(this.btnSortear_Click);
            // 
            // listQuestoes
            // 
            this.listQuestoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listQuestoes.FormattingEnabled = true;
            this.listQuestoes.ItemHeight = 15;
            this.listQuestoes.Location = new System.Drawing.Point(3, 73);
            this.listQuestoes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listQuestoes.Name = "listQuestoes";
            this.listQuestoes.Size = new System.Drawing.Size(408, 184);
            this.listQuestoes.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(351, 457);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 45);
            this.btnCancelar.TabIndex = 41;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGravar
            // 
            this.btnGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGravar.Location = new System.Drawing.Point(270, 457);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(75, 45);
            this.btnGravar.TabIndex = 40;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // txtQtdQuestoes
            // 
            this.txtQtdQuestoes.Location = new System.Drawing.Point(376, 89);
            this.txtQtdQuestoes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtQtdQuestoes.Name = "txtQtdQuestoes";
            this.txtQtdQuestoes.Size = new System.Drawing.Size(53, 23);
            this.txtQtdQuestoes.TabIndex = 42;
            this.txtQtdQuestoes.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // rdbPrimeiraSerie
            // 
            this.rdbPrimeiraSerie.AutoSize = true;
            this.rdbPrimeiraSerie.Checked = true;
            this.rdbPrimeiraSerie.Location = new System.Drawing.Point(107, 120);
            this.rdbPrimeiraSerie.Name = "rdbPrimeiraSerie";
            this.rdbPrimeiraSerie.Size = new System.Drawing.Size(69, 19);
            this.rdbPrimeiraSerie.TabIndex = 43;
            this.rdbPrimeiraSerie.TabStop = true;
            this.rdbPrimeiraSerie.Text = "Primeira";
            this.rdbPrimeiraSerie.UseVisualStyleBackColor = true;
            this.rdbPrimeiraSerie.CheckedChanged += new System.EventHandler(this.rdbPrimeiraSerie_CheckedChanged);
            // 
            // rdbSegundaSerie
            // 
            this.rdbSegundaSerie.AutoSize = true;
            this.rdbSegundaSerie.Location = new System.Drawing.Point(182, 120);
            this.rdbSegundaSerie.Name = "rdbSegundaSerie";
            this.rdbSegundaSerie.Size = new System.Drawing.Size(71, 19);
            this.rdbSegundaSerie.TabIndex = 44;
            this.rdbSegundaSerie.Text = "Segunda";
            this.rdbSegundaSerie.UseVisualStyleBackColor = true;
            this.rdbSegundaSerie.CheckedChanged += new System.EventHandler(this.rdbSegundaSerie_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 15);
            this.label6.TabIndex = 45;
            this.label6.Text = "Série:";
            // 
            // TelaCriacaoTesteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 519);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rdbSegundaSerie);
            this.Controls.Add(this.rdbPrimeiraSerie);
            this.Controls.Add(this.txtQtdQuestoes);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkRecuperacao);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbMaterias);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbDisciplinas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TelaCriacaoTesteForm";
            this.Text = "Geração de Testes";
            this.groupBox1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtdQuestoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMaterias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDisciplinas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkRecuperacao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSortear;
        private System.Windows.Forms.ListBox listQuestoes;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.NumericUpDown txtQtdQuestoes;
        private System.Windows.Forms.RadioButton rdbPrimeiraSerie;
        private System.Windows.Forms.RadioButton rdbSegundaSerie;
        private System.Windows.Forms.Label label6;
    }
}