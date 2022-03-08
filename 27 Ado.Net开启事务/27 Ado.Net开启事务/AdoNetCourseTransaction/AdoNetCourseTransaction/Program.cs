using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AdoNetCourseTransaction
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            //Ado.Net调用数据库事务
            //using (SqlConnection conn = new SqlConnection(connStr))
            //{
            //    //调用存储过程 
            //    SqlCommand cmd = new SqlCommand("AddUserByTran", conn);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Clear();
            //    SqlParameter[] paras =
            //    {
            //        new SqlParameter("@UserName","Lilye"),
            //        new SqlParameter("@UserPwd","12345"),
            //        new SqlParameter("@Age",32),
            //        new SqlParameter("@DeptName","采购部"),
            //        new SqlParameter("@reValue",SqlDbType.Int,4)
            //    };
            //    paras[4].Direction = ParameterDirection.ReturnValue;//返回值参数
            //    cmd.Parameters.AddRange(paras);
            //    //执行
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    int state = int.Parse(paras[4].Value.ToString());
            //    if(state==1)
            //        Console.WriteLine("用户信息添加成功！");
            //    else
            //        Console.WriteLine("用户信息添加失败！");
            //}


            //Ado.Net SqlTransaction开启事务

            //Ado.Net中一切操作都是由SqlCommand来完成的  
            //事务中，一般执行的都增删改操作

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlTransaction tran = null;
                try
                {
                    //条件：连接打开 
                    conn.Open();

                    //IsolationLevel.ReadCommitted  默认的隔离级别
                    tran = conn.BeginTransaction();//开启一个事务
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = tran;//设置要执行的事务

                    //定义要执行的操作
                    cmd.CommandText = "insert into DeptInfos (DeptName) values(@DeptName);select @@identity";
                    cmd.Parameters.Add(new SqlParameter("@DeptName", "采购部2"));
                    object oId = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();//即时清空参数集合
                    int deptId = 0;
                    if (oId != null)
                        deptId = int.Parse(oId.ToString());
                    cmd.CommandText = "insert into UserInfos(UserName,UserPwd,Age,DeptId) values(@UserName, @UserPwd, @Age, @deptId)";
                    SqlParameter[] paras =
                    {
                    new SqlParameter("@UserName", "lycchun"),
                    new SqlParameter("@UserPwd", "12345"),
                    new SqlParameter("@Age", 33),
                    new SqlParameter("@deptId", deptId)
                };
                    cmd.Parameters.AddRange(paras);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    //下一步操作
                    tran.Commit();//事务的提交 
                    Console.WriteLine("用户添加成功！");
                }
                catch(SqlException ex)
                {
                    tran.Rollback();//回滚
                }
                finally
                {
                    tran.Dispose();
                    conn.Close();
                }
            }


                Console.ReadKey();
        }
    }
}