<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CompanyFileCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.CompanyFileCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "320px", "height": "120px", "background-color": "white" });
            //$("#lbtnCustomer").click(function () {
            //    $("#ddlDepartment").css("display", "none");
            //    $("#txtTopName").css("display", "block");
            //    $("#lbtnCustomer").css("display", "none");
            //    $("#lbtnNotCustomer").css("display", "block");
            //});

            //$("#lbtnNotCustomer").click(function () {
            //    $("#ddlDepartment").css("display", "block");
            //    $("#txtTopName").css("display", "none");
            //    $("#lbtnCustomer").css("display", "block");
            //    $("#lbtnNotCustomer").css("display", "none");
            //});
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_add">
        <table>
            <tr>
                <td>顶级名称</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTopName" ClientIDMode="Static" Style="width: 90px;" Height="18px" />
                    <asp:DropDownList runat="server" ID="ddlParentFileName" />
                    <span style="color: red">*</span>
                </td>
            </tr>
            <tr runat="server" id="tr_FielName">
                <td>文件名称</td>
                <td>
                    <asp:TextBox runat="server" ID="txtFileName" />
                    <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button runat="server" ID="btnConfirm" CssClass="btn btn-primary" Text="确定" OnClick="btnConfirm_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
