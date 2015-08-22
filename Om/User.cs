using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto.Om
{
    /// <summary>
    /// ObAuto应用用户（SqliteDB）
    /// </summary>
    public class User
    {
        private long userID;

        public long UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string pwd;

        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }
        private int userType = 0;

        public int UserType
        {
            get { return userType; }
            set { userType = value; }
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


    }
}
