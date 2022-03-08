using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AdoNetCourse09
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    //创建命令，执行命令的对象  执行命令
                    //命令 ---T-SQL或存储过程 ---数据库里已创建好
                    //SqlCommand 对SQLServer数据库执行的一个T-SQL语句或存储过程。
                    //SqlCommand对象：Ado.Net中执行数据库命令的对象。
                    //SqlCommand创建
                    {
                        string sql = "select* from UserInfos";
                        //1.
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = sql;
                        // cmd.CommandType = CommandType.Text; 没有必要的
                        //cmd.CommandType = CommandType.StoredProcedure;//如果是存储过程，必须设置
                        //2.
                        SqlCommand cmd1 = new SqlCommand(sql);
                        cmd1.Connection = conn;
                        //3. sql语句  连接对象  推荐的
                        SqlCommand cmd2 = new SqlCommand(sql, conn);

                        //4. Connection对象
                        SqlCommand cmd3 = conn.CreateCommand();
                        cmd3.CommandText = sql;
                        //5. 
                        string delSql = "delete from UserInfos where UserId>3";
                        //SqlCommand cmd4 = new SqlCommand(delSql, conn, null);

                    }


                    //conn.Close();
                }
            }
            catch(SqlException ex)
            {

            }
            
            
               

        }
    }
}
