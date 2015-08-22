using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Model
{
    /// <summary>
    /// 状态枚举，适用所有实体
    /// 一般用于Query：EnumField<StatusType>
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// 正常
        /// </summary>
        NORMAL,
        /// <summary>
        /// 删除
        /// </summary>
        DELETE,
        /// <summary>
        /// 非删除
        /// </summary>
        NOTDEL,
		/// <summary>
        /// 缺省默认
        /// </summary>
		DEFAULT,
        /// <summary>
        /// 全部
        /// </summary>
        ALL	
    }
}
