using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Model
{
    /// <summary>
    /// 结果集大小，适用所有实体
    /// 一般用于Query：EnumField<ResultSetSize>
    /// </summary>
    public enum ResultSetSize
    {
        /// <summary>
        /// 最少
        /// </summary>
        TINY,
        /// <summary>
        /// 简单
        /// </summary>
        SIMPLE,
        /// <summary>
        /// 标准，普通
        /// </summary>
        NORMAL,
        /// <summary>
        /// 全部
        /// </summary>
        ALL,
		/// <summary>
		/// 自定义，按输入参数字段名输出对应结果字段，无对应的忽略掉
		/// </summary>
		CUSTOM,
        /// <summary>
        /// 默认，按DA层返回结果字段
        /// </summary>
        DEFAULT
    }
}
