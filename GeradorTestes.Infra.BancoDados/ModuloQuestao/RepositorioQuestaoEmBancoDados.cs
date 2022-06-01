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
    public class RepositorioQuestaoEmBancoDeDados : IRepositorioQuestao
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
	                D.NUMERO = M.DISCIPLINA_NUMERO";

        private const string sqlInserirAlternativas =
           @"INSERT INTO [TBALTERNATIVA]
                    (
		                [QUESTAO_NUMERO],
		                [LETRA],
		                [DESCRICAO]
	                )
                     VALUES
                    (
		                @QUESTAO_NUMERO,
		                @LETRA,
		                @DESCRICAO
	                ); SELECT SCOPE_IDENTITY();";

        private const string sqlSelecionarAlternativas =
           @"SELECT 
	                [NUMERO],
                    [QUESTAO_NUMERO],
                    [LETRA],
                    [DESCRICAO]
                  FROM 
	                [TBALTERNATIVA]
                  WHERE 
	                [QUESTAO_NUMERO] = @QUESTAO_NUMERO";

        private const string sqlExcluirAlternativa =
          @"DELETE FROM [TBALTERNATIVA]
		            WHERE
			            [QUESTAO_NUMERO] = @QUESTAO_NUMERO";

        #endregion

        public ValidationResult Inserir(Questao novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;


            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosQuestao(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Questao registro)
        {
            var resultadoValidacao = Validar(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosQuestao(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            AdicionarAlternativas(registro, registro.Alternativas);

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Questao registro)
        {
            ExcluirAlternativa(registro);

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

        private Questao ConverterParaQuestao(SqlDataReader leitorQuestao)
        {
            int numero = Convert.ToInt32(leitorQuestao["NUMERO"]);
            string enunciado = Convert.ToString(leitorQuestao["ENUNCIADO"]);

            int numeroMateria = Convert.ToInt32(leitorQuestao["MATERIA_NUMERO"]);
            string nomeMateria = Convert.ToString(leitorQuestao["MATERIA_NOME"]);

            int numeroDisciplina = Convert.ToInt32(leitorQuestao["DISCIPLINA_NUMERO"]);
            string nomeDisciplina = Convert.ToString(leitorQuestao["DISCIPLINA_NOME"]);

            var questao = new Questao
            {
                Numero = numero,
                Enunciado = enunciado,

                Materia = new Materia
                {
                    Numero = numeroMateria,
                    Nome = nomeMateria,

                    Disciplina = new Disciplina
                    {
                        Numero = numeroDisciplina,
                        Nome = nomeDisciplina
                    }
                }
            };

            return questao;
        }

        private static void ConfigurarParametrosQuestao(Questao novaQuestao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novaQuestao.Numero);
            comando.Parameters.AddWithValue("MATERIA_NUMERO", novaQuestao.Materia.Numero);
            comando.Parameters.AddWithValue("ENUNCIADO", novaQuestao.Enunciado);
        }

        public AbstractValidator<Questao> ObterValidador()
        {
            return new ValidadorQuestao();

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

        public void AdicionarAlternativas(Questao questao, List<Alternativa> alternativas)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            conexaoComBanco.Open();

            foreach (Alternativa alternativa in alternativas)
            {
                bool alternativaAdicionada = questao.AdicionarAlternativa(alternativa);

                if (alternativaAdicionada)
                {
                    SqlCommand comandoInsercao = new SqlCommand(sqlInserirAlternativas, conexaoComBanco);

                    ConfigurarParametrosAlternativa(alternativa, comandoInsercao);
                    var numero = comandoInsercao.ExecuteScalar();
                    alternativa.Numero = Convert.ToInt32(numero);
                }
            }

            conexaoComBanco.Close();

        }

        private void ConfigurarParametrosAlternativa(Alternativa alternativa, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", alternativa.Numero);
            comando.Parameters.AddWithValue("LETRA", alternativa.Letra);
            comando.Parameters.AddWithValue("RESPOSTA", alternativa.Resposta);
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

            var alternativa = new Alternativa
            {
                Numero = numero,
                Letra = letra,
                Resposta = resposta
            };

            return alternativa;
        }

        //public List<Questao> Sortear(Materia materia, int quantidade)
        //{
        //    int limite = 0;
        //    List<Questao> questoesSorteadas = new List<Questao>();
        //    List<Questao> questoesMateriaSelecionada = SelecionarTodos().Where(x => x.Materia.Nome.Equals(materia.Nome)).ToList();

        //    Random random = new Random();
        //    List<Questao> questoes = questoesMateriaSelecionada.OrderBy(item => random.Next()).ToList();

        //    foreach (Questao questao in questoes)
        //    {
        //        questoesSorteadas.Add(questao);
        //        limite++;
        //        if (limite == quantidade)
        //            break;
        //    }


        //    return questoesSorteadas;
        //}

        //public List<Questao> SortearQuestoesProvao(Disciplina disciplina, int quantidade)
        //{
        //    {
        //        throw new NotImplementedException();
        //        int limite = 0;
        //        List<Questao> questoesSorteadas = new List<Questao>();
        //        List<Questao> questoesDisciplinaSelecionada = SelecionarTodos().Where(x => x.Disciplina.Nome.Equals(disciplina.Nome)).ToList();

        //        Random random = new Random();
        //        List<Questao> questoes = questoesDisciplinaSelecionada.OrderBy(item => random.Next()).ToList();

        //        foreach (Questao questao in questoes)
        //        {
        //            questoesSorteadas.Add(questao);
        //            limite++;
        //            if (limite == quantidade)
        //                break;
        //        }

        //        return questoesSorteadas;
        //    }
        //}

        private void ExcluirAlternativa(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluirAlternativa, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

    }
}

