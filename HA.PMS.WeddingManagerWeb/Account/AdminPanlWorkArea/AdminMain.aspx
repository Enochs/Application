<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.AdminMain" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head runat="server">
    <title>HoldLove Admin</title>
    <meta charset="UTF-8" />
    <script src="/Scripts/jquery-1.7.1.js"></script>
</head>

<frameset id="main" rows="*" cols="220,11,*" frameborder="no" border="0" framespacing="0">
  <frame   src="MenuTree.aspx?EmployeeID=<%=Request["EmployeeID"] %>" name="leftFrame" scrolling="auto" noresize="noresize" id="leftFrame" title="leftFrame" />
  <frame src="Hide.html" name="leftFrame" scrolling="auto" noresize="noresize" id="Frame1" title="leftFrame" />
  <frameset rows="40,*" frameborder="no" border="0" framespacing="0">
    <frame src="AdminTop.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" />
    <frame src="AdminFirstpanel.aspx" class="MainPanel" name="mainFrame" id="mainFrame" title="mainFrame" />
  </frameset>
</frameset>
<noframes>
<body>
<p>你的浏览器不支持框架，请升级你的浏览器</p>
</body>
</noframes>
</html>
