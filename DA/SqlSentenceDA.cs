using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using ObAuto.Om;

namespace ObAuto.DA
{
    public class SqlSentenceDA
    {
        #region SELECT
        internal SqlSentence SelectByName(string sqlName)
        {
            return SelectByName(sqlName, 1);
        }
        internal SqlSentence SelectByName(string sqlName, int status)
        {
            SqlSentence snt = new SqlSentence();
            string sql = "SELECT * FROM SqlSentence WHERE Status = "+ status +" AND SqlName='" + sqlName + "'";
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(sql);
            DataSet ds = SQLiteHelper.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataModelHandler<SqlSentence> dmh = new DataModelHandler<SqlSentence>();
                snt = dmh.FillModel(dt.Rows[0]);
            }
            return snt; 
        }

        internal List<SqlSentence> Select(int status)
        {
            List<SqlSentence> sss = new List<SqlSentence>();
            string sql = "SELECT * FROM SqlSentence WHERE Status = " + status;
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(sql);
            DataSet ds = SQLiteHelper.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataModelHandler<SqlSentence> dmh = new DataModelHandler<SqlSentence>();
                sss = dmh.FillModel(dt);
            }
            return sss;
        }
        #endregion 
    }
}
