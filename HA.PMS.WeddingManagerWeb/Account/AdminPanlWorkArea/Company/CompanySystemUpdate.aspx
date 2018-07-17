<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CompanySystemUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.CompanySystemUpdate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "380px", "height": "150px", "background-color": "white" });
        });
        $(window).load(function () {
            BindString(32, '<%=txtSystemTitle.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div>
            <table class="table table-bordered table-striped" style="width: 80%;">
                <tr runat="server" id="tr_Title">
                    <td style="white-space: nowrap;">制度名称</td>
                    <td>
                        <asp:TextBox ID="txtSystemTitle" check="1" tip="限32个字符！" runat="server" MaxLength="32"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr runat="server" id="tr_Parent">
                    <td style="white-space: nowrap;">父级制度</td>
                    <td style="white-space: nowrap;">
                        <asp:DropDownList ID="ddlParent" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="tr_Name">
                    <td style="white-space: nowrap;">文件名称</td>
                    <td style="white-space: nowrap;">
                        <asp:TextBox runat="server" ID="txtFileName" />
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" /></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
