<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_QuotedCatgoryCreateEdit.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_QuotedCatgoryCreateEdit" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register assembly="HA.PMS.EditoerLibrary" namespace="HA.PMS.EditoerLibrary" tagprefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content1">
    <script type="text/javascript">
    $(document).ready(function () {
        $("html,body").css({ "height": "680px", "background-color": "transparent" });
    });
    $(window).load(function () {
        BindCtrlRegex();
        BindCtrlEvent('input[check],textarea[check]');
        $("#<%=btnCreate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtDeparName.ClientID%>');
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <a href="#" id="SelectDepartmetnByThis" style="display: none;">选择部门</a>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>创建/修改类别</h5>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>类别名:</td>
                    <td>
                        <asp:TextBox ID="txtDeparName" check="1" tip="限20个字符" MaxLength="20" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTitle" runat="server" Text="属性"></asp:Label></td>
                    <td>
                        <asp:RadioButtonList ID="rdolist" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">专业团队</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">物料</asp:ListItem>
                            <asp:ListItem Value="2">花艺</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
 
                <tr>
                    <td>

                        <asp:Button ID="btnCreate" runat="server" CssClass="btn btn-success"   Text="保存" OnClick="btnSaveChange_Click"  />
         

                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
