using Obsidian.Edm; //引入框架
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YS.Model
{
    /// <summary>
    /// ObModel实体
    /// 1.继承 Obsidian.Edm.OModel
    /// 2.命名规范：以Info结尾
    /// </summary>
    public class ObModelInfo : OModel
    {
        private string DbcsName = "yingsheng";  //bin目录下，配置文件AppConfig.xml中的数据库连接字符串别名
        private string TableName = "TbObModel"; //数据库中的表名

        #region 实体属性（只保留get方法）
        private LongField obId;
        public LongField ObId { get { return obId; } }

        private IntField obLevel;
        public IntField ObLevel { get { return obLevel; } }

        private StringField obName;
        public StringField ObName { get { return obName; } }

        private StringField obDescri;
        public StringField ObDescri { get { return obDescri; } }

        private BoolField obEnabled;
        public BoolField ObEnabled { get { return obEnabled; } }

        private DecimalField obMoney;
        public DecimalField ObMoney { get { return obMoney; } }

        private DoubleField obScore;
        public DoubleField ObScore { get { return obScore; } }

        private DateTimeField obCreated;
        public DateTimeField ObCreated { get { return obCreated; } }

        private LongField userId;
        /// <summary>
        /// （非必须）用户ID（对应YsMemberInfo的UserId）
        /// </summary>
        public LongField UserId { get { return userId; } }
        #endregion

        #region 构造方法一（单表）
        public ObModelInfo()
        {
            base.InitModel(DbcsName, TableName, new IModelField[]{
                obId = new LongField(this,"Id","obId"),           // (this,"表字段名称","别名")
                obLevel = new IntField(this,"Level","obLevel"),
                obName = new StringField(this,"Name","obName"),
                obDescri = new StringField(this,"Descri","obDescri"),
                obEnabled = new BoolField(this,"Enabled","obEnabled"),
                obMoney = new DecimalField(this,"Money", "obMoney"),
                obScore = new DoubleField(this,"Score","obScore"),
                obCreated = new DateTimeField(this,"Created", "obCreated"),
                userId = new LongField(this,"UserId","userId")
            });
        }
        #endregion

        #region 构造方法二（多表）
        /// <summary>
        /// 如果是分区表，则使用构造方法一即可，名称为主表名称
        /// long uid = userId % 1024;
        /// string tableNameFormat = @"TbObModel_{0}";     //多个表（同一前缀的或分表，需要表结构一致）
        /// string tableName = String.Format(tableNameFormat, uid);
        /// ObModelInfo om = new ObModelInfo(tableName);
        /// </summary>
        /// <param name="TableName"></param>
        public ObModelInfo(string TableName)
        {
            base.InitModel(DbcsName, TableName, new IModelField[]{
                obId = new LongField(this,"Id","obId"),           // (this,"表字段名称","别名","默认值")
                obLevel = new IntField(this,"Level","obLevel"),
                obName = new StringField(this,"Name","obName"),
                obDescri = new StringField(this,"Descri","obDescri"),
                obEnabled = new BoolField(this,"Enabled","obEnabled"),
                obMoney = new DecimalField(this,"Money", "obMoney"),
                obScore = new DoubleField(this,"Score","obScore"),
                obCreated = new DateTimeField(this,"Created", "obCreated"),
                userId = new LongField(this,"UserId","userId")
            });
        }
        #endregion
    }
}
