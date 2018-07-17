<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectDepartmentBythis.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectDepartmentBythis" %>

<%@ Register src="../Control/SelectDepartment.ascx" tagname="SelectDepartment" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
          <script src="/Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "400px", "height": "300px", "background-color": "transparent" });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:SelectDepartment ID="SelectDepartment1" runat="server" />
    
    </div>
    </form>
</body>
</html>
