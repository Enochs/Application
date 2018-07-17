<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_GuradianLevenCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian.FD_GuradianLevenCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">

         $(document).ready(function () {

             $("html,body").css({ "width": "400px", "height": "200px"});
         });
         $(window).load(function () {
             BindCtrlRegex();
             BindCtrlEvent('input[check]');
             $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check]');
            });
         });
        function BindCtrlRegex() {
            BindString(20, '<%=txtLevenName.ClientID%>');
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        
        <div class="widget-content">
            <table  class="table table-bordered table-striped">
                <tr>
                    <td>等级名称</td>
                    <td>
                        <asp:TextBox ID="txtLevenName" check="1" tip="限20个字符" MaxLength="20" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSave_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
