<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectFourGuardian.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectFourGuardian" %>

<%@ Register src="../Control/SelectFourGuardian.ascx" tagname="SelectSupplier" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="/App_Themes/Default/css/bootstrap.min.css" />
    <script src="/Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "300px", "height": "400px", "background-color": "transparent" });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:SelectSupplier ID="SelectFourGuardian" runat="server" />
    
    </div>
    </form>
</body>
</html>
