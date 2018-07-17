<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CreditAddDecrease.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Credit.CreditAddDecrease" %>
<%@ Register src="../../Control/CarrytaskCustomerTitle.ascx" tagname="CarrytaskCustomerTitle" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;" border="1">
        <tr>
            <td colspan="2">
                <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>积分类型</td>
            <td>
                <asp:Label ID="lblPointType" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>分数</td>
            <td>
                <asp:TextBox ID="txtPoint" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>原因</td>
            <td>
                <asp:TextBox ID="txtNode" runat="server"  Width="150"></asp:TextBox>
            </td>
        </tr>
    
        <tr>
            <td>保存</td>
            <td>
                <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClick="btnSaveChange_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
