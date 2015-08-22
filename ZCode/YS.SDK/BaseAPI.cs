using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

using Newtonsoft.Json.Linq;
using Obsidian.Runtime;
using Obsidian.Action;
using Obsidian.Web;
using Obsidian.Utils;
using Obsidian.Tools;

namespace YS.SDK
{
    public class BaseAPI
    {

        private string _appKey = null;
        public string AppKey
        {
            get { return this._appKey; }
            set { this._appKey = value; }
        }

        public ApiResponse Execute(ActionRequest req)
        {
            return this.Execute(req, null);
        }

        public ApiResponse Execute(ActionRequest req, string sessionKey)
        {
            string appKey = this.AppKey;
            ActReqHandler arh = new ActReqHandler();
            if (String.IsNullOrEmpty(appKey))
                arh = new ActReqHandler();
            else
                arh = new ActReqHandler(appKey);

            arh.AddRequest(req);

            ApiResponse apiRes = new ApiResponse();

            try
            {
                if (String.IsNullOrEmpty(sessionKey))
                    arh.Execute();
                else
                    arh.Execute(sessionKey);
            }
            catch (EndOfStreamException e) { }
            catch (WebException wex)
            {
                string code = Logger.WebError(wex, "远程调用接口异常:" + req.Action);
                apiRes.SetActSucc(false);
                apiRes.SetActMessage("发生系统异常:" + code);
                return apiRes;
            }
            

            apiRes.SetStatusCode(arh.Status);
            //apiRes.SetResponseText(arh);
            
            if (arh.Status != ActReqHandler.STATUS_OK)
            {
                apiRes.SetApiSucc(false);
                apiRes.SetApiMessage(arh.Message);
                if (arh.Status == "SESSION_KEY_INVALID")
                    apiRes.SetApiStatus(ApiResStatus.SESSION_KEY_INVALID);
                else
                    apiRes.SetApiStatus(ApiResStatus.ERROR);
                return apiRes;
            }

            apiRes.SetApiSucc(true);
            apiRes.SetApiStatus(ApiResStatus.OK);

            ActionResponse res = req.Response;
            if (res.StatusCode != 1)
            {
                apiRes.SetActSucc(false);
                apiRes.SetActMessage(res.Message);
                return apiRes;
            }

            apiRes.SetActSucc(true);

            return apiRes;
        }

        public string Post(string url, string method, Dictionary<string, object> prms)
        {
            if (!String.IsNullOrWhiteSpace(method))
                url += "/" + method;

            //StringBuilder sb = new StringBuilder();
            //bool first = true;
            //foreach (KeyValuePair<string, object> pair in prms)
            //{
            //    if (first)
            //        first = false;
            //    else
            //        sb.Append("&");
            //    sb.Append(pair.Key).Append("=").Append(WebUtil.UrlEncode(Convert.ToString(pair.Value), "utf-8"));
            //}

            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var item in prms)
            {
                sb.Append(item.Key + "=" + HttpUtility.UrlEncode(Utils.ObjToStr(item.Value, ""), System.Text.Encoding.UTF8));
                if (i < prms.Count)
                {
                    sb.Append("&");
                }
                i++;
            }
            string responseText = HttpRequestor.Post(url, sb.ToString());
            return responseText ;
        }

        protected ActionRequest PrepareViewRequest(string viewCode, Entity oQuery)
        {
            return PrepareViewRequest(viewCode, oQuery, ApiVersion.V2);
        }

