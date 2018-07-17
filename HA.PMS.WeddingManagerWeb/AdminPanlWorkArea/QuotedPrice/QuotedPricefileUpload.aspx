<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPricefileUpload.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPricefileUpload" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/ServerFileUpLoad.ascx" TagPrefix="HA" TagName="ServerFileUpLoad" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
     <script type="text/javascript">

         $(document).ready(function () {
             $("html,body").css({ "width": "720px", "height": "580px" });
         });
    </script>
    请注意：单个文件不能超过25M  
    <HA:ServerFileUpLoad runat="server" ID="ServerFileUpLoad" PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx" />
    <asp:Button ID="btnSaveChange" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveChange_Click" />
</asp:Content>