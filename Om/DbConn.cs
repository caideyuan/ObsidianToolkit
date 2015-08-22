using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto.Om
{
    /// <summary>
    /// 数据库连接字符串（SqliteDB）
    /// </summary>
    public class DbConn
    {
        private long connID;

        public long ConnID
        {
            get { return connID; }
            set { connID = value; }
        }
        private string connName;

        public string ConnName
        {
            get { return connName; }
            set { connName = value; }
        }
        //private string connString;

        //public string ConnString
        //{
        //    get { return connString; }
        //    set { connString = value; }
        //}
        private int connType = 0;

        public int ConnType
        {
            get { return connType; }
            set { connType = value; }
        }
        private int sort = 0;

        public int Sort
        {
            get { return sort; }
            set { sort = value; }
        }
        private bool status;

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        private string dataSource;

        public string DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }
        private string catalog;

        public string Catalog
        {
            get { return catalog; }
            set { catalog = value; }
        }
        private string userId;

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
