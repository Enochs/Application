<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_ImageWarehouseUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ImageWarehouse.FD_ImageWarehouseUpdate" %>

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
         BindString(20, '<%=txtImageName.ClientID%>');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>酒店管理页面</h5>
            <span class="label label-info">管理页面</span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>图片名称</td>
                    <td>
                        <asp:TextBox ID="txtImageName" check="1" tip="限20个字符！" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>图片类型</td>
                    <td>
                        <asp:DropDownList ID="ddlImageType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button CssClass="btn btn-success" ID="btnSave" runat="server" Text="确定" OnClick="btnSave_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
