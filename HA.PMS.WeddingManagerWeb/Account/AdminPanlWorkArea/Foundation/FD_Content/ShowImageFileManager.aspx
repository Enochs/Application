<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="ShowImageFileManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.ShowImageFileManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Div_Show">
        <table class="table table-bordered">
            <tr>
                <th>图片</th>
                <th>说明</th>
            </tr>
            <asp:Repeater runat="server" ID="rptShows">
                <ItemTemplate>
                    <tr>
                        <td><img runat="server" id="imgshows" /></td>
                        <td></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
