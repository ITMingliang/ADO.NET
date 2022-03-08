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
    public class DBHelper
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
        public static int ExecuteNonQuery(string sql, int cmdType, params SqlParameter[] paras)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                //SqlCommand cmd = new SqlCommand(sql, conn);
                //if (cmdType == 2)
                //    cmd.CommandType = CommandType.StoredProcedure;
                //if (paras != null && paras.Length > 0)
                //    cmd.Parameters.AddRange(paras);
                //conn.Open();
                SqlCommand cmd = BuildCommand(conn, sql, cmdType, null, paras);
                count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
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
            object o = null;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                //SqlCommand cmd = new SqlCommand(sql, conn);
                //if (cmdType == 2)
                //    cmd.CommandType = CommandType.StoredProcedure;
                //if (paras != null && paras.Length > 0)
                //    cmd.Parameters.AddRange(paras);
                //conn.Open();
                SqlCommand cmd = BuildCommand(conn, sql, cmdType, null, paras);
                o = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
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
            //SqlCommand cmd = new SqlCommand(sql, conn);
            //if (cmdType == 2)
            //    cmd.CommandType = CommandType.StoredProcedure;
            //if (paras != null && paras.Length > 0)
            //    cmd.Parameters.AddRange(paras);
            SqlCommand cmd = BuildCommand(conn, sql, cmdType, null, paras);
            try
            {
                //conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询异常", ex);
            }

            return dr;
        }

        /// <summary>
        /// 填充DataSet  一个或多个结果集 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, int cmdType, params SqlParameter[] paras)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                // SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                //SqlCommand cmd = new SqlCommand(sql, conn);
                //if (cmdType == 2)
                //    cmd.CommandType = CommandType.StoredProcedure;
                //if (paras != null && paras.Length > 0)
                //    cmd.Parameters.AddRange(paras);
                SqlCommand cmd = BuildCommand(conn, sql, cmdType, null, paras);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(ds);
                conn.Close();
            }
            return ds;
        }

        /// <summary>
        /// 填充DataTable  一个结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, int cmdType, params SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                // SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                //SqlCommand cmd = new SqlCommand(sql, conn);
                //if (cmdType == 2)
                //    cmd.CommandType = CommandType.StoredProcedure;
                //if (paras != null && paras.Length > 0)
                //    cmd.Parameters.AddRange(paras);
                SqlCommand cmd = BuildCommand(conn, sql, cmdType, null, paras);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 事务  一系列SQL语句    操作：Insert update delete
        /// </summary>
        /// <param name="listSQL"></param>
        /// <returns></returns>
        public static bool ExecuteTrans(List<string> listSQL)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                //SqlCommand cmd = conn.CreateCommand();
                //cmd.Transaction = trans;
                SqlCommand cmd = BuildCommand(conn, "", 1, trans);

                try
                {
                    for (int i = 0; i < listSQL.Count; i++)
                    {
                        cmd.CommandText = listSQL[i];
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();//提交
                    return true;
                }
                catch (SqlException ex)
                {
                    trans.Rollback();//回滚
                    throw new Exception("执行事务出现异常", ex);
                }
                finally
                {
                    trans.Dispose();
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 执行事务   每个操作封装到CmdInfo中
        /// </summary>
        /// <param name="listCmd"></param>
        /// <returns></returns>

        public static bool ExecuteTrans(List<CmdInfo> listCmd)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                //SqlCommand cmd = conn.CreateCommand();
                //cmd.Transaction = trans;
                SqlCommand cmd = BuildCommand(conn, "", 1, trans);
                try
                {
                    for (int i = 0; i < listCmd.Count; i++)
                    {
                        cmd.CommandText = listCmd[i].CommandText;
                        if (listCmd[i].CmdType == 2)
                            cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        if(listCmd[i].Parameters!=null && listCmd[i].Parameters.Length >0)
                            cmd.Parameters.AddRange(listCmd[i].Parameters);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();//很重要
                    }
                    trans.Commit();//提交
                    return true;
                }
                catch (SqlException ex)
                {
                    trans.Rollback();//回滚
                    throw new Exception("执行事务出现异常", ex);
                }
                finally
                {
                    trans.Dispose();
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 构造SqlCommand
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="trans"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static SqlCommand BuildCommand(SqlConnection conn,string sql,int cmdType,SqlTransaction trans,params SqlParameter[] paras)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (cmdType == 2)
                cmd.CommandType = CommandType.StoredProcedure;
            if (paras != null && paras.Length > 0)
                cmd.Parameters.AddRange(paras);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            if (trans != null)
                cmd.Transaction = trans;

            return cmd;
        }




    }
}
