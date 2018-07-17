<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="Sys_EmpLoyeeHigherupsCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_EmpLoyeeHigherupsCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "500px", "height": "250px", "background-color": "transparent" });
        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check]');
            $("#<%=btnEmployeeHigherups.ClientID%>").click(function () {
                return ValidateForm('input[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtType.ClientID%>');
            BindString(100, '<%=txtCode.ClientID%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>员工详细信息界面</h5>
       
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>类型:</td>
                    <td>
                        <asp:TextBox ID="txtType" check="1" tip="限20个字符！" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>Code:</td>
                    <td>
                        <asp:TextBox ID="txtCode" check="1" tip="限100个字符！" CssClass="{required:true}" runat="server" MaxLength="100"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button CssClass="btn btn-success" ID="btnEmployeeHigherups" runat="server" Text="确定" OnClick="btnEmployeeHigherups_Click" /></td>
                    <td></td>
                </tr>


            </table>
        </div>
    </div>
</asp:Content>
