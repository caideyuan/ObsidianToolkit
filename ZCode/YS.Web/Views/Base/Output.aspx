<%@ Page Language="C#"%>

<%@ Import Namespace="Obsidian.Mvc" %>

<%
    OView vCurr = OView.CurrentView;
    string message;
    vCurr.TryGetData<string>("message", out message);
%>

<div></div>
