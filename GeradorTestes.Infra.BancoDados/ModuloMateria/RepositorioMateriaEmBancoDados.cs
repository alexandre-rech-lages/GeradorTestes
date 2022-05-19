using FluentValidation.Results;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.BancoDados.ModuloMateria
{
    public class RepositorioMateriaEmBancoDados : IRepositorioMateria
    {
        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=eAgendaDb;" +
              "Integrated Security=True;" +
              "Pooling=False";

        private const string sqlInserir =
            @"INSERT INTO [TBMATERIA]
                   (
                        [NOME],
                        [SERIE],
                        [DISCIPLINA_NUMERO]
                   )
                VALUES
                   (
                        @NOME,
                        @SERIE,
                        @DISCIPLINA_NUMERO
                    ); 

                SELECT SCOPE_IDENTITY()";

        private const string sqlSelecionarTodos =
            @"SELECT 
	            MT.NUMERO,
	            MT.NOME,
	            MT.SERIE,

	            D.NUMERO AS DISCIPLINA_NUMERO,
	            D.NOME AS DISCIPLINA_NOME

            FROM
	            TBMATERIA AS MT INNER JOIN TBDISCIPLINA AS D ON
	            MT.DISCIPLINA_NUMERO = D.NUMERO";

        public ValidationResult Inserir(Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMateria(materia, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            materia.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return new ValidationResult();
        }

        private static void ConfigurarParametrosMateria(Materia materia, SqlCommand comandoInsercao)
        {
            comandoInsercao.Parameters.AddWithValue("NUMERO", materia.Numero);
            comandoInsercao.Parameters.AddWithValue("NOME", materia.Nome);
            comandoInsercao.Parameters.AddWithValue("SERIE", materia.Serie);
            comandoInsercao.Parameters.AddWithValue("DISCIPLINA_NUMERO", materia.Disciplina.Numero);
        }

        public ValidationResult Editar(Materia registro)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Excluir(Materia registro)
        {
            throw new NotImplementedException();
        }

        

        public Materia SelecionarPorNumero(int numero)
        {
            throw new NotImplementedException();
        }
       
        public List<Materia> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            List<Materia> materias = new List<Materia>();

            while (leitorMateria.Read())
            {
                Materia materia = ConverterParaMateria(leitorMateria);

                materias.Add(materia);
            }

            conexaoComBanco.Close();

            return materias;
        }

        private Materia ConverterParaMateria(SqlDataReader leitorMateria)
        {
            int numero = Convert.ToInt32(leitorMateria["NUMERO"]);
            string nome = Convert.ToString(leitorMateria["NOME"]);
            int serie = Convert.ToInt32(leitorMateria["SERIE"]);

            int numeroDisciplina = Convert.ToInt32(leitorMateria["DISCIPLINA_NUMERO"]);
            string nomeDisciplina = Convert.ToString(leitorMateria["DISCIPLINA_NOME"]);

            var materia = new Materia
            {
                Numero = numero,
                Nome = nome,
                Serie= (SerieMateriaEnum)serie,
                Disciplina = new Disciplina
                {
                    Numero = numeroDisciplina,
                    Nome = nomeDisciplina
                }
            };

            return materia;
        }
    }
}
