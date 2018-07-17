<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseProductUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseProductUpdate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () { $("html,body").css({ "width": "450px", "height": "700px", "background-color": "transparent" }); });
        $(window).load(function () {
            BindString(20, '<%=txtSourceProductName.ClientID%>:<%=txtUnit.ClientID%>:<%=txtPosi.ClientID%>');
            BindMoney('<%=txtPurchasePrice.ClientID%>:<%=txtSaleOrice.ClientID%>');
            BindUInt('<%=txtSourceCount.ClientID%>');
            BindText(200, '<%=txtSpecifications.ClientID%>:<%=txtRemark.ClientID%>');
            BindDate('<%=txtPutStoreDates.ClientID%>');
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
                    <td><asp:TextBox ID="txtSourceProductName" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>入库时间</td>
                    <td><asp:TextBox ID="txtPutStoreDates" check="1" ClientIDMode="Static" onclick="WdatePicker();" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>类别</td>
                    <td><cc2:CategoryDropDownList ID="ddlCategory" ParentID="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></cc2:CategoryDropDownList>

                    </td>
                </tr>
                <tr>
                    <td>项目</td>
                    <td><cc2:CategoryDropDownList ID="ddlProject" runat="server"></cc2:CategoryDropDownList></td>
                </tr>

                <tr>
                    <td>产品\服务描述</td>
                    <td><asp:TextBox ID="txtSpecifications" check="0" tip="限200个字符！" runat="server" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>图片</td>
                    <td><asp:FileUpload Width="150" ID="flieUp" ClientIDMode="Static" runat="server" /></td>
                </tr>
                <tr>
                    <td>采购价</td>
                    <td><asp:TextBox ID="txtPurchasePrice" check="1" tip="（必填）采购单价" runat="server" MaxLength="10"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>销售价</td>
                    <td><asp:TextBox ID="txtSaleOrice" check="1" tip="（必填）销售单价" CssClass="{digits:true}" runat="server" MaxLength="10"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>单位</td>
                    <td><asp:TextBox ID="txtUnit" check="1" tip="（必填）产品数量的计量，如：个、套" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td>数量</td>
                    <td><asp:TextBox ID="txtSourceCount" check="0" tip="数量只能为整数，不填默认为 0" runat="server" MaxLength="16"></asp:TextBox>
                    </td>
                </tr>
                <tr >
                    <td>仓位</td>
                    <td><asp:TextBox ID="txtPosi" check="0" tip="产品的存放位置，限20个字符" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>一次性</td>
                    <td><asp:DropDownList ID="ddlDisposible" runat="server" Width="65">
                            <asp:ListItem Value="1">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>备注</td>
                    <td><asp:TextBox ID="txtRemark" check="0" tip="限200个字符！" runat="server" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button Text="保存"  ID="btnCreate" clientidmode="Static" cssclass="btn btn-success" onclick="btnSave_Click" runat="server" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
