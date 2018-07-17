<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="UpdatePassWord.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Personal.UpdatePassWord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../App_Themes/Default/js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnConfirm").click(function () {
                if ($("#txtOldPass").val() == "") {
                    alert("请输入原密码");
                    return false;
                } else if ($("#txtNewPass").val() == "") {
                    alert("请输入新密码");
                    return false;
                } else if ($("#txtNewPass").val() != "") {
                    if ($("#txtNewPass").val().length < 6) {
                        alert("密码的长度不得小于六位");
                        return false;
                    }
                } else if ($("#txtConfirmPass").val() == "") {
                    alert("请输入确认密码");
                    return false;
                } else if ($("#txtConfirmPass").val() != $("#txtNewPass").val()) {
                    alert("两次密码输入不一致");
                    return false;
                }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div div_main">
        <table>
            <tr>
                <td>原密码:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtOldPass" Text="" ClientIDMode="Static" TextMode="Password"  />
                </td>
            </tr>
            <tr>
                <td>新密码:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtNewPass" Text="" ClientIDMode="Static" TextMode="Password" /></td>
            </tr>
            <tr>
                <td>确认密码:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtConfirmPass" Text="" ClientIDMode="Static" TextMode="Password" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button runat="server" ID="btnConfirm" Text="确认" CssClass="btn btn-primary" ClientIDMode="Static" OnClientClick="javascript:OnSthCreated();" OnClick="btnConfirm_Click" /></td>
            </tr>
        </table>

    </div>
</asp:Content>
