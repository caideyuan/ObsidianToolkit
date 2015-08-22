using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成业务逻辑接口类文件
    /// </summary>
    public class AutoBL
    {
        AutoObject ao;
        List<FieldObject> fdos;
        string info = "";
        string query = "";
        string da = "";
        string bl = "";
        string funcPrefix = "";
        //public AutoBL()
        //{
        //}

        public AutoBL(AutoObject autoObject, List<FieldObject> fieldObjects)
        {
            ao = autoObject;
            fdos = fieldObjects;
            info = ao.ClsName + "Info";
            query = ao.ClsName + "Query";
            da = ao.ClsName + "DA";
            bl = ao.ClsName + "BL";
            funcPrefix = "." + StringUtil.FristLower(ao.ClsName, "_") + ".";
        }

        public string CreateCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using Obsidian.Action;");
            sb.AppendLine("using Obsidian.Edm;");
            sb.AppendLine("using Obsidian.Runtime;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using " + ao.ModelNS + ";");
            sb.AppendLine("using " + ao.DalNS + ";");
            sb.AppendLine("using YS.Core.BLL;   //业务逻辑接口基类");
            sb.AppendLine();
            //
            sb.AppendLine("namespace " + ao.BllNS);
            sb.AppendLine("{");
            //
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "[" + ao.ClsName + " 业务逻辑类（接口方法类）]");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "0.继承YS.Core.BLL.YingShengBL （基类YingShengBL封装了用户和权限）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "1.需要引入Obsidian.Edm（Model层）和对应的DA类");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "2.需要引入Obsidian.Action （接口基类）, Obsidian.Edm （实体）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "3.需要引入Obsidian.Runtime （Logger写日志）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "4.需要实现构造方法（ObsidianV3版）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "5.命名规范：以DA结尾");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_END);
            //
            sb.AppendLine(FARGS.TAB + "public class " + bl + " : " + ao.BllBaseCls);
            sb.AppendLine(FARGS.TAB + "{");
            //
            sb.AppendLine(CreateBLProperty());
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(CreateBLStructure());   //ObsidianV3版本才需要
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[接口方法]");
            sb.AppendLine(CreateFunctionDescription());   //接口方法说明
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(CreateAddFunction());
            //sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(CreateUpdateFunction());
            //sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(CreateGetFunction());
            //sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(CreateListFunction());
            //sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(CreateDeleteFunction());
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[接口方法]");
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(CreateAssistFunction());
            sb.AppendLine(FARGS.TAB2);
            //
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine("}");

            return sb.ToString();
        }

        public string CreateBLProperty()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[字段属性]");
            sb.AppendLine(FARGS.TAB2 + "private const string RESULT = \"result\";   //结果集参数名称（主要用于删除接口）");
            sb.AppendLine(FARGS.TAB2 + "private const string RESULT_FIELDS = \"code,msg\";  //结果集参数对应字段");
            sb.AppendLine(FARGS.TAB2 + "private const string QUERY = \"qry\";   //请求列表主参数名称（主要用于查询列表接口）");
            sb.AppendLine(FARGS.TAB2 + "private const string LISTATTR = \"listAttr\";  //返回分页参数名称");
            sb.AppendLine(FARGS.TAB2 + "private const string LISTATTR_FIELDS = \"itemsCount,pageNo,pageSize,pagesCount\";  //分页结果集字段");
            sb.AppendLine(FARGS.TAB2 + "private const int MIN_PAGESIZE = 10;     //默认分页大小");
            sb.AppendLine(FARGS.TAB2 + "private const int MAX_PAGESIZE = 100;    //最大分页大小");
            sb.AppendLine(FARGS.TAB2 + "private const string VERIFY_ERROR = \"用户非法，无法进行此操作\"; //用户非法操作时返回给请求端信息");
            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB2 + "private const string ENTITY_REQ = \""+ StringUtil.FristLower(ao.ClsName,"_") +"\"; //[请求]主参数名称");
            sb.AppendLine(FARGS.TAB2 + "private const string ENTITY_RES = \"" + StringUtil.FristLower(ao.ClsName, "_") + "s\"; //[返回]主参数名称");
            sb.AppendLine(FARGS.TAB2 + "private const string ENTITY_FIELDS = \"\";  //[返回]主参数名称对应字段名称");
            sb.AppendLine(FARGS.TAB4 + "//（需要和ObModel实体ObModelInfo的属性别名一致；可按需控制返回给请求端那些字段）");
            sb.AppendLine(FARGS.TAB2 + "private const string ENTITY_STR = \"[记录]\"; //自定义日志记录的实体名称");
            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[字段属性]");
            return sb.ToString();
        }

        public string CreateBLStructure()
        {
            //(Obsidian V3版本必须有构造方法)
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[构造方法]");
            sb.AppendLine(FARGS.TAB2 + "public " + bl + "() : base() { }");
            sb.AppendLine(FARGS.TAB2 + "public " + bl + "(YsSession session) : base(session) { }");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[构造方法]");
            return sb.ToString();
        }

        public string CreateFunctionDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + "/**");
            sb.AppendLine(FARGS.TAB2 + "* 1.接口命名方式 [程序集别名.接口类别名.接口方法别名]");
            sb.AppendLine(FARGS.TAB2 + "*  除首字母外，接口命名是区分大小写的！");
            sb.AppendLine(FARGS.TAB2 + "*  程序集别名：bin目录下，配置文件AppConfig.xml中的apiAssemblies节点下的assembly节点的name属性");
            sb.AppendLine(FARGS.TAB2 + "*  接口类别名：业务逻辑类名称去掉BL后缀（如ObModelBL => obModel）");
            sb.AppendLine(FARGS.TAB2 + "*  接口方法别名：如 add,update,get,list,delete等");
            sb.AppendLine(FARGS.TAB2 + "*  示例：ys.obModel.add; ys.obModel.get");
            sb.AppendLine(FARGS.TAB2 + "* 2.接口调用权限：配置文件AppConfig.xml中的apiServes节点下的serv节点");
            sb.AppendLine(FARGS.TAB2 + "* 3.接口执行权限：通过继承基类，使用YsSession进行用户角色判断（ObsidianV3版本）");
            sb.AppendLine(FARGS.TAB2 + "*/");
            return sb.ToString();
        }

        public string CreateAddFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[新增]");
            sb.AppendLine(FARGS.TAB2 + "public void Add(ActionRequest req, ActionResponse res)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "string msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "if (!CheckAdd())");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(VERIFY_ERROR);");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + info + " ei = req.GetModelByNameOrFirst<" + info + ">(ENTITY_REQ);");
            sb.AppendLine(FARGS.TAB4 + "ei = AddEntity(ei, out msg);");
            sb.AppendLine(FARGS.TAB4 + "if(ei == null)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(msg);");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "ActionResult ar = res.AddResult(ENTITY_RES, ENTITY_FIELDS); //定义返回结果集名称和字段名");
            sb.AppendLine(FARGS.TAB4 + "ar.AddModel(ei);    //添加结果集到ActionResult");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch (Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"" + funcPrefix + "add 接口调用异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "res.Error(msg);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "}");
            ////
            sb.AppendLine(FARGS.TAB2 + "public " + info + " AddEntity(" + info + " ei, out string msg)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + info + " om = null;");
            sb.AppendLine(FARGS.TAB3 + "msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "if(!Verify(ei, out msg))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "return null;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "//根据需求是否要判断记录已存在");
            sb.AppendLine(FARGS.TAB4 + "//" + da + " da = new " + da + "();");
            sb.AppendLine(FARGS.TAB4 + "//if(da.Exists(ei.Id.Value))");
            sb.AppendLine(FARGS.TAB4 + "//{");
            sb.AppendLine(FARGS.TAB5 + "//msg = \"该\"+ ENTITY_STR +\"已存在\";");
            sb.AppendLine(FARGS.TAB5 + "//return null;");
            sb.AppendLine(FARGS.TAB4 + "//}");
            sb.AppendLine(FARGS.TAB4 + "om = ei;");
            sb.AppendLine(FARGS.TAB4 + "//设置空或NULL参数属性的默认值");
            sb.AppendLine(FARGS.TAB4 + "if(ei.Content.IsNullOrWhiteSpace)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "om.Content.Set(\"\");");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "om.CreateTime.Now();");
            sb.AppendLine(FARGS.TAB4 + "om.Save();");
            sb.AppendLine(FARGS.TAB4 + "if(om == null)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "msg = \"添加\" + ENTITY_STR + \"失败\";");
            sb.AppendLine(FARGS.TAB5 + "return null;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch(Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"添加\" + ENTITY_STR + \"异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "throw;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "return om;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[新增]");
            return sb.ToString();
        }

        public string CreateUpdateFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[更新]");
            sb.AppendLine(FARGS.TAB2 + "public void Update(ActionRequest req, ActionResponse res)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "string msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + info + " ei = req.GetModelByNameOrFirst<" + info + ">(ENTITY_REQ);");
            sb.AppendLine(FARGS.TAB4 + "if (!CheckUpdate(ei))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(VERIFY_ERROR);");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "ei = UpdateEntity(ei, out msg);");
            sb.AppendLine(FARGS.TAB4 + "if(ei == null)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(msg);");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "ActionResult ar = res.AddResult(ENTITY_RES, ENTITY_FIELDS); //定义返回结果集名称和字段名");
            sb.AppendLine(FARGS.TAB4 + "ar.AddModel(ei);    //添加结果集到ActionResult");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch (Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"" + funcPrefix + "update 接口调用异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "res.Error(msg);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "}");
            ////
            sb.AppendLine(FARGS.TAB2 + "public " + info + " UpdateEntity(" + info + " ei, out string msg)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + info + " om = null;");
            sb.AppendLine(FARGS.TAB3 + "msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "if(ei.Id.IsNull)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "msg = \"记录ID不能为空\";");
            sb.AppendLine(FARGS.TAB5 + "return null;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "" + da + " da = new " + da + "();");
            sb.AppendLine(FARGS.TAB4 + "om = da.SelectById(ei.Id.Value);");
            sb.AppendLine(FARGS.TAB4 + "if(om == null)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "msg = \"该\"+ ENTITY_STR +\"不存在\";");
            sb.AppendLine(FARGS.TAB5 + "return null;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "");
            sb.AppendLine(FARGS.TAB4 + "//设置非空参数属性要修改的值");
            sb.AppendLine(FARGS.TAB4 + "om.ResetAssigned();");
            sb.AppendLine(FARGS.TAB4 + "if(!ei.Content.IsNullOrWhiteSpace)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "om.Content.Set(ei.Content.Value);");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "");
            sb.AppendLine(FARGS.TAB4 + "if(!om.Update())");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "msg = \"更新\" + ENTITY_STR + \"失败\";");
            sb.AppendLine(FARGS.TAB5 + "return null;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch(Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"更新\" + ENTITY_STR + \"异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "throw;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "return om;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[更新]");
            return sb.ToString();
        }

        public string CreateGetFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[获取]");
            sb.AppendLine(FARGS.TAB2 + "public void Get(ActionRequest req, ActionResponse res)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "string msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "ActReqParam param;");
            sb.AppendLine(FARGS.TAB4 + "if (!req.TryGetParam(ENTITY_REQ, out param))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(\"参数\" + ENTITY_REQ + \"错误\");");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "long id = 0;");
            sb.AppendLine(FARGS.TAB4 + "if(!param.TryGetFirstLong(\"id\", out id))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(\"参数\" + ENTITY_REQ + \"属性id错误\");");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "if (!CheckGet(id))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(VERIFY_ERROR);");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");

            sb.AppendLine(FARGS.TAB4 + "//用户相关");
            sb.AppendLine(FARGS.TAB4 + "/** long userId = 0;");
            sb.AppendLine(FARGS.TAB4 + "if(!param.TryGetFirstLong(\"userId\", out userId))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(\"参数\" + ENTITY_REQ + \"属性userId错误\");");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}*/");
            sb.AppendLine(FARGS.TAB4 + info + " ei = GetEntity(id, out msg);");
            sb.AppendLine(FARGS.TAB4 + "//" + info + " ei = new " + info + "();");
            sb.AppendLine(FARGS.TAB4 + "//ei.Id.set(id);");
            sb.AppendLine(FARGS.TAB4 + "//if (!CheckGet(ei))");
            sb.AppendLine(FARGS.TAB4 + "//{");
            sb.AppendLine(FARGS.TAB5 + "//res.Error(VERIFY_ERROR);");
            sb.AppendLine(FARGS.TAB5 + "//return;");
            sb.AppendLine(FARGS.TAB4 + "//}");
            sb.AppendLine(FARGS.TAB4 + "//ei = GetEntity(ei, out msg);");
            sb.AppendLine(FARGS.TAB4 + "//ei = GetEntity(ei, userId, out msg);  //用户相关");            
            sb.AppendLine(FARGS.TAB4 + "if(ei == null)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(msg);");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "ActionResult ar = res.AddResult(ENTITY_RES, ENTITY_FIELDS);");
            sb.AppendLine(FARGS.TAB4 + "ar.AddModel(ei);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch (Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"" + funcPrefix + "get 接口调用异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "res.Error(msg);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + "");
            //
            //
            sb.AppendLine(FARGS.TAB2 + "public " + info + " GetEntity(long id, out string msg)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + info + " om = null;");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + da + " da = new " + da + "();");
            sb.AppendLine(FARGS.TAB4 + "om = da.SelectById(id);");
            sb.AppendLine(FARGS.TAB4 + "//om = OModel.GetByPk<" + info + ">(id);   //通过主键获取记录");
            sb.AppendLine(FARGS.TAB4 + "if(om == null)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "msg = \"获取\" + ENTITY_STR + \"为空\";");
            sb.AppendLine(FARGS.TAB5 + "return null;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch (Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"获取\" + ENTITY_STR + \"异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "return null;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "return om;");
            sb.AppendLine(FARGS.TAB2 + "}");
            //
            sb.AppendLine(FARGS.TAB2 + "public " + info + " GetEntity(" + info + " ei, out string msg)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + info + " om = null;");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + da + " da = new " + da + "();");
            sb.AppendLine(FARGS.TAB4 + "om = da.SelectById(ei.Id.Value);");
            sb.AppendLine(FARGS.TAB4 + "//om = OModel.GetByPk<" + info + ">(ei.Id.Value);   //通过主键获取记录");
            sb.AppendLine(FARGS.TAB4 + "if(om == null)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "msg = \"获取\" + ENTITY_STR + \"为空\";");
            sb.AppendLine(FARGS.TAB5 + "return null;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch (Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"获取\" + ENTITY_STR + \"异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "return null;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "return om;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[获取]");
            return sb.ToString();
        }

        public string CreateListFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[列表]");
            sb.AppendLine(FARGS.TAB2 + "public void List(ActionRequest req, ActionResponse res)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "string msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "ListAttrInfo la;");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + query + " qry = req.GetModelByNameOrFirst<" + query + ">(QUERY);");
            sb.AppendLine(FARGS.TAB4 + "if (!CheckGet(qry))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(VERIFY_ERROR);");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "List<" + info + "> list = GetEntityList(qry, out la, out msg);");
            sb.AppendLine(FARGS.TAB4 + "if(list == null)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(msg);");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "ActionResult ar = res.AddResult(ENTITY_RES, ENTITY_FIELDS);");
            sb.AppendLine(FARGS.TAB4 + "ar.AddModels<" + info + ">(list);");
            sb.AppendLine(FARGS.TAB4 + "ActionResult arAttr = res.AddResult(LISTATTR, LISTATTR_FIELDS);");
            sb.AppendLine(FARGS.TAB4 + "arAttr.AddModel(la);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch (Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"" + funcPrefix + "list 接口调用异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "res.Error(msg);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + "");
            //
            sb.AppendLine(FARGS.TAB2 + "public List<" + info + "> GetEntityList(" + query + " qry, out ListAttrInfo la, out string msg)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "List<" + info + "> list = null;");
            sb.AppendLine(FARGS.TAB3 + "la = null;");
            sb.AppendLine(FARGS.TAB3 + "msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "if (!qry.CheckPagingAttrs(MIN_PAGESIZE, MAX_PAGESIZE, out msg))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 +"la = null;");
            sb.AppendLine(FARGS.TAB5 + "return null;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + da + " da = new " + da + "();");
            sb.AppendLine(FARGS.TAB4 + "list = da.SelectList(qry, out la);");
            sb.AppendLine(FARGS.TAB4 + "if(list == null)");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "msg = \"获取\" + ENTITY_STR + \"列表为空\";");
            sb.AppendLine(FARGS.TAB5 + "la = null;");
            sb.AppendLine(FARGS.TAB5 + "return null;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch (Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"获取\" + ENTITY_STR + \"列表异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "return null;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "return list;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[列表]");
            return sb.ToString();
        }

        public string CreateDeleteFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[删除]");
            sb.AppendLine(FARGS.TAB2 + "public void Delete(ActionRequest req, ActionResponse res)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "string msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "ActReqParam param;");
            sb.AppendLine(FARGS.TAB4 + "if (!req.TryGetParam(ENTITY_REQ, out param))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(\"参数\" + ENTITY_REQ + \"错误\");");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "long id = 0;");
            sb.AppendLine(FARGS.TAB4 + "if (!param.TryGetLong(0, \"id\", out id))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(\"参数\" + ENTITY_REQ + \"属性id错误\");");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "if (!CheckGet(id))");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "res.Error(VERIFY_ERROR);");
            sb.AppendLine(FARGS.TAB5 + "return;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "int code = 0;");
            sb.AppendLine(FARGS.TAB4 + "code = DeleteEntity(id, out msg);");
            sb.AppendLine(FARGS.TAB4 + "ActionResult ar = res.AddResult(RESULT, RESULT_FIELDS);");
            sb.AppendLine(FARGS.TAB4 + "ar.AddValues(code, msg);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch (Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"" + funcPrefix + "delete 接口调用异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "res.Error(msg);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + "");
            //
            sb.AppendLine(FARGS.TAB2 + "public int DeleteEntity(long id, out string msg)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "int code = 0;");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + info + " om = GetEntity(id, out msg);");
            sb.AppendLine(FARGS.TAB4 + "if(om == null) return 0;");
            sb.AppendLine(FARGS.TAB4 + "if(om.Delete())");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "code = 1;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB4 + "else");
            sb.AppendLine(FARGS.TAB4 + "{");
            sb.AppendLine(FARGS.TAB5 + "msg = \"删除\" + ENTITY_STR + \"失败\";");
            sb.AppendLine(FARGS.TAB5 + "return code;");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch (Exception ex)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"删除\" + ENTITY_STR + \"异常\";");
            sb.AppendLine(FARGS.TAB4 + "Logger.Error(ex, msg);");
            sb.AppendLine(FARGS.TAB4 + "return -1;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "return code;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[删除]");
            return sb.ToString();
        }

        public string CreateAssistFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[私有辅助方法]");
            sb.AppendLine(FARGS.TAB2 + "////验证请求实体参数方法，一般用于新增实体");
            sb.AppendLine(FARGS.TAB2 + "private bool Verify(" + info + " om, out string msg)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "if(om.Title.IsNullOrWhiteSpace)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "msg = \"不能为空\";");
            sb.AppendLine(FARGS.TAB4 + "return false;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "msg = \"\";");
            sb.AppendLine(FARGS.TAB3 + "return true;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + "");

            //
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[" + ao.ClsName + " 用户操作权限判断方法]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "0.需要权限实体" + ao.ClsName + "Permit");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "1.需要根据业务需要自定义判断权限");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            //
            sb.AppendLine(FARGS.TAB2 + "public " + ao.ClsName + "Permit GetPermission(" + info + " om)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + ao.ClsName + "Permit prmt =  new " + ao.ClsName + "Permit();");
            sb.AppendLine(FARGS.TAB3 + "prmt.Create.Set(CheckAdd());");
            sb.AppendLine(FARGS.TAB3 + "prmt.Read.Set(CheckRead(om));");
            sb.AppendLine(FARGS.TAB3 + "prmt.ReadList.Set(CheckReadList(om));");
            sb.AppendLine(FARGS.TAB3 + "prmt.Update.Set(CheckUpdate(om));");
            sb.AppendLine(FARGS.TAB3 + "prmt.Delete.Set(CheckDelete(om.Id.Value));");
            sb.AppendLine(FARGS.TAB3 + "return prmt;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + "");

            sb.AppendLine(FARGS.TAB2 + "//判断创建实体权限");
            sb.AppendLine(FARGS.TAB2 + "private bool CheckAdd()");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "return Session.IsAdmin;");
            sb.AppendLine(FARGS.TAB2 + "}");

            sb.AppendLine(FARGS.TAB2 + "//判断获取实体权限");
            sb.AppendLine(FARGS.TAB2 + "private bool CheckRead(" + info + " ei)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "//string status = ei.Status.Value;  //根据状态获取");
            sb.AppendLine(FARGS.TAB3 + "//return (Session.IsAdmin) ? true : status.Equals(\"normal\");");
            sb.AppendLine(FARGS.TAB3 + "return Session.IsAdmin;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + "private bool CheckRead(long id)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "//根据id和当前用户判断是否有获取权限");
            sb.AppendLine(FARGS.TAB3 + "//return (Session.IsAdmin) ? true : id > 0;");
            sb.AppendLine(FARGS.TAB3 + "return Session.IsAdmin;");
            sb.AppendLine(FARGS.TAB2 + "}");

            sb.AppendLine(FARGS.TAB2 + "//判断获取列表权限");
            sb.AppendLine(FARGS.TAB2 + "private bool CheckReadList(" + info + " ei)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "//string status = ei.Status.Value;  //根据状态获取");
            sb.AppendLine(FARGS.TAB3 + "//return (Session.IsAdmin) ? true : status.Equals(\"normal\");");
            sb.AppendLine(FARGS.TAB3 + "return Session.IsAdmin;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + "private bool CheckReadList(" + query + " qry)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "//根据qry和当前用户判断是否有获取权限");
            sb.AppendLine(FARGS.TAB3 + "//return (Session.IsAdmin) ? true : qry.Id.Value > 0;");
            sb.AppendLine(FARGS.TAB3 + "return Session.IsAdmin;");
            sb.AppendLine(FARGS.TAB2 + "}");

            sb.AppendLine(FARGS.TAB2 + "//判断更新实体权限");
            sb.AppendLine(FARGS.TAB2 + "private bool CheckUpdate(" + info + " ei)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "return Session.IsAdmin;");
            sb.AppendLine(FARGS.TAB2 + "}");

            sb.AppendLine(FARGS.TAB2 + "//判断删除实体权限");
            sb.AppendLine(FARGS.TAB2 + "private bool CheckDelete(long id)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "return Session.IsAdmin;");
            sb.AppendLine(FARGS.TAB2 + "}");

            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[私有辅助方法]");
            return sb.ToString();
        }
    }
}
