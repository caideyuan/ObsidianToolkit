using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    public class AutoPermit
    {
        AutoObject ao;
        List<FieldObject> fdos;
        string permit = "";

        public AutoPermit(AutoObject autoObject, List<FieldObject> fieldObjects)
        {
            ao = autoObject;
            fdos = fieldObjects;
            permit = ao.ClsName + "Permit";
        }

        public string CreateCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using Obsidian.Edm;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine();
            //
            sb.AppendLine("namespace " + ao.ModelNS);
            sb.AppendLine("{");
            //
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "[" + ao.ClsName + " 权限实体类]");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "1.继承 Obsidian.Edm.OModel");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "2.命名规范：以Permit结尾");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "3.当前只包含CRUD基本权限，根据实际业务扩展权限属性");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "4.该类只用于对应BL类");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_END);
            //
            sb.AppendLine(FARGS.TAB + "public class " + permit + " : OModel");
            sb.AppendLine(FARGS.TAB + "{");
            //构造方法
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[构造方法]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public " + permit + "() ");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "base.InitModel(new IModelField[] {");
            sb.AppendLine(FARGS.TAB4 + "create = new BoolField(this,null,\"create\"),");
            sb.AppendLine(FARGS.TAB4 + "read = new BoolField(this,null,\"read\"),");
            sb.AppendLine(FARGS.TAB4 + "readList = new BoolField(this,null,\"readList\"),");
            sb.AppendLine(FARGS.TAB4 + "update = new BoolField(this,null,\"update\"),");
            sb.AppendLine(FARGS.TAB4 + "delete = new BoolField(this,null,\"delete\")");
            sb.AppendLine(FARGS.TAB3 + "});");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2);

            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[字段属性]");
            //字段属性
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(FARGS.TAB2 + "private BoolField create;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "是否可以创建");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public BoolField Create { get { return create; }}");
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(FARGS.TAB2 + "private BoolField read;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "是否可以读取实体");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public BoolField Read { get { return read; }}");
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(FARGS.TAB2 + "private BoolField readList;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "是否可以读取实体列表");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public BoolField ReadList { get { return readList; }}");
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(FARGS.TAB2 + "private BoolField update;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "是否可以更新");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public BoolField Update { get { return update; }}");
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(FARGS.TAB2 + "private BoolField delete;");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "是否可以创建");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public BoolField Delete { get { return delete; }}");
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[字段属性]");
            //
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
