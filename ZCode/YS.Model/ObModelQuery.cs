using Obsidian.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YS.Model
{
    /// <summary>
    /// ObModel查询条件实体
    /// 1.继承 Obsidian.Edm.QueryInfo
    /// 2.命名规范：以Query结尾
    /// </summary>
    public class ObModelQuery : QueryInfo
    {
        #region 实体属性（只保留get方法）
        private LongListField ids;
        /// <summary>
        /// 记录ID字符串，以英文逗号分隔
        /// </summary>
        public LongListField Ids { get { return ids; } }

        private LongListField filterIds;
        /// <summary>
        /// 过滤ID字符串，以英文逗号分隔
        /// </summary>
        public LongListField FilterIds { get { return filterIds; } }

        private IntField level;
        public IntField Level { get { return level; } }

        private StringField keyword;
        /// <summary>
        /// 关键字
        /// </summary>
        public StringField Keyword { get { return keyword; } }

        private BoolField enabled;
        /// <summary>
        /// 记录状态
        /// </summary>
        public BoolField Enabled { get { return enabled; } }

        private DateTimeField startTime;
        /// <summary>
        /// 开始日期时间
        /// </summary>
        public DateTimeField StartTime { get { return startTime; } }

        private DateTimeField endTime;
        /// <summary>
        /// 结束日期时间
        /// </summary>
        public DateTimeField EndTime { get { return endTime; } }

        private OrderField orderBy;
        /// <summary>
        /// 排序（支持多字段）
        /// 正序：asc；倒序：desc
        /// 传入的字符串示例：obId:desc,obLevel:asc,obName:asc,obScore:desc
        /// </summary>
        public OrderField OrderBy { get { return orderBy; } }

        /// <summary>
        /// 排序字段（非必须，如果需要此属性，其值必须对应数据库实体的属性别名！）
        /// 如：ObModelInfo构造方法中的每个属性字段对应的别名
        /// </summary>
        public const string OrderByFields = "obId,obLevel,obName,obEnabled,obMoney,obScore,obCreated";

        private BoolField getRelation;
        /// <summary>
        /// !是否获取关联表（非必须）
        /// </summary>
        public BoolField GetRelation { get { return getRelation; } }

        private EnumField<ResultSetSize> setSize;
        /// <summary>
        /// 结果集大小
        /// </summary>
        public EnumField<ResultSetSize> SetSize { get { return setSize; } }

        #endregion

        #region 构造方法（注意区别与数据库实体的写法）
        public ObModelQuery()
        {
            base.InitModel(new IModelField[]{
                ids = new LongListField(this,null,"ids",','),           // (this,null,"别名",'分隔字符') 
                level = new IntField(this,null,"level"),
                keyword = new StringField(this,null,"keyword"),
                enabled = new BoolField(this,null,"enabled"),
                startTime = new DateTimeField(this,null,"startTime"),
                endTime = new DateTimeField(this,null,"endTime"),
                orderBy = new OrderField(this,"sorts", OrderByFields),   // (this,"别名","排序字段");
                getRelation = new BoolField(this,null,"getRelation"),
                setSize = new EnumField<ResultSetSize>(this, null, "setSize", ResultSetSize.DEFAULT, EnumFieldToStringCase.UpperCase)
            });
        }
        #endregion
    }
}
