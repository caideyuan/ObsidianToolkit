using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成数据访问层类文件
    /// </summary>
    public class AutoDA
    {
        public AutoDA()
        {
        }

        public string CreateCodeString(AutoObject ao, List<FieldObject> fdos)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using Obsidian.Edm;");
            sb.AppendLine("using Obsidian.Data.Sql;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using " + ao.ModelNS + ";");
            sb.AppendLine();
            //
            sb.AppendLine("namespace " + ao.DalNS);
            sb.AppendLine("{");
            //
            //
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "[" + ao.ClsName + " 数据访问类]");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "1.需要引入Model层");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "2.需要引入Obsidian.Data.Sql");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "3.需要引入Obsidian.Edm");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "4.方法统一使用internal修饰符（只能在同一程序集内访问）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "5.命名规范：以DA结尾");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_END);
            //
            sb.AppendLine(FARGS.TAB + "public class " + ao.ClsName + "DA");
            sb.AppendLine(FARGS.TAB + "{");
            //
            sb.AppendLine(CreateSelectFunction(ao, fdos));
            //
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine("}");

            return sb.ToString();
        }

        public string CreateSelectFunction(AutoObject ao, List<FieldObject> fdos)
        {
            string info = ao.ClsName + "Info";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[SELECT]");
            //SelectById
            sb.AppendLine(FARGS.TAB2 + "internal " + info + " SelectById(long id)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + info + " om = new " + info + "();");
            sb.AppendLine(FARGS.TAB3 + "IDbHandler dbh = om.CreateHandler();    //创建数据库操作句柄接口");
            sb.AppendLine(FARGS.TAB3 + "//设置返回的实体字段，相当于EF的Select字段");
            sb.AppendLine(FARGS.TAB3 + "dbh.SetFields(om.Fields);   //om.Fields为所有字段");
            sb.AppendLine(FARGS.TAB3 + "//dbh.SetFields(om.Id, om.Enabled, om.CreateTime);    //自定义返回的实体字段");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "dbh.Where(om.Id.Equals(id));    //组装Where条件语句");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "DataReader dr = dbh.Select();   //执行查询");
            sb.AppendLine(FARGS.TAB3 + "om = dr.ReadFirst<" + info + ">();    //获取结果集方法一（自动关闭读取器）");
            sb.AppendLine(FARGS.TAB3 + "//获取结果集方法二（手动关闭读取器，更灵活）");
            sb.AppendLine(FARGS.TAB3 + "/*");
            sb.AppendLine(FARGS.TAB3 + "if (dr.Read())");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dr.ReadTo(om);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "else");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "om = null;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "dr.Close();");
            sb.AppendLine(FARGS.TAB3 + "*/");
            sb.AppendLine(FARGS.TAB3 + "return om;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + "//参照SelectById()，可按需构建SelectByName(),SelectByCode()等方法");
            /******************************/
            sb.AppendLine(FARGS.TAB2 + "");
            //SelectList
            sb.AppendLine(FARGS.TAB2 + "internal List<" + info + "> SelectList(" + ao.ClsName + "Query qry, out ListAttrInfo la)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + info + " om = new " + info + "();");
            sb.AppendLine(FARGS.TAB3 + "IDbHandler dbh = om.CreateHandler();");
            sb.AppendLine(FARGS.TAB3 + "dbh.SetFields(om.Fields);");
            sb.AppendLine(FARGS.TAB3 + "//[BEGIN]::组装Where组合条件语句");
            sb.AppendLine(FARGS.TAB3 + "//组装日期时间条件");
            sb.AppendLine(FARGS.TAB3 + "string dtFormat = \"yyyy-MM-dd HH:mm:ss\";");
            sb.AppendLine(FARGS.TAB3 + "string startTime;");
            sb.AppendLine(FARGS.TAB3 + "string endTime;");
            sb.AppendLine(FARGS.TAB3 + "if (!qry.StartTime.IsNull && (!qry.StartTime.IsMinValue))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "startTime = qry.StartTime.Value.ToString(dtFormat);");
            sb.AppendLine(FARGS.TAB4 + "dbh.Where(om.CreateTime.Compare(\">=\", startTime));");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "if (!qry.EndTime.IsNull && (!qry.EndTime.IsMinValue))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "endTime = qry.EndTime.Value.ToString(dtFormat);");
            sb.AppendLine(FARGS.TAB4 + "dbh.Where(om.CreateTime.Compare(\"<=\", endTime));");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//组装记录ID条件");
            sb.AppendLine(FARGS.TAB3 + "if(!qry.Id.IsNull)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dbh.Where(om.Id.Equals(qry.Id.Value));");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//组装记录ID串条件");
            sb.AppendLine(FARGS.TAB3 + "if(!qry.Ids.IsNull && qry.Ids.Count > 0)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dbh.Where(om.Id.In(qry.Ids.Value));");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//组装关键字条件");
            sb.AppendLine(FARGS.TAB3 + "if(!qry.Keyword.IsNullOrWhiteSpace)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dbh.Where(om.Content.Like(qry.Keyword.Value));");
            sb.AppendLine(FARGS.TAB4 + "//需要复合条件则使用下面的写法");
            sb.AppendLine(FARGS.TAB4 + "//string kw = qry.Keyword.Value;");
            sb.AppendLine(FARGS.TAB4 + "//CompoundCondition cc = new CompoundCondition();    //复合条件类");
            sb.AppendLine(FARGS.TAB4 + "//cc.Or(om.Title.Like(kw)).Or(om.Content.Like(kw));");
            sb.AppendLine(FARGS.TAB4 + "//dbh.Where(cc);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//组装关键字串条件");
            sb.AppendLine(FARGS.TAB3 + "if(!qry.Keywords.IsNull && qry.Keywords.Count > 0)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "CompoundCondition cc = new CompoundCondition();    //复合条件类");
            sb.AppendLine(FARGS.TAB4 + "IList<string> kws = qry.Keywords.Value;");
            sb.AppendLine(FARGS.TAB4 + "for(int i=0,j=kws.Count; i<j; i++)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "cc.Or(om.Title.Like(kws[i]));");
            sb.AppendLine(FARGS.TAB5 + "cc.Or(om.Content.Like(kws[i]));");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "dbh.Where(cc);");
            sb.AppendLine(FARGS.TAB4 + "//dbh.Where(om.Content.Contanis(qry.Keywords.Value)); //Contanis，需要修正为Contains");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//组装记录状态条件");
            sb.AppendLine(FARGS.TAB3 + "if(!qry.Enabled.IsNull)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dbh.Where(om.Enabled.Equals(qry.Enabled.Value));");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//组装JOIN条件（预留）");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "//组装排序条件（放在Where子句最后，分页时，必须有OrderBy）");
            sb.AppendLine(FARGS.TAB3 + "if(!qry.OrderBy.IsNull && qry.OrderBy.Count>0)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "OrderInfo[] ois = qry.OrderBy.GetArray();");
            sb.AppendLine(FARGS.TAB4 + "foreach (OrderInfo oi in ois)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "//拼装的order by条件，case条件字段值需要和Query查询实体的OrderByFields属性一致");
            sb.AppendLine(FARGS.TAB5 + "//排序字段别名转化为小写");
            sb.AppendLine(FARGS.TAB5 + "switch (oi.FieldAlias.ToLower())");
            sb.AppendLine(FARGS.TAB5 + "{");
            sb.AppendLine(FARGS.TAB6 + "case \"title\":");
            sb.AppendLine(FARGS.TAB7 + "dbh.OrderBy(om.Title, oi.OrderType);");
            sb.AppendLine(FARGS.TAB7 + "break;");
            sb.AppendLine(FARGS.TAB6 + "case \"id\":");
            sb.AppendLine(FARGS.TAB6 + "default:");
            sb.AppendLine(FARGS.TAB7 + "dbh.OrderBy(om.Id, oi.OrderType);");
            sb.AppendLine(FARGS.TAB7 + "break;");
            sb.AppendLine(FARGS.TAB5 + "}");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "else");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dbh.OrderBy(om.Id, OrderType.DESC); //默认排序条件");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//[END]::组装Where组合条件语句");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "List<" + info + "> list = new List<" + info + ">();");
            sb.AppendLine(FARGS.TAB3 + "DataReader dr = dbh.Select(qry, out la);");
            sb.AppendLine(FARGS.TAB3 + "list = dr.ReadList<" + info + ">(); //获取结果集方法一（自动关闭读取器）");
            sb.AppendLine(FARGS.TAB3 + "//获取结果集方法二（手动关闭读取器，更灵活）");
            sb.AppendLine(FARGS.TAB3 + "/**");
            sb.AppendLine(FARGS.TAB3 + "while (dr.Read())");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + info + " ei = new " + info + "();");
            sb.AppendLine(FARGS.TAB4 + "dr.ReadTo(ei);");
            sb.AppendLine(FARGS.TAB4 + "list.Add(ei);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "dr.Close();");
            sb.AppendLine(FARGS.TAB3 + "*/");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "return list;");
            sb.AppendLine(FARGS.TAB2 + "}");
            //
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[SELECT]");

            return sb.ToString();
        }

        
        public string CreateExistsFunction(AutoObject ao, List<FieldObject> fdos)
        {
            StringBuilder sb = new StringBuilder();
            //预留（判断是否存在）
            return sb.ToString();
        }
    }
}
