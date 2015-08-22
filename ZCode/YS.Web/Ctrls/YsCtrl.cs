using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

using Obsidian.Utils;
using Obsidian.Config;
using Obsidian.Mvc;
using YS.SDK;
using Obsidian.Runtime;


namespace YS.Web.Ctrls
{
    public class YsCtrl : Controller
    {
        /// <summary>
        /// 检查接口返回信息
        /// </summary>
        /// <returns></returns>
        public bool CheckApiResponse(ApiResponse apiRes)
        {
            return this.CheckApiResponse(apiRes, true);
        }

        /// <summary>
        /// 检查接口返回信息
        /// </summary>
        /// <param name="autoProc">是否自动跳转到登录页面</param>
        /// <returns></returns>
        public bool CheckApiResponse(ApiResponse apiRes, bool autoProc)
        {
            if (!apiRes.ApiSucc)
            {
                if (autoProc)
                {
                    if (apiRes.ResStatus == ApiResStatus.SESSION_KEY_INVALID)
                    {
                        GotoLogin();
                        return false;
                    }
                    OView view = this.GetView("Base/Page500");
                    view.SetData("title", "系统运行错误");
                    view.SetData("message", apiRes.ApiMessage);
                    view.Display();
                }
                return false;
            }

            if (!apiRes.ActSucc)
            {
                return false;
            }

            return true;
        }

        public void Error(int code)
        {
            this.Error(code, null, null, true);
        }

        public void Error(int code, string title, string msg)
        {
            this.Error(code, title, msg, true);
        }

        public void Error(int code, string title, string msg, bool gotoErrorPage)
        {
            //新页面没完成之前，直接跳当前错误页面
            System.Web.HttpContext.Current.Response.Clear();
            string error_msg = code + "|" + title + "|" + msg;
            Logger.Error(error_msg);
            if (gotoErrorPage)
            {
                try
                {
                    switch (code) { 
                        case 404:
                            System.Web.HttpContext.Current.Response.StatusCode = 404;
                            System.Web.HttpContext.Current.Response.Status = "404 Not Found";
                            break;
                        default:
                            System.Web.HttpContext.Current.Response.StatusCode = 500;
                            //System.Web.HttpContext.Current.Response.Status = "500 Error";
                            break;
                    }
                    //System.Web.HttpContext.Current.Response.Status = "404 Not Found";
                    //System.Web.HttpContext.Current.Response.Redirect("/" + code + ".apsx", true);
                    WebUtil.ResponseEnd();
                }
                catch (Exception ex) { }
                return;
                /*
                //错误处理页面
                OView view = this.GetView("Base/Page" + code);
                view.SetData("title", title);
                view.SetData("message", msg);
                view.Display();
                this.End();
                 * */
            }
        }

        public void TestOutput(string msg)
        {
            OView view = this.GetView("Base/Output");
            view.SetData("message", msg);
            view.Display();
        }

        /// <summary>
        /// 跳转到登录页面
        /// </summary>
        public static void GotoLogin()
        {
            GotoLogin(null);
        }

        /// <summary>
        /// 跳转到登录页面
        /// </summary>
        public static void GotoLogin(string redirectUrl)
        {
            if (String.IsNullOrEmpty(redirectUrl))
                redirectUrl = HttpContext.Current.Request.RawUrl;

            string xPath = "appConfig/ys/loginUrl";
            string placeholderRedirectUrl = "%REDIRECT_URL%";

            XmlDocument xmlDoc = AppConfig.GetXml();
            XmlNode node = xmlDoc.SelectSingleNode(xPath);
            if (node == null)
                throw new Exception(String.Format("未能加载配置项 {0} ", xPath));
            string loginUrl = node.Value;
            if (String.IsNullOrEmpty(loginUrl))
                throw new Exception(String.Format("配置项 {0} 不能为空", xPath));

            redirectUrl = WebUtil.GetAbsoluteUrl(redirectUrl);
            redirectUrl = WebUtil.UrlEncode(redirectUrl, "utf-8");
            loginUrl = loginUrl.Replace(placeholderRedirectUrl, redirectUrl);
            try
            {
                HttpContext.Current.Response.Redirect(loginUrl, true);
            }
            catch (Exception e) { }
        }

        protected void PrepareApiRequest(BaseAPI api, ApiVersion ver)
        {
            if (ver == ApiVersion.V2)
                api.AppKey = "webapp";
            else if(ver == ApiVersion.V3)
                api.AppKey = "webapp_v3";
        }
    }
}