<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationPackagePriceSpanUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationPackagePriceSpanUpdate" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforEmpLoyee.ascx" TagPrefix="HA" TagName="MessageBoardforEmpLoyee" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "450px", "height": "250px", "background-color": "transparent" });
        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtSpan.ClientID%>');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-content">
                <table class="table table-bordered table-striped">
                    <tr>
                        <td>价格段</td>
                        <td>
                            <asp:TextBox ID="txtSpan" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox>
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
    </div>
</asp:Content>
