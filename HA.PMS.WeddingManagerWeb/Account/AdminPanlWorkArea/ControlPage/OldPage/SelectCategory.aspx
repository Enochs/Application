<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCategory.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.OldPage.SelectCategory" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="../../Control/OldControl/SelectCategory.ascx" TagName="SelectCategory" TagPrefix="uc1" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "640px", "height": "320px", "background-color": "transparent" });
        });
    </script>




    <uc1:SelectCategory ID="SelectCategory1" runat="server" />




</asp:Content>
