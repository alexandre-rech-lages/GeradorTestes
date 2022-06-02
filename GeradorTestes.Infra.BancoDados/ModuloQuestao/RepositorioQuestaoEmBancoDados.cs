using FluentValidation;
using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GeradorTestes.Infra.BancoDados.ModuloQuestao
{
    public class RepositorioQuestaoEmBancoDados : IRepositorioQuestao
    {

        private const string enderecoBanco =
                "Data Source=(LocalDb)\\MSSQLLocalDB;" +
                "Initial Catalog=GeradorTeste;" +
                "Integrated Security=True;" +
                "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBQUESTAO]
                (
                    [ENUNCIADO],
                    [MATERIA_NUMERO]
	            )
	            VALUES
                (
                    @ENUNCIADO,                    
                    @MATERIA_NUMERO
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE [TBQUESTAO]	
		            SET                    
                        [ENUNCIADO] = @ENUNCIADO,
                        [MATERIA_NUMERO] = @MATERIA_NUMERO
		            WHERE
			            [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBQUESTAO]
		            WHERE
			            [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
	                Q.NUMERO,
	                Q.ENUNCIADO,

	                M.NUMERO AS MATERIA_NUMERO,
	                M.NOME AS MATERIA_NOME,
                    M.SERIE AS MATERIA_SERIE,

	                D.NUMERO AS DISCIPLINA_NUMERO,
	                D.NOME AS DISCIPLINA_NOME

                FROM 
	                TBQUESTAO Q INNER JOIN TBMATERIA M 
                ON 
	                Q.MATERIA_NUMERO = M.NUMERO INNER JOIN TBDISCIPLINA D 
                ON 
	                D.NUMERO = M.DISCIPLINA_NUMERO";

        private const string sqlSelecionarPorNumero =
           @"SELECT 
	                Q.NUMERO,
	                Q.ENUNCIADO,

	                M.NUMERO AS MATERIA_NUMERO,
	                M.NOME AS MATERIA_NOME,

	                D.NUMERO AS DISCIPLINA_NUMERO,
	                D.NOME AS DISCIPLINA_NOME

                FROM 
	                TBQUESTAO Q INNER JOIN TBMATERIA M 
                ON 
	                Q.MATERIA_NUMERO = M.NUMERO INNER JOIN TBDISCIPLINA D 
                ON 
	                D.NUMERO = M.DISCIPLINA_NUMERO
                WHERE 
                    Q.NUMERO = @NUMERO";

        private const string sqlInserirAlternativas =
           @"INSERT INTO [TBALTERNATIVA]
                    (
		                [QUESTAO_NUMERO],
		                [LETRA],
		                [RESPOSTA],
		                [CORRETA]
	                )
                     VALUES
                    (
		                @QUESTAO_NUMERO,
		                @LETRA,
		                @RESPOSTA,
		                @CORRETA
	                ); SELECT SCOPE_IDENTITY();";

        private const string sqlSelecionarAlternativas =
           @"SELECT 
	                [NUMERO],
                    [QUESTAO_NUMERO],
                    [LETRA],
                    [RESPOSTA],
                    [CORRETA]
                  FROM 
	                [TBALTERNATIVA]
                  WHERE 
	                [QUESTAO_NUMERO] = @QUESTAO_NUMERO";

        private const string sqlExcluirAlternativas =
          @"DELETE FROM [TBALTERNATIVA]
		            WHERE
			            [QUESTAO_NUMERO] = @QUESTAO_NUMERO";

        #endregion

        public ValidationResult Inserir(Questao questao)
        {
            var resultadoValidacao = Validar(questao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosQuestao(questao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            questao.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            AdicionarAlternativas(questao);

            return resultadoValidacao;
        }

        public ValidationResult Editar(Questao questao)
        {
            var resultadoValidacao = Validar(questao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosQuestao(questao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            RemoverAlternativas(questao);

            AdicionarAlternativas(questao);

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Questao registro)
        {
            RemoverAlternativas(registro);

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", registro.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Questao SelecionarPorNumero(int numero)
        {

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            Questao questao = null;

            if (leitorQuestao.Read())
                questao = ConverterParaQuestao(leitorQuestao);

            conexaoComBanco.Close();

            CarregarAlternativas(questao);

            return questao;
        }

        public List<Questao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            List<Questao> questoes = new List<Questao>();

            while (leitorQuestao.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestao);
                CarregarAlternativas(questao);

                questoes.Add(questao);
            }

            conexaoComBanco.Close();

            return questoes;
        }
      
        public AbstractValidator<Questao> ObterValidador()
        {
            return new ValidadorQuestao();

        }

        #region Métodos Privados

        private Questao ConverterParaQuestao(SqlDataReader leitorQuestao)
        {
            int numero = Convert.ToInt32(leitorQuestao["NUMERO"]);
            string enunciado = Convert.ToString(leitorQuestao["ENUNCIADO"]);

            int numeroMateria = Convert.ToInt32(leitorQuestao["MATERIA_NUMERO"]);
            string nomeMateria = Convert.ToString(leitorQuestao["MATERIA_NOME"]);
            SerieMateriaEnum serieMateria = (SerieMateriaEnum)leitorQuestao["MATERIA_SERIE"];

            int numeroDisciplina = Convert.ToInt32(leitorQuestao["DISCIPLINA_NUMERO"]);
            string nomeDisciplina = Convert.ToString(leitorQuestao["DISCIPLINA_NOME"]);

            var questao = new Questao
            {
                Numero = numero,
                Enunciado = enunciado
            };

            var materia = new Materia
            {
                Numero = numeroMateria,
                Nome = nomeMateria,
                Serie = serieMateria
            };

            var disciplina = new Disciplina
            {
                Numero = numeroDisciplina,
                Nome = nomeDisciplina
            };

            materia.ConfigurarDisciplina(disciplina);
            questao.ConfigurarMateria(materia);

            return questao;
        }

        private static void ConfigurarParametrosQuestao(Questao novaQuestao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novaQuestao.Numero);
            comando.Parameters.AddWithValue("MATERIA_NUMERO", novaQuestao.Materia.Numero);
            comando.Parameters.AddWithValue("ENUNCIADO", novaQuestao.Enunciado);
        }

        private ValidationResult Validar(Questao registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var nomeEncontrado = SelecionarTodos()
               .Select(x => x.Enunciado.ToLower())
               .Contains(registro.Enunciado.ToLower());

            if (nomeEncontrado && registro.Numero == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Enunciado já está cadastrado"));

            return resultadoValidacao;
        }

        private void AdicionarAlternativas(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            conexaoComBanco.Open();

            foreach (Alternativa alternativa in questao.Alternativas)
            {
                SqlCommand comandoInsercao = new SqlCommand(sqlInserirAlternativas, conexaoComBanco);

                ConfigurarParametrosAlternativa(alternativa, comandoInsercao);
                var numero = comandoInsercao.ExecuteScalar();
                alternativa.Numero = Convert.ToInt32(numero);
            }

            conexaoComBanco.Close();
        }

        private void ConfigurarParametrosAlternativa(Alternativa alternativa, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", alternativa.Numero);
            comando.Parameters.AddWithValue("LETRA", alternativa.Letra);
            comando.Parameters.AddWithValue("RESPOSTA", alternativa.Resposta);
            comando.Parameters.AddWithValue("CORRETA", alternativa.Correta);
            comando.Parameters.AddWithValue("QUESTAO_NUMERO", alternativa.Questao.Numero);
        }

        private void CarregarAlternativas(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarAlternativas, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitorAlternativa = comandoSelecao.ExecuteReader();

            while (leitorAlternativa.Read())
            {
                Alternativa alternativa = ConverterParaAlternativa(leitorAlternativa);

                questao.AdicionarAlternativa(alternativa);
            }

            conexaoComBanco.Close();
        }

        private Alternativa ConverterParaAlternativa(SqlDataReader leitorItemTarefa)
        {
            var numero = Convert.ToInt32(leitorItemTarefa["NUMERO"]);
            var letra = Convert.ToChar(leitorItemTarefa["LETRA"]);
            var resposta = Convert.ToString(leitorItemTarefa["RESPOSTA"]);
            var correta = Convert.ToBoolean(leitorItemTarefa["CORRETA"]);

            var alternativa = new Alternativa
            {
                Numero = numero,
                Letra = letra,
                Resposta = resposta,
                Correta = correta
            };

            return alternativa;
        }

        private void RemoverAlternativas(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluirAlternativas, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        #endregion
    }
}

