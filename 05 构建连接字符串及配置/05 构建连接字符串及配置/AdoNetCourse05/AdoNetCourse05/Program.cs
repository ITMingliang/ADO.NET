using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace AdoNetCourse05
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection();
            //手写
            //string connStr = "server=.;database=TestBase;uid=lyc;pwd=123456";

            //SqlConnectionStringBuilder生成连接字符串
            //SqlConnectionStringBuilder connStrBuilder = new SqlConnectionStringBuilder();
            //connStrBuilder.DataSource = ".";//设置数据源
            //connStrBuilder.InitialCatalog = "TestBase";//数据库名
            //connStrBuilder.UserID = "lyc";//账号
            //connStrBuilder.Password = "123456";//密码
            //connStrBuilder.Pooling = false;//禁用连接池
            //string connStr = connStrBuilder.ConnectionString;

            //读取配置文件里的连接字符串
            //string connStr =   ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;//读取连接字符串
            string connStr = ConfigurationManager.AppSettings["connStr"].ToString();
            conn.ConnectionString = connStr ;
            //连接字符串如果任何一个元素设置不正确，都不能正常打开conn
            conn.Open();


            conn.Close();

        }
    }
}
