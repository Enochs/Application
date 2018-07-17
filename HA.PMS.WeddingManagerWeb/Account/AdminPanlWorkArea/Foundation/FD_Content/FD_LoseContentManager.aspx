<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_LoseContentManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_LoseContentManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" Title="邀约流失原因" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check]');
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtTitle.ClientID%>');
            $("input").each(function () {
                var ctrl = $(this);
                ctrl.attr("orival", ctrl.val());
            }).attr("reg", "^.{1,20}$");
        }
        function CheckSuccess(ctrl) {
            return ValidateForm('input[check]');
        }
        function ValidateThis(ctrl) {
            var valc = $(ctrl).parent("td").prev("td").children("input");
            if (valc.val() == '') {
                valc.val(valc.attr("orival"));
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <table class="table table-bordered table-striped" style="width:75%;">
        <tr>
            <td>原因</td>
            <td>保存</td>

        </tr>

        <asp:Repeater runat="server" ID="repLostContentList" OnItemCommand="repLostContentList_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="txtTitle" MaxLength="20" runat="server" Text='<%#Eval("Title") %>'></asp:TextBox></td>
                    <td>
                        <asp:LinkButton ID="lbnbtnSave" OnClientClick="return ValidateThis(this);" CommandName="Edit" runat="server" CommandArgument='<%#Eval("ContentID") %>' CssClass="btn btn-primary btn-mini">修改</asp:LinkButton></td>

                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td>
                <asp:TextBox ID="txtTitle" check="1" tip="限20个字符！" MaxLength="20" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtContent" runat="server" Visible="false"></asp:TextBox>
                <asp:LinkButton ID="lnkbtnSave" OnClientClick="return CheckSuccess(this);" runat="server" OnClick="lnkbtnSave_Click"  CssClass="btn btn-success btn-mini">保存</asp:LinkButton>
            </td>



        </tr>
    </table>
</asp:Content>
