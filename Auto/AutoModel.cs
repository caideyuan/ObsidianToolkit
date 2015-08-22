using System;
using System.Collections.Generic;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成Model类文件
    /// 
    /// </summary>
    public class AutoModel
    {
        public AutoModel()
        {

        }

        public string CreateCodeString(AutoObject ao, List<FieldObject> fdos)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using Obsidian.Edm;  //引入Obsidian框架");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine();
            //
            sb.AppendLine("namespace " + ao.ModelNS);
            sb.AppendLine("{");
            //
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "[" + ao.ClsName + " 实体类描述]");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "1.继承 Obsidian.Edm.OModel");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "2.命名规范：以Info结尾");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_END);
            //
            sb.AppendLine(FARGS.TAB + "public class " + ao.ClsName + "Info : OModel");
            sb.AppendLine(FARGS.TAB + "{");
            //sb.AppendLine(FARGS.TAB2);
            //构造方法
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[构造方法]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public " + ao.ClsName + "Info() ");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "base.InitModel(DbcsName, TableName, new IModelField[] {");

            foreach (FieldObject fo in fdos)
            {
                sb.Append(FARGS.TAB4 + "" + fo.PrivateProp + " = new " + fo.Type + "(this, \""
                    + fo.Name + "\" , \"" + fo.Alias + "\")");
                sb.AppendLine(",");
            }
            //去掉最末尾的","符号
            sb.Remove(sb.ToString().LastIndexOf(','), 1);

            sb.AppendLine(FARGS.TAB3 + "});");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2);

            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[字段属性]");

            //字段属性
            sb.AppendLine(FARGS.TAB2 + "private string DbcsName = \"" + ao.DbName + "\";  //bin目录下，配置文件AppConfig.xml中的数据库连接字符串别名");
            sb.AppendLine(FARGS.TAB2 + "private string TableName = \"" + ao.TbName + "\"; //数据库中的表名");
            sb.AppendLine(FARGS.TAB2);
            foreach (FieldObject fo in fdos)
            {
                sb.AppendLine(FARGS.TAB2 + "private " + fo.Type + " " + fo.PrivateProp + ";");
                sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
                sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + fo.Descr);
                sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
                sb.AppendLine(FARGS.TAB2 + "public " + fo.Type + " " + fo.PublicProp + " {"
                    + " get { return " + fo.PrivateProp + "; }}");
            }
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[字段属性]");
            //
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
