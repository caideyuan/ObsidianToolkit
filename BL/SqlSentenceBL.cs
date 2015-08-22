using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObAuto.DA;
using ObAuto.Om;

namespace ObAuto.BL
{
    public class SqlSentenceBL
    {

        #region Select
        public SqlSentence GetSql(string name)
        {
            SqlSentenceDA da = new SqlSentenceDA();
            return da.SelectByName(name);
        }

        public List<SqlSentence> GetSqls()
        {
            SqlSentenceDA da = new SqlSentenceDA();
            return da.Select(1);
        }
        #endregion

    }
}
