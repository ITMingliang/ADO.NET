using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdoNetCourse
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.创建连接
            SqlConnection conn = new SqlConnection();
            //打开门---钥匙，连接字符串---钥匙
            conn.ConnectionString = "server=.;database=TestBase;uid=lyc;pwd=123456";//连接字符串 
               // conn.Database ; 要连接的数据库名称
               //conn.DataSource  //数据源  local  .  Ip,端口号
                //conn.State//连接的状态
               //conn.ConnectionTimeout  15s  
            //2.打开连接
            conn.Open();//打开连接
            //3.创建执行命令对象
            conn.CreateCommand();//创建一个与conn关联的SqlCommand对象

            //4.执行命令


            //5.关闭连接
            conn.Close();//关闭连接


            //conn.Open();
            //conn.Dispose();//释放连接
            //conn.Open();
            //差别：Close() 后还可以再打开，连接字符串还有
            //Dispose() 后，连接字符串没有，为空，重新设置连接字符串
        }
    }
}
