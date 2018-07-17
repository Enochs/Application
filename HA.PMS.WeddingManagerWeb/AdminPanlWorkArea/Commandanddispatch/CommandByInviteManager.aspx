<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommandByInviteManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandByInviteManager" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CountTableManager/InviteTable.ascx" TagPrefix="HA" TagName="InviteTable" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:InviteTable runat="server" id="InviteTable" />
    <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
</asp:Content>
