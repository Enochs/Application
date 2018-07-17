<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectCase.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectCase" %>
<style type="text/css">
    .cb td {
        padding: 10px;
    }

    .cb label {
        display: inline-block;
        margin-left: 5px;
    }
</style>
<div style="width: 400px; height: 400px;">
    <table style="width:350px;">
        <tr>
            <td>
                <asp:Button runat="server" ID="btnSelect" Text="确认选择" CssClass="btn btn-success" /></td>
        </tr>
        <asp:Repeater runat="server" ID="rptCase">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox runat="server" ID="chkCaseName" CssClass="cb" Text='<%#Eval("CaseName") %>' ToolTip='<%#Eval("CaseName") %>' />
                        <asp:HiddenField runat="server" ID="HideCaseID" Value='<%#Eval("CaseID") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td>
                <asp:Button runat="server" ID="btnSelects" Text="确认选择" CssClass="btn btn-success" OnClick="btnSelect_Click" /></td>
        </tr>
        <tr>
            <td><asp:TextBox runat="server" ID="txtShowName" Width="250px" /></td>
        </tr>
    </table>
</div>
