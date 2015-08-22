using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObAuto.DA;
using ObAuto.Om;
using System.Data;

namespace ObAuto.BL
{
    public class DbConnBL
    {
        #region Select
        public DbConn GetDbConn(string name)
        {
            DbConnDA da = new DbConnDA();
            return da.SelectByName(name);
        }

        public List<DbConn> GetDbConns()
        {
            DbConnDA da = new DbConnDA();
            return da.Select(1);
        }

        public DataTable GetDbConnTable()
        {
            DbConnDA da = new DbConnDA();
            string connString = "data source={0};initial catalog={1};user id={2};Password={3}";
            DataTable dt = da.SelectTable(1);

            if (dt.Rows.Count > 0)
            {
                DataColumn dc = new DataColumn("ConnString");
                dt.Columns.Add(dc);

                foreach (DataRow dr in dt.Rows)
                {
                    dr["ConnString"] = string.Format(connString,
                        dr["DataSource"], dr["Catalog"], dr["UserId"], dr["Password"]);
                }
            }
            return dt;
        }
        #endregion
    }
}
