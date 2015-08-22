<%@ Page Language="C#" %>
<%@ Import Namespace="Obsidian.Mvc" %>

<%
    OView vCurr = OView.CurrentView;
    string title;
    string message;
    vCurr.TryGetData<string>("title", out title);
    vCurr.TryGetData<string>("message", out message);
%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=title%></title>
</head>
<body>
    <div><%=message%></div>
</body>
</html>
