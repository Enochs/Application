<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectEmployee.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectEmployee" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
 

<%@ Register src="../Control/SelectEmployee.ascx" tagname="SelectEmployee" tagprefix="uc1" %>
 

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
 
    <uc1:SelectEmployee ID="SelectEmployee1" runat="server" />
 
</asp:Content>
 