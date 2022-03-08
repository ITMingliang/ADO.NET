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



            Console.ReadKey();
        }
    }
}
