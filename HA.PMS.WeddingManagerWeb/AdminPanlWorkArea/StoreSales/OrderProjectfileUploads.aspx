<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="OrderProjectfileUploads.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.OrderProjectfileUploads" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/ServerFileUpLoad.ascx" TagPrefix="HA" TagName="ServerFileUpLoad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>

         $(document).ready(function () {
             $("html,body").css({ "width": "720px", "height": "580px" });

         });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:ServerFileUpLoad runat="server" ID="ServerFileUpLoad" />
</asp:Content>
