using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AdoNetCourseSqlDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "select UserId,UserName,Age from UserInfos";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();//必须在执行之前
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);//创建

                //DataTable dt = new DataTable();
                //dt.Load(dr);//将dr对象加载到DataTable

                List<UserInfoModel> list = new List<UserInfoModel>();
                if (dr.HasRows)
                {
                    int indexId = dr.GetOrdinal("UserId");//获取指定列名的列序号
                                                          //string idName = dr.GetName(0);//获取指定列序号的列名
                    int indexName = dr.GetOrdinal("UserName");
                    int indexAge = dr.GetOrdinal("Age");
                    //读一行存一行，dr读一行丢一行 
                    //List集合
                 
                    while (dr.Read())//检测是否有数据
                    {
                        //int userId = (int)dr[0];//列序号 int.Parse(dr[0].ToString())
                        //string userName = dr["UserName"].ToString();//列名读取

                        //int userId = dr.GetInt32(indexId);//int 不用再次进行拆箱操作
                        //string userName = dr.GetString(indexName);
                        //int age = dr.GetInt32(indexAge);

                        UserInfoModel model = new UserInfoModel();
                        model.UserId = dr.GetInt32(indexId);
                        model.UserName = dr.GetString(indexName);
                        model.Age = dr.GetInt32(indexAge);
                        list.Add(model);
                    }

                }
                dr.Close();
            }
        }
    }
}
