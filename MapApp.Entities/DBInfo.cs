using System;
using System.Collections.Generic;
using System.Text;

namespace MapApp.Entities
{

    public class DBInfo
    {
        private String _Name;
        private String _MWSPath;
        private String _MDBPath;

        /// <summary>
        /// 名称
        /// </summary>
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        /// <summary>
        /// MWS文件路径
        /// </summary>
        public String MWSPath
        {
            get
            {
                return _MWSPath;
            }
            set
            {
                _MWSPath = value;
            }
        }
        /// <summary>
        /// MDB数据库路径
        /// </summary>
        public String MDBPath
        {
            get
            {
                return _MDBPath;
            }
            set
            {
                _MDBPath = value;
            }
        }
    }
}
