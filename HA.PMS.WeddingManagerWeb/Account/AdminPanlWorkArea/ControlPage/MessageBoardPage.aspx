<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageBoardPage.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.MessageBoardPage1" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforEmpLoyee.ascx" TagPrefix="HA" TagName="MessageBoardforEmpLoyee" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">

        $(document).ready(function() {
            $("html,body").css({
                "width": "600px", "height": "260px", "background-color": "transparent"
            });
        });
    </script>
    <HA:MessageBoardforEmpLoyee runat="server" ID="MessageBoardforEmpLoyee" />
</asp:Content>
 