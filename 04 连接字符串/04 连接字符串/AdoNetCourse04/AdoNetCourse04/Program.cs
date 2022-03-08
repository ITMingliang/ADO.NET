using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace AdoNetCourse04
{
    class Program
    {
        static void Main(string[] args)
        {
            //钥匙  --连接字符串
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "";//连接字符串
            //本地：local / .表示  远程：IP,端口号
            //SQLServer身份验证  安全连接
            string connStr = "Data Source=.;Initial Catalog=TestBase;User Id=lyc;Password=123456";
            //Data Source --- server    Initial Catalog---database   User Id--uid   Password---pwd
            //Windows身份验证  可信连接
            string connStr1 = "Data Source=.;Initial Catalog=TestBase;Integrated Security=SSPI";


        }
    }
}
