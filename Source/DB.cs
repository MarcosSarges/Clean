using System.Data.SqlClient;

namespace Limp
{
    internal class DB
    {
        Config config;
        private SqlConnection conn;

        private void Connect()
        {
            string auth = $@"Data Source={config.Servidor};Initial Catalog={config.NomeBanco};User ID={config.Usuario};Password={config.Senha};TrustServerCertificate=True;";
            this.conn = new SqlConnection(auth);
            this.conn.Open();
        }

        public void RunDelete()
        {

            using (SqlCommand sqlCommand = this.conn.CreateCommand())
            {
                sqlCommand.CommandText = $"use {config.NomeBanco}; DELETE FROM pagamentos WHERE pdv = {config.PDV};";
                sqlCommand.ExecuteNonQuery();
            }
        }
        public float RunCount()
        {
            using (SqlCommand sqlCommand = this.conn.CreateCommand())
            {
                sqlCommand.CommandText = $@"
                    use {config.NomeBanco};
                    SELECT
                        SUM(ValorLiquido) AS ValorLiquido
                    FROM
                    (
                        SELECT
                            MAX(CAST(cancelado AS INT)) AS CANCELADO,
                            SUM(CASE WHEN cancelado <> 1 THEN valor ELSE 0 END ) AS ValorLiquido,
                            pdv
                        FROM
                            pagamentos
                        GROUP BY
                            movimento,
                            pdv
                    ) AS aliastable
                    WHERE
                        pdv = {config.PDV}
                    GROUP BY 
                        pdv 
                ";
                SqlDataReader leitor = sqlCommand.ExecuteReader();
                var acumulador = 0.0f;
                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        var row_value = leitor.GetFloat(leitor.GetOrdinal("ValorLiquido"));
                        acumulador += row_value;
                    }
                }
                leitor.Close();
                return acumulador;
            }
        }


        public DB(Config config)
        {
            this.config = config;
            this.Connect();
        }
        public void Fechar()
        {
            this.conn.Close();
        }
    }
}
