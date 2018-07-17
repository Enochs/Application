<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommandByPlanManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandByPlanManager" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CountTableManager/PlanTable.ascx" TagPrefix="HA" TagName="PlanTable" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:PlanTable runat="server" id="PlanTable" />
    <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
</asp:Content>
