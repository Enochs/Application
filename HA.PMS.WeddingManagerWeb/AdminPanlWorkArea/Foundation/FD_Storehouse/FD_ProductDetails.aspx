<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_ProductDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_ProductDetails" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>产品明细</h5>
            <span class="label label-info">明细界面</span>
        </div>
        <div class="widget-content">

            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>订单编号</th>
                        <th>产品名称</th>
                        <th>产品类别</th>
                        <th>项目</th>
                        <th>产品\服务描述</th>
                        <th>资料</th>
                        <th>单价</th>
                        <th>销售价</th>
                        <th>说明</th>
                        <th>数量</th>
                        <th>单位</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptStoreHouse"  runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("OrderCoder") %></td>
                                <td><%#Eval("ProductName") %></td>
                                <td><%#Eval("ProductCategory") %></td>
                                <td><%#Eval("ProjectCategory") %></td>
                                <td><%#Eval("Specifications") %></td>
                                <td><%#Eval("Data") %></td>
                                <td><%#Eval("PurchasePrice") %></td>
                                <td><%#Eval("SalePrice") %></td>
                                <td><%#Eval("Explain") %></td>
                                <td><%#Eval("Count") %></td>
                                <td><%#Eval("Unit") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="ProductPager"  AlwaysShow="true" OnPageChanged="ProductPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>

</asp:Content>
