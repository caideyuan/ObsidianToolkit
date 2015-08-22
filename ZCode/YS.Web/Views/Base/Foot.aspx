<%@ Page Language="C#" %>

<%@ Import Namespace="Obsidian.Web" %>
<%@ Import Namespace="YS.SDK" %>
<%@ Import Namespace="Obsidian.Mvc" %>
<%@ Import Namespace="GlobalCommon" %>
<%  
    //载入主视图
    OView vFoot = OView.CurrentView;

    //接收数据
    Entity Prms = (Entity)vFoot.GetData("oPrms"); //参数接收

    //友情链接
    object val;
    ArrayList friendLink = null;
    if (vFoot.TryGetData("friendLink", out val))
    {
        friendLink = (ArrayList)val;
    }
    else
        friendLink = null;    
    
%>

<!--------------中间内容结束-------------->

<%if (Prms["function"].ToString() == "Afree" || Prms["function"].ToString() == "index") { }else   //首页及每日一课（享免费）不要显示
  {%>
<!--跟随广告-->
<script type="text/javascript" src="/Content/js/index_scroll_ad.js"></script>

<%} %>

<!--------------友链开始-------------->
<!--友情链接-->
<%if (friendLink != null)
  {%>
<div class="global_friend">
    <div class="box">
        <strong class="s1">友情链接：</strong>
        <div class="desc">
            <!--<%=friendLink.Count%>-->
            <%
      foreach (Hashtable friendLinkItem in friendLink)
      {
          if (friendLinkItem["type"].ToString() == "0")
          {
              Response.Write("<a target=\"_blank\" href=\"" + friendLinkItem["url"] + "\">" + friendLinkItem["name"] + "</a>\r\n");
          }
      }
            %>
        </div>
    </div>
    <div class="clear"></div>
</div>
<%}%>
<!--------------友链结束-------------->

<!--------------底部开始-------------->

<div class="clear"></div>
<%= YS.Global.MainFooter.Html %>

<%--<div style="display: none">
    <script type="text/javascript">
        var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
        document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F55fb330d006f37d00d270ef9160914de' type='text/javascript'%3E%3C/script%3E"));
    </script>
</div>
<!--<script src="http://www.yingsheng.com/js/tou.js"></script>-->
<!--<script src="http://www.yingsheng.com/js/lanmu.js"></script>-->
<!--<script src=h"ttp://www.yingsheng.com/js/style_new.js" type="text/javascript"></script>-->--%>

<!--------------底部结束-------------->


<%--<!------手机跳转处理----->
<script src="http://siteapp.baidu.com/static/webappservice/uaredirect.js" type="text/javascript"></script>
<script type="text/javascript">
    <%switch (Prms["function"].ToString())
      {  %>

    <%case "course": %>
        <%if (Convert.ToInt64(Prms["courseCatId"].ToString()) == 0 && Convert.ToInt64(Prms["jobsId"].ToString()) == 0)
          {%>
    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/classList1");        /*课程列表-主页跳转*/
    }
        <%}
          else if (Convert.ToInt64(Prms["jobsId"].ToString()) == 0)
          {%>
    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/classList1/class-<%=Prms["courseCatId"]%>.html");    /*课程列表-分类跳转*/
    }
        <% }
          else
          {%>
    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/classList3/gangwei-<%=Prms["jobsId"]%>.html");     /*课程列表-岗位跳转*/
    }
    <%}%>
    <%break;%>

        <%case "Afree":%>

    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/mianfei/");     /*享免费-主页与列表页跳转*/
    }

    <%break;%>

    <%case "Instrs":%>
    <%break;%>

        <%case "Spring":%>

    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/springsale");    /*抢特惠-主页*/
    }

    <%break;%>
        <%case "Springdetail":%>

    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/springsale/<%=Prms["jobPosId"]%>.html");     /*抢特惠-内容页*/
    }
    <%break;%>

        <%case "Vip":%>

    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/#joinvip");    /*VIP-主页*/
    }

    <%break;%>
   
        <%case "App":%>

    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://res.yingsheng.com/app/");    /*App-主页*/
    }

    <%break;%>
       


        <%case "job": %>
    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/category/performance");  /*系统班主页*/
    }
    <%break;%>

        <%case "jobList": %>
    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/gangwei-1/setMeal-<%=Prms["jobPosId"]%>.html");   /*系统班分类*/
    }
    <%break;%>

        <%case "AskIndex": %>
    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/ask");   /*问吧首页*/
    }
    <%break;%>

        <%case "AskList": %>
    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/ask");   /*问吧列表页*/
    }
    <%break;%>

        <%case "AskDetail": %>
    if ($.cookie("displaySite") == null || $.cookie("displaySite") == "") {
        uaredirect("http://m.yingsheng.com/ask-<%=Prms["id"]%>.html");   /*问吧内页*/
    }
    <%break;%>

    <%} %>
</script>--%>

<%//System.Web.HttpContext.Current.Response.Write(System.Web.HttpContext.Current.Request.Url.ToString());
    if ((System.Web.HttpContext.Current.Request.Url.ToString().IndexOf("8080") > 0 || System.Web.HttpContext.Current.Request.Url.ToString().IndexOf("yingsheng.cc") > 0))
    { %>
<!--测试图标-->
<div onclick="$(this).hide();" title="测试站点，点击隐藏" style="position: fixed; top: 0px; left: 0px; z-index: 99999; cursor: pointer;">
    <img src="http://www.yingsheng.com/Content/images/global/test.png" />
</div>
<%} %>

<%--<%=YS.SDK.Global.YSTOOLS.HolidayShow()%>--%>

</body>
</html>