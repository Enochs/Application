<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="QuotedPrintTitleSet.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.SystemConfig.QuotedPrintTitleSet" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%">
        <tr>
            <td>报价单表头</td>
            <td>
                <cc1:CKEditorTool ID="txtTop" runat="server"></cc1:CKEditorTool></td>
        </tr>
        <tr>
            <td>报价单底部</td>
            <td>
                <cc1:CKEditorTool ID="txtDown" runat="server"></cc1:CKEditorTool></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
