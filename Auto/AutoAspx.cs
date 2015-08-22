using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成一般格式的后台ASPX页面
    /// </summary>
    public class AutoAspx
    {
        AutoObject ao;
        List<FieldObject> fdos;

        public AutoAspx(AutoObject autoObject, List<FieldObject> fieldObjects)
        {
            ao = autoObject;
            fdos = fieldObjects;
        }

        public string CreateCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<%@ Page Language=\"C#\" %>");
            sb.AppendLine("<!--文件命名一般以Manager作为后缀-->");
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine(CreateHead());
            sb.AppendLine(CreateBody());
            sb.AppendLine("</html>");
            return sb.ToString();
        }

        public string CreateHead()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<head>");
            sb.AppendLine(FARGS.TAB + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            sb.AppendLine(FARGS.TAB + "<title></title>");
            sb.AppendLine(FARGS.TAB + "<!--引入jQuery easyui类库和相关主题样式，图标，本地化，包括jQuery，jQuery cookie-->");
            sb.AppendLine(FARGS.TAB + "<link href=\"../../scripts/easyui/themes/default/easyui.css\" rel=\"stylesheet\" />");
            sb.AppendLine(FARGS.TAB + "<link href=\"../../scripts/easyui/themes/icon.css\" rel=\"stylesheet\" />");
            sb.AppendLine(FARGS.TAB + "<script src=\"../../scripts/easyui/jquery1.8.0.min.js\"></script>");
            sb.AppendLine(FARGS.TAB + "<script src=\"../../scripts/easyui/jquery1.3.2.easyui.min.js\"></script>");
            sb.AppendLine(FARGS.TAB + "<script src=\"../../scripts/easyui/locale/easyui-lang-zh_CN.js\"></script>");
            sb.AppendLine(FARGS.TAB + "<script src=\"../../scripts/easyui/jquery.cookie.js\"></script>");
            sb.AppendLine(FARGS.TAB + "<!--引入必要的第三方js类库-->");
            sb.AppendLine(FARGS.TAB + "<!--引入onyx类库-->");
            sb.AppendLine(FARGS.TAB + "<script src=\"../../res/libs/onyx/onyx-full.js\"></script>");
            sb.AppendLine(FARGS.TAB + "<!--引入页面对应逻辑的同名js-->");
            sb.AppendLine(FARGS.TAB + "<script src=" + ao.ClsName + "Manager.js\"></script>");
            sb.AppendLine(FARGS.TAB + "<!--引入公共js类库-->");
            sb.AppendLine(FARGS.TAB + "<script src=\"../../scripts/common.js\"></script>");
            sb.AppendLine(FARGS.TAB + "<!--自定义样式-->");
            sb.AppendLine("</head>");
            return sb.ToString();
        }

        public string CreateBody()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<body class=\"easyui-layout\">");
            sb.AppendLine(FARGS.TAB + "<div id=\"divCenter\" data-options=\"region:'center',title:'记录列表'\">");
            sb.AppendLine(FARGS.TAB2 + "<div id=\"divToolbar\">");
            sb.AppendLine(FARGS.TAB3 + "<div id=\"divSearch\">");
            sb.AppendLine(FARGS.TAB4 + "状态<select id=\"selStatus\" class=\"easyui-combobox\" panelheight=\"auto\">");
            sb.AppendLine(FARGS.TAB5 + "<option value=\"\" selected=\"selected\">全部</option>");
            sb.AppendLine(FARGS.TAB5 + "<option value=\"1\">启用</option>");
            sb.AppendLine(FARGS.TAB5 + "<option value=\"0\">禁用</option>");
            sb.AppendLine(FARGS.TAB4 + "</select>");
            sb.AppendLine(FARGS.TAB4 + "关键字<input id=\"inpKeyword\" type=\"text\" maxlength=\"24\" />");
            sb.AppendLine(FARGS.TAB4 + "<a id=\"btnSearch\" href=\"#\" class=\"easyui-linkbutton\" plain=\"true\" iconcls=\"icon-search\">搜索</a>");
            sb.AppendLine(FARGS.TAB4 + "<a id=\"btnAdd\" href=\"#\" class=\"easyui-linkbutton\" iconcls=\"icon-add\" plain=\"true\">新建</a>");
            sb.AppendLine(FARGS.TAB4 + "<a id=\"btnEdit\" href=\"#\" class=\"easyui-linkbutton\" iconcls=\"icon-edit\" plain=\"true\">编辑</a>");
            sb.AppendLine(FARGS.TAB4 + "<a id=\"btnDelete\" href=\"#\" class=\"easyui-linkbutton\" iconcls=\"icon-remove\" plain=\"true\">删除</a>");
            sb.AppendLine(FARGS.TAB4 + "<a id=\"btnCancel\" href=\"#\" class=\"easyui-linkbutton\" iconcls=\"icon-undo\" plain=\"true\">取消选择</a>");
            sb.AppendLine(FARGS.TAB3 + "</div>");
            sb.AppendLine(FARGS.TAB2 + "</div>");
            sb.AppendLine(FARGS.TAB2 + "<div id=\"divDataList\" style=\"width: 100%; height: 100%;\">");
            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB2 + "</div>");
            sb.AppendLine(FARGS.TAB + "</div>");
            sb.AppendLine();
            sb.AppendLine(FARGS.TAB + "<div id=\"divEdit\" class=\"easyui-window\" title=\"编辑记录\" style=\"width: 500px; height: 300px;\"");
            sb.AppendLine(FARGS.TAB2 + "data-options=\"iconCls:'icon-save',modal:true, closed:true, draggable: true, minimizable:false,resizable:false\">");
            sb.AppendLine(FARGS.TAB2 + "<div class=\"easyui-layout\" data-options=\"fit:true\">");
            sb.AppendLine(FARGS.TAB3 + "<div data-options=\"region:'center'\">");
            sb.AppendLine(FARGS.TAB4 + "<form id=\"formEdit\" method=\"post\">");
            sb.AppendLine(FARGS.TAB5 + "<table style=\"width:auto;\">");
            sb.AppendLine(FARGS.TAB6 + "<tr>");
            sb.AppendLine(FARGS.TAB7 + "<td>记录ID：</td>");
            sb.AppendLine(FARGS.TAB7 + "<td>");
            sb.AppendLine(FARGS.TAB8 + "<input id=\"action\" name=\"action\" hidden=\"hidden\"/><!--动作：add，update-->");
            sb.AppendLine(FARGS.TAB8 + "<input id=\"__t\" name=\"__t\" hidden=\"hidden\"/><!--后台SessionKey-->");
            sb.AppendLine(FARGS.TAB8 + "<input id=\"id\" name=\"id\" value=\"\" style=\"border: none;\" readonly=\"readonly\" />");
            sb.AppendLine(FARGS.TAB7 + "</td>");
            sb.AppendLine(FARGS.TAB7 + "<td>状态：</td>");
            sb.AppendLine(FARGS.TAB7 + "<td>");
            sb.AppendLine(FARGS.TAB8 + "<select id=\"status\" name=\"status\" class=\"easyui-combobox\" panelheight=\"auto\">");
            sb.AppendLine(FARGS.TAB8 + FARGS.TAB + "<option value=\"0\">禁用</option>");
            sb.AppendLine(FARGS.TAB8 + FARGS.TAB + "<option value=\"1\">启用</option>");
            sb.AppendLine(FARGS.TAB8 + "</select>");
            sb.AppendLine(FARGS.TAB7 + "</td>");
            sb.AppendLine(FARGS.TAB6 + "</tr>");
            sb.AppendLine(FARGS.TAB6 + "<tr>");
            sb.AppendLine(FARGS.TAB7 + "");
            sb.AppendLine(FARGS.TAB7 + "");
            sb.AppendLine(FARGS.TAB6 + "</tr>");
            sb.AppendLine(FARGS.TAB6 + "<tr>");
            sb.AppendLine(FARGS.TAB7 + "<td style=\"text-align:center;\" colspan=\"4\">");
            sb.AppendLine(FARGS.TAB8 + "<a id=\"formSubmit\" href=\"#\" class=\"easyui-linkbutton\" plain=\"true\" iconcls=\"icon-save\">确认</a>");
            sb.AppendLine(FARGS.TAB8 + "<a id=\"formCancel\" href=\"#\" class=\"easyui-linkbutton\" plain=\"true\" iconcls=\"icon-undo\">取消</a>");
            sb.AppendLine(FARGS.TAB7 + "</td>");
            sb.AppendLine(FARGS.TAB6 + "</tr>");
            sb.AppendLine(FARGS.TAB4 + "</form>");
            sb.AppendLine(FARGS.TAB3 + "</div>");
            sb.AppendLine(FARGS.TAB2 + "</div>");
            sb.AppendLine(FARGS.TAB + "</div>");
            sb.AppendLine("</body>");
            return sb.ToString();
        }
    }
}
