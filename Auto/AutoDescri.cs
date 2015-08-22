using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成接口功能描述
    /// </summary>
    public class AutoDescri
    {
        AutoObject ao;
        List<FieldObject> fdos;

        public AutoDescri()
        {
        }
        public AutoDescri(AutoObject autoObject, List<FieldObject> fieldObjects)
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

        public string CreateEntityDescri()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateListDescri()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateGetDescri()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateAddDescri()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateUpdateDescri()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            return sb.ToString();
        }

        public string CreateDeleteDescri()
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
