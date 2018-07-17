<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CS_MemberServiceMethodUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.CS_MemberServiceMethodUpdate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSaveEdit.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtTyperName.ClientID%>');
            BindText(400,'<%=txtTempLeate.ClientID%>');
        }

    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <table class="table table-bordered table-striped">
        <tr>
            <td>服务方式:</td>
            <td>
                <asp:TextBox ID="txtTyperName" check="1" tip="限20个字符" MaxLength="20" runat="server"></asp:TextBox>
                <span style="color: red">*</span>
            </td>
        </tr>
        <tr>
            <td colspan="2">标签&amp;Name&amp;为新人姓名 
            </td>
        </tr>
        <tr>
            <td>短信模版</td>
            <td>&nbsp;<asp:TextBox ID="txtTempLeate" check="0" tip="限400个字符！" runat="server" TextMode="MultiLine" Rows="10" Width="300"> </asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:Button ID="btnSaveEdit" CssClass="btn" runat="server"  OnClick="btnSaveEdit_Click" Text="保存" />
            </td>
        </tr>
    </table>
</asp:Content>
