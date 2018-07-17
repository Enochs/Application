<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_QuotedCatgoryFileServer.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_QuotedCatgoryFileServer" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<%@ Register src="../../Control/ServerFileUpLoad.ascx" tagname="ServerFileUpLoad" tagprefix="HA" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
            <script type="text/javascript">
                $(document).ready(function () {
                    $(".Productdiv").hide();
                    $("html,body").css({ "width": "720px", "height": "580px" });
                    $("#TablerepProductByCatogryforWarehouseList").show();
                    $("#TablerepProductByCatogryforWarehouseList").addClass("active");
                });
    </script>
    <HA:ServerFileUpLoad runat="server" ID="ServerFileUpLoad" PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx" />
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Button ID="btnSaveImage" runat="server"  CssClass="btn btn-success" Text="保存" OnClick="btnSaveImage_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

 