<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>测试</title>
    <script src="../Content/jquery.min.js"></script>
    <link href="../Content/easyui/css/default.css" rel="stylesheet" />
    <link href="../Content/easyui/js/themes/default/easyui.css" rel="stylesheet" />
    <link href="../Content/easyui/js/themes/icon.css" rel="stylesheet" />
    <%--<script type="text/javascript" src="../Content/easyui/js/jquery-1.4.4.min.js"></script>--%>
    <script  type="text/javascript" src="../Content/easyui/js/jquery.easyui.min.1.2.2.js"></script>
    <script  type="text/javascript" src="../Content/easyui/js/outlook2.js"></script>

</head>
<body>
   <div id="tabs">
        <ul class="tabs" style="margin-top: 50px;">
            <li class="tabs-selected">
                <a href="javascript:void(0)" class="tabs-inner">
                    <span class="tabs-title tabs-closable tabs-with-icon">用户管理</span>
                    <span class="tabs-icon icon icon-nav"></span>
                </a>
                <a href="javascript:void(0)" class="tabs-close"></a>
            </li>
            <li>
                <a href="javascript:void(0)" class="tabs-inner">
                    <span class="tabs-title tabs-closable tabs-with-icon">权限管理</span>
                    <span class="tabs-icon icon icon-nav"></span>
                </a>
                <a href="javascript:void(0)" class="tabs-close"></a>
            </li>
        </ul>
        <div style="border: 1px solid gray; height: auto;width:100%">
            
        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    $(function () {
        alert("加载");
    });
</script>
