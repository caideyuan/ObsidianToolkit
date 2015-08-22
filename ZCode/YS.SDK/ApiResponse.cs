using System;
using System.Collections.Generic;
using System.Text;

namespace YS.SDK
{
    public class ApiResponse
    {
        private Dictionary<string, object> dictData = new Dictionary<string, object>();
        private bool _apiSucc = false;
        private string _apiMessage = null;
        private string _statusCode = null;
        private ApiResStatus _resStatus = ApiResStatus.OK;
        private bool _actSucc = false;
        private string _actMessage = null;
        private string _responseText = null;

        /// <summary>
        /// 接口调用是否成功
        /// </summary>
        public bool ApiSucc
        {
            get { return this._apiSucc; }
        }

        /// <summary>
        /// 接口调用返回信息
        /// </summary>
        public string ApiMessage
        {
            get { return this._apiMessage; }
        }

        /// <summary>
        /// 接口调用状态码
        /// </summary>
        public string StatusCode
        {
            get { return _statusCode; }
        }

        /// <summary>
        /// API调用响应状态
        /// </summary>
        public ApiResStatus ResStatus
        {
            get { return this._resStatus; }
        }

        /// <summary>
        /// 方法调用是否成功
        /// </summary>
        public bool ActSucc
        {
            get { return this._actSucc; }
        }

        /// <summary>
        /// 方法调用返回信息
        /// </summary>
        public string ActMessage
        {
            get { return this._actMessage; }
        }

        public string ResponseText
        {
            get { return this._responseText; }
        }

        /// <summary>
        /// 获得返回结果集
        /// </summary>
        /// <param name="name">结果集名称</param>
        /// <returns></returns>
        public object GetData(string name)
        {
            return dictData[name];
        }

        /// <summary>
        /// 是否存在结果集
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasData(string name)
        {
            return dictData.ContainsKey(name);
        }

        /// <summary>
        /// 获得结果集
        /// </summary>
        /// <param name="name">结果集名称</param>
        /// <param name="data">结果集数据</param>
        /// <returns></returns>
        public bool TryGetData(string name, out object data)
        {
            return dictData.TryGetValue(name, out data);
        }

        internal void SetApiSucc(bool apiSucc)
        {
            this._apiSucc = apiSucc;
        }

        internal void SetApiMessage(string message)
        {
            this._apiMessage = message;
        }

        internal void SetStatusCode(string statusCode)
        {
            this._statusCode = statusCode;
        }

        internal void SetApiStatus(ApiResStatus resStatus)
        {
            this._resStatus = resStatus;
        }

        internal void SetActSucc(bool actSucc)
        {
            this._actSucc = actSucc;
        }

        internal void SetActMessage(string message)
        {
            this._actMessage = message;
        }

        internal void SetResponseText(string responseText)
        {
            this._responseText = responseText;
        }

        internal void SetData(string name, object data)
        {
            dictData[name] = data;
        }

    }

    /// <summary>
    /// API响应状态
    /// </summary>
    public enum ApiResStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        OK,
        /// <summary>
        /// 错误
        /// </summary>
        ERROR,
        /// <summary>
        /// SessionKey无效
        /// </summary>
        SESSION_KEY_INVALID
    }
}
