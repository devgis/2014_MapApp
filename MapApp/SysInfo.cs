using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace MapApp
{
    public static class SysInfo
    {
        static SysInfo()
        {
            _TypeDataTable = new DataTable();
            _TypeDataTable.Columns.Add("JKTypeName", typeof(String));
            _TypeDataTable.Columns.Add("JKType", typeof(Int32));
            _TypeDataTable.Columns.Add("BMPFileName", typeof(String));

            DataRow dr1 = _TypeDataTable.NewRow();
            dr1["JKTypeName"] = "类型1";
            dr1["JKType"] = "1";
            dr1["BMPFileName"] = "HOUS1-32.BMP";
            _TypeDataTable.Rows.Add(dr1);

            DataRow dr2 = _TypeDataTable.NewRow();
            dr2["JKTypeName"] = "类型2";
            dr2["JKType"] = "2";
            dr2["BMPFileName"] = "LITE2-32.BMP";
            _TypeDataTable.Rows.Add(dr2);

            DataRow dr3 = _TypeDataTable.NewRow();
            dr3["JKTypeName"] = "类型3";
            dr3["JKType"] = "3";
            dr3["BMPFileName"] = "MOSQ1-32.BMP";
            _TypeDataTable.Rows.Add(dr3); 
        }

        /// <summary>
        /// 保存系统信息
        /// </summary>
        private static Hashtable _HTDBInfo;//
        public static Hashtable HTDBInfo
        {
            get
            {
                return _HTDBInfo;
            }
            set
            {
                _HTDBInfo = value;
            }
        }

        private static DataTable _TypeDataTable;
        public static DataTable TypeDataTable
        {
            get
            {
                return _TypeDataTable;
            }
        }

    }
}
