<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SupplierCreate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            //$("#ContentPlaceHolder1_txtStarDate").datepicker({ dateFormat: 'yy-mm-dd ' });
            showPopuWindows($("#CreateSupplier").attr("href"), 800, 1000, "a#CreateSupplier");
        });
        $(window).load(function () {
            BindString(20, '<%=txtName.ClientID%>');
            BindString(5, '<%=txtLinkman.ClientID%>');
            BindDate('<%=txtStarDate.ClientID%>');
            BindMobile('<%=txtCellPhone.ClientID%>');
            BindEmail('<%=txtEmail.ClientID%>');
            BindTel('<%=txtTelPhone.ClientID%>');
            BindString(30, '<%=txtAddress.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=BtnCreate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
            $('#<%=txtStarDate.ClientID%>').change(function () { $(this).blur(); });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>供应商名</td>
                    <td>
                        <asp:TextBox ID="txtName" ToolTip="限20个字符！" check="1" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                    <td>地址</td>
                    <td>
                        <asp:TextBox ID="txtAddress" check="0" CssClass="{required:true}" runat="server" MaxLength="30"></asp:TextBox>
                    </td>
                    <td>开始合作时间</td>
                    <td>
                        <asp:TextBox ID="txtStarDate" check="0" onclick="WdatePicker();" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>联系人</td>
                    <td>
                        <asp:TextBox ID="txtLinkman" check="1" CssClass="{required:true}" runat="server" MaxLength="10"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                    <td>联系电话</td>
                    <td>
                        <asp:TextBox ID="txtCellPhone" ToolTip="手机号码为11位数字！" check="1" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                    <td>QQ</td>
                    <td>
                        <asp:TextBox ID="txtQQ" check="0" CssClass="{required:true,number:true,rangelength:[6,15],messages:{number:'请你输入数字',rangelength:'请你输入6到15位的qq号码'}}" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>
                        <asp:TextBox ID="txtEmail" check="0" CssClass="{required:true,email:true}" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>座机</td>
                    <td>
                        <asp:TextBox ID="txtTelPhone" check="0" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>供应商类别</td>
                    <td>
                        <asp:DropDownList ID="ddlSupplierType" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>开户银行</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtBankName" />
                    </td>
                    <td>银行帐号</td>
                    <td>

                        <asp:TextBox ID="txtAccount" check="0" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="BtnCreate" CssClass="btn btn-success" runat="server" Text="保存" OnClick="btnSave_Click" />
                        <%--<cc1:ClickOnceButton ID="btnSave" CssClass="btn btn-success" runat="server" Text="保存" OnClick="btnSave_Click" />--%>
                       &nbsp;
                        <asp:LinkButton ID="btnFreshen" OnClick="btnFreshen_Click" runat="server" Visible="false" CssClass="btn btn-success" Text="刷新供应商类别" />
                        <asp:Button ID="btnEdit" CssClass="btn btn-primary" OnClick="btnEdit_Click" runat="server" Text="编辑" />
                    </td>
                    <td><a href="../../Sys/Personnel/FD_SupplierTypeManager.aspx" class="btn btn-primary" id="CreateSupplier">管理供应商类别</a></td>
                    <td>
                        <asp:HyperLink ID="hlCreateProduct" Visible="false" CssClass="btn btn-primary  btn-mini"
                            runat="server">为该供应商添加产品</asp:HyperLink>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
