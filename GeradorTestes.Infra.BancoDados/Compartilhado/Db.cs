using System;
using System.Data.SqlClient;

namespace GeradorTestes.Infra.BancoDados.Compartilhado
{
    public static class Db
    {
        private const string enderecoBanco =
            "Data Source=(LocalDb)\\MSSQLLocalDB;" +
            "Initial Catalog=GeradorTeste;" +
            "Integrated Security=True;" +
            "Pooling=False";

        public static bool ExisteTestesGravados()
        {
            string sql = "select Count(*) from [TBTeste];";

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoConsulta = new SqlCommand(sql, conexaoComBanco);

            conexaoComBanco.Open();
            var resultado = comandoConsulta.ExecuteScalar();
            conexaoComBanco.Close();

            return Convert.ToInt32(resultado) > 0;
        }

        public static void LimparTabelas()
        {
            string sql =
                @"  DELETE FROM [TBTESTE_TBQUESTAO];                    

                    DELETE FROM [TBTESTE];
                    DBCC CHECKIDENT (TBTESTE, RESEED, 0);

                    DELETE FROM [TBALTERNATIVA];
                    DBCC CHECKIDENT (TBALTERNATIVA, RESEED, 0);

                    DELETE FROM [TBQUESTAO];
                    DBCC CHECKIDENT (TBQUESTAO, RESEED, 0);

                    DELETE FROM [TBMATERIA];
                    DBCC CHECKIDENT (TBMATERIA, RESEED, 0);

                    DELETE FROM [TBDISCIPLINA];
                    DBCC CHECKIDENT (TBDISCIPLINA, RESEED, 0)";

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sql, conexaoComBanco);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();            
            conexaoComBanco.Close();
        }
    }
}