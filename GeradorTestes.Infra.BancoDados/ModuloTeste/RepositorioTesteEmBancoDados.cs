using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GeradorTestes.Infra.BancoDados.ModuloTeste
{
    public class RepositorioTesteEmBancoDados : IRepositorioTeste
    {
        private const string enderecoBanco =
            "Data Source=(LocalDb)\\MSSQLLocalDB;" +
            "Initial Catalog=GeradorTeste;" +
            "Integrated Security=True;" +
            "Pooling=False";

        private const string sqlInserir =
          @"INSERT INTO [TBTESTE]
                (
                    [TITULO],                    
                    [DATAGERACAO],
                    [PROVAO],
                    [MATERIA_NUMERO],
                    [DISCIPLINA_NUMERO]
	            )
	            VALUES
                (
                    @TITULO,
                    @DATAGERACAO,
                    @PROVAO,
                    @MATERIA_NUMERO,
                    @DISCIPLINA_NUMERO

                );SELECT SCOPE_IDENTITY();";

        private const string sqlAdicionarQuestao =
          @"INSERT INTO [TBTESTE_TBQUESTAO]
                (
                    [TESTE_NUMERO],                    
                    [QUESTAO_NUMERO]
	            )
	            VALUES
                (
                    @TESTE_NUMERO,
                    @QUESTAO_NUMERO

                );";


        private const string sqlSelecionarTodos =
          @"SELECT        
	            T.TITULO, 
	            T.DATAGERACAO, 
	            T.PROVAO, 	
	            T.NUMERO, 

	            D.NUMERO DISCIPLINA_NUMERO, 
	            D.NOME DISCIPLINA_NOME, 

	            M.NUMERO MATERIA_NUMERO, 
	            M.NOME MATERIA_NOME,
	            M.SERIE MATERIA_SERIE 		

            FROM            	
	            TBDISCIPLINA AS D INNER JOIN
	            TBMATERIA AS M ON D.NUMERO = M.DISCIPLINA_NUMERO RIGHT JOIN
	            TBTESTE AS T ON D.NUMERO = T.DISCIPLINA_NUMERO AND M.NUMERO = T.MATERIA_NUMERO";

        public ValidationResult Inserir(Teste teste)
        {
            ValidadorTeste validador = new ValidadorTeste();

            var resultadoValidacao = validador.Validate(teste);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosTeste(teste, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            teste.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            AdicionarQuestoesNoTeste(teste);

            return resultadoValidacao;
        }

        private void AdicionarQuestoesNoTeste(Teste teste)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            conexaoComBanco.Open();

            foreach (var questao in teste.Questoes)
            {
                SqlCommand comandoInsercao = new SqlCommand(sqlAdicionarQuestao, conexaoComBanco);

                comandoInsercao.Parameters.AddWithValue("TESTE_NUMERO", teste.Numero);
                comandoInsercao.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

                comandoInsercao.ExecuteNonQuery();
            }

            conexaoComBanco.Close();
        }
       
        private void ConfigurarParametrosTeste(Teste teste, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", teste.Numero);
            comando.Parameters.AddWithValue("TITULO", teste.Titulo);
            comando.Parameters.AddWithValue("DATAGERACAO", teste.DataGeracao);
            comando.Parameters.AddWithValue("MATERIA_NUMERO", teste.Disciplina.Numero);
            comando.Parameters.AddWithValue("DISCIPLINA_NUMERO", teste.Materia?.Numero);
            comando.Parameters.AddWithValue("PROVAO", teste.Provao);
        }

        public ValidationResult Editar(Teste registro)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Excluir(Teste registro)
        {
            throw new NotImplementedException();
        }

        public Teste SelecionarPorNumero(int numero)
        {
            throw new NotImplementedException();
        }

        public List<Teste> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

            List<Teste> testes = new List<Teste>();

            while (leitorTeste.Read())
            {
                Teste teste = ConverterParaTeste(leitorTeste);                

                testes.Add(teste);
            }

            conexaoComBanco.Close();

            return testes;
        }

        private Teste ConverterParaTeste(SqlDataReader leitorTeste)
        {
            var numero = Convert.ToInt32(leitorTeste["NUMERO"]);
            string titulo = Convert.ToString(leitorTeste["TITULO"]);
            var provao = Convert.ToBoolean(leitorTeste["PROVAO"]);
            var dataGeracao = Convert.ToDateTime(leitorTeste["DATAGERACAO"]);
            
            int numeroMateria = Convert.ToInt32(leitorTeste["MATERIA_NUMERO"]);
            string nomeMateria = Convert.ToString(leitorTeste["MATERIA_NOME"]);
            SerieMateriaEnum serieMateria = (SerieMateriaEnum)leitorTeste["MATERIA_SERIE"];

            int numeroDisciplina = Convert.ToInt32(leitorTeste["DISCIPLINA_NUMERO"]);
            string nomeDisciplina = Convert.ToString(leitorTeste["DISCIPLINA_NOME"]);

            var teste = new Teste
            {
                Numero = numero,
                Titulo = titulo,
                Provao = provao,
                DataGeracao = dataGeracao
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
        }
    }
}
