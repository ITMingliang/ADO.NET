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
           
            SqlConnection conn = new SqlConnection(connStr);

            //创建方式
            {
                //1.设置SelectCommand
                //SqlDataAdapter adapter = new SqlDataAdapter();
                //adapter.SelectCommand = new SqlCommand(sql, conn);
                //2.通过一个SqlCommand对象来实例化一个adapter
                //SqlCommand cmd = new SqlCommand(sql, conn);
                //SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                //3.查询语句和连接对象来实例化一个adapter
                //SqlDataAdapter adapter2 = new SqlDataAdapter(sql, conn);
                //4.查询语句 和 连接字符串，也可以构建一个adapter? 可以的
                //SqlDataAdapter adapter3 = new SqlDataAdapter(sql, connStr);

                //如果是T-SQL查询语句，选择第三种方式
                //带参数，添加参数，SqlCommand  选择第二种或第一种
                //不推荐大家使用第4种
            }
            //填充数据 DataSet
            {
                //string sql = "select * from UserInfos;select * from DeptInfos";
                //SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                //DataSet ds = new DataSet();
                //表名映射要在填充之前进行设置
                //da.TableMappings.Add("Table", "Users"); //设置ds 中DataTable的表名映射
                //da.TableMappings.Add("Table1", "Dept");
                // da.Fill(ds);
                //da.Fill(ds,"User");


                //多个结果集，表名 Table Table1....Tablen
            }
            //填充数据 DataTable 针对单个结果集

            {
                string sql0 = "select * from UserInfos";
                SqlDataAdapter da = new SqlDataAdapter(sql0, conn);
                DataTable dt = new DataTable("User");
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            //真的没有去使用conn？--错
            //没有显式打开关闭conn----  Fill前后 conn 状态都是 Closed
            //显式打开关闭conn----Fill前后 conn 状态都 Open
            //da保证conn执行Fill，conn状态恢复到Fil之前
            Console.ReadKey();
        }
    }
}
