<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_CategoryCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_CategoryCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "500px", "height": "170px", "background-color": "transparent" });
        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                  return ValidateForm('input[check],textarea[check]');
              });
          });
          function BindCtrlRegex() {
              BindString(20, '<%=txtCategoryName.ClientID%>');
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td class="auto-style1">产品分类名:</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtCategoryName" Width="130" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <span style="color: red">*</span>
                    </td>
                    <td class="auto-style1">&nbsp;</td>
                </tr>
                <tr>
                    <td>属性&nbsp;
                    </td>
                    <td colspan="3">
                        <asp:RadioButtonList ID="rdolist" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">物料</asp:ListItem>
                            <asp:ListItem Value="1">专业团队</asp:ListItem>

                            <asp:ListItem Value="2">花艺</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSave_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>


</asp:Content>
