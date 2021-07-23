using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace FrmLogin
{ 

    class Koneksi
    {
        public SqlConnection getKoneksi()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=DESKTOP-T5J1LVM\\SMKN2;initial catalog=DB_LKS;integrated security=True;";
            return conn;
        }
    public SqlCommand GetData(string query, SqlConnection conn)
    {
        SqlCommand cmd = new SqlCommand(query, conn);
            return cmd;
    }
}
}
