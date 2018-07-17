<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.NoticeCreate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <br />
    <div class="widget-box">
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td><span style="color: red">*</span>消息标题</td>
                    <td>
                        <asp:TextBox ID="txtTitle" check="1" MaxLength="20" tip="限20个字符！" runat="server"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>消息内容</td>
                    <td><%--<asp:TextBox ID="txtContent" check="1" tip="限65535个字符！" TextMode="MultiLine" Rows="10" Width="400" runat="server" />--%>
                        <cc1:CKEditorTool ID="txtContent" Height="200" Width="95%" runat="server" BasePath="~/Scripts/ckeditor/">
                        </cc1:CKEditorTool>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCreate" runat="server" Text="保存" OnClick="btnCreate_Click" /></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").height(450).css({ "background-color": "transparent" });;
        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnCreate.ClientID%>").click(function () {
                  return ValidateForm('input[check],textarea[check]');
              });
          });
          function BindCtrlRegex() {
              BindString(20, '<%=txtTitle.ClientID%>');
            BindText(65535, '<%=txtContent.ClientID%>');
        }
    </script>

</asp:Content>

