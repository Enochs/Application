<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseProductStatistics.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseProductStatistics" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/OrderSelector.ascx" TagPrefix="HA" TagName="OrderSelector" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="overflow-x: auto;">
        <table>
            <tr>
                <td style="display:none">每页显示个数<asp:TextBox ID="txtShowCount" Width="30" runat="server" Text="10" MaxLength="5"/></td>
                <td style="display:none"><HA:OrderSelector runat="server" ID="OrderSelector" /><asp:DropDownList ID="ddlOrderColumn" runat="server"></asp:DropDownList></td>
            </tr>
        </table>
        <table class="table table-bordered table-striped table-select">
            <thead>
                <tr>
                    <th>产品名称</th>
                    <th>入库时间</th>
                    <th>产品类别</th>
                    <th>项目</th>
                    <th>采购单价</th>
                    <th>销售单价</th>
                    <th>数量</th>
                    <th>单位</th>
                    <th>剩余数量</th>
                    <th>最近使用时间</th>
                    <th>使用总次数</th>
                    <th>仓位</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptStorehouse" runat="server">
                    <ItemTemplate>
                        <tr skey='FD_Storehouse<%#Eval("SourceProductId") %>'>
                            <td><%#Eval("SourceProductName") %></td>
                            <td><%#Eval("PutStoreDate") %></td>
                            <td><%#GetCategoryName(Eval("ProductCategory")) %></td>
                            <td><%#GetCategoryName(Eval("ProductProject")) %></td>
                            <td><%#Eval("PurchasePrice") %></td>
                            <td><%#Eval("SaleOrice") %></td>
                            <td><%#Eval("SourceCount") %></td>
                            <td><%#Eval("Unit") %></td>
                            <td><%#Eval("LeaveCount") %></td>
                            <td><%#Eval("LastUsedDate") %></td>
                            <td><%#Eval("UsedTimes") %></td>
                            <td><%#Eval("Position") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="12">
                        <cc1:AspNetPagerTool ID="StorePager" PageSize="20" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
                    </td>
                </tr>
            </tfoot>
        </table>

    </div>
</asp:Content>
