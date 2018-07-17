<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_StorehouseProductSourceCount.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseProductSourceCount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/Scripts/jquery-1.7.1.min.js"></script>
    <script src="/Scripts/Validator.js"></script>
    <link href="/Scripts/Tooltip.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "320px", "height": "84px", "background-color": "transparent", "overflow-y": "hidden" });
        });

        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSaveChange.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindUInt('<%=txtCount.ClientID%>');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <span style="color:red">*</span>产品数量
        <asp:TextBox ID="txtCount" check="1" MaxLength="10" tip="添加的产品数量只能为正整数！" runat="server"></asp:TextBox>
        <asp:Button ID="btnSaveChange" CssClass="" runat="server" Text="保存" OnClick="btnSaveChange_Click" />
    </div>
    </form>
</body>
</html>
