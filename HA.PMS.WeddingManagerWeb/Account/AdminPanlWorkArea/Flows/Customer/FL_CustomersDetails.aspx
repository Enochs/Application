<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_CustomersDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.FL_CustomersDetails" %>


<%@ Register Src="~/AdminPanlWorkArea/Control/CustomerDetailsAll.ascx" TagPrefix="uc1" TagName="CustomerDetailsAll" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <uc1:CustomerDetailsAll runat="server" id="CustomerDetailsAll" />
</asp:Content>
