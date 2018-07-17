<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectStoreProduct.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.OldPage.SelectStoreProduct" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register src="../Control/SelectStoreProduct.ascx" tagname="SelectStoreProduct" tagprefix="uc1" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:SelectStoreProduct ID="SelectStoreProduct1" runat="server" />
    </asp:Content>