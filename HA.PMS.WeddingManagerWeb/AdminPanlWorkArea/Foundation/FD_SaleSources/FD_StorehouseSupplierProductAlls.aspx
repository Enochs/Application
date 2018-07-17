<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseSupplierProductAlls.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_StorehouseSupplierProductAlls" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html").css({ "overflow-x": "hidden", "background-color": "transparent" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>供应商名称<asp:TextBox ID="txtSupplierName" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td>产品名称<asp:TextBox ID="txtProductName" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td>产品类别<cc2:CategoryDropDownList ID="ddlCategory" ParentID="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></cc2:CategoryDropDownList>
                        <cc2:CategoryDropDownList ID="ddlProject" runat="server"></cc2:CategoryDropDownList></td>
                    <td><asp:Button ID="btnQuery" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="btnQuery_Click" /></td>
                </tr>
                <tr><td><asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="btn btn-warning" Text="保存选择" /></td></tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th></th>
                        <th>供应商名称</th>
                        <th>产品名称</th>
                        <th>产品类别</th>
                        <th>项目</th>
                        <th>规格尺寸</th>
                        <th>采购单价</th>
                        <th>销售单价</th>
                        <th>数量</th>
                        <th>单位</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptMain" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox style="padding:0;margin:0" ID="ckSinger" ToolTip='<%#Eval("Keys") %>' runat="server" /></td>
                                <td style="width:120px"><%#Eval("SupplierName") %></td>
                                <td style="width:120px"><%#Eval("ProductName") %></td>
                                <td style="width:120px"><%#GetCategoryName(Eval("ProductCategory")) %></td>
                                <td style="width:120px"><%#GetCategoryName(Eval("ProjectCategory")) %></td>
                                <td style="width: 200px"><%#Eval("Specifications") %></td>
                                <td><%#Eval("PurchasePrice") %></td>
                                <td><%#Eval("SalePrice") %></td>
                                <td><%#Eval("Count") %></td>
                                <td><%#Eval("Unit") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
