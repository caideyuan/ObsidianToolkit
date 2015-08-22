using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ObAuto
{
    public class AutoUtil
    {
        public DataTable GetDatabases(string sql)
        {
            return MsSqlHelper.ExecuteDataTable(sql);
        }

        public DataTable GetTables(string dbName, string sql)
        {
            sql = string.Format(sql, dbName);
            DataTable dt = MsSqlHelper.ExecuteDataTable(sql);
            return dt;
        }

        public DataTable GetFields(string dbName, string tbName, string sql)
        {
            sql = string.Format(sql, tbName, dbName);
            DataTable dt = MsSqlHelper.ExecuteDataTable(sql);
            return dt;
        }
    }
}
