<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_ImageTypeUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ImageWarehouse.FD_ImageTypeUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     $(window).load(function () {
         BindCtrlRegex();
         BindCtrlEvent('input[check],textarea[check]');
         $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtImageType.ClientID%>');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>图片库修改界面</h5>
            <span class="label label-info">修改页面</span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>图片类型</td>
                    <td>
                        <asp:TextBox ID="txtImageType" check="1" tip="限20个字符！" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Button ID="btnSave"  CssClass="btn btn-success"  runat="server" Text="保存" OnClick="btnSave_Click" />

                    </td>
                    <td></td>
                </tr>

            </table>
        </div>
    </div>
</asp:Content>
