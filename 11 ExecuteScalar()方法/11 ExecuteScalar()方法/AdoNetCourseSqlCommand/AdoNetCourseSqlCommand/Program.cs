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

            TestExecuteScalar();

            Console.ReadKey();
        }

        //cmd.ExecuteScalar()
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

        //执行cmd.ExecuteNonQuery
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
