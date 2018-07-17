<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="Sys_EmployeeTypeUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_EmployeeTypeUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "500px", "height": "250px", "background-color": "transparent" });

        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check]');
            $("#<%=btnEmployeeType.ClientID%>").click(function () {
                return ValidateForm('input[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtEmployeeType.ClientID%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>修改人员类型</h5>
        </div>
        <div class="widget-content">
        <table class="table table-bordered table-striped">
            <tr>
                <td>人员类型</td>
                <td >
                    <asp:TextBox ID="txtEmployeeType" check="1" tip="限20个字符！" runat="server" MaxLength="20" />
                    <span style="color:red">*</span>
                </td>
            </tr>
            <tr>
                <td> <asp:Button ID="btnEmployeeType"  CssClass="btn btn-success" runat="server" Text="确认" OnClick="btnEmployeeType_Click" /></td>
                <td></td>
            </tr>
        </table>
  </div></div>
</asp:Content>
