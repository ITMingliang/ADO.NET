using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AdoNetCourseAdapter
{
    class Program
    {
       

        static void Main(string[] args)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            string sql = "select * from UserInfos";
            SqlConnection conn = new SqlConnection(connStr);
            //1.设置SelectCommand
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, conn);
            //2.通过一个SqlCommand对象来实例化一个adapter
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            //3.查询语句和连接对象来实例化一个adapter
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql, conn);
            //4.查询语句 和 连接字符串，也可以构建一个adapter? 可以的
            SqlDataAdapter adapter3 = new SqlDataAdapter(sql, connStr);

            //如果是T-SQL查询语句，选择第三种方式
            //带参数，添加参数，SqlCommand  选择第二种或第一种
            //不推荐大家使用第4种


            Console.ReadKey();
        }
    }
}
