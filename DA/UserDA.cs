using ObAuto.Om;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ObAuto.DA
{
    public class UserDA
    {

        #region SELECT 
        internal User SelectByName(string userName)
        {
            return SelectByName(userName, 1);
        }
        internal User SelectByName(string userName, int status)
        {
            User user = new User();
            string sql = "SELECT * FROM User WHERE Status = "+ status +" AND UserName = '" + userName + "'";
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(sql);
            DataSet ds = SQLiteHelper.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataModelHandler<User> dmh = new DataModelHandler<User>();
                user = dmh.FillModel(dt.Rows[0]);
            }
            else
                user = null;

            return user;
        }

        internal List<User> Select(int status)
        {
            List<User> users = new List<User>();
            string sql = "SELECT * FROM User WHERE Status = " + status;
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(sql);
            DataSet ds = SQLiteHelper.ExecuteDataSet(cmd);
            DataTable dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataModelHandler<User> dmh = new DataModelHandler<User>();
                users = dmh.FillModel(dt);
            }
            return users;
        }
        #endregion
    }
}
