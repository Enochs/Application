<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="DepartmentDynamicIndex.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium.DepartmentDynamicIndex" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CountMananger/DepartmentDynamicIndexCount.ascx" TagPrefix="HA" TagName="DepartmentDynamicIndexCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:DepartmentDynamicIndexCount runat="server" id="DepartmentDynamicIndexCount" />
   
</asp:Content>
