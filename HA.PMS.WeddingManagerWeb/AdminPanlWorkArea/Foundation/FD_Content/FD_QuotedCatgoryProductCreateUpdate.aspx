<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_QuotedCatgoryProductCreateUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_QuotedCatgoryProductCreateUpdate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () { $("html,body").css({ "width": "450px", "height": "700px", "background-color": "transparent" }); });
        $(window).load(function () {
            BindString(20, '<%=txtSourceProductName.ClientID%>:<%=txtUnit.ClientID%>');

 
            BindText(200, '<%=txtSpecifications.ClientID%>:<%=txtRemark.ClientID%>');

            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnCreate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>产品名称</td>
                    <td>
                        <asp:TextBox ID="txtSourceProductName" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>类别</td>
                    <td>
                        <asp:DropDownList ID="ddlParentCatogry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlParentCatogry_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>项目</td>
                    <td>
                        <asp:DropDownList ID="ddlSecondCatgory" runat="server"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>产品\服务描述</td>
                    <td>
                        <asp:TextBox ID="txtSpecifications" check="0" tip="限200个字符！" runat="server" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>原价</td>
                    <td>
                        <asp:TextBox ID="txtPurchasePrice" check="0" tip="限200个字符！" runat="server" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>销售价</td>
                    <td>
                        <asp:TextBox ID="txtSaleOrice" check="1" tip="（必填）销售单价" CssClass="{digits:true}" runat="server" MaxLength="10"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>单位</td>
                    <td>
                        <asp:TextBox ID="txtUnit" check="1" tip="（必填）产品数量的计量，如：个、套" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>产品属性</td>
                    <td>
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>道具</asp:ListItem>
                            <asp:ListItem>花艺</asp:ListItem>
                            <asp:ListItem>灯光</asp:ListItem>
                            <asp:ListItem>人员</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>备注</td>
                    <td>
                        <asp:TextBox ID="txtRemark" check="0" tip="限200个字符！" runat="server" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:RadioButton ID="rdohstoPakge" runat="server" Visible="false" Text="将本产品加入到标准产品中" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button Text="保存" ID="btnCreate" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnSave_Click" runat="server" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

