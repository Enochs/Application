<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCaseByThis.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectCaseByThis" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/SelectCase.ascx" TagPrefix="HA" TagName="SelectCase" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/App_Themes/Default/css/bootstrap.min.css" />
    <script src="/Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript">
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <HA:SelectCase runat="server" ID="SelectCase" />
        </div>
    </form>
</body>
</html>
