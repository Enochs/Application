<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys_JurisdictionforButtonManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction.Sys_JurisdictionforButtonManager" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">

    <table style="width: 100%;">
        <tr>
            <td>
                <input id="Checkbox1" type="checkbox" />全选/取消</td>
            <td>控名称件</td>
            <td>所属模块</td>
            <td>控件类型</td>
        </tr>
        <asp:Repeater ID="RepControlList" runat="server">

            <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkControl" runat="server" />
                        <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("ControlID") %>' />

                    </td>
                    <td><%#Eval("ControlName") %></td>
                    <td><%#GetChannelNameByID()%></td>
                    <td><%#Eval("ServerType") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>

        <tr>
            <td colspan="3">
                <asp:Button ID="btnSaveChange" runat="server" Text="保存权限" OnClick="btnSaveChange_Click" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
