using Obsidian.Edm; //引入框架
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace [#ModelNS#]
{
	/// <summary>
    /// [[#ClsName#] 实体类描述]
    /// 1.继承 Obsidian.Edm.OModel
    /// 2.命名规范：以Info结尾
    /// </summary>
    public class [#ClsName#]Info : OModel
    {
        private string DbcsName = "[#DBName#]";		//bin目录下，配置文件AppConfig.xml中的数据库连接字符串别名
        private string TableName = "[#TBName#]";	//数据库中的表名

		#region 构造方法
        public [#ClsName#]Info()
        {
            base.InitModel(DbcsName, TableName, new IModelField[] {
				[#IMPORT FOR#][#ModelFieldInit.tpl#]
            });
        }
        #endregion

        #region 实体属性（只保留get方法）
        [#IMPORT FOR#][#ModelField.tpl#]
        #endregion
	}
}