<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CellphoneSelector.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CellphoneSelector" %>
<div>
    <asp:Label runat="server" ID="lblTitles" Text="联系电话" />
    <asp:DropDownList runat="server" ID="ddlPhoneTypes" ToolTip="搜索条件" Width="100px">
        <asp:ListItem Text="新娘电话" Value="1" />
        <asp:ListItem Text="新郎电话" Value="2" />
    </asp:DropDownList>
    <asp:TextBox runat="server" ID="txtCellPhone" ToolTip="新人联系电话" MaxLength="11" Width="80" />
</div>
