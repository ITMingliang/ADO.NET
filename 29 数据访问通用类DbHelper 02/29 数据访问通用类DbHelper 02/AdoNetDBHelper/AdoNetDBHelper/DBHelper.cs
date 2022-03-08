using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AdoNetDBHelper
{
    public  class DBHelper
    {
        //连接字符串
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        SqlConnection conn = null;

        //private SqlConnection GetConn()
        //{
        //    conn = new SqlConnection(ConnStr);
        //    return conn;
        //}

        /// <summary>
        /// 执行T-SQL ，返回受影响的行数  Insert Update Delete
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql,int cmdType,params SqlParameter[] paras)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmdType == 2)
                    cmd.CommandType = CommandType.StoredProcedure;
                if(paras!=null && paras.Length>0)
                   cmd.Parameters.AddRange(paras);
                conn.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return count;
        }

        /// <summary>
        ///  执行查询，返回结果集第一行第一列的值，忽略其他行或列 object
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, int cmdType, params SqlParameter[] paras)
        {
            object  o = null;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (cmdType == 2)
                    cmd.CommandType = CommandType.StoredProcedure;
                if (paras != null && paras.Length > 0)
                    cmd.Parameters.AddRange(paras);
                conn.Open();
                o = cmd.ExecuteScalar();
                conn.Close();
            }
            return o;
        }

        /// <summary>
        /// 执行查询，生成SqlDataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, int cmdType, params SqlParameter[] paras)
        {
            SqlDataReader dr = null;
            SqlConnection conn = new SqlConnection(ConnStr);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (cmdType == 2)
                cmd.CommandType = CommandType.StoredProcedure;
            if (paras != null && paras.Length > 0)
                cmd.Parameters.AddRange(paras);
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch(SqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询异常", ex);
            }
            
            return dr;
        }

    }
}
