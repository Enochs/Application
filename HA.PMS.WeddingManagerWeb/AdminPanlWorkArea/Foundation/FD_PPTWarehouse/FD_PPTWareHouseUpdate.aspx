<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_PPTWareHouseUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_PPTWarehouse.FD_PPTWareHouseUpdate" StylesheetTheme="" %>

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
            BindString(20, '<%=txtPPTTitle.ClientID%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>PPT修改界面</h5>
            
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>模版名称</td>
                    <td>
                        <asp:TextBox ID="txtPPTTitle" check="1" tip="限20个字符！" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>模板风格</td>
                    <td>
                        <asp:DropDownList ID="ddlImageType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>关键字</td>
                    <td>
                        <asp:TextBox ID="txtLoadLabel"  CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Button CssClass="btn btn-success" ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
