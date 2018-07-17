<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateRanger.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.DateRanger" %>
<div>
    <asp:Label ID="lblTitle" runat="server"></asp:Label>
    <asp:TextBox ID="txtDateStart" ToolTip="起始日期" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'});" runat="server" />
    －
    <asp:TextBox ID="txtDateEnd" ToolTip="截止日期" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd'});" runat="server" />
</div>
