using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetCourseSqlParameter
{
    class Program
    {
        static void Main(string[] args)
        {
            //参数的构造方法
            //1.参数
            SqlParameter pra1 = new SqlParameter();
            pra1.ParameterName = "@userName";//参数名
            pra1.SqlDbType = SqlDbType.VarChar;//数据类型
            pra1.Value = "admin";//参数值
            pra1.Size = 20;//大小
            //2.参数名，值
            SqlParameter para2 = new SqlParameter("@Age", 24);
            //3.参数名  SqlDbType
            SqlParameter para3 = new SqlParameter("@DeptId", SqlDbType.Int);
            para3.Size = 4;
            para3.Value = 3;
            //4. 参数名 类型  大小
            SqlParameter para4 = new SqlParameter("@UserPwd", SqlDbType.VarChar, 50);
            para4.Value = "123456";
            //5. 参数名称  类型  大小 源列名（对应DataTable中的列名）
            SqlParameter para5 = new SqlParameter("@UserName", SqlDbType.VarChar, 20, "UName");
            SqlCommand cmd = new SqlCommand();
           

        }
    }
}
