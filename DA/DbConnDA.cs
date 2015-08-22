using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using ObAuto.Om;

namespace ObAuto.DA
{
    public class DbConnDA
    {

        #region SELECT
        internal DbConn SelectByName(string connName)
        {
            return SelectByName(connName, 1);
        }
        internal DbConn SelectByName(string connName, int status)
        {
            DbConn conn = new DbConn();
            string sql = "SELECT * FROM DbConn WHERE Status = " + status + " AND ConnName = '" + connName + "'";
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(sql);
            DataSet ds = SQLiteHelper.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataModelHandler<DbConn> dmh = new DataModelHandler<DbConn>();
                conn = dmh.FillModel(dt.Rows[0]);
            }
            return conn;
        }

        internal DbConn SelectFirst()
        {
            return SelectFirst(1);
        }

        internal DbConn SelectFirst(int status)
        {
            DbConn conn = new DbConn();
            string sql = "SELECT TOP 1 * FROM DbConn WHERE Status = " + status + "";
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(sql);
            DataSet ds = SQLiteHelper.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataModelHandler<DbConn> dmh = new DataModelHandler<DbConn>();
                conn = dmh.FillModel(dt.Rows[0]);
            }
            return conn;
        }

        internal List<DbConn> Select(int status)
        {
            List<DbConn> conns = new List<DbConn>();
            string sql = "SELECT * FROM DbConn WHERE Status = " + status;
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(sql);
            DataSet ds = SQLiteHelper.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataModelHandler<DbConn> dmh = new DataModelHandler<DbConn>();
                conns = dmh.FillModel(dt);
            }
            return conns;
        }

        internal DataTable SelectTable(int status)
        {
            string sql = "SELECT * FROM DbConn WHERE Status = " + status;
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(sql);
            DataSet ds = SQLiteHelper.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            return dt;
        }
        #endregion 
    }
}
