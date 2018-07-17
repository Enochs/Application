<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SedimentationUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SedimentationUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "500px", "height": "500px", "background-color": "transparent" });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h4 style="font-weight:bold;">
        文本预览：
        </h4>
        <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
    </div>
</asp:Content>