        protected ActionRequest PrepareViewRequest(string viewCode, Entity oQuery, ApiVersion ver)
        {
            if (ver == ApiVersion.V2)
            {
                ActionRequest req = new ActionRequest("ext.view.display");

                Dictionary<string, object> dictViewQry = new Dictionary<string, object>();
                dictViewQry.Add("viewCode", viewCode);
                req.AddParam("viewQry", dictViewQry);

                Dictionary<string, object> dictQryCourse = new Dictionary<string, object>();
                req.AddParam("qry", oQuery.ToDictionary());

                return req;
            }
            else if (ver == ApiVersion.V3)
            {
                ActionRequest req = new ActionRequest("odv.view.display");

                Dictionary<string, object> dictViewQry = new Dictionary<string, object>();
                dictViewQry.Add("viewCode", viewCode);
                req.AddParam("view", dictViewQry);

                Dictionary<string, object> dictQryCourse = new Dictionary<string, object>();
                req.AddParam("qry", oQuery.ToDictionary());

                return req;
            }
            throw new Exception("未知API版本");
        }

        public ApiResponse PostResponse(string url, string method)
        {
            return PostResponse(url, method, null, "");
        }

        public ApiResponse PostResponse(string url, string method, string sessionKey)
        {
            return PostResponse(url, method, null, sessionKey);
        }

        public ApiResponse PostResponse(string url, string method, Entity o)
        {
            return PostResponse(url, method, o, null);
        }

