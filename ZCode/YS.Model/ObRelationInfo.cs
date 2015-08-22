using Obsidian.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Model
{
    /// <summary>
    /// ObRelation实体
    /// 1.继承 Obsidian.Edm.OModel
    /// 2.命名规范：以Info结尾
    /// 3.该类作为ObModel关联演示用
    /// </summary>
    public class ObRelationInfo : OModel
    {
        private string DbcsName = "yingsheng";
        private string TableName = "TbObRelation";

        #region 实体属性（只保留get方法）
        private LongField rId;
        public LongField RId { get { return rId; } }

        private StringField rName;
        public StringField RName { get { return rName; } }

        private BoolField enabled;
        public BoolField Enabled { get { return enabled; } }

        private DateTimeField created;
        public DateTimeField Created { get { return created; } }

        private LongField obId;
        /// <summary>
        /// ObModelInfo对应表的ObId（类似外键属性字段）
        /// </summary>
        public LongField ObId { get { return obId; } }
        #endregion

        #region 构造方法
        public ObRelationInfo()
        {
            base.InitModel(DbcsName, TableName, new IModelField[]{
                rId = new LongField(this,"Id","rId"),           // (this,"表字段名称","别名")
                rName = new StringField(this,"Name","rName"),
                enabled = new BoolField(this,"Enabled","enabled"),
                created = new DateTimeField(this,"Created", "created"),
                obId = new LongField(this,"ObId","obId")
            });
        }
        #endregion
    }
}
