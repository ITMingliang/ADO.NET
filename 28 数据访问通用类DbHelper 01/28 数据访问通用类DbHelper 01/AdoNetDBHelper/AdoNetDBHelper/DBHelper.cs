using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace AdoNetDBHelper
{
    public  class DBHelper
    {
        //连接字符串
        private readonly string ConnStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        SqlConnection conn = null;

        //private SqlConnection GetConn()
        //{
        //    conn = new SqlConnection(ConnStr);
        //    return conn;
        //}


        
    }
}
