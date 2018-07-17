<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerActiveCreateUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CustomerProject.JinseBainian.CustomerActiveCreateUpdate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content1">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2">

    <table style="width: 100%;" class="table table-bordered table-striped with-check table-select">
        <tr>
            <td style="white-space: nowrap;">预订时间</td>
            <td>
                <asp:Label ID="lblPartyDate" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>录入人</td>
            <td>
                <asp:Label ID="lblEmployeeName" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>时段</td>
            <td>
                <asp:Label ID="lblTimerSpan" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>状态</td>
            <td style="white-space: nowrap;">
                <asp:RadioButtonList ID="rdoState" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="预订"></asp:ListItem>
                    <asp:ListItem Text="暂定"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>备注</td>
            <td>
                <asp:TextBox ID="txtNode" runat="server" Rows="5" TextMode="MultiLine" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClick="btnSaveChange_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="取消订单" OnClick="btnDelete_Click" />
                <asp:Button ID="btnChange" runat="server" Text="保存更改" OnClick="btnChange_Click" /></td>
        </tr>
    </table>


</asp:Content>

