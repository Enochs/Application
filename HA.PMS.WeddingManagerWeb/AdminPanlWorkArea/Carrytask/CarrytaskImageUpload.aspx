<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskImageUpload.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskImageUpload"Title="上传执行表图片" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/ServerFileUpLoad.ascx" TagPrefix="HA" TagName="ServerFileUpLoad" %>


<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                $(".Productdiv").hide();
                $("html,body").css({ "width": "720px", "height": "580px" });
                $("#TablerepProductByCatogryforWarehouseList").show();
                $("#TablerepProductByCatogryforWarehouseList").addClass("active");
            });
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:ServerFileUpLoad runat="server" ID="ServerFileUpLoad" PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx" />
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Button ID="btnSaveImage" runat="server"  CssClass="btn btn-success" Text="保存图片" OnClick="btnSaveImage_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
