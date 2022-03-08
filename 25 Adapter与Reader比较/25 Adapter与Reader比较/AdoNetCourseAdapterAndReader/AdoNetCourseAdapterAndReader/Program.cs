using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetCourseAdapterAndReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //while(dr.Read())
                //{
                //    //读取与存储
                //}
                //dr.NextResult();//前进到下一个结果集
                //while (dr.Read())
                //{
                //    //下一个结果集读取与存储
                //}
                //dr.NextResult();//前进到下一个结果集
                //while (dr.Read())
                //{
                //    //下一个结果集读取与存储
                //}
                DataSet ds1 = new DataSet();

               //ds1.Load(dr,)//指定多个表里，
                DataTable dt0 = new DataTable();
                //dt0.Load()
                dr.Close();//不然会一直占用连接
            }
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("", conn);
                //da.Fill(dt);
                da.Fill(ds);
            }

        }
    }
}
