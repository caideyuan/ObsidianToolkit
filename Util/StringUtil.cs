using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace ObAuto
{
    public class StringUtil
    {
        /// <summary>
        /// 格式化字符串
        /// 去掉分隔符，首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        public static string FirstUpper(string str, char split)
        {
            string titleCaseStr = "";
            string[] splitStr = str.Split(split);
            if (splitStr != null && splitStr.Length > 0)
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                foreach (string s in splitStr)
                {
                    textInfo.ToTitleCase(s);
                    titleCaseStr += s;
                }
            }
            return titleCaseStr;
        }

        public static string FristLower(string str, string split)
        {
            string lowerCaseStr = "";
            string firstChar = str.Substring(0, 1).ToLower();
            lowerCaseStr = firstChar + str.Substring(1, str.Length - 1);
            lowerCaseStr = lowerCaseStr.Replace(split, "");
            return lowerCaseStr;
        }
    }
}
