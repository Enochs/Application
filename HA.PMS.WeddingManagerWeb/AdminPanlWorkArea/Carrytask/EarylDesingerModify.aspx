<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="EarylDesingerModify.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.EarylDesingerModify" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
    <div class="div_Main">
        <table class="table table-bordered">
            <tr>
                <td valign="top">详细内容:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtContent" TextMode="MultiLine" Width="660px" Height="200px" /></td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton runat="server" ID="lbtnSave" Text="保存" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                    <a href="CarryTaskEarlyList.aspx" class="btn btn-primary">返回</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
