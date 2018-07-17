<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierTypeUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.FD_SupplierTypeUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "400px", "height": "250px", "background-color": "transparent" });
        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check]');
            $("#<%=btnSend.ClientID%>").click(function () {
                return ValidateForm('input[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtSupplieType.ClientID%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
     <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>供应商类别修改界面</h5>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>类别名</td>
                    <td>
                        <asp:TextBox ID="txtSupplieType" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnSend" runat="server" CssClass="btn btn-success" Text="保存"  OnClick="btnSend_Click" /></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
