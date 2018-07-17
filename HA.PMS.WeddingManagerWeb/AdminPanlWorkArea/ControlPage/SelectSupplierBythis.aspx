<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectSupplierBythis.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectSupplierBythis" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/SelectSupplier.ascx" TagPrefix="HA" TagName="SelectSupplier" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/App_Themes/Default/css/bootstrap.min.css" />
    <script src="/Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "400px", "height": "400px", "background-color": "transparent" });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <HA:SelectSupplier runat="server" id="SelectSupplier" />
    </div>
    </form>
</body>
</html>
