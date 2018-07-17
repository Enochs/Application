<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admintop.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Admintop" StylesheetTheme="Default" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head runat="server">
    <title>HoldLove Admin</title>
    <meta charset="UTF-8" />
    <script src="/Scripts/jquery-1.7.1.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script type="text/javascript">

    </script>
</head>
<body style="background-color: #2E363F;">
    
    <form id="form1" runat="server">
        <!--top-Header-menu-->
        <div id="user-nav" class="navbar navbar-inverse">
            <ul class="nav">
                <li class=""><a title="" href="/AdminPanlWorkArea/AdminFirstpanel.aspx" target="mainFrame"><i class="icon icon-cog"></i><span class="text">我的工作台</span></a></li>
                <li class="li_Message"><a title="" href="/AdminPanlWorkArea/Flows/Mission/FL_MessageforEmployeeShow.aspx" target="mainFrame">
                    <i class="icon icon-cog"></i><span class="text">新消息</span><span class="label label-important">
                        <asp:Literal ID="lblSumCount" runat="server"></asp:Literal></span></a>
                </li>
                        <li class="li_Message"><a title="" href="BugSystem/BugSystemManager.aspx" target="mainFrame">
                            <i class="icon icon-cog"></i><span class="text">Bug消息</span><span class="label label-important">
                                <asp:Literal ID="lblBugCount" runat="server"></asp:Literal></span></a>
                        </li>
                <li class="">
                    <asp:LinkButton ID="lkbtnExit" OnClick="lkbtnExit_Click" runat="server"><i class="icon icon-cog"></i><span class="text">退出系统</span></asp:LinkButton>
                </li>
            </ul>
        </div>
        <!--close-top-Header-menu-->

    </form>
</body>
</html>
