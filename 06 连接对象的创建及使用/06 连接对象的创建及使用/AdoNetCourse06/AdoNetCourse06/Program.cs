using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

using Con = System.Configuration.ConfigurationManager;//命别名
namespace AdoNetCourse06
{
    class Program
    {
        static void Main(string[] args)
        {
            //SqlConnection conn1 = null;
            //try
            //{
            //    string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            //    //创建conn第一种
            //    //SqlConnection conn = new SqlConnection();
            //    //conn.ConnectionString = connStr;
            //    //2.创建conn  推荐使用
            //    conn1 = new SqlConnection(connStr);

            //    Console.WriteLine($"连接对象打开之前 ：");
            //    Console.WriteLine($"Data Source:{conn1.DataSource}");
            //    Console.WriteLine($"DataBase:{conn1.Database}");
            //    Console.WriteLine($"ServerVersion:{conn1.ServerVersion}");
            //    Console.WriteLine($"State:{conn1.State}");
            //    //打开连接，进行与数据库的交互，操作数据、查询
            //    conn1.Open();

            //    Console.WriteLine($"连接对象打开之后 ：");
            //    Console.WriteLine($"Data Source:{conn1.DataSource}");
            //    Console.WriteLine($"DataBase:{conn1.Database}");
            //    Console.WriteLine($"ServerVersion:{conn1.ServerVersion}");
            //    Console.WriteLine($"State:{conn1.State}");

            //    //交互----执行命令，查询、添加、更新、删除 



            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    conn1.Close();  //Dispose()
            //}
            //连接对象离开using区域后，状态变成Closed 关闭
            string connStr = Con.ConnectionStrings["connStr"].ConnectionString;
            SqlConnection conn1 = null;
            //使用using进行释放的对象，它必须继承于IDisposable接口
            using ( conn1=new SqlConnection(connStr))
            {
                Console.WriteLine($"连接对象打开之前 ：");
                Console.WriteLine($"Data Source:{conn1.DataSource}");
                Console.WriteLine($"DataBase:{conn1.Database}");
                //Console.WriteLine($"ServerVersion:{conn1.ServerVersion}");
                Console.WriteLine($"State:{conn1.State}");
                //打开连接，进行与数据库的交互，操作数据、查询
                conn1.Open();

                Console.WriteLine($"连接对象打开之后 ：");
                Console.WriteLine($"Data Source:{conn1.DataSource}");
                Console.WriteLine($"DataBase:{conn1.Database}");
                Console.WriteLine($"ServerVersion:{conn1.ServerVersion}");
                Console.WriteLine($"State:{conn1.State}");
                //conn1.Close();
            }
            Console.WriteLine($"State:{conn1.State}");
            Console.ReadKey();

        }
    }
}
