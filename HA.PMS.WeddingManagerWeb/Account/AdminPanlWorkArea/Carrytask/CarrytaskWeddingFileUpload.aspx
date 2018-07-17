<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CarrytaskWeddingFileUpload.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWeddingFileUpload" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/ServerFileUpLoad.ascx" TagPrefix="HA" TagName="ServerFileUpLoad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:ServerFileUpLoad runat="server" ID="ServerFileUpLoad" PostServer = "/AdminPanlWorkArea/Control/FileServer.aspx" />
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server"  CssClass="btn btn-success" Text="保存" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
