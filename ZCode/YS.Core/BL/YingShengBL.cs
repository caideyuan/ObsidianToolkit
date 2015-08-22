using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

using Obsidian.Action;
using YS.Model;
using Obsidian.Runtime;
using Obsidian.Utils;
using YS.Core.DAL;

namespace YS.Core.BLL
{
    public class YingShengBL : IAction
    {

        #region 通用静态属性
        public static string RESULT_NAME = "result";          //结果集参数名称（主要用于删除）
        public static string RESULT_FIELDS = "code,msg"; //结果集参数对应字段
        //
        public static string QUERY_NAME = "qry";          //请求列表主参数名称（主要用于查询列表）
        public static string LISTATTR_NAME = "listAttr";  //返回分页参数名称
        public static string LISTATTR_FIELDS = "itemsCount,pageNo,pageSize,pagesCount";  //分页结果集字段
        public static int MIN_PAGESIZE = 10;     //默认分页大小
        public static int MAX_PAGESIZE = 100;    //最大分页大小
        #endregion

        private YsSession _session;

        public YingShengBL() { }

        public YingShengBL(YsSession session)
        {
            this._session = session;
        }

        public YsSession Session
        {
            get { return this._session; }
        }

        private Dictionary<string, object> attacheData;

        public void SetAttacheData(Dictionary<string, object> htData)
        {
            this.attacheData = htData;
        }

    }
}
