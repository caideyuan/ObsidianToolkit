using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObAuto
{
    /// <summary>
    /// 自动代码类：生成后台Aspx页面对应的JS文件
    /// </summary>
    public class AutoJs
    {
        AutoObject ao;
        List<FieldObject> fdos;
        string apiClsName = "";

        public AutoJs(AutoObject autoObject, List<FieldObject> fieldObjects)
        {
            ao = autoObject;
            fdos = fieldObjects;
            apiClsName = StringUtil.FristLower(ao.ClsName, "_");
        }

        public string CreateCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(CreateDocumentReady());
            sb.AppendLine(CreateApiFunction());
            return sb.ToString();
        }

        public string CreateDocumentReady()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*[BEGIN] 0.$(document).ready */");
            sb.AppendLine("/**[BEGIN] 1.ajax调用配置 **/");
            sb.AppendLine(FARGS.TAB + "var DomUtil = onyx.util.DomUtil,EventUtil = onyx.util.EventUtil;");
            sb.AppendLine(FARGS.TAB + "onyx.conf({");
            sb.AppendLine(FARGS.TAB2 + "/** AJAX 配置选项，配置默认服务端处理程序ashx */");
            sb.AppendLine(FARGS.TAB2 + "ajax: {defaultHandler: \"ysapi\",handlers: { ysapi: \"../../Admin/Ajax.ashx\"}}");
            sb.AppendLine(FARGS.TAB + "});");
            sb.AppendLine("/**[END] 1.ajax调用配置 **/");
            //
            sb.AppendLine("/**[BEGIN] datagrid自适应大小**/");
            sb.AppendLine(FARGS.TAB + "window.onresize = function () {setTimeout(function () {$('#tbDataList').datagrid('resize', {height: $('#divDataList').height(),width: $('#divDataList').width()});}, 10);};");
            sb.AppendLine(FARGS.TAB + "$('#divCenter').panel({onResize: function () {setTimeout(function () {$('#tbDataList').datagrid('resize', {height: $('#divDataList').height(),width: $('#divDataList').width()});}, 10);}});");
            sb.AppendLine("/**[END] datagrid自适应大小**/");
            //
            sb.AppendLine("/**[BEGIN] 2.数据控件初始化**/");
            sb.AppendLine(FARGS.TAB + "$('#tbDataList').datagrid({");
            sb.AppendLine(FARGS.TAB2 + "iconCls: 'icon-search',");
            sb.AppendLine(FARGS.TAB2 + "rownumbers: true,");
            sb.AppendLine(FARGS.TAB2 + "pagination: true,");
            sb.AppendLine(FARGS.TAB2 + "pageSize: 20,");
            sb.AppendLine(FARGS.TAB2 + "pageList: [20],");
            sb.AppendLine(FARGS.TAB2 + "fit: true,");
            sb.AppendLine(FARGS.TAB2 + "singleSelect: true,");
            sb.AppendLine(FARGS.TAB2 + "toolbar: '#divToolbar',");
            sb.AppendLine(FARGS.TAB2 + "nowrap: true,");
            sb.AppendLine(FARGS.TAB2 + "loadMsg: '正在努力加载数据...',");
            sb.AppendLine(FARGS.TAB2 + "onLoadError: function () {console.log(\"加载数据失败！\");}");
            sb.AppendLine(FARGS.TAB + "});");
            sb.AppendLine(FARGS.TAB + ao.ClsName + ".getList(1, 20, function () { });");
            sb.AppendLine("/**[END] 2.数据控件初始化**/");
            //
            sb.AppendLine("/**[BEGIN] 3.监听按钮事件**/");
            sb.AppendLine(FARGS.TAB + "$('#btnSearch').click(function () {");
            sb.AppendLine(FARGS.TAB2 + "console.log(\"btnSearch Click!\");");
            sb.AppendLine(FARGS.TAB2 + ao.ClsName + ".getList(1, 20);");
            sb.AppendLine(FARGS.TAB2 + "$('#tbDataList').datagrid('getPager').pagination({");
            sb.AppendLine(FARGS.TAB3 + "onSelectPage: function (pageNo, pageSize) {");
            sb.AppendLine(FARGS.TAB4 + "if (pageNo == undefined) pageNo = 1;");
            sb.AppendLine(FARGS.TAB4 + ao.ClsName + ".getList(pageNo, pageSize);");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "});");
            sb.AppendLine(FARGS.TAB + "});");
            //
            sb.AppendLine(FARGS.TAB + "$('#btnAdd').click(function () {");
            sb.AppendLine(FARGS.TAB2 + "console.log(\"btnAdd Click!\");");
            sb.AppendLine(FARGS.TAB2 + "$('#formEdit').form('reset');");
            sb.AppendLine(FARGS.TAB2 + "$('#formEdit').form('load', {");
            sb.AppendLine(FARGS.TAB3 + "action: 'add',  //新增");
            sb.AppendLine(FARGS.TAB3 + "__t: $.cookie('__SESSION_KEY'),    //获取后台用户SessionKey");
            sb.AppendLine(FARGS.TAB3 + "//设置form控件值");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB2 + "});");
            sb.AppendLine(FARGS.TAB2 + "$('#divEdit').window('open');");
            sb.AppendLine(FARGS.TAB + "});");
            //
            sb.AppendLine(FARGS.TAB + "$('#btnEdit').click(function () {");
            sb.AppendLine(FARGS.TAB2 + "console.log(\"btnEdit Click!\");");
            sb.AppendLine(FARGS.TAB2 + "$('#formEdit').form('reset');");
            sb.AppendLine(FARGS.TAB2 + "var row = $('#tbDataList').datagrid('getSelected');");
            sb.AppendLine(FARGS.TAB2 + "if (row) {");
            sb.AppendLine(FARGS.TAB3 + "$('#formEdit').form('load', row);");
            sb.AppendLine(FARGS.TAB3 + "$('#action').val('update'); //更新");
            sb.AppendLine(FARGS.TAB3 + "$('#__t').val($.cookie('__SESSION_KEY'));   //获取后台用户SessionKey");
            sb.AppendLine(FARGS.TAB3 + "//设置form控件值");
            sb.AppendLine(FARGS.TAB3 + "");
            sb.AppendLine(FARGS.TAB3 + "$('#divEdit').window('open');");
            sb.AppendLine(FARGS.TAB2 + "} else {");
            sb.AppendLine(FARGS.TAB3 + "$.messager.alert('警告', '没有选择任何记录！', 'warning');");
            sb.AppendLine(FARGS.TAB3 + "return;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB + "});");
            //
            sb.AppendLine(FARGS.TAB + "$('#btnDelete').click(function () {");
            sb.AppendLine(FARGS.TAB2 + "console.log(\"btnDelete Click!\");");
            sb.AppendLine(FARGS.TAB2 + "var row = $('#tbDataList').datagrid('getSelected');");
            sb.AppendLine(FARGS.TAB2 + "if (row) {");
            sb.AppendLine(FARGS.TAB3 + "$.messager.confirm('提示', '确定删除记录[' + row.Id + ']吗？', function (r) {");
            sb.AppendLine(FARGS.TAB4 + "if (r) {");
            sb.AppendLine(FARGS.TAB5 + "var o = {};");
            sb.AppendLine(FARGS.TAB5 + "o.Id = row.Id;");
            sb.AppendLine(FARGS.TAB5 + ao.ClsName + ".deleteItem(o);");
            sb.AppendLine(FARGS.TAB4 + "} else {");
            sb.AppendLine(FARGS.TAB5 + "$.messager.show({ title: '信息', msg: '已经取消了删除操作' });");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "});");
            sb.AppendLine(FARGS.TAB2 + "} else {");
            sb.AppendLine(FARGS.TAB3 + "$.messager.alert('警告', '没有选择任何记录！', 'warning');");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB + "});");
            //
            sb.AppendLine(FARGS.TAB + "$('#btnCancel').click(function () {");
            sb.AppendLine(FARGS.TAB2 + "console.log(\"btnCancel Click!\");");
            sb.AppendLine(FARGS.TAB2 + "$('#tbDataList').datagrid('unselectAll');");
            sb.AppendLine(FARGS.TAB2 + "$('#tbDataList').datagrid('uncheckAll');");
            sb.AppendLine(FARGS.TAB + "});");
            //
            sb.AppendLine(FARGS.TAB + "$('#formSubmit').click(function () {");
            sb.AppendLine(FARGS.TAB2 + "console.log(\"formSubmit Click!\");");
            sb.AppendLine(FARGS.TAB2 + "//获取form控件值，判断输入值有效性");
            sb.AppendLine(FARGS.TAB2 + "var action = $('#action').val();    //判断动作类型");
            sb.AppendLine(FARGS.TAB2 + "var o = {}; //构造请求参数实体");
            sb.AppendLine(FARGS.TAB2 + "switch (action) {");
            sb.AppendLine(FARGS.TAB3 + "case \"add\":");
            sb.AppendLine(FARGS.TAB4 + ao.ClsName + ".addItem(o);");
            sb.AppendLine(FARGS.TAB4 + "break;");
            sb.AppendLine(FARGS.TAB3 + "case \"update\":");
            sb.AppendLine(FARGS.TAB4 + "o.Id = $('#id').val();");
            sb.AppendLine(FARGS.TAB4 + ao.ClsName + ".updateItem(o);");
            sb.AppendLine(FARGS.TAB4 + "break;");
            sb.AppendLine(FARGS.TAB3 + "default:");
            sb.AppendLine(FARGS.TAB4 + "var err = \"请求接口动作错误\";");
            sb.AppendLine(FARGS.TAB4 + "console(err);");
            sb.AppendLine(FARGS.TAB4 + "alert(err);");
            sb.AppendLine(FARGS.TAB4 + "break;");
            sb.AppendLine(FARGS.TAB2 + "}");
            sb.AppendLine(FARGS.TAB + "});");
            //
            sb.AppendLine(FARGS.TAB + "$('#formCancel').click(function () {");
            sb.AppendLine(FARGS.TAB2 + "console.log(\"formCancel Click!\");");
            sb.AppendLine(FARGS.TAB2 + "$('#divEdit').window('close');");
            sb.AppendLine(FARGS.TAB2 + "//清空form控件值（视具体情况）");
            sb.AppendLine(FARGS.TAB2 + "");
            sb.AppendLine(FARGS.TAB + "});");
            sb.AppendLine("/**[END] 3.监听按钮事件**/");
            //
            sb.AppendLine("/*[END] 0.$(document).ready*/");
            return sb.ToString();
        }

        public string CreateApiFunction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*[BEGIN] Onyx Ajax API接口调用方法*/");
            sb.AppendLine("var " + ao.ClsName + " = {");
            sb.AppendLine(FARGS.TAB + "onyxAjax: function (o) {");
            sb.AppendLine(FARGS.TAB2 + "var ActReqHandler = onyx.widget.data.ActReqHandler; //引入接口调用处理类");
            sb.AppendLine(FARGS.TAB2 + "ActReqHandler.post(o, {");
            sb.AppendLine(FARGS.TAB3 + "onLoad: function (rso) {    //接口调用返回，首先会回调此方法");
            sb.AppendLine(FARGS.TAB4 + "console.log(\"ActReqHandler Post Result: \" + rso.st);  //返回状态：OK,ERROR,SESSION_KEY_INVALID");
            sb.AppendLine(FARGS.TAB4 + "//return false; //返回false时，不会调用请求的callback");
            sb.AppendLine(FARGS.TAB3 + "},");
            sb.AppendLine(FARGS.TAB4 + "onJsonParseErr: function () {   //当返回结果集无法转成JS对象时触发");
            sb.AppendLine(FARGS.TAB4 + "var err = \"Json Parse Error!\";");
            sb.AppendLine(FARGS.TAB4 + "console.log(err);");
            sb.AppendLine(FARGS.TAB3 + "},");
            sb.AppendLine(FARGS.TAB3 + "handler: \"ysapi\"  //ajax调用配置，不传会选择默认配置项（参见onyx.conf({})）");
            sb.AppendLine(FARGS.TAB2 + "});");
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine(FARGS.TAB + "getList: function (pageNo, pageSize, func) {");
            sb.AppendLine(FARGS.TAB2 + "//定义查询条件");
            sb.AppendLine(FARGS.TAB2 + "var status = $('#selStatus').combobox('getValue');");
            sb.AppendLine(FARGS.TAB2 + "var keyword = $('#inpKeyword').val();");
            sb.AppendLine(FARGS.TAB2 + "//设置默认分页");
            sb.AppendLine(FARGS.TAB2 + "if (!pageNo) pageNo = 1;");
            sb.AppendLine(FARGS.TAB2 + "if (!pageSize) pageSize = 20;");
            sb.AppendLine(FARGS.TAB2 + "//设置接口名称，请求参数名，返回参数名（根据具体接口要求进行配置）");
            sb.AppendLine(FARGS.TAB2 + "var url = \"." + apiClsName + ".list\";");
            sb.AppendLine(FARGS.TAB2 + "var reqPrms = { 'keyword': keyword, 'status': status, 'pageNo': pageNo, 'pageSize': pageSize };");
            sb.AppendLine(FARGS.TAB2 + "var resPrms = \"" + apiClsName + "s\", laname = \"listAttr\", rs = { \"total\": 0, \"rows\": [] };   //主结果集，分页，适应easyui的datagrid结果集");
            sb.AppendLine(FARGS.TAB2 + "this.onyxAjax({");
            sb.AppendLine(FARGS.TAB3 + "act: url,");
            sb.AppendLine(FARGS.TAB3 + "prms: { qry: reqPrms },");
            sb.AppendLine(FARGS.TAB3 + "callback: function (rt) {");
            sb.AppendLine(FARGS.TAB4 + "if (!rt.succ()) {");
            sb.AppendLine(FARGS.TAB5 + "console.log(\"接口[\" + url + \"]返回错误[\" + rt.msg() + \"]");//返回的错误信息");
            sb.AppendLine(FARGS.TAB4 + "} else {");
            sb.AppendLine(FARGS.TAB5 + "var rsMains = rt.result(resPrms);");
            sb.AppendLine(FARGS.TAB5 + "var rss = rs;");
            sb.AppendLine(FARGS.TAB5 + "if (rsMains != null) {");
            sb.AppendLine(FARGS.TAB6 + "var rsLA = rt.result(la).first();");
            sb.AppendLine(FARGS.TAB6 + "var la = rt.result(laname).first();");
            sb.AppendLine(FARGS.TAB6 + "if (la != null && la.itemsCount > 0) {");
            sb.AppendLine(FARGS.TAB7 + "rss = { \"total\": la.itemsCount, \"rows\": rsMains }");
            sb.AppendLine(FARGS.TAB6 + "}");
            sb.AppendLine(FARGS.TAB5 + "} else {");
            sb.AppendLine(FARGS.TAB6 + "console.log(\"接口[\" + url + \"]返回无记录\");");
            sb.AppendLine(FARGS.TAB5 + "}");
            sb.AppendLine(FARGS.TAB5 + "$('#tbDataList').datagrid('loadData', rss); //绑定数据给easyui datagrid");
            sb.AppendLine(FARGS.TAB5 + "//if (func) func.call(this, rss); //如果需要执行回调则传入func");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "});");
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine(FARGS.TAB + "getItem: function (o) {");
            sb.AppendLine(FARGS.TAB2 + "var url = \"." + apiClsName + ".get\";");
            sb.AppendLine(FARGS.TAB2 + "var id = o;");
            sb.AppendLine(FARGS.TAB2 + "var reqPrms = { " + apiClsName + ": { 'id': id } };");
            sb.AppendLine(FARGS.TAB2 + "var resPrms = \"" + apiClsName + "s\";");
            sb.AppendLine(FARGS.TAB2 + "this.onyxAjax({");
            sb.AppendLine(FARGS.TAB3 + "act: url,");
            sb.AppendLine(FARGS.TAB3 + "prms: reqPrms,");
            sb.AppendLine(FARGS.TAB3 + "callback: function (rt) {");
            sb.AppendLine(FARGS.TAB4 + "if (!rt.succ()) {");
            sb.AppendLine(FARGS.TAB5 + "console.log(\"接口[\" + url + \"]返回错误[\" + rt.msg() + \"]\");");
            sb.AppendLine(FARGS.TAB4 + "} else {");
            sb.AppendLine(FARGS.TAB5 + "var rs = rt.result(resPrms);");
            sb.AppendLine(FARGS.TAB5 + "if (rs != null) {");
            sb.AppendLine(FARGS.TAB6 + "var item = rs.first();");
            sb.AppendLine(FARGS.TAB6 + "console.log(\"接口[\" + url + \"]返回结果集\" + item);");
            sb.AppendLine(FARGS.TAB5 + "} else {");
            sb.AppendLine(FARGS.TAB6 + "console.log(\"接口[\" + url + \"]返回无结果\");");
            sb.AppendLine(FARGS.TAB5 + "}");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "});");
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine(FARGS.TAB + "addItem: function (o) {");
            sb.AppendLine(FARGS.TAB2 + "var url = \"." + apiClsName + ".add\";");
            sb.AppendLine(FARGS.TAB2 + "var reqPrms = { " + apiClsName + ": o };");
            sb.AppendLine(FARGS.TAB2 + "var resPrms = \"" + apiClsName + "s\";");
            sb.AppendLine(FARGS.TAB2 + "this.onyxAjax({");
            sb.AppendLine(FARGS.TAB3 + "act: url,");
            sb.AppendLine(FARGS.TAB3 + "prms: reqPrms,");
            sb.AppendLine(FARGS.TAB3 + "callback: function (rt) {");
            sb.AppendLine(FARGS.TAB4 + "if (!rt.succ()) {");
            sb.AppendLine(FARGS.TAB5 + "console.log(\"接口[\" + url + \"]返回错误[\" + rt.msg() + \"]\");");
            sb.AppendLine(FARGS.TAB4 + "} else {");
            sb.AppendLine(FARGS.TAB5 + "var rsMain = rt.result(resPrms);");
            sb.AppendLine(FARGS.TAB5 + "if (rsMain != null) {");
            sb.AppendLine(FARGS.TAB6 + "var item = rsMain.first();");
            sb.AppendLine(FARGS.TAB6 + "console.log(\"接口[\" + url + \"]执行成功\");");
            sb.AppendLine(FARGS.TAB5 + "} else {");
            sb.AppendLine(FARGS.TAB6 + "console.log(\"接口[\" + url + \"]执行失败\");");
            sb.AppendLine(FARGS.TAB5 + "}");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "});");
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine(FARGS.TAB + "updateItem: function (o) {");
            sb.AppendLine(FARGS.TAB2 + "var url = \"." + apiClsName + ".update\";");
            sb.AppendLine(FARGS.TAB2 + "var id = o;");
            sb.AppendLine(FARGS.TAB2 + "var reqPrms = { " + apiClsName + ": { 'id': id } };");
            sb.AppendLine(FARGS.TAB2 + "var resPrms = \"" + apiClsName + "s\";");
            sb.AppendLine(FARGS.TAB2 + "this.onyxAjax({");
            sb.AppendLine(FARGS.TAB3 + "act: url,");
            sb.AppendLine(FARGS.TAB3 + "prms: reqPrms,");
            sb.AppendLine(FARGS.TAB3 + "callback: function (rt) {");
            sb.AppendLine(FARGS.TAB4 + "if (!rt.succ()) {");
            sb.AppendLine(FARGS.TAB5 + "console.log(\"接口[\" + url + \"]返回错误[\" + rt.msg() + \"]\");");
            sb.AppendLine(FARGS.TAB4 + "} else {");
            sb.AppendLine(FARGS.TAB5 + "var rsMain = rt.result(resPrms);");
            sb.AppendLine(FARGS.TAB5 + "if (rsMain != null) {");
            sb.AppendLine(FARGS.TAB6 + "var item = rsMain.first();");
            sb.AppendLine(FARGS.TAB6 + "console.log(\"接口[\" + url + \"]执行成功\");");
            sb.AppendLine(FARGS.TAB5 + "} else {");
            sb.AppendLine(FARGS.TAB6 + "console.log(\"接口[\" + url + \"]执行失败\");");
            sb.AppendLine(FARGS.TAB5 + "}");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "});");
            sb.AppendLine(FARGS.TAB + "}");
            //
            sb.AppendLine(FARGS.TAB + "deleteItem: function (o) {");
            sb.AppendLine(FARGS.TAB2 + "var url = \"." + apiClsName + ".add\";");
            sb.AppendLine(FARGS.TAB2 + "var reqPrms = { " + apiClsName + ": o };");
            sb.AppendLine(FARGS.TAB2 + "var resPrms = \"result\";   //返回结果集：result {code, msg}");
            sb.AppendLine(FARGS.TAB2 + "this.onyxAjax({");
            sb.AppendLine(FARGS.TAB3 + "act: url,");
            sb.AppendLine(FARGS.TAB3 + "prms: reqPrms,");
            sb.AppendLine(FARGS.TAB3 + "callback: function (rt) {");
            sb.AppendLine(FARGS.TAB4 + "if (!rt.succ()) {");
            sb.AppendLine(FARGS.TAB5 + "console.log(\"接口[\" + url + \"]返回错误[\" + rt.msg() + \"]\");");
            sb.AppendLine(FARGS.TAB4 + "} else {");
            sb.AppendLine(FARGS.TAB5 + "var rsMain = rt.result(resPrms);");
            sb.AppendLine(FARGS.TAB5 + "if (rsMain != null) {");
            sb.AppendLine(FARGS.TAB6 + "var item = rsMain.first();");
            sb.AppendLine(FARGS.TAB6 + "console.log(\"接口[\" + url + \"]返回:[code=\" + item.code + \"&msg=\" + item.msg + \"]\");");
            sb.AppendLine(FARGS.TAB5 + "} else {");
            sb.AppendLine(FARGS.TAB6 + "console.log(\"接口[\" + url + \"]返回无结果\");");
            sb.AppendLine(FARGS.TAB5 + "}");
            sb.AppendLine(FARGS.TAB4 + "}");
            sb.AppendLine(FARGS.TAB3 + "}");
            sb.AppendLine(FARGS.TAB2 + "});");
            sb.AppendLine(FARGS.TAB + "}");
            sb.AppendLine("}");
            sb.AppendLine("/*[END] Onyx Ajax API接口调用方法*/");
            return sb.ToString();
        }
    }
}
