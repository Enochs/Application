<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnniversaryShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Anniversary.AnniversaryShow" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () { $("html,body").css({ "width": "600px", "height": "300px" }); });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr><td>类型：</td><td><asp:Label ID="lblType" runat="server"></asp:Label></td></tr>
        <tr><td>内容：</td><td><asp:Label ID="lblContent" runat="server"></asp:Label></td></tr>
        <tr><td>服务人员：</td><td><asp:Label ID="lblEmployeeName" runat="server"></asp:Label></td></tr>
        </table>
</asp:Content>
