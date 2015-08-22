using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成表结构脚本
    /// </summary>
    public class AutoSql
    {
        AutoObject ao;
        List<FieldObject> fdos;

        public AutoSql()
        {
        }
        public AutoSql(AutoObject autoObject, List<FieldObject> fieldObjects)
        {
            ao = autoObject;
            fdos = fieldObjects;
        }

        public string CreateCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateTableSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateSelectSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateInsertSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateUpdateSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateDeleteSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }
    }
}
