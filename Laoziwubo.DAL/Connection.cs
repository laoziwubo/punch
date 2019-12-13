using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using MySql.Data.MySqlClient;

namespace Laoziwubo.DAL
{
    public class Connection
    {
        private const string SqlServerConnectionString = @"Server=172.17.56.91;Database=db_laoziwubo;User ID = ywzcb; Password=ywzcb@12315;";
        private const string MySqlConnectionString = @"server=155.138.207.205;database=laoziwubo;uid=root;pwd=Juebie@2;SslMode=none;";

        public IDbConnection GetSqlConnection()
        {
            IDbConnection conn = new MySqlConnection(MySqlConnectionString);
            conn.Open();
            return conn;
        }
    }
}
