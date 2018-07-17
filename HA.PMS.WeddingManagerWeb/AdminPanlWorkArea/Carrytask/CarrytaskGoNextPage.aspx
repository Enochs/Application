<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskGoNextPage.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskGoNextPage" Title="跳转页面" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content1">
    <script>
        $(document).ready(function () {
            var OpenURI = $("#hideOpen").val();
            if (OpenURI != "1") {
                //("#OperURI").attr("href", OpenURI);
                //window.open(OpenURI);
                // $("#ContentTable").hide();
                // $("#OperURI").click();
                location.href = $("#hideLocation").val();
            }
        });
    </script>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2">
    <asp:HiddenField ID="hideOpen" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="hideLocation" runat="server" ClientIDMode="Static" Value="1" />
    <a class="a_QuotedPrice"></a>
</asp:Content>
