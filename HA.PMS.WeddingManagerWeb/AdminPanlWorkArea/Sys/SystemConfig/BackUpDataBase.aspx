<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="BackUpDataBase.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.SystemConfig.BackUpDataBase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div divider-vertical">
        <table class="table table-bordered" style="width: 30%">
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnBackUp" Text="数据库备份" CssClass="btn btn-primary" OnClick="btnBackUp_Click" />
                </td>
                <td><a href="D:/BackUp/PMS_Weding.bak" class="btn btn-primary" style="display: none;">导入本地</a></td>
            </tr>
            <tr>
                <td colspan="2" height="25px"></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblDescription" Text="备份数据库路径 D:\BackUp\PMS_Wedding.bak (服务器)" Style="color: #808080" /></td>
            </tr>


        </table>
    </div>
</asp:Content>
