using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AdoNetCourseDataTable
{
    class Program
    {
        static void Main(string[] args)
        {
            //独立创建与使用
            //1.
            //DataTable dt = new DataTable();
            //dt.TableName = "UserInfo";

            //2.
            DataTable dt1 = new DataTable("UserInfo");
            //表是空的,没有架构  列 约束 主键
            DataColumn dc = new DataColumn();
            dc.ColumnName = "UserId";
            dc.DataType = typeof(int);//Type.GetType("System.Int32")
            dt1.Columns.Add(dc);//添加一列
            dt1.Columns.Add("UserName", typeof(string));//添加一列   推荐
            dt1.Columns.Add("Age", typeof(int));
            dt1.PrimaryKey = new DataColumn[] { dt1.Columns[0] };//设置主键
            dt1.Constraints.Add(new UniqueConstraint(dt1.Columns[1]));//添加唯一约束
                                                                      //架构定义好了,添加数据
            DataRow dr = dt1.NewRow();//具有相同的架构
            dr[0] = 1;
            dr["UserName"] = "admin";
            dr["Age"] = 24;   //Detached
            //这条数据并没有添加到dt1表中
            dt1.Rows.Add(dr);//添加到dt1里 Added
            //dt1.RejectChanges();//回滚
            dt1.AcceptChanges();//提交更改   UnChanged

            dr["Age"] = 27;  //修改   Modified  已修改

            dt1.AcceptChanges();//UnChanged

            //dr.Delete();//Deleted 已删除

            //dt1.AcceptChanges();//Detached 
            //dt1.Rows.Remove(dr);//Detached  
            //dt1.Rows.Remove(dr)相当于  dr.Delete();dt1.AcceptChanges();
            //DataRow   ---RowState :Detached  Added  UnChanged  Modified Deleted Detached

            //dt1.Clear();//清除数据

            DataTable dt2 = dt1.Copy();//复制架构和数据
            DataTable dt3 = dt1.Clone();//只复制架构,不包含数据
            DataRow dr1 = dt2.NewRow();//具有相同的架构
            dr1[0] = 2;
            dr1["UserName"] = "lyc";
            dr1["Age"] = 26;  
          
            dt2.Rows.Add(dr1);
            dt1.Merge(dt2); //合并

            DataRow[] rows = dt1.Select();//获取所有的行
            DataRow[] rows1 = dt1.Select("UserId>1","UserId desc");//按条件筛选,排序
        }
    }
}
