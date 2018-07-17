<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CompanySystemUpLoad.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.CompanySystemUpLoad" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/ServerFileUpLoad.ascx" TagPrefix="HA" TagName="ServerFileUpLoad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Productdiv").hide();
            $("html,body").css({ "width": "720px", "height": "580px" });
            $("#TablerepProductByCatogryforWarehouseList").show();
            $("#TablerepProductByCatogryforWarehouseList").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:ServerFileUpLoad runat="server" ID="ServerFileUpLoad" PostServer="/AdminPanlWorkArea/Control/FileServer.aspx" />
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Button ID="btnSaveImage" runat="server" CssClass="btn btn-success" Text="保存" OnClick="btnSaveImage_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
