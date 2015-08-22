using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成API接口调用SDK类文件
    /// </summary>
    public class AutoSDK
    {
        AutoObject ao;
        List<FieldObject> fdos;
        string api = "";
        string funcPrefix = "";

        public AutoSDK()
        {
        }
        public AutoSDK(AutoObject autoObject, List<FieldObject> fieldObjects)
        {
            ao = autoObject;
            fdos = fieldObjects;
            api = ao.ClsName + "API";
            funcPrefix = "." + StringUtil.FristLower(ao.ClsName, "_") + ".";
        }

        public string CreateCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using Obsidian.Action;");
            sb.AppendLine("using Obsidian.Runtime;");
            sb.AppendLine("using Obsidian.Tools;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine();
            //
            sb.AppendLine("namespace " + ao.SdkNS);
            sb.AppendLine("{");
            //
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "[" + ao.ClsName + " 前端业务SDK类（接口方法调用类）]");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "0.继承YS.SDK.BaseAPI （基类BaseAPI封装了接口HTTP调用基础方法）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "1.需要引入Obsidian.Action （接口基类）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "2.需要引入Obsidian.Runtime （Logger写日志）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "3.根据需要引入其他SDK类");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "4.根据需要引入Obsidian.Tools（Ob工具类）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "5.命名规范：以API结尾");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_END);
            //
            sb.AppendLine(FARGS.TAB + "public class " + api + " : " + ao.SdkBaseCls);
            sb.AppendLine(FARGS.TAB + "{");
            //
            sb.AppendLine(CreateSDKProperty());
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(CreateSDKStructure());
            sb.AppendLine(FARGS.TAB2);
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[SDK方法]");
            sb.AppendLine(CreateListFunction()); 
            sb.AppendLine(CreateGetFunction());
            sb.AppendLine(CreateAddFunction()); 
            sb.AppendLine(CreateUpdateFunction());
            sb.AppendLine(CreateDeleteFunction());
            sb.AppendLine(CreateAssistFunction());
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[SDK方法]");
            //
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine("}");
            return sb.ToString();
        }

        public string CreateSDKProperty()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[字段属性]");
            sb.AppendLine(FARGS.TAB2 + "//根据接口规范约定名称");
            sb.AppendLine(FARGS.TAB2 + "private const string RESULT = \"result\";   //删除结果集参数名称");
            sb.AppendLine(FARGS.TAB2 + "private const string LISTATTR = \"listAttr\";  //分页结果集名称");
            sb.AppendLine(FARGS.TAB2 + "private const int DEFAULT_PAGESIZE = 10;     //默认分页大小");
            sb.AppendLine(FARGS.TAB2 + "private const string QUERY = \"qry\";   //[请求]列表主参数名称");
            sb.AppendLine(FARGS.TAB2 + "private const string ENTITY_REQ = \"" + StringUtil.FristLower(ao.ClsName, "_") + "\"; //[请求]主参数名称");
            sb.AppendLine(FARGS.TAB2 + "private const string ENTITY_RES = \"" + StringUtil.FristLower(ao.ClsName, "_") + "s\"; //[返回]主参数名称");
            sb.AppendLine(FARGS.TAB2 + "private const string API_GET_URL = \"" + funcPrefix + "get\"");
            sb.AppendLine(FARGS.TAB2 + "private const string API_LIST_URL = \"" + funcPrefix + "list\"");
            sb.AppendLine(FARGS.TAB2 + "private const string API_ADD_URL = \"" + funcPrefix + "add\"");
            sb.AppendLine(FARGS.TAB2 + "private const string API_UPDATE_URL = \"" + funcPrefix + "update\"");
            sb.AppendLine(FARGS.TAB2 + "private const string API_DELETE_URL = \"" + funcPrefix + "delete\"");
            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[字段属性]");
            return sb.ToString();
        }

        public string CreateSDKStructure()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[构造方法]");
            sb.AppendLine(FARGS.TAB2 + "public " + api + "() { }");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[构造方法]");
            return sb.ToString();
        }

        public string CreateListFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[列表]");
            sb.AppendLine(FARGS.TAB2 + "public ApiResponse GetList(Entity entity, int pageNo, int pageSize)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes;");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "Dictionary<string, object> dict = new Dictionary<string, object>();");
            sb.AppendLine(FARGS.TAB3 + "long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (entity.TryGetLong(\"id\", out id))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dict.Add(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//添加其他查询参数属性到dict");
            sb.AppendLine(FARGS.TAB3 + "ActionRequest req = new ActionRequest(API_LIST_URL);");
            sb.AppendLine(FARGS.TAB3 + "req.AddParam(QUERY, dict);");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = Execute(req);  //=Execute(req, YsSession.SessionKey);");
            sb.AppendLine(FARGS.TAB3 + "if (!apiRes.ApiSucc || !apiRes.ActSucc)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "ActionResponse res = req.Response;");
            sb.AppendLine(FARGS.TAB3 + "EntitySet es = GetEntitySet(res, ENTITY_RES);");
            sb.AppendLine(FARGS.TAB3 + "Entity la =GetEntity(res, LISTATTR);");
            sb.AppendLine(FARGS.TAB3 + "apiRes.SetData(ENTITY_REQ, entity);");
            sb.AppendLine(FARGS.TAB3 + "apiRes.SetData(LISTATTR, la);");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[列表]");
            return sb.ToString();
        }

        public string CreateGetFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[获取]");
            sb.AppendLine(FARGS.TAB2 + "public ApiResponse Get(long id)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "Entity ei = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "ei.Set(\"id\",id);");
            sb.AppendLine(FARGS.TAB3 + "return GetEntity(ei);");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB2 + "public ApiResponse Get(Entity entity)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes;");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "Dictionary<string, object> dict = new Dictionary<string, object>();");
            sb.AppendLine(FARGS.TAB3 + "long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (entity.TryGetLong(\"id\", out id))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dict.Add(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "ActionRequest req = new ActionRequest(API_GET_URL);");
            sb.AppendLine(FARGS.TAB3 + "req.AddParam(ENTITY_REQ, dict);");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = Execute(req);  //=Execute(req, YsSession.SessionKey);");
            sb.AppendLine(FARGS.TAB3 + "if (!apiRes.ApiSucc || !apiRes.ActSucc)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "ActionResponse res = req.Response;");
            sb.AppendLine(FARGS.TAB3 + "entity = GetEntity(res, ENTITY_RES);");
            sb.AppendLine(FARGS.TAB3 + "apiRes.SetData(ENTITY_REQ, entity);");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[获取]");
            return sb.ToString();
        }

        public string CreateAddFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[新增]");
            sb.AppendLine(FARGS.TAB2 + "public ApiResponse Add(Entity entity)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes;");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "Dictionary<string, object> dict = new Dictionary<string, object>();");
            sb.AppendLine(FARGS.TAB3 + "long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (entity.TryGetLong(\"id\", out id))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dict.Add(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//添加其他实体参数属性到dict");
            sb.AppendLine(FARGS.TAB3 + "ActionRequest req = new ActionRequest(API_ADD_URL);");
            sb.AppendLine(FARGS.TAB3 + "req.AddParam(ENTITY_REQ, dict);");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = Execute(req, YsSession.SessionKey);");
            sb.AppendLine(FARGS.TAB3 + "if (!apiRes.ApiSucc || !apiRes.ActSucc)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "ActionResponse res = req.Response;");
            sb.AppendLine(FARGS.TAB3 + "entity = GetEntity(res, ENTITY_RES);");
            sb.AppendLine(FARGS.TAB3 + "apiRes.SetData(ENTITY_REQ, entity);");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[新增]");
            return sb.ToString();
        }

        public string CreateUpdateFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[更新]");
            sb.AppendLine(FARGS.TAB2 + "public ApiResponse Update(Entity entity)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes;");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "Dictionary<string, object> dict = new Dictionary<string, object>();");
            sb.AppendLine(FARGS.TAB3 + "long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (entity.TryGetLong(\"id\", out id))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dict.Add(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//添加其他实体参数属性到dict");
            sb.AppendLine(FARGS.TAB3 + "ActionRequest req = new ActionRequest(API_UPDATE_URL);");
            sb.AppendLine(FARGS.TAB3 + "req.AddParam(ENTITY_REQ, dict);");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = Execute(req, YsSession.SessionKey);");
            sb.AppendLine(FARGS.TAB3 + "if (!apiRes.ApiSucc || !apiRes.ActSucc)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "ActionResponse res = req.Response;");
            sb.AppendLine(FARGS.TAB3 + "entity = GetEntity(res, ENTITY_RES);");
            sb.AppendLine(FARGS.TAB3 + "apiRes.SetData(ENTITY_REQ, entity);");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[更新]");
            return sb.ToString();
        }

        public string CreateDeleteFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[删除]");
            sb.AppendLine(FARGS.TAB2 + "public ApiResponse Delete(Entity entity)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes;");
            sb.AppendLine(FARGS.TAB3 + "try");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "Dictionary<string, object> dict = new Dictionary<string, object>();");
            sb.AppendLine(FARGS.TAB3 + "long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (entity.TryGetLong(\"id\", out id))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "dict.Add(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "//添加其他实体参数属性到dict");
            sb.AppendLine(FARGS.TAB3 + "ActionRequest req = new ActionRequest(API_DELETE_URL);");
            sb.AppendLine(FARGS.TAB3 + "req.AddParam(ENTITY_REQ, dict);");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = Execute(req, YsSession.SessionKey);");
            sb.AppendLine(FARGS.TAB3 + "if (!apiRes.ApiSucc || !apiRes.ActSucc)");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "ActionResponse res = req.Response;");
            sb.AppendLine(FARGS.TAB3 + "entity = GetEntity(res, ENTITY_RES);");
            sb.AppendLine(FARGS.TAB3 + "apiRes.SetData(ENTITY_REQ, entity);");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "catch");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "return apiRes;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[删除]");
            return sb.ToString();
        }

        public string CreateAssistFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[私有辅助方法]");

            sb.AppendLine(FARGS.TAB2 + "////扩展实体属性");
            sb.AppendLine(FARGS.TAB2 + "private Entity ExtendEntityProp(Entity entity)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "string extPropName = \"extProp\";");
            sb.AppendLine(FARGS.TAB3 + "object extPropValue = null;");
            sb.AppendLine(FARGS.TAB3 + "entity.Set(extPropName,extPropValue);");
            sb.AppendLine(FARGS.TAB3 + "return entity;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[私有辅助方法]");
            return sb.ToString();
        }
    }
}
