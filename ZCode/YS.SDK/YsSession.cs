using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;

using Obsidian.Runtime;
using YS.SDK.Core;

namespace YS.SDK
{
    public class YsSession : IRequiresSessionState
    {
        private const string CK_TICKET = "__t";
        private const string SK_USER = "__USER__";
        private const string SK_SESSION_KEY = "__SESSION_KEY__";

        /// <summary>
        /// SessionKey
        /// </summary>
        public static string SessionKey
        {
            get
            {
                HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
                HttpCookie cookie = cookies[CK_TICKET];
                if (cookie == null)
                    return null;
                return cookie.Value;
            }
        }

        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                Entity oUser = LoginUser;
                string userType = oUser.GetString("userType");
                return userType.Equals("user");
            }
        }

        /// <summary>
        /// 获得登陆用户的UserId，未登陆值为-1
        /// </summary>
        public static long UserId
        {
            get
            {
                if (!IsLogin)
                    return -1;
                Entity oUser = LoginUser;
                return oUser.GetLong("userId");
            }
        }

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public static Entity LoginUser
        {
            get
            {
                HttpSessionState session = HttpContext.Current.Session;
                //获得Cookie中的SessionKey
                string ckSessionKey = SessionKey;
                string ssSessionKey = null;

                if (String.IsNullOrEmpty(ckSessionKey))
                {
                    return SetGuestState(); //创建游客状态
                }
                else
                {
                    //获得Session中的SessionKey
                    object val = session[SK_SESSION_KEY];
                    if (val != null)   //如果Session中有SessionKey
                    {
                        ssSessionKey = Convert.ToString(val);
                        if (!ssSessionKey.Equals(ckSessionKey)) //如果Cookie和Session中的SessionKey不一样
                        {
                            //移除Session中的SessionKey
                            session.Remove(SK_SESSION_KEY);
                            ssSessionKey = null;
                        }
                    }
                }


                /*
                Entity oUser = null;
                object ssUser = session[SK_USER];

                if (ssSessionKey == null || ssUser == null)
                {
                    //获得远程服务器信息
                    oUser = GetRemoteUser(ckSessionKey);
                    if (oUser != null)
                    {
                        oUser.Set("userType", "user");
                        session[SK_SESSION_KEY] = ckSessionKey;
                        session[SK_USER] = oUser;
                    }
                }
                else
                {
                    oUser = (Entity)ssUser;
                }
                 * */

                //获得远程服务器信息
                Entity oUser = GetRemoteUser(ckSessionKey);
                if (oUser != null)
                {
                    oUser.Set("userType", "user");
                    session[SK_SESSION_KEY] = ckSessionKey;
                }

                if (oUser == null)
                    return SetGuestState(); //创建游客状态


                return oUser;

            }
        }

        /// <summary>
        /// 清除登录状态
        /// </summary>
        public static void ClearLoginState()
        {
            HttpSessionState session = HttpContext.Current.Session;
            if (session[SK_USER] != null)
                session.Remove(SK_USER);
        }

        /// <summary>
        /// 创建游客身份状态
        /// </summary>
        /// <returns></returns>
        private static Entity SetGuestState()
        {
            ClearLoginState();
            HttpSessionState session = HttpContext.Current.Session;
            Entity oGuest = new Entity();
            oGuest.Set("userType", "guest");
            session[SK_USER] = oGuest;
            return oGuest;
        }

        /// <summary>
        /// 获得远程服务器用户信息
        /// </summary>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        private static Entity GetRemoteUser(string sessionKey)
        {
            //调用接口获取登录用户信息
            UserAPI userAPI = new UserAPI();
            ApiResponse arGetMember = userAPI.GetUser(SessionKey);
            if (!arGetMember.ApiSucc)//接口调用异常
            {
                //记录日志
                string code = Logger.Error(new Exception(arGetMember.ApiMessage), "接口调用异常");
                return null;
            }
            if (!arGetMember.ActSucc)
            {
                return null;
            }
            Entity oUser = (Entity)arGetMember.GetData("user");
            return oUser;
        }

    }
}
