using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace pro
{
    public class Datebase
    {
        //maybe internal
        static string connection = "Data Source=YURA-PC;Initial Catalog=Site;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection cn = new SqlConnection(connection);
        public void openConnection()
        {
            if (cn.State == System.Data.ConnectionState.Closed)
            {
                cn.Open();
            }
        }
        public void closeConnection()
        {
            if (cn.State == System.Data.ConnectionState.Open)
            {
                cn.Close();
            }
        }
        public SqlConnection getConnection()
        {
            return cn;
        }
    }
}
