<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectEmpLoyeesBythis.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectEmpLoyeesBythis" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/SelectEmpLoyeesBythis.ascx" TagPrefix="uc1" TagName="SelectEmpLoyeesBythis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/App_Themes/Default/css/bootstrap.min.css" />
    <script src="/Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "150px", "height": "500px", "background-color": "transparent" });
        });
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div>

            <uc1:SelectEmpLoyeesBythis runat="server" ID="SelectEmpLoyeesBythis1" />

        </div>
    </form>
</body>
</html>
