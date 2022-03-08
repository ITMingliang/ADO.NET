using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace AdoNetCourse08
{
    class Program
    {
        static void Main(string[] args)
        {
            //string connStr = "server=.;database=TestBase;uid=lyc;pwd=123456;Max Pool Size=5;";
            //for (int i = 0; i < 10; i++)
            //{
            //    SqlConnection conn = new SqlConnection(connStr);
            //    conn.Open();
            //    Console.WriteLine($"第{i+1}个连接已打开！");
            //}
            //1.证明：Ado.Net默认是启用连接池的。


            //string connStr = "server=.;database=TestBase;uid=lyc;pwd=123456;Max Pool Size=5;Pooling=false";
            //for (int i = 0; i < 10; i++)
            //{
            //    SqlConnection conn = new SqlConnection(connStr);
            //    conn.Open();
            //    Console.WriteLine($"第{i + 1}个连接已打开！");
            //}
            //2.Pooling=false禁止启用连接池 Max Pool Size=5是无效的


            //string connStr = "server=.;database=TestBase;uid=lyc;pwd=123456;Pooling=false";
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //for (int i = 0; i < 100; i++)
            //{
            //    SqlConnection conn = new SqlConnection(connStr);
            //    conn.Open();
            //   // Console.WriteLine($"第{i + 1}个连接已打开！");
            //    conn.Close();
            //}
            //sw.Stop();
            //Console.WriteLine($"不启用连接池，耗时：{sw.ElapsedMilliseconds} ms！"); //609 ms

            //string connStr1 = "server=.;database=TestBase;uid=lyc;pwd=123456;";
            //Stopwatch sw1 = new Stopwatch();
            //sw1.Start();

            //for (int i = 0; i < 100; i++)
            //{
            //    SqlConnection conn = new SqlConnection(connStr1);
            //    conn.Open();
            //    // Console.WriteLine($"第{i + 1}个连接已打开！");
            //    conn.Close();
            //}
            //sw1.Stop();
            //Console.WriteLine($"启用连接池，耗时：{sw1.ElapsedMilliseconds} ms！");  //4 ms

            //3.测试启用与不启用连接池，性能差，连接池耗时很少


            string connStr1 = "server=.;database=TestBase;uid=lyc;pwd=123456;Max Pool Size=5";

            string connStr2 = "server=.;database=TestBase; uid=lyc;pwd=123456;Max Pool Size=5";

            string connStr3 = "server=.;database=TestBase;uid=lyc;pwd=123456;Max Pool Size=5";
            //会产生几个连接池
            for (int i = 0; i < 5; i++)
            {
                SqlConnection conn1 = new SqlConnection(connStr1);
                conn1.Open();
                Console.WriteLine($"conn1 第{i + 1}个连接已打开！");
                SqlConnection conn2 = new SqlConnection(connStr2);
                conn2.Open();
                Console.WriteLine($"conn2 第{i + 1}个连接已打开！");
                SqlConnection conn3 = new SqlConnection(connStr3);
                conn3.Open();
                Console.WriteLine($"conn3 第{i + 1}个连接已打开！");
            }
            //connStr1与connStr3一样的，所以它们共用一个连接池  connStr2会单独创建一个连接池    2个连接池 
            //连接字符串区分连接池类型
            Console.ReadKey();
        }
    }
}
