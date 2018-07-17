<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierProductList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.SupplierProductList" Title="供应商物料单" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" Title="我的执行明细" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

 

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc11" %>

<%@ Register src="../Control/MyCarryTask.ascx" tagname="MyCarryTask" tagprefix="uc1" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content1">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Conten2">
    <uc1:MyCarryTask ID="MyCarryTask1" runat="server" />
</asp:Content>


