<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommandbySaleSourceManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandbySaleSourceManager" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CountTableManager/TelemarketingTable.ascx" TagPrefix="HA" TagName="TelemarketingTable" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:TelemarketingTable runat="server" ID="TelemarketingTable" />
    <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
</asp:Content>
