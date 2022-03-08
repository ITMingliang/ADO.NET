using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AdoNetCourseSqlCommand
{
    class Program
    {
       private static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        static void Main(string[] args)
        {

            //TestExecuteNonQuery();

            //TestExecuteScalar();

            //TestExecuteReader();

            TestExecuteReaderNoUsing();

            Console.ReadKey();
        }

        //cmd.ExecuteReader() 查询 返回一个对象：SqlDataReader 
        //SqlDataReader 数据流 实时读取  游标 指针  固定--不灵活：只进不出，只能前进，不能后退  只读
        //适用：只是读取数据，不做修改的情况下   数据量比较小
        private static void TestExecuteReader()
        {
            SqlDataReader dr = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //string sql = "select * from UserInfos";
                //统计UserInfos表有多个条记录
                // string sql = "select count(1) from UserInfos where Age>25";
                string sql = "select UserId,UserName,Age from UserInfos";
                //创建执行命令的对象 SqlCommand
                SqlCommand cmd = new SqlCommand(sql, conn);
                //执行命令
                //1.执行T-SQL语句或存储过程，并返回返回查询结果集中的第一行第一列的值，忽略其他行或列。
                //适用：作查询，返回一个值   记录数  数据运算而出的结果 
                //命令类型：查询---- DQL 数据查询语言 
                //共有的条件：conn状态 必须是Open
                //连接使用原则：最晚打开，最早关闭

                conn.Open();
                //dr读取数据整个过程，conn必须保持Open
                 dr = cmd.ExecuteReader();
                //dr读取数据过程中，要即时保存，读一条丢一条
                while (dr.Read())//是否可以前进到一条记录
                {
                    int userId = int.Parse(dr["UserId"].ToString());
                    string userName = dr["UserName"].ToString();
                    int age = int.Parse(dr["Age"].ToString());
                    Console.WriteLine($"UserId:{userId},UserName:{userName},Age:{age}");
                }
                dr.Close();
                //conn.Close();

            }
            //这里来读取，是无法读取数据
            //while (dr.Read())//是否可以前进到一条记录
            //{
            //    int userId = int.Parse(dr["UserId"].ToString());
            //    string userName = dr["UserName"].ToString();
            //    int age = int.Parse(dr["Age"].ToString());
            //    Console.WriteLine($"UserId:{userId},UserName:{userName},Age:{age}");
            //}
            //dr.Close();
            //conn.Close();

        }

        private static void TestExecuteReaderNoUsing()
        {
            SqlDataReader dr = null;
            SqlConnection conn = new SqlConnection(connStr);
            string sql = "select UserId,UserName,Age from UserInfos";
            //创建执行命令的对象 SqlCommand
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                //dr读取数据整个过程，conn必须保持Open
                //dr = cmd.ExecuteReader();//关闭dr，并没有释放conn
                //不管是关闭dr，还是conn，它们都会一致关闭
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch(Exception ex)
            {
                Console.WriteLine("执行查询异常");
                conn.Close();
            }
            //这里来读取，是无法读取数据
            while (dr.Read())//是否可以前进到一条记录
            {
                int userId = int.Parse(dr["UserId"].ToString());
                string userName = dr["UserName"].ToString();
                int age = int.Parse(dr["Age"].ToString());
                Console.WriteLine($"UserId:{userId},UserName:{userName},Age:{age}");
            }
            //dr.Close();
            
            conn.Close();
        }

        //cmd.ExecuteScalar() 查询 DQL  返回一个值 object 
        private static  void  TestExecuteScalar()
        {
            object o = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //string sql = "select * from UserInfos";
                //统计UserInfos表有多个条记录
                // string sql = "select count(1) from UserInfos where Age>25";
                string sql = "insert into DeptInfos(DeptName) values ('采购部');select @@identity";
                //创建执行命令的对象 SqlCommand
                SqlCommand cmd = new SqlCommand(sql, conn);
                //执行命令
                //1.执行T-SQL语句或存储过程，并返回返回查询结果集中的第一行第一列的值，忽略其他行或列。
                //适用：作查询，返回一个值   记录数  数据运算而出的结果 
                //命令类型：查询---- DQL 数据查询语言 
                //共有的条件：conn状态 必须是Open
                //连接使用原则：最晚打开，最早关闭

                conn.Open();

                o = cmd.ExecuteScalar();

                conn.Close();

            }

            if(o!=null)
                Console.WriteLine("返回值："+o.ToString());
        }

        //执行cmd.ExecuteNonQuery 增删改 
        private static void TestExecuteNonQuery()
        {
            int count = 0;
            //conn默认状态是Closed

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string uName = "jason";
                string uPwd = "1234";
                int age = 25;
                int deptId = 3;
                //创建命令  T-SQL 存储过程
                //拼接式SQL  致命弱点:很容易被SQL注入
                //string sql = "insert into UserInfos (UserName,UserPwd,Age,DeptId) values ('"+uName+"','"+uPwd+"',"+age+","+deptId+")";
                string delSql = "delete from UserInfos where UserId=3";
                //创建执行命令的对象 SqlCommand
                SqlCommand cmd = new SqlCommand(delSql, conn);
                //执行命令
                //1.执行T-SQL语句或存储过程，并返回受影响的行数
                //命令类型：插入、更新、删除---- DML 
                //共有的条件：conn状态 必须是Open
                //连接使用原则：最晚打开，最早关闭

                conn.Open();

                count = cmd.ExecuteNonQuery();

                conn.Close();

            }
            if (count > 0)
            {
                // Console.WriteLine("用户信息添加 成功！");
                Console.WriteLine("用户信息删除 成功！");
            }
        }
    }
}
