using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

using Obsidian.Utils;

namespace YS.SDK
{
    public class Entity
    {

        private Dictionary<string, object> values;

        public Entity()
        {
            values = new Dictionary<string, object>();
        }

        public Entity(Dictionary<string, object> dict)
        {
            values = dict;
        }

        public Dictionary<string, object> ToDictionary()
        {
            return values;
        }

        public Dictionary<string, object>.KeyCollection Keys
        {
            get
            {
                return values.Keys;
            }
        }

        public string this[string key]
        {
            get
            {
                string tagChar = key.Substring(0, 1);
                if (tagChar == "@" || tagChar == "#")
                {
                    key = key.Substring(1);
                }
                else
                {
                    tagChar = null;
                }


                if (tagChar == "@")
                    return this.Format(key);

                object o;
                if (!this.TryGetValue(key, out o))
                    return "";
                if (o == null)
                    return "";
                string str = Convert.ToString(o);

                if (tagChar == "#")
                    str = WebUtil.HtmlToText(str);


                return str;
            }
        }

        public string Format(string format)
        {
            return this.Format(format, "#", "#", new object[] { });
        }

        public string Format(string format, object[] args)
        {
            return this.Format(format, "#", "#", args);
        }

        public string Format(string format, string startTag, string endTag, params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            int startIndex = format.IndexOf(startTag);
            int preEndIndex = 0;
            while (startIndex >= 0)
            {
                sb.Append(format.Substring(preEndIndex, startIndex - preEndIndex));

                int attrNameStartIndex = startIndex + startTag.Length;
                int endIndex = format.IndexOf(endTag, attrNameStartIndex);
                if (endIndex <= 0)
                {
                    sb.Append(format.Substring(startIndex));
                    break;
                }

                string attrName = format.Substring(attrNameStartIndex, endIndex - attrNameStartIndex);
                string attrVal = this[attrName];
                sb.Append(attrVal);

                endIndex += endTag.Length;
                if (endIndex > format.Length - 1)
                    break;

                startIndex = format.IndexOf(startTag, endIndex);
                preEndIndex = endIndex;

                if (startIndex < 0)
                {
                    sb.Append(format.Substring(endIndex));
                    break;
                }
            }
            if(args.Length > 0)
                format = String.Format(format, args);
            return sb.ToString();
        }

        public Entity Set(string name, object val)
        {
            values[name] = val;
            return this;
        }

        public object Get(string name)
        {
            object val;
            if (!values.TryGetValue(name, out val))
                throw new Exception(String.Format("属性{0}不存在", name));
            return val;
        }

        public string GetString(string name)
        {
            object val = this.Get(name);
            return Convert.ToString(val);
        }

        public int GetInt(string name)
        {
            object val = this.Get(name);
            return Convert.ToInt32(val);
        }

        public long GetLong(string name)
        {
            object val = this.Get(name);
            return Convert.ToInt64(val);
        }

        public bool GetBool(string name)
        {
            object val = this.Get(name);
            return Convert.ToBoolean(val);
        }

        public decimal GetDecimal(string name)
        {
            object val = this.Get(name);
            return Convert.ToDecimal(val);
        }

        public string GetHtmlText(string name)
        {
            string str = this.GetString(name);
            return WebUtil.HtmlToText(str);
        }

        public bool TryGetValue(string name, out object val)
        {
            return values.TryGetValue(name, out val);
        }

        public bool TryGetString(string name, out string val)
        {
            object o;
            if (!this.TryGetValue(name, out o))
            {
                val = null;
                return false;
            }
            val = Convert.ToString(o);
            return true;
        }

        public bool TryGetBool(string name, out bool val)
        {
            string str;
            if (!this.TryGetString(name, out str))
            {
                val = false;
                return false;
            }
            return bool.TryParse(str, out val);
        }

        public bool TryGetLong(string name, out long val)
        {
            string str;
            if (!this.TryGetString(name, out str))
            {
                val = -1;
                return false;
            }
            return long.TryParse(str, out val);
        }

        public bool TryGetInt(string name, out int val)
        {
            string str;
            if (!this.TryGetString(name, out str))
            {
                val = -1;
                return false;
            }
            return int.TryParse(str, out val);
        }

        public bool TryGetDateTime(string name, out DateTime val)
        {
            string str;
            if (!this.TryGetString(name, out str))
            {
                val = DateTime.MinValue;
                return false;
            }
            return DateTime.TryParse(str, out val);
        }

        public bool TryGetDecimal(string name, out decimal val)
        {
            string str;
            if (!this.TryGetString(name, out str))
            {
                val = -1;
                return false;
            }
            return decimal.TryParse(str, out val);
        }


        public string ToJsonString()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(this.values);
        }

    }
}
