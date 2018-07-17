<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CstmNameSelector.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CstmNameSelector" %>
<div>
    <asp:Label ID="lblTitle" Text="新人姓名" runat="server" />
    <asp:DropDownList ToolTip="搜索选项" Width="100px" ID="ddlNameType" runat="server">
        <asp:ListItem Text="新娘" Value="1" />
        <asp:ListItem Text="新郎" Value="2" />
    </asp:DropDownList>
    <asp:TextBox runat="server" ToolTip="新人姓名关键字" MaxLength="9" Width="80" ID="txtName" />
</div>
