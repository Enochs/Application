<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_ProductCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_ProductCreate" %>

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
            BindString(20, '<%=txtProductName.ClientID%>:<%=txtSpecifications.ClientID%>');
            BindMoney('<%=txtPrice.ClientID%>:<%=txtSalePrice.ClientID%>');
            BindUInt('<%=txtCount.ClientID%>');
            BindString(10, '<%=txtUnit.ClientID%>');
            BindString(50, '<%=txtExplain.ClientID%>:<%=txtData.ClientID%>');
        }

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>创建产品界面</h5>
            <span class="label label-info">创建界面</span>
        </div>
        <div class="widget-content">

            <table class="table table-bordered table-striped">

                <tr>
                    <td>产品名:</td>
                    <td>
                        <asp:TextBox ID="txtProductName" check="1" tip="限20个字符！" MaxLength="20" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
              
            <tr>
                <td>分类名:</td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
                <tr>
                    <td>供应商:</td>
                    <td>
                        <asp:DropDownList ID="ddlSupplier" runat="server"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>单价:</td>
                    <td>
                        <asp:TextBox ID="txtPrice" check="1" MaxLength="10" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>

                <tr>
                    <td>销售价:</td>
                    <td>
                        <asp:TextBox ID="txtSalePrice" check="1" MaxLength="10" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                 <tr>
                    <td>规格尺寸:</td>
                    <td>
                        <asp:TextBox ID="txtSpecifications" check="0" tip="限20个字符！" MaxLength="20" runat="server"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td>资料:</td>
                    <td>
                        <asp:TextBox ID="txtData" check="0" tip="限50个字符！" runat="server"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td>单位:</td>
                    <td>
                        <asp:TextBox ID="txtUnit" check="1" MaxLength="10" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                  <tr>
                    <td>说明:</td>
                    <td>
                        <asp:TextBox ID="txtExplain" check="0" tip="限50个字符！" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>数量:</td>
                    <td>
                        <asp:TextBox ID="txtCount" check="1" MaxLength="8" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave"  CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSave_Click" /></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
