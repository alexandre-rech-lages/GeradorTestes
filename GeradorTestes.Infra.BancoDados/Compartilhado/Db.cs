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
            string sql = "select Count(*) from [dbo].[TBTeste];";

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
                @"  delete from [dbo].[TBTeste_TBQuestao];
                    delete from [dbo].[TBTeste];
                    delete from [dbo].[TBAlternativa];
                    delete from [dbo].[TBQuestao];
                    delete from [dbo].[TBMateria];
                    delete from [dbo].[TBDisciplina];";

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sql, conexaoComBanco);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();            
            conexaoComBanco.Close();
        }


    }
}