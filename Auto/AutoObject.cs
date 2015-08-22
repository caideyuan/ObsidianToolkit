using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码实体类
    /// </summary>
    public class AutoObject
    {
        private string modelNS;
        /// <summary>
        /// Model类命名空间
        /// </summary>
        public string ModelNS
        {
            get { return modelNS; }
            set { modelNS = value; }
        }
        private string queryNS;
        /// <summary>
        /// Query类命名空间
        /// </summary>
        public string QueryNS
        {
            get { return queryNS; }
            set { queryNS = value; }
        }
        private string permitNS;
        /// <summary>
        /// Permit类命名空间
        /// </summary>
        public string PermitNS
        {
            get { return permitNS; }
            set { permitNS = value; }
        }

        private string dalNS;
        /// <summary>
        /// DA类命名空间
        /// </summary>
        public string DalNS
        {
            get { return dalNS; }
            set { dalNS = value; }
        }
        private string bllNS;
        /// <summary>
        /// BL类命名空间
        /// </summary>
        public string BllNS
        {
            get { return bllNS; }
            set { bllNS = value; }
        }

        private string sdkNS;
        /// <summary>
        /// SDK类命名空间
        /// </summary>
        public string SdkNS
        {
            get { return sdkNS; }
            set { sdkNS = value; }
        }
        private string ctrlNS;
        /// <summary>
        /// Ctrl类命名空间
        /// </summary>
        public string CtrlNS
        {
            get { return ctrlNS; }
            set { ctrlNS = value; }
        }
        private string viewNS;
        /// <summary>
        /// View类命名空间
        /// </summary>
        public string ViewNS
        {
            get { return viewNS; }
            set { viewNS = value; }
        }

        private string clsName;
        /// <summary>
        /// 类名称（不带后缀）
        /// 对应的类文件命名规则：
        ///     Model类后缀 Info（.cs）
        ///     Query类后缀 Query（.cs）
        ///     BL类后缀 BL（.cs）
        ///     DA类后缀 DA（.cs）
        ///     SDK类后缀 API（.cs）
        ///     Js类后缀 Manager（.js）
        ///     Html类后缀 Manager（.aspx）
        /// </summary>
        public string ClsName
        {
            get { return clsName; }
            set { clsName = value; }
        }

        private string bllBaseCls;
        /// <summary>
        /// BL基类名称
        /// BL基类封装统一的验证方法
        /// </summary>
        public string BllBaseCls
        {
            get { return bllBaseCls; }
            set { bllBaseCls = value; }
        }
        private string sdkBaseCls;
        /// <summary>
        /// SDK基类名称
        /// </summary>
        public string SdkBaseCls
        {
            get { return sdkBaseCls; }
            set { sdkBaseCls = value; }
        }
        private string ctrlBaseCls;
        /// <summary>
        /// Ctrl基类名称
        /// </summary>
        public string CtrlBaseCls
        {
            get { return ctrlBaseCls; }
            set { ctrlBaseCls = value; }
        }

        private string dbName;
        /// <summary>
        /// 数据库对应库名称
        /// </summary>
        public string DbName
        {
            get { return dbName; }
            set { dbName = value; }
        }
        private string tbName;
        /// <summary>
        /// 数据库对应表名称
        /// </summary>
        public string TbName
        {
            get { return tbName; }
            set { tbName = value; }
        }
    }

    /// <summary>
    /// 字段属性实体类
    /// </summary>
    public class FieldObject
    {
        private bool primaryKey;
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }
        private bool foreignKey;
        /// <summary>
        /// 是否外键
        /// </summary>
        public bool ForeignKey
        {
            get { return foreignKey; }
            set { foreignKey = value; }
        }
        private bool identity;
        /// <summary>
        /// 是否标识字段（自增）
        /// </summary>
        public bool Identity
        {
            get { return identity; }
            set { identity = value; }
        }
        private int sort;
        /// <summary>
        /// 排序（在表字段中位置）
        /// </summary>
        public int Sort
        {
            get { return sort; }
            set { sort = value; }
        }
        private string name;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string alias;
        /// <summary>
        /// 字段别名
        /// </summary>
        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }
        private string type;
        /// <summary>
        /// 字段类型
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int bytes;
        /// <summary>
        /// 字段占用字节大小
        /// </summary>
        public int Bytes
        {
            get { return bytes; }
            set { bytes = value; }
        }
        private int length;
        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        private int digits;
        /// <summary>
        /// 小数位数
        /// </summary>
        public int Digits
        {
            get { return digits; }
            set { digits = value; }
        }
        private bool nullable;
        /// <summary>
        /// 是否可为NULL
        /// </summary>
        public bool Nullable
        {
            get { return nullable; }
            set { nullable = value; }
        }
        private string defaultValue;
        /// <summary>
        /// 字段默认值
        /// </summary>
        public string DefaultValue
        {
            get { return defaultValue; }
            set { defaultValue = value; }
        }
        private string descr;
        /// <summary>
        /// 字段说明描述
        /// </summary>
        public string Descr
        {
            get { return descr; }
            set { descr = value; }
        }

        private string privateProp;
        /// <summary>
        /// 私有属性名称
        /// </summary>
        public string PrivateProp
        {
            get { return privateProp; }
            set { privateProp = value; }
        }
        private string publicProp;
        /// <summary>
        /// 公有属性名称
        /// </summary>
        public string PublicProp
        {
            get { return publicProp; }
            set { publicProp = value; }
        }
    }
}
