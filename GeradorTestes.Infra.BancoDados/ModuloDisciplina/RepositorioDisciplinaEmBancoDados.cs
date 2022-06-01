using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GeradorTestes.Infra.BancoDados.ModuloDisciplina
{
    public class RepositorioDisciplinaEmBancoDados : IRepositorioDisciplina
    {
        private const string enderecoBanco =
             "Data Source=(LocalDB)\\MSSqlLocalDB;" +
             "Initial Catalog=GeradorTeste;" +
             "Integrated Security=True;" +
             "Pooling=False";

        #region Sql Queries

        private const string sqlInserir =
            @"INSERT INTO [TBDISCIPLINA]
                (
                    [NOME]
                )    
                 VALUES
                (
                    @NOME
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBDISCIPLINA]	
		        SET
			        [NOME] = @NOME
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBDISCIPLINA]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [NUMERO], 
		            [NOME] 
	            FROM 
		            [TBDISCIPLINA]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [NUMERO], 
		            [NOME]
	            FROM 
		            [TBDISCIPLINA]
		        WHERE
                    [NUMERO] = @NUMERO";

        private const string sqlSelecionarMateriasDaDisciplina =
            @"SELECT 
		            [NUMERO] MATERIA_NUMERO, 
		            [NOME] MATERIA_NOME, 
                    [SERIE] MATERIA_SERIE
	            FROM 
		            [TBMATERIA]
		        WHERE
                    [DISCIPLINA_NUMERO] = @DISCIPLINA_NUMERO";

        private const string sqlSelecionarQuestoesDaMateria =
            @"SELECT 
		            [NUMERO] QUESTAO_NUMERO, 
		            [ENUNCIADO] QUESTAO_ENUNCIADO
	            FROM 
		            [TBQUESTAO]
		        WHERE
                    [MATERIA_NUMERO] = @MATERIA_NUMERO";

        #endregion

        public ValidationResult Inserir(Disciplina novaDisciplina)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidacao = validador.Validate(novaDisciplina);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexao = new SqlConnection(enderecoBanco);
            SqlCommand cmdInserir = new SqlCommand(sqlInserir, conexao);

            ConfigurarParametrosDisciplina(novaDisciplina, cmdInserir);
            conexao.Open();

            var numero = cmdInserir.ExecuteScalar();

            novaDisciplina.Numero = Convert.ToInt32(numero);
            conexao.Close();

            return resultadoValidacao;

        }

        public ValidationResult Editar(Disciplina disciplina)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidacao = validador.Validate(disciplina);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosDisciplina(disciplina, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Disciplina disciplina)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", disciplina.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Disciplina> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            List<Disciplina> disciplinas = new List<Disciplina>();

            while (leitorDisciplina.Read())
            {
                Disciplina disciplina = ConverterParaDisciplina(leitorDisciplina);

                CarregarMaterias(disciplina);

                disciplinas.Add(disciplina);
            }

            conexaoComBanco.Close();

            return disciplinas;
        }

        private void CarregarMaterias(Disciplina disciplina)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarMateriasDaDisciplina, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("DISCIPLINA_NUMERO", disciplina.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            while (leitorMateria.Read())
            {
                Materia materia = ConverterParaMateria(leitorMateria);

                CarregarQuestoes(materia);

                disciplina.AdicionarMateria(materia);
            }

            conexaoComBanco.Close();
        }

        private void CarregarQuestoes(Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarQuestoesDaMateria, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("MATERIA_NUMERO", materia.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestoes = comandoSelecao.ExecuteReader();

            while (leitorQuestoes.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestoes);

                materia.AdicionaQuestao(questao);                                        
            }

            conexaoComBanco.Close();
        }

        private Questao ConverterParaQuestao(SqlDataReader leitorQuestoes)
        {
            int numero = Convert.ToInt32(leitorQuestoes["QUESTAO_NUMERO"]);
            string enunciado = Convert.ToString(leitorQuestoes["QUESTAO_ENUNCIADO"]);

            return new Questao
            {
                Numero = numero,
                Enunciado = enunciado
            };
        }

        private Materia ConverterParaMateria(SqlDataReader leitorMateria)
        {
            int numero = Convert.ToInt32(leitorMateria["MATERIA_NUMERO"]);
            string nome = Convert.ToString(leitorMateria["MATERIA_NOME"]);
            var serie = (SerieMateriaEnum)leitorMateria["MATERIA_SERIE"];

            var materia = new Materia
            {
                Numero = numero,
                Nome = nome,
                Serie = serie
            };

            return materia;
        }

        public Disciplina SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            Disciplina disciplina = null;
            if (leitorDisciplina.Read())
                disciplina = ConverterParaDisciplina(leitorDisciplina);

            conexaoComBanco.Close();

            CarregarMaterias(disciplina);

            return disciplina;
        }

        private Disciplina ConverterParaDisciplina(SqlDataReader leitorDisciplina)
        {
            int numero = Convert.ToInt32(leitorDisciplina["NUMERO"]);
            string nome = Convert.ToString(leitorDisciplina["NOME"]);

            var disciplina = new Disciplina
            {
                Numero = numero,
                Nome = nome,
            };

            return disciplina;
        }

        private static void ConfigurarParametrosDisciplina(Disciplina novaDisciplina, SqlCommand cmdInserir)
        {
            cmdInserir.Parameters.AddWithValue("NUMERO", novaDisciplina.Numero);
            cmdInserir.Parameters.AddWithValue("NOME", novaDisciplina.Nome);
        }
    }
}
