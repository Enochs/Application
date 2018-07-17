<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_TargetCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget.FL_TargetCreate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register assembly="HA.PMS.EditoerLibrary" namespace="HA.PMS.EditoerLibrary" tagprefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
     <script type="text/javascript">

         $(document).ready(function () {
             $("html,body").css({ "width": "400px", "height": "360px", "background-color": "transparent" });
         });
         $(window).load(function () {
             BindCtrlRegex();
             BindCtrlEvent('input[check],textarea[check]');
             $("#<%=btnSaveChange.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
         });
        function BindCtrlRegex() {
            BindString(20, '<%=txtTitle.ClientID%>');
            BindText(200, '<%=txtRemark.ClientID%>');
        }
    </script>
    <table  class="table table-bordered table-striped">
        <tr>
            <td><span style="color:red">*</span>标题</td>
            <td>
                <asp:TextBox ID="txtTitle" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td><span style="color:red">*</span>说明</td>
            <td>
                <asp:TextBox ID="txtRemark" check="1" tip="限200个字符！" runat="server" Rows="3" TextMode="MultiLine" Width="200px" MaxLength="200"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>所属模块</td>
            <td>
                <cc1:ddlSysChannel ID="ddlSysChannel1" runat="server">
                </cc1:ddlSysChannel>
            </td>
        </tr>

        <tr>
            <td>类型</td>
            <td>
                <asp:DropDownList ID="ddltype" runat="server">
                    <asp:ListItem Value="1">质量指标</asp:ListItem>
                    <asp:ListItem Value="2">一般指标</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnSaveChange" runat="server" CssClass="btn  btn-success" Text="保存" OnClick="btnSaveChange_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
