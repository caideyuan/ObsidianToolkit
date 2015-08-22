using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成前台项目视图类文件
    /// </summary>
    public class AutoView
    {
        AutoObject ao;
        List<FieldObject> fdos;

        public AutoView()
        {
        }
        public AutoView(AutoObject autoObject, List<FieldObject> fieldObjects)
        {
            ao = autoObject;
            fdos = fieldObjects;
        }

        public string CreateCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<%@ Page Language=\"C#\" %>");
            sb.AppendLine("");
            sb.AppendLine("<%@ Import Namespace=\"Obsidian.Mvc\" %>");
            sb.AppendLine("<%@ Import Namespace=\"Obsidian.Tools\"%>");
            sb.AppendLine("<%@ Import Namespace=\"Obsidian.Utils\"%>");
            sb.AppendLine("<%@ Import Namespace=\"Obsidian.Web\" %>");            
            sb.AppendLine("<%@ Import Namespace=\"Obsidian.Web.Ui\" %>");
            sb.AppendLine("<%@ Import Namespace=\"YS.SDK\" %>");
            sb.AppendLine("");
            sb.AppendLine(CreateView());
            sb.AppendLine(CreateHtml());
            //sb.AppendLine(CreateConentView());    //内容视图
            return sb.ToString();
        }

        public string CreateView()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<%");
            sb.AppendLine(FARGS.TAB + "OView vMain = OView.CurrentView; //载入主视图");
            sb.AppendLine(FARGS.TAB + "//OView vHtmlHead;");
            sb.AppendLine(FARGS.TAB + "//vMain.TryGetData<OView>(\"vHtmlHead\", out vHtmlHead);   //载入Html Head视图");
            sb.AppendLine(FARGS.TAB + "OView vHead;");
            sb.AppendLine(FARGS.TAB + "vMain.TryGetData<OView>(\"vHead\", out vHead);   //载入头部视图");
            sb.AppendLine(FARGS.TAB + "OView vFoot;");
            sb.AppendLine(FARGS.TAB + "vMain.TryGetData<OView>(\"vFoot\", out vFoot);   //载入底部视图");
            sb.AppendLine(FARGS.TAB + "Entity oe = (Entity)vMain.GetData(\"\");  //主结果集");
            sb.AppendLine(FARGS.TAB + "EntitySet oes = (EntitySet)vMain.GetData(\"\");  //主结果集列表");
            sb.AppendLine(FARGS.TAB + "Entity listAttr = (Entity)vMain.GetData(\"listAttr\");   //分页结果集");
            sb.AppendLine(FARGS.TAB + "int pageNo = listAttr.GetInt(\"pageNo\");");
            sb.AppendLine(FARGS.TAB + "int pagesCount = listAttr.GetInt(\"pagesCount\");");
            sb.AppendLine(FARGS.TAB + "{");
            sb.AppendLine(FARGS.TAB + "System.Web.HttpContext.Current.Response.Clear();");
            sb.AppendLine(FARGS.TAB + "System.Web.HttpContext.Current.Response.Status = \"404 Not Found\";");
            sb.AppendLine(FARGS.TAB + "WebUtil.ResponseEnd();");
            sb.AppendLine(FARGS.TAB + "}");
            sb.AppendLine(FARGS.TAB + "PagingBar pagingBar = new PagingBar(null, pageNo, pagesCount);  //第一个参数设置为null则不自动加页码，改为pageNo则自动加页码");
            sb.AppendLine(FARGS.TAB + "pagingBar.ShowNum = 5;  //中间页数个数");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine("%>");
            return sb.ToString();
        }

        public string CreateHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<!--自定义HTML Head-->");
            sb.AppendLine(FARGS.TAB + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            sb.AppendLine(FARGS.TAB + "<link rel=\"shortcut icon\" type=\"image/x-icon\" href=\"http://www.yingsheng.com/Content/images/favicon.ico\" />");
            sb.AppendLine(FARGS.TAB + "<title></title>");
            sb.AppendLine(FARGS.TAB + "<meta content=\"\" name=\"keywords\" />");
            sb.AppendLine(FARGS.TAB + "<meta content=\"\" name=\"description\" />");
            sb.AppendLine(FARGS.TAB + "<link rel=\"stylesheet\" type=\"text/css\" href=\"\" />");
            sb.AppendLine(FARGS.TAB + "<script type=\"text/javascript\" src=\"\"></script>");
            sb.AppendLine("<!--统一HTML Head-->");
            sb.AppendLine(FARGS.TAB + "<!--<% vHtmlHead.Display(); %>-->");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<!--统一BODY头部-->");
            sb.AppendLine(FARGS.TAB + "<!--<% vHead.Display(); %>-->");
            sb.AppendLine("<!--------头部开始-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "<!--------导航开始-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "<!--------导航结束-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine("<!--------头部结束-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "<!--------内容开始-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "<!--------内容结束-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine("<!--------底部开始-------->");
            sb.AppendLine("<!--统一BODY底部-->");
            sb.AppendLine(FARGS.TAB + "<!--<% vFoot.Display(); %>-->");
            sb.AppendLine("<!--------底部结束-------->");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }

        public string CreateConentView()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<!--统一HTML Head-->");
            sb.AppendLine(FARGS.TAB + "<!--<% vHtmlHead.Display(); %>-->");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<!--统一BODY头部-->");
            sb.AppendLine(FARGS.TAB + "<% vHead.Display(); %>");
            sb.AppendLine("<!--------头部开始-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "<!--------导航开始-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "<!--------导航结束-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine("<!--------头部结束-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "<!--------内容开始-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine(FARGS.TAB + "<!--------内容结束-------->");
            sb.AppendLine(FARGS.TAB + "");
            sb.AppendLine("<!--------底部开始-------->");
            sb.AppendLine("<!--统一BODY底部-->");
            sb.AppendLine(FARGS.TAB + "<% vFoot.Display(); %>");
            sb.AppendLine("<!--------底部结束-------->");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }
    }
}
