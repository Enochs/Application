<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SysEmployeeJobUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.SysEmployeeJobUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                $("html,body").css({ "width": "500px", "height": "250px", "background-color": "transparent" });
            });
            $(window).load(function () {
                BindCtrlRegex();
                BindCtrlEvent('input[check],textarea[check]');
                $("#<%=btnSure.ClientID%>").click(function () {
                    return ValidateForm('input[check],textarea[check]');
                });
            });
            function BindCtrlRegex() {
                BindString(20, '<%=txtJobName.ClientID%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>修改职务</h5>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>职务名称</td>
                    <td>
                        <asp:TextBox ID="txtJobName" check="1" tip="限20个字符！" runat="server" MaxLength="20" />
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnSure" CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSure_Click" /></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
