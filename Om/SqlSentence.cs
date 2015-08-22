using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto.Om
{
    /// <summary>
    /// SQL语句（SqliteDB）
    /// </summary>
    public class SqlSentence
    {
        private long sqlID;

        public long SqlID
        {
            get { return sqlID; }
            set { sqlID = value; }
        }
        private string sqlName;

        public string SqlName
        {
            get { return sqlName; }
            set { sqlName = value; }
        }
        private int sqlType;

        public int SqlType
        {
            get { return sqlType; }
            set { sqlType = value; }
        }
        private string sqlValue;

        public string SqlValue
        {
            get { return sqlValue; }
            set { sqlValue = value; }
        }
        private bool status;

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