        public ApiResponse PostResponse(string url, string method, Entity o, string sessionKey)
        {
            Dictionary<string, object> dictPrms = new Dictionary<string, object>();
            if (o != null)
            {
                foreach (string key in o.Keys)
                    dictPrms[key] = o.Get(key);
            }

            //if (String.IsNullOrEmpty(sessionKey))
            //    sessionKey = "";

            if(sessionKey != null)
                dictPrms["sessionkey"] = sessionKey;
            dictPrms["app_id"] = WebConfig.APP_ID;

            //生成签名
            string sign = GeneratePostResponseSign(dictPrms, WebConfig.APP_Key);
            dictPrms["sign"] = sign;

            string strJson = "";
            ActReqHandler arh = new ActReqHandler();
            ApiResponse apiRes = new ApiResponse();
            try
            {
                strJson = this.Post(url, method, dictPrms);
            }
            catch (EndOfStreamException e) { }
            catch (WebException wex)
            {
                HttpWebResponse response = (HttpWebResponse)wex.Response;
                Stream stream = response.GetResponseStream();
                string responseText = IOUtil.StreamToString(stream);
                StringBuilder sbError = new StringBuilder();
                sbError.Append("远程调用接口异常:" + method);
                sbError.AppendLine(responseText);
                string code = Logger.Error(sbError.ToString());

                apiRes.SetActSucc(false);
                apiRes.SetActMessage("发生系统异常:" + code);
                return apiRes;
            }
            catch (Exception ex)
            {
                apiRes.SetActSucc(false);
                apiRes.SetActMessage(ex.Message);
                return apiRes;
            }
            apiRes.SetStatusCode(arh.Status);
            //apiRes.SetResponseText(arh);

            if (arh.Status != ActReqHandler.STATUS_OK)
            {
                apiRes.SetApiSucc(false);
                apiRes.SetApiMessage(arh.Message);
                return apiRes;
            }

            apiRes.SetApiSucc(true);
            apiRes.SetActSucc(true);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                Dictionary<string, object> dict = jss.Deserialize<Dictionary<string, object>>(strJson);
                int intResult = 0;
                object objResult;
                if (dict.TryGetValue("result", out objResult))
                {
                    intResult = Convert.ToInt32(objResult);
                }
                else
                {
                    intResult = 1;
                }
                if (intResult <= 0)
                {
                    if (dict.TryGetValue("code", out objResult).Equals("10003"))
                    {
                        apiRes.SetActSucc(false);
                        apiRes.SetActMessage(dict["msg"].ToString());
                        return apiRes;
                    }
                    else
                    {
                        //apiRes.SetApiSucc(false);
                        apiRes.SetApiMessage(dict["msg"].ToString());
                        //return apiRes;
                    }
                }
                apiRes.SetResponseText(strJson);
            }
            catch (Exception ex)
            {
                apiRes.SetApiSucc(false);
                apiRes.SetApiMessage(ex.Message);
                return apiRes;
            }
            return apiRes;
        }
        public ApiResponse HTTPPost(string url, string method, Entity o, string sessionKey)
        {
            ActReqHandler arh = new ActReqHandler();
            ApiResponse apiRes = new ApiResponse();
            apiRes.SetStatusCode(arh.Status);
            //apiRes.SetResponseText(arh);

            if (arh.Status != ActReqHandler.STATUS_OK)
            {
                apiRes.SetApiSucc(false);
                apiRes.SetApiMessage(arh.Message);
                return apiRes;
            }

            apiRes.SetApiSucc(true);
            apiRes.SetActSucc(true);
            Dictionary<string, object> dictPrms = new Dictionary<string, object>();
            if (o != null)
            {
                foreach (string key in o.Keys)
                    dictPrms[key] = o.Get(key);
            }

            if (String.IsNullOrEmpty(sessionKey))
                sessionKey = "";

            if (!string.IsNullOrEmpty(sessionKey)) { dictPrms["sessionkey"] = sessionKey; }
            dictPrms["appId"] = WebConfig.APP_ID;

            //生成签名
            string sign = GeneratePostResponseSign(dictPrms, WebConfig.APP_Key);
            dictPrms["sign"] = sign;
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, object> pair in dictPrms)
                list.Add(pair.Key + "=" + WebUtil.UrlEncode(Convert.ToString(pair.Value), "utf-8"));
            list.Sort();
            string str = ArrayUtil.Join(list, "&");

            
            //string text = HttpRequestor.Get(url + "?"+ str);
            apiRes.SetResponseText(url + "?" + str);
            return apiRes;
        }
        //生成签名
        public static string GeneratePostResponseSign(Dictionary<string, object> prms, string appKey)
        {
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();
            prms = (from entry in prms
                          orderby entry.Key ascending
                          select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            int i = 0;
            foreach (var item in prms)
            {
                sb.Append(item.Key.ToLower() + "=" + HttpUtility.UrlEncode(Utils.ObjToStr(item.Value,""), System.Text.Encoding.UTF8));
                if (i < prms.Count)
                {
                    sb.Append("&");
                }
                i++;
            }
            list.Sort();
            string str = sb.ToString() + appKey;
            string sign = StringUtil.Md5Encrypt(str);
            return sign;
        }

        /// <summary>
        /// 方法调用失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResponse ActError(string msg)
        {
            ApiResponse res = new ApiResponse();
            res.SetApiSucc(true);
            res.SetActSucc(false);
            res.SetActMessage(msg);
            return res;
        }

        /// <summary>
        /// 方法调用成功
        /// </summary>
        /// <returns></returns>
        public static ApiResponse ActSucc()
        {
            ApiResponse res = new ApiResponse();
            res.SetApiSucc(true);
            res.SetActSucc(true);
            res.SetStatusCode("OK");
            res.SetApiStatus(ApiResStatus.OK);
            return res;
        }

        /// <summary>
        /// SessionKey无效
        /// </summary>
        /// <returns></returns>
        public static ApiResponse SessionKeyInvalid()
        {
            ApiResponse res = new ApiResponse();
            res.SetApiSucc(false);
            res.SetApiMessage("SessionKey无效");
            res.SetApiStatus(ApiResStatus.SESSION_KEY_INVALID);
            return res;
        }

        public static Entity GetEntity(ActionResponse res, string resultName)
        {
            ActionResult rs = res.GetResult(resultName);
            Dictionary<string, object> dict = rs.GetDict(0);
            Entity o = new Entity(dict);
            return o;
        }

        public static EntitySet GetEntitySet(ActionResponse res, string resultName)
        {
            ActionResult rs = res.GetResult(resultName);
            EntitySet es = new EntitySet();
            for (int i = 0; i < rs.Count; i++)
            {
                Entity o = new Entity(rs.GetDict(i));
                es.Add(o);
            }
            return es;

        }

    }

    public enum ApiVersion
    {
        V2,
        V3
    }
}
