<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceCheckNode.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceCheckNode" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" Title="报价单审核说明" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <table style="width:40%">
        <tr>
            <td>审核状态：</td>
            <td>
                <asp:Label ID="lblCheckState" runat="server" Text="Label"></asp:Label></td>
        </tr>
                <tr>
            <td>审核说明：</td>
            <td><asp:Label ID="lblCheckNode" runat="server" Text="Label"></asp:Label></td>
        </tr>
    </table>
</asp:Content>
 