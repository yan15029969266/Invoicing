using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace CoreLogic
{
    public class BasicOperation
    {
        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLServerConnString"].ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
