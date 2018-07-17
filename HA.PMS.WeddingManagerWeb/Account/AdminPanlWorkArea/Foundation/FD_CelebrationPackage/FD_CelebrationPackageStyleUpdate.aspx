<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationPackageStyleUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationPackageStyleUpdate" %>

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
            BindString(20, '<%=txtStyleName.ClientID%>');
            BindText(50, '<%=txtExplain.ClientID%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-content">
                <table class="table table-bordered table-striped">
                    <tr>
                        <td>套系风格名</td>
                        <td>
                            <asp:TextBox ID="txtStyleName"  MaxLength="20"  check="1" tip="限20个字符！" runat="server"></asp:TextBox>
                            <span style="color:red">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td>风格简介</td>
                        <td>
                            <asp:TextBox ID="txtExplain" TextMode="MultiLine" Rows="4" MaxLength="50" check="0" tip="限50个字符！" runat="server"></asp:TextBox>
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
