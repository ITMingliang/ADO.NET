using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AdoNetCourseDataRelation
{
    class Program
    {
        static void Main(string[] args)
        {
            //关系：一对一  一个表列数太多，拆分成两张表
            //      一对多  父----子  一条对多条
            //       多对多  中间表建立  权限分配

            DataSet ds = new DataSet("ds");
            DataTable dt1 = new DataTable("User");//子表
            DataTable dt2 = new DataTable("Dept");//父表
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            dt1.Columns.Add("UserId", typeof(int));
            dt1.Columns.Add("UserName", typeof(string));
            dt1.Columns.Add("Age", typeof(int));
            dt1.Columns.Add("DeptId", typeof(int));

            dt2.Columns.Add("DeptId", typeof(int));
            dt2.Columns.Add("DeptName", typeof(string));

            //dt1.PrimaryKey = new DataColumn[] { dt1.Columns[0] };//主键 --主键约束
            //dt2.Constraints.Add(new UniqueConstraint("uc", dt2.Columns[1]));//添加一个唯一约束
            //dt1.Constraints.Add(new ForeignKeyConstraint("fk",dt2.Columns[0], dt1.Columns[3]));//外键约束 
            //默认情况：建立关系，就自动为父表中列建立唯一约束，子表中外键列建立一个外键约束
            //建立关系
            DataRelation relation = new DataRelation("relation", dt2.Columns[0], dt1.Columns[3],true);
            ds.Relations.Add(relation);//添加到ds.Relations中
            InitData(dt1, dt2);//准备一些数据

            //使用关系
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    DataRow[] rows = dr.GetChildRows(relation);
            //    foreach(DataRow r in rows)
            //    {
            //        Console.WriteLine($"UserId:{r[0].ToString()},UserName:{r[1].ToString()},Age:{r[2].ToString()},DeptId:{r[3].ToString()}");
            //    }
            //}

            //通过父表读取子表中的数据
            //DataRow[] rows = dt2.Rows[2].GetChildRows(relation);
            //foreach (DataRow r in rows)
            //{
            //    Console.WriteLine($"UserId:{r[0].ToString()},UserName:{r[1].ToString()},Age:{r[2].ToString()},DeptId:{r[3].ToString()}");
            //}

            // 子表来读取父表中的数据？---可以的
            DataRow row = dt1.Rows[1].GetParentRow(relation);
            Console.WriteLine($"DepId:{row[0].ToString()},DeptName:{row[1].ToString()}");

            Console.ReadKey();

        }
        //初始化数据
        static void InitData(DataTable dt1,DataTable dt2)
        {
            DataRow dr2 = dt2.NewRow();
            dr2["DeptId"] = 1;
            dr2["DeptName"] = "人事部";
            dt2.Rows.Add(dr2);

            dr2 = dt2.NewRow();
            dr2["DeptId"] = 2;
            dr2["DeptName"] = "管理部";
            dt2.Rows.Add(dr2);

            dr2 = dt2.NewRow();
            dr2["DeptId"] = 3;
            dr2["DeptName"] = "销售部";
            dt2.Rows.Add(dr2);

            DataRow dr1 = dt1.NewRow();
            dr1["UserId"] = 1;
            dr1["UserName"] = "李明";
            dr1["Age"] = 22;
            dr1["DeptId"] = 3;
            dt1.Rows.Add(dr1);

            dr1 = dt1.NewRow();
            dr1["UserId"] = 2;
            dr1["UserName"] = "刘丽";
            dr1["Age"] = 24;
            dr1["DeptId"] = 1;
            dt1.Rows.Add(dr1);

            dr1 = dt1.NewRow();
            dr1["UserId"] = 3;
            dr1["UserName"] = "王力";
            dr1["Age"] = 23;
            dr1["DeptId"] = 3;
            dt1.Rows.Add(dr1);
           

        }
    }
}
