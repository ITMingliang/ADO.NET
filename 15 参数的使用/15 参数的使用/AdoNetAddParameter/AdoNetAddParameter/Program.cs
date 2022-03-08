using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AdoNetAddParameter
{
    class Program
    {
        static void Main(string[] args)
        {

            //TestAddPara();

           

            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            //返回值参数
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("GetUserAge", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //输入输出参数 既传入值，也输出值

                SqlParameter[] paras = {
                    new SqlParameter("@UserId", 31),
                    new SqlParameter("@reValue",SqlDbType.Int,4)
            };
                paras[1].Direction = ParameterDirection.ReturnValue;//返回值参数
                cmd.Parameters.AddRange(paras);

                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();

                Console.WriteLine(paras[1].Value.ToString());
            }


            Console.ReadKey();
        }

        //输入输出参数---双向参数
        static void TestInputOutPutPara(string connStr)
        { 
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("GetDeptNameNew", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //输入输出参数 既传入值，也输出值
                SqlParameter paraName = new SqlParameter("@DeptName", SqlDbType.NVarChar, 50);
                paraName.Value = "发";
                paraName.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(paraName); //添加单个参数

                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();

                Console.WriteLine(paraName.Value.ToString());
            }
        }

        //输出参数的使用
        static void TestOutPutPara(string connStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {

                SqlCommand cmd = new SqlCommand("GetDeptName", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //参数化Sql语句或一般存储过程里使用的是输入参数

                SqlParameter paraId = new SqlParameter("@DeptId", 2);
                cmd.Parameters.Add(paraId); //添加单个参数
                //输出参数 是不传入值，它是只输出
                SqlParameter paraName = new SqlParameter("@DeptName", SqlDbType.NVarChar, 50);
                paraName.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paraName); //添加单个参数

                conn.Open();
                cmd.ExecuteScalar();
                conn.Close();

                Console.WriteLine(paraName.Value.ToString());
            }
        }

        static void TestAddPara()
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //int userId =int.Parse( Console.ReadLine());
                //SQL注入  很恐怖  ---解决这个问题？----
                //''单引号  转义 又怎么解决？
                //参数  
                //string sql = "select UserName,UserPwd from UserInfos where UserId="+userId+"";
                //string uName = "' or 1=1  --";
                string sql = "select * from UserInfos where UserName=@userName";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //参数化Sql语句或一般存储过程里使用的是输入参数
                //cmd.Parameters.Add(new SqlParameter("@userName", "ling'ping"));

                //SqlParameter paraName = new SqlParameter("@userName", "ling'ping");
                //cmd.Parameters.Add(paraName); //添加单个参数

                // cmd.Parameters.Add("@userName", "ling'ping");//过时了

                cmd.Parameters.AddWithValue("@userName", "ling'ping");//单个参数添加，推荐使用这种

                //SqlParameter pra = cmd.Parameters.Add("@userName", SqlDbType.VarChar, 20);
                //pra.Value = "ling'ping";

                //添加多个参数时使用
                //SqlParameter[] paras = {
                //    new SqlParameter("@userName", "ling'ping")
                //                                                      };
                //cmd.Parameters.AddRange(paras);
                conn.Open();
                object o = cmd.ExecuteScalar();
                conn.Close();
                Console.WriteLine(o.ToString());
            }
        }
    }
}
