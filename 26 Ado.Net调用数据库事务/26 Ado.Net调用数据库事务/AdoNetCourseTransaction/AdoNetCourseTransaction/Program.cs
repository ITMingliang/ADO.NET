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
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //调用存储过程 
                SqlCommand cmd = new SqlCommand("AddUserByTran", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                SqlParameter[] paras =
                {
                    new SqlParameter("@UserName","Lilye"),
                    new SqlParameter("@UserPwd","12345"),
                    new SqlParameter("@Age",32),
                    new SqlParameter("@DeptName","采购部"),
                    new SqlParameter("@reValue",SqlDbType.Int,4)
                };
                paras[4].Direction = ParameterDirection.ReturnValue;//返回值参数
                cmd.Parameters.AddRange(paras);
                //执行
                conn.Open();
                cmd.ExecuteNonQuery();
                int state = int.Parse(paras[4].Value.ToString());
                if(state==1)
                    Console.WriteLine("用户信息添加成功！");
                else
                    Console.WriteLine("用户信息添加失败！");
            }


            Console.ReadKey();
        }
    }
}