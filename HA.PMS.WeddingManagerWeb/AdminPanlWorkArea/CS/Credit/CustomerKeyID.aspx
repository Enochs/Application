<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Default" CodeBehind="CustomerKeyID.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Credit.CustomerKeyID" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="../../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="table table-bordered">
            <tr>
                <td>新郎姓名：</td>
                <td>
                    <asp:Label runat="server" ID="lblGroomName" /></td>
                <td>新娘姓名：</td>
                <td>
                    <asp:Label runat="server" ID="lblBrideName" />
                </td>
            </tr>
            <tr>
                <td>联系电话：</td>
                <td>
                    <asp:Label runat="server" ID="lblGroomPhone" /></td>
                <td>联系电话：</td>
                <td>
                    <asp:Label runat="server" ID="lblBridePhone" />
                </td>
            </tr>
            <tr>
                <td>身份证号：</td>
                <td>
                    <asp:TextBox ID="txtGroomIDCard" runat="server"></asp:TextBox>
                </td>
                <td>身份证号：</td>
                <td>
                    <asp:TextBox ID="txtBrideIDCard" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>领卡时间：</td>
                <td>
                    <asp:TextBox ID="txtGetCardDate" runat="server" onclick="WdatePicker();"></asp:TextBox>
                </td>
                <td>会员卡号：</td>
                <td>
                    <asp:TextBox ID="txtCardID" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>保存</td>
                <td colspan="3">
                    <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClick="btnSaveChange_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
