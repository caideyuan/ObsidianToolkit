using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Oak.Core;
using Oak.Model;
using Oak.Base;
using Oak.Base.Logic;
using YS.Model;
using YS.Core.BLL;

namespace YS.Core
{
    public class YsSession : OakSession
    {

        private BsIAccountInfo account;
        private YsMemberInfo user;

        public YsSession(string sessionKey) : base(sessionKey)
        {
            BsIAccountInfo acc = this.Account;
            if (acc != null && acc.AccountType.Value == AccountType.User)
            {
                MUserBL userBL = new MUserBL();
                string msg;
                user = userBL.GetYsMemeber(acc.AccountId.Value, out msg); //获取登录用户（只获取前台用户，后台用户本类自动获取）
                if (user == null)
                    return;
                this.account = acc;
            }
        }


        public YsMemberInfo User
        {
            get { return user; }
        }


        public new long AccountId
        {
            get
            {
                long accountId = base.AccountId;
                if (accountId == -1 && this.account.AccountType.Value == AccountType.User && user != null)
                    return user.UserId.Value;

                return -1;

            }
        }

        public new bool IsLogin
        {
            get
            {
                if (this.account == null)
                    return false;
                if (this.account.AccountType.Value.Equals(AccountType.User) && user != null)
                    return true;

                return base.IsLogin;
            }
        }

    }

}
