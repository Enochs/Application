<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectEmpLoyeeBythis.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectEmpLoyeeBythis" %>

<%@ Register Src="../Control/SelectEmpLoyeeBythis.ascx" TagName="SelectEmpLoyeeBythis" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/App_Themes/Default/css/bootstrap.min.css" />
    <script src="/Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $("html,body").css({ "width": "380", "height": "500", "background-color": "transparent" });
        //});
    </script>
</head>

<body>
    <form id="form1" runat="server" >
        <div style="overflow-y:auto;height:375px;width:440px">
            <uc1:SelectEmpLoyeeBythis ID="SelectEmpLoyeeBythis1" runat="server" />
        </div>
    </form>
</body>
</html>
