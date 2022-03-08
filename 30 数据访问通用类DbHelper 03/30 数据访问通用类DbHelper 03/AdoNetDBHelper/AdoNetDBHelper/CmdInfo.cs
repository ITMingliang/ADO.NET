using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDBHelper
{
    //封装事务中每个操作类：包括命令名称，类型，参数
    public class CmdInfo
    {
        public string CommandText;//sql或存储过程名
        public SqlParameter[] Parameters; //参数列表
        public int CmdType; //是存储过程还是T-SQL语句
        public CmdInfo()
        {
        }

        public CmdInfo(string comText, int cmdType)
        {
            this.CommandText = comText;
            this.CmdType = cmdType;
        }

        public CmdInfo(string sqlText, int cmdType, SqlParameter[] paras)
        {
            this.CommandText = sqlText;
            this.Parameters = paras;
            this.CmdType = cmdType;
        }
    }
}
