using System;
using System.Collections.Generic;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成Query类文件
    /// 
    /// </summary>
    public class AutoQuery
    {
        public AutoQuery()
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
            sb.AppendLine("namespace " + ao.QueryNS);
            sb.AppendLine("{");
            //
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "[" + ao.ClsName + " 查询实体类描述]");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "1.继承 Obsidian.Edm.QueryInfo");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "2.命名规范：以Query结尾");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_END);
            //
            sb.AppendLine(FARGS.TAB + "public class " + ao.ClsName + "Query : QueryInfo");
            sb.AppendLine(FARGS.TAB + "{");
            
            //构造方法
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[构造方法（注意区别和OModel实体的写法）]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public " + ao.ClsName + "Query() ");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "base.InitModel(new IModelField[] {");
            //foreach (FieldObject fo in fdos)
            //{
            //    sb.Append(FARGS.TAB4 + "" + fo.PrivateProp + " = new " + fo.Type + "(this, null , \"" + fo.Alias + "\")");
            //    sb.AppendLine(",");
            //}
            sb.AppendLine(CreateQueryStructure());
            //去掉最末尾的","符号
            sb.Remove(sb.ToString().LastIndexOf(','), 1);
            sb.AppendLine(FARGS.TAB3 + "});");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2);

            //对查询实体字段，确定通用字段名
            //
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[查询字段属性]");
            //foreach (FieldObject fo in fdos)
            //{
            //    sb.AppendLine(FARGS.TAB2 + "private " + fo.Type + " " + fo.PrivateProp + ";");
            //    sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            //    sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + fo.Descr);
            //    sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            //    sb.AppendLine(FARGS.TAB2 + "public " + fo.Type + " " + fo.PublicProp + " {"
            //        + " get { return " + fo.PrivateProp + "; }}");
            //}
            sb.AppendLine(CreateQueryProperty());
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[查询字段属性]");
            sb.AppendLine(FARGS.TAB + "}");
            sb.AppendLine("}");
            //
            return sb.ToString();
        }

        public string CreateQueryProperty()
        {
            StringBuilder sb = new StringBuilder();
            //
            sb.AppendLine(FARGS.TAB2 + "private LongField id;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "记录ID，单个");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public LongField Id { get { return id; } }");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "private LongListField ids;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "记录ID字符串，多个以英文逗号分隔");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public LongListField Ids { get { return ids; } }");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "private LongListField filtIds;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "过滤记录ID字符串，多个以英文逗号分隔");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public LongListField FiltIds { get { return filtIds; } }");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "private StringField keyword;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "关键字，单个");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public StringField Keyword { get { return keyword; } }");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "private StringListField keywords;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "关键字字符串，多个以英文逗号分隔");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public StringListField Keywords { get { return keywords; } }");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "private BoolField enabled;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "记录状态：true:启用，false:禁用");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public BoolField Enabled { get { return enabled; } }");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "private StringField status;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "数据状态");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public StringField Status { get { return status; } }");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "private DateTimeField startTime;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "开始（日期）时间");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public DateTimeField StartTime { get { return startTime; } }");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "private DateTimeField endTime;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "结束（日期）时间");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public DateTimeField EndTime { get { return endTime; } }");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "/**");
            sb.AppendLine(FARGS.TAB2 + "* 排序类型");
            sb.AppendLine(FARGS.TAB2 + "private OrderField orderBy;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "排序（支持多字段）；正序：asc；倒序：desc");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public OrderField OrderBy { get { return orderBy; } }");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "排序字段（非必须，如果需要此属性，其值必须对应数据库实体的属性别名！）");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "传入的字符串示例：obId:desc,obLevel:asc,obName:asc,obScore:desc");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "//public const string OrderByFields = \"\";");
            sb.AppendLine(FARGS.TAB2 + "**/");
            sb.AppendLine();
            //
            sb.AppendLine(FARGS.TAB2 + "/**");
            sb.AppendLine(FARGS.TAB2 + "* 枚举类型");
            sb.AppendLine(FARGS.TAB2 + "private EnumField<ResultSetSize> setSize;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "结果集大小");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public EnumField<ResultSetSize> SetSize { get { return setSize; } }");
            sb.AppendLine(FARGS.TAB2 + "**/");
            sb.AppendLine();
            
            return sb.ToString();
        }

        public string CreateQueryStructure()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(FARGS.TAB4 + "id = new LongField(this, null , \"id\"),");
            sb.AppendLine(FARGS.TAB4 + "ids = new LongListField(this, null , \"ids\",','),");
            sb.AppendLine(FARGS.TAB4 + "filtIds = new LongListField(this, null , \"filtIds\",','),");
            sb.AppendLine(FARGS.TAB4 + "keyword = new StringField(this, null , \"keyword\"),");
            sb.AppendLine(FARGS.TAB4 + "keywords = new StringListField(this, null , \"keywords\",','),");
            sb.AppendLine(FARGS.TAB4 + "enabled = new BoolField(this, null , \"enabled\"),");
            sb.AppendLine(FARGS.TAB4 + "status = new StringField(this, null , \"status\"),");
            sb.AppendLine(FARGS.TAB4 + "startTime = new DateTimeField(this, null , \"startTime\"),");
            sb.AppendLine(FARGS.TAB4 + "endTime = new DateTimeField(this, null , \"endTime\"),");
            sb.AppendLine(FARGS.TAB4 + "//orderBy = new OrderField(this, \"orderBy\", OrderByFields),");
            sb.AppendLine(FARGS.TAB4 + "//setSize = new EnumField<ResultSetSize>(this, null, \"setSize\", ResultSetSize.DEFAULT, EnumFieldToStringCase.UpperCase),");
            return sb.ToString();
        }
    }
}
