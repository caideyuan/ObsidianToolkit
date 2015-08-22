using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    public class OEnum
    {
    }

    /// <summary>
    /// CRUD相关操作枚举
    /// </summary>
    public enum EOPT
    {
        CREATE,
        UPDATE,
        DELETE,

        BLUK_CRAETE,
        BLUK_UPDATE,
        BLUK_DELETE,

        EXISTS,
        READ,
        BLUK_READ,

        DO,
        GET,
        POST,

        DEFAULT
    }

    /// <summary>
    /// 代码类型
    /// </summary>
    public enum LNG
    {
        CS,
        JS,
        ASPX,
        SQL
    }
}
