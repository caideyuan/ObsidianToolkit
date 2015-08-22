<%@ Page Language="C#" %>

<%@ Import Namespace="Obsidian.Web" %>
<%@ Import Namespace="YS.SDK" %>
<%@ Import Namespace="Obsidian.Mvc" %>
<%  
    //载入主视图
    OView vHead = OView.CurrentView;

    //接收数据
    Entity Prms = (Entity)vHead.GetData("oPrms"); //参数接收
    Entity Seo = (Entity)vHead.GetData("oSeo"); //SEO数据
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" type="image/x-icon" href="http://www.yingsheng.com/Content/images/favicon.ico" />

    <title><%=Seo["title"]%></title>
    <meta name="keywords" content="<%=Seo["keywords"]%>" />
    <meta name="description" content="<%=Seo["description"]%>" />
    <meta name="author" content="英盛网" />

    <!--JS文件-->
    <script type="text/javascript" src="/js/libs/jquery/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="/js/libs/layer/v1.8.5/layer.min.js"></script>
    <script type="text/javascript" src="/js/libs/cookie/jquery.cookie.js"></script>
    

    <script type="text/javascript" src="http://www.yingsheng.com/js/onyx.js" charset="utf-8"></script>
    <script type="text/javascript" src="http://www.yingsheng.com/js/ys.js" charset="utf-8"></script>
    <%--<script type="text/javascript" src="http://www.yingsheng.com/js/tv.js"></script>    --%>
    <script type="text/javascript" src="http://www.yingsheng.com/js/style.js"></script>
    <script type="text/javascript" src="http://www.yingsheng.com/e/downsys/play/a.js"></script>

    <!--选择栏目样式-->
    <%switch (Prms["function"].ToString())
      {  %>
    <%case "index": %>
    <!--默认文件加载-->
    <%--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/index.css" />--%>
    <script type="text/javascript" src="/js/libs/lazyload/jquery.lazyload.js"></script>
    <link rel="stylesheet" type="text/css" href="/Content/css/index.css" />
    <script type="text/javascript" src="/Content/js/global_msclass.js"></script>
    <%break; %>

    <%case "course": %>
    <!------找课程页面添加----->
    <link rel="stylesheet" type="text/css" href="/Content/css/global_channel.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_course_list.css" />
    <script type="text/javascript" src="/Content/js/channel_course_list.js"></script>
    <%break;%>

    <%case "Afree": %>
    <!------享免费页面添加----->
    <link rel="stylesheet" type="text/css" href="/Content/css/global_channel.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_course_afree.css" />
    <script type="text/javascript" src="/Content/js/channel_course_afree.js"></script>
    <%break;%>

    <%case "ZoneHome": %>
    <%case "ZoneList": %>
    <!------找老师主页与列表页添加----->
    <link rel="stylesheet" type="text/css" href="/Content/css/zone_index.css" />
    <script type="text/javascript" src="/Content/js/zone_index.js"></script>
    <%break;%>

    <%case "Zone": %>
    <!------找老师页面添加----->
    <!--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/style_new.css" /> -->
    <link href="http://www.yingsheng.com/css/teacher.css" rel="stylesheet" type="text/css" />
    <link href="/Content/css/channel_zone_index.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Content/js/channel_zone_index.js"></script>
    <%break;%>

    <%case "Spring": %>
    <!------抢特惠页面添加----->
    <!--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/style_new.css" /> -->
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_spring.css" />
    <%break;%>

    <%case "Springdetail": %>
    <!------抢特惠内容页面添加----->
    <!--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/style_new.css" /> -->
    <script type="text/javascript" src="/Content/js/global_memberFav.js"></script>
    <link rel="stylesheet" type="text/css" href="/Content/css/video_detail.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_spring_detail.css" />

    <%break;%>

    <%case "Ability": %>
    <!------岗位列表页面添加----->
    <!--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/style_new.css" /> -->
    <link rel="stylesheet" type="text/css" href="/Content/css/global_channel.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_course_ability.css" />
    <script type="text/javascript" src="/Content/js/channel_course_ability.js"></script>

    <%break;%>

    <%case "Vip": %>
    <!------岗位列表页面添加----->
    <!--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/style_new.css" /> -->
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_vip.css" />

    <%break;%>

    <%case "Search": %>
    <!------岗位列表页面添加----->
    <!--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/style_new.css" /> -->
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_search.css" />
    <script type="text/javascript" src="/Content/js/channel_search.js"></script>

    <%string searchSource = (Prms["type"].ToString() == "course") ? "/search/" : "/search-" + Prms["type"] + "/";
      int searchPage = (YS.SDK.Global.YS_GCTOOLS.toPage(Prms["page"]));
      string searchPageStr = (searchPage > 1) ? "-" + searchPage : "";
    %>

    <link rel="canonical" href="http://www.yingsheng.com<%=searchSource+Prms["kw"]+searchPageStr%>" />

    <%break;%>

    <%case "App": %>
    <!------APP页面添加----->
    <link href="/Content/css/down_app.css" type="text/css" rel="stylesheet" />

    <%break;%>

    <%case "job": %>
    <!------岗位首页添加----->
    <!--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/style_new.css" /> -->
    <link rel="stylesheet" type="text/css" href="/Content/css/global_channel.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_job_index.css" />
    <script type="text/javascript" src="/Content/js/channel_job_list.js"></script>
    <%break;%>

    <%case "jobList": %>
    <!------岗位列表页面添加----->
    <!--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/style_new.css" /> -->
    <link rel="stylesheet" type="text/css" href="/Content/css/global_channel.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_job_list.css" />
    <script type="text/javascript" src="/Content/js/channel_job_list.js"></script>
    <%break;%>

    <%case "Forme": %>
    <!------单页页面添加----->
    <!--<link rel="stylesheet" type="text/css" href="http://www.yingsheng.com/css/style_new.css" /> -->
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_forme.css" />
    <%break;%>

    <%case "SpecialIndex": %>
    <%case "SpecialList": %>
    <!------专题主页添加----->
    <link rel="stylesheet" type="text/css" href="/Content/css/global_special.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_special.css" />
    <%break;%>

    <%case "SpecialDetail": %>
    <!------专题内页添加----->
    <link rel="stylesheet" type="text/css" href="/Content/css/global_special.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_special_detail.css" />
    <%break;%>

    <%case "AskIndex": %>
    <%case "AskList": %>
    <%case "AskDetail": %>
    <%case "AskSearch": %>
    <%case "AskMyCenter": %>
    <!------问吧----->
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_ask_global.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_ask_index.css" />
    <link rel="stylesheet" type="text/css" href="/Content/css/channel_ask_style.css" />
    <script type="text/javascript" src="/Content/js/channel_ask_global.js"></script>
    <!--sso登录部分-->
    <link rel="stylesheet" type="text/css" href="http://sso.yingsheng.com/global/css/global.css" />
    <script src="http://sso.yingsheng.com/global/js/global.js" type="text/javascript"></script>
    <%break;%>

    <%} %>
</head>
<body>


    <!--------------头部开始-------------->
    <%if (Prms["function"].ToString() == "AskIndex" || Prms["function"].ToString() == "AskList" || Prms["function"].ToString() == "AskDetail" || Prms["function"].ToString() == "AskMyCenter" || Prms["function"].ToString() == "AskSearch")
      { }
      else if (Prms["function"].ToString() == "ZoneHome" || Prms["function"].ToString() == "ZoneList" || Prms["function"].ToString() == "ZoneIndex" || Prms["function"].ToString() == "ZoneFS" || Prms["function"].ToString() == "ZoneCourse" || Prms["function"].ToString() == "ZoneBowen" || Prms["function"].ToString() == "ZoneBowenDetail" || Prms["function"].ToString() == "ZoneAnswer" || Prms["function"].ToString() == "ZoneComment") { }
      else
      {%>
    <script src="http://sso.yingsheng.com/js/YS.SSO.VS.Login.js"></script>
    <div id="YingShengSSOTopDIV" style="background-color: #288CE6; height: 65px;"></div>
    <%= YS.Global.MainNav.Html  %>
    <%} %>
    <!--------------头部结束-------------->


    <!--------------中间内容开始-------------->
