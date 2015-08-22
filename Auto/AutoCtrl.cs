using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成前台项目控制器类文件
    /// </summary>
    public class AutoCtrl
    {
        AutoObject ao;
        List<FieldObject> fdos;
        string ctrl = "";
        string api = "";

        public AutoCtrl()
        {
        }
        public AutoCtrl(AutoObject autoObject, List<FieldObject> fieldObjects)
        {
            ao = autoObject;
            fdos = fieldObjects;
            ctrl = ao.ClsName + "Ctrl";
            api = ao.ClsName + "API";
        }

        public string CreateCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using Obsidian.Mvc;");
            sb.AppendLine("using Obsidian.Runtime;");
            sb.AppendLine("using Obsidian.Tools;");
            sb.AppendLine("using Obsidian.Utils;");
            sb.AppendLine("using Obsidian.Web;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using YS.SDK;");
            sb.AppendLine();
            //
            sb.AppendLine("namespace " + ao.CtrlNS);
            sb.AppendLine("{");
            //
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "[" + ao.ClsName + " 前端业务Ctrl类（View和SDK的桥梁）]");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "0.继承YS.Web.Ctrls.YsCtrl （基类YsCtrl封装了检查接口调用，页面跳转，错误页面的方法）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "1.需要引入Obsidian.Mvc （Obsidian MVC 框架）");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY + "3.命名规范：以Ctrl结尾");
            sb.AppendLine(FARGS.TAB + FARGS.SUMMARY_END);
            //
            sb.AppendLine(FARGS.TAB + "public class " + ctrl + " : " + ao.CtrlBaseCls);
            sb.AppendLine(FARGS.TAB + "{");
            //
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[Ctrl方法]");
            sb.AppendLine(CreateListFunction());
            sb.AppendLine(CreateGetFunction());
            sb.AppendLine(CreateAddFunction());
            sb.AppendLine(CreateUpdateFunction());
            sb.AppendLine(CreateDeleteFunction());
            sb.AppendLine(CreateAssistFunction());
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[Ctrl方法]");
            //
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine("}");
            return sb.ToString();
        }

        public string CreateIndexFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[Index]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public void Index(HttpContext context)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + "long id;");
            sb.AppendLine(FARGS.TAB3 + "if (!ParamsReceiver.TryGetInt64(\"id\", out id))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "this.Error(404);");
            sb.AppendLine(FARGS.TAB4 + "return;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "OView vMain = new OView(\"/\");    //主视图文件设置");
            sb.AppendLine(FARGS.TAB3 + "OView vHead = new OView(\"Base/Head\"); //头部视图文件设置");
            sb.AppendLine(FARGS.TAB3 + "OView vFoot = new OView(\"Base/Foot\"); //尾部视图文件设置");
            sb.AppendLine(FARGS.TAB3 + "vMain.SetData(\"vHead\", vHead);");
            sb.AppendLine(FARGS.TAB3 + "vMain.SetData(\"vFoot\", vFoot);");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "vMain.SetData(\"isLogin\", YsSession.IsLogin);");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 数据接收提交");
            sb.AppendLine(FARGS.TAB3 + "Entity oPrms = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "oPrms.Set(\"\",\"\");");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "vHead.SetData(\"\",oPrms);");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 数据接收提交");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "vMain.Display();");
            sb.AppendLine(FARGS.TAB2 + "}");
            return sb.ToString();
        }

        public string CreateListFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[List]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public void List(HttpContext context)");
            sb.AppendLine(FARGS.TAB2 + "{");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "int pageNo = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (!ParamsReceiver.TryGetInt32(\"pageNo\", out pageNo) || (pageNo <= 0))");
            sb.AppendLine(FARGS.TAB4 + "pageNo = 1;");
            sb.AppendLine(FARGS.TAB3 + "int pageSize = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (!ParamsReceiver.TryGetInt32(\"pageSize\", out pageSize) || (pageSize <= 0))");
            sb.AppendLine(FARGS.TAB4 + "pageSize = 1;");
            sb.AppendLine(FARGS.TAB3 + "//long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "//if (!ParamsReceiver.TryGetInt64(\"id\", out id) || (id <= 0))");
            sb.AppendLine(FARGS.TAB4 + "//id = 0;");
            sb.AppendLine(FARGS.TAB3 + "string orderField = \"created\";");
            sb.AppendLine(FARGS.TAB3 + "string orderType = \"DESC\";");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 视图设置");
            sb.AppendLine(FARGS.TAB3 + "OView vMain = new OView(\"/\");    //主视图文件设置");
            sb.AppendLine(FARGS.TAB3 + "//OView vHtmlHead = new OView(\"Base/HtmlHead\"); //Html Head 视图文件设置");
            sb.AppendLine(FARGS.TAB3 + "//vMain.SetData(\"vHtmlHead\", vHtmlHead);");
            sb.AppendLine(FARGS.TAB3 + "OView vHead = new OView(\"Base/Head\"); //头部视图文件设置");
            sb.AppendLine(FARGS.TAB3 + "OView vFoot = new OView(\"Base/Foot\"); //尾部视图文件设置");
            sb.AppendLine(FARGS.TAB3 + "vMain.SetData(\"vHead\", vHead);");
            sb.AppendLine(FARGS.TAB3 + "vMain.SetData(\"vFoot\", vFoot);");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 视图设置");
            sb.AppendLine(FARGS.TAB3 + "");
            
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 数据接收提交");
            sb.AppendLine(FARGS.TAB3 + "Entity oPrms = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "oPrms.Set(\"pageNo\", pageNo);");
            sb.AppendLine(FARGS.TAB3 + "oPrms.Set(\"pageSize\", pageSize);");
            sb.AppendLine(FARGS.TAB3 + "oPrms.Set(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "vHead.SetData(\"oPrms\", oPrms);");
            sb.AppendLine(FARGS.TAB3 + "vFoot.SetData(\"oPrms\", oPrms);");
            sb.AppendLine(FARGS.TAB3 + "vMain.SetData(\"oPrms\", oPrms);");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 数据接收提交");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "Entity prms = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "prms.Set(\"pageNo\", pageNo);");
            sb.AppendLine(FARGS.TAB3 + "prms.Set(\"pageSize\", pageSize);");
            sb.AppendLine(FARGS.TAB3 + "prms.Set(\"orderField\", pageSize);");
            sb.AppendLine(FARGS.TAB3 + "prms.Set(\"orderType\", pageSize);");
            sb.AppendLine(FARGS.TAB3 + api + " reqAPI = new " + api + "();");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = reqAPI.GetList(prms);  //调用SDK的GetList方法");
            sb.AppendLine(FARGS.TAB3 + "if (!this.CheckApiResponse(apiRes))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "this.Error(500, \"列表数据返回错误\", apiRes.ActMessage);");
            sb.AppendLine(FARGS.TAB4 + "return;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "//根据调用返回参数设置数据");
            sb.AppendLine(FARGS.TAB3 + "EntitySet es = (EntitySet)apiRes.GetData(\"\"); //主结果集返回参数名");
            sb.AppendLine(FARGS.TAB3 + "Entity listAttr = (Entity)apiRes.GetData(\"listAttr\"); //分页结果集返回参数名");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 视图添加数据");
            sb.AppendLine(FARGS.TAB3 + "vMain.SetData(\"\", es);");
            sb.AppendLine(FARGS.TAB3 + "vMain.SetData(\"listAttr\", listAttr);");
            sb.AppendLine(FARGS.TAB3 + "vMain.Display();   //显示主视图");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 视图添加数据");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB2 + "}");
            return sb.ToString();
        }

        public string CreateGetFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[Get]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public void Get(HttpContext context)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (!ParamsReceiver.TryGetInt64(\"id\", out id) || (id <= 0))");
            sb.AppendLine(FARGS.TAB4 + "id = 0;");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "Entity oResult = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "Entity prms = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "prms.Set(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + api + " reqAPI = new " + api + "();");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = reqAPI.Get(prms);  //调用SDK的Get方法");
            sb.AppendLine(FARGS.TAB3 + "if (!this.CheckApiResponse(apiRes, false))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "oResult.Set(\"code\", 0);");
            sb.AppendLine(FARGS.TAB4 + "oResult.Set(\"msg\", apiRes.ActMessage);");
            sb.AppendLine(FARGS.TAB4 + "context.Response.Write(oResult.ToJsonString());");
            sb.AppendLine(FARGS.TAB4 + "return;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "Entity et = (Entity)apiRes.GetData(\"\"); //主结果集返回参数名");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB2 + "}");
            return sb.ToString();
        }

        public string CreateAddFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[Add]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public void Add(HttpContext context)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (!ParamsReceiver.TryGetInt64(\"id\", out id) || (id <= 0))");
            sb.AppendLine(FARGS.TAB4 + "id = 0;");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "Entity oResult = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "Entity prms = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "prms.Set(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + api + " reqAPI = new " + api + "();");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = reqAPI.Get(prms);  //调用SDK的Get方法");
            sb.AppendLine(FARGS.TAB3 + "if (!this.CheckApiResponse(apiRes, false))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "oResult.Set(\"code\", 0);");
            sb.AppendLine(FARGS.TAB4 + "oResult.Set(\"msg\", apiRes.ActMessage);");
            sb.AppendLine(FARGS.TAB4 + "context.Response.Write(oResult.ToJsonString());");
            sb.AppendLine(FARGS.TAB4 + "return;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "Entity et = (Entity)apiRes.GetData(\"\"); //主结果集返回参数名");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB2 + "}");
            return sb.ToString();
        }

        public string CreateUpdateFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[Update]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public void Update(HttpContext context)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (!ParamsReceiver.TryGetInt64(\"id\", out id) || (id <= 0))");
            sb.AppendLine(FARGS.TAB4 + "id = 0;");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "Entity oResult = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "Entity prms = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "prms.Set(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + api + " reqAPI = new " + api + "();");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = reqAPI.Get(prms);  //调用SDK的Get方法");
            sb.AppendLine(FARGS.TAB3 + "if (!this.CheckApiResponse(apiRes, false))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "oResult.Set(\"code\", 0);");
            sb.AppendLine(FARGS.TAB4 + "oResult.Set(\"msg\", apiRes.ActMessage);");
            sb.AppendLine(FARGS.TAB4 + "context.Response.Write(oResult.ToJsonString());");
            sb.AppendLine(FARGS.TAB4 + "return;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "Entity et = (Entity)apiRes.GetData(\"\"); //主结果集返回参数名");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB2 + "}");
            return sb.ToString();
        }

        public string CreateDeleteFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_BEGIN);
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY + "[Delete]");
            sb.AppendLine(FARGS.TAB2 + FARGS.SUMMARY_END);
            sb.AppendLine(FARGS.TAB2 + "public void Delete(HttpContext context)");
            sb.AppendLine(FARGS.TAB2 + "{");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "long id = 0;");
            sb.AppendLine(FARGS.TAB3 + "if (!ParamsReceiver.TryGetInt64(\"id\", out id) || (id <= 0))");
            sb.AppendLine(FARGS.TAB4 + "id = 0;");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 数据接收处理，检查参数");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_BEGIN + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "Entity oResult = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "Entity prms = new Entity();");
            sb.AppendLine(FARGS.TAB3 + "prms.Set(\"id\", id);");
            sb.AppendLine(FARGS.TAB3 + api + " reqAPI = new " + api + "();");
            sb.AppendLine(FARGS.TAB3 + "ApiResponse apiRes = reqAPI.Get(prms);  //调用SDK的Get方法");
            sb.AppendLine(FARGS.TAB3 + "if (!this.CheckApiResponse(apiRes, false))");
            sb.AppendLine(FARGS.TAB3 + "{");
            sb.AppendLine(FARGS.TAB4 + "oResult.Set(\"code\", 0);");
            sb.AppendLine(FARGS.TAB4 + "oResult.Set(\"msg\", apiRes.ActMessage);");
            sb.AppendLine(FARGS.TAB4 + "context.Response.Write(oResult.ToJsonString());");
            sb.AppendLine(FARGS.TAB4 + "return;");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "Entity result = (Entity)apiRes.GetData(\"result\"); //主结果集返回参数名");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + FARGS.REGION_END + " 调用请求数据");
            sb.AppendLine(FARGS.TAB3 + "");

            sb.AppendLine(FARGS.TAB2 + "}");
            return sb.ToString();
        }

        public string CreateAssistFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_BEGIN + "[私有辅助方法]");
            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB2 + FARGS.REGION_END + "[私有辅助方法]");
            return sb.ToString();
        }
    }
}
