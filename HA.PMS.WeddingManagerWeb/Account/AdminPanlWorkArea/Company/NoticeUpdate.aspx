<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="NoticeUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.NoticeUpdate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").height(450).css({ "background-color": "transparent" });
        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSure.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtTitle.ClientID%>');
            <%--BindText(65535, '<%=txtNoticeContent.ClientID%>');--%>
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td><span style="color: red">*</span>消息标题</td>
                    <td>
                        <asp:TextBox ID="txtTitle" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>消息内容</td>
                    <td>
                        <%--<asp:TextBox ID="txtNoticeContent" check="1" tip="限65535个字符！" TextMode="MultiLine" Rows="10" Width="400" runat="server" MaxLength="200" />--%>
                        <cc1:CKEditorTool ID="txtNoticeContent" Height="200" Width="99%" runat="server" BasePath="~/Scripts/ckeditor/">
                        </cc1:CKEditorTool>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSure" runat="server" Text="保存" OnClick="btnSure_Click" /></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
