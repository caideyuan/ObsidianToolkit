using ObAuto.DA;
using ObAuto.Om;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto.BL
{
    public class UserBL
    {
        #region Select
        public User GetUser(string name)
        {
            UserDA da = new UserDA();
            return da.SelectByName(name);
        }

        public List<User> GetUsers()
        {
            UserDA da = new UserDA();
            return da.Select(1);
        }
        #endregion
    }
}
