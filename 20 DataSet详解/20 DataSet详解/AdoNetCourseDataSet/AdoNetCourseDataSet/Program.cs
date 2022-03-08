using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AdoNetCourseDataSet
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建
            //1.
            //DataSet ds = new DataSet();
            // ds.DataSetName = "ds1"; //默认名称 NewDataSet
            //2.
            DataSet ds = new DataSet("ds1");

            //常用属性
            //DataSetName  ds名称
            DataTable dt1 = new DataTable();
            //Tables DataTable集合
            ds.Tables.Add(dt1);//添加dt1到ds中
            DataTable dt = ds.Tables[0];//获取表
            //ds.Relations.Add() 添加DataRelation 到ds


            //常用方法：
            ds.AcceptChanges();//提交
            ds.RejectChanges();//回滚
            ds.Clear();//清除所有表中所有行的数据
            ds.Copy();//复制结构和数据
            ds.Clone();//复制架构，不包含数据
            //ds.Merge(rows/DataTable/dataset); 合并
            ds.Reset();//
            //ds.Load(IDataReader);


            Console.ReadKey();

        }
    }
}
