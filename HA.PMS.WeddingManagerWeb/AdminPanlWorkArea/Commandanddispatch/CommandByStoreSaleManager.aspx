<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommandByStoreSaleManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandByStoreSaleManager" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CountTableManager/StoreSaleTable.ascx" TagPrefix="HA" TagName="StoreSaleTable" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:StoreSaleTable runat="server" id="StoreSaleTable" />
    <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
</asp:Content>
