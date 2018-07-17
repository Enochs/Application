<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_DeliveryScheduleDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_DeliveryScheduleDetails" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("html").css({ "overflow-x": "hidden" }).css({ "background-color": "transparent" }); });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: 30px; border: 0px;">
                <table>
                    <tr>
                        <td>供应商类别：<asp:DropDownList ID="ddlSupplierType" runat="server"></asp:DropDownList></td>
                        <td>供应商名称：<asp:TextBox ID="txtSupplierName" runat="server" MaxLength="20"></asp:TextBox></td>
                        <td><HA:DateRanger Title="合作日期：" runat="server" ID="CreateDateRanger" /></td>
                        <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="BinderData" /></td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>供应商名称</th>
                        <th>产品类别</th>
                        <th>联系人</th>
                        <th>联系电话</th>
                        <th>合作起始日</th>
<%--                        <th>在库产品总数量</th>
                        <th>供货次数</th>
                        <th>差错次数</th>
                        <th>满意度</th>
                        <th>本月结算额</th>
                        <th>累计结算额</th>--%>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTestSupplier" runat="server">
                        <ItemTemplate>
                            <tr skey='FD_SaleSourcesSupplierID<%#Eval("SupplierID") %>'>
                                <td><a href="FD_SupplierUpdate.aspx?OnlyView=1&SupplierID=<%#Eval("SupplierID") %>" target="_blank"><%#Eval("Name") %></a></td>
                                <td><%#GetSupplierTypeName(Eval("CategoryID")) %></td>
                                <td><%#Eval("Linkman") %></td>
                                <td><%#Eval("CellPhone") %></td>
                                <td><%#ShowShortDate(Eval("StarDate")) %></td>
                               <%-- <td><%#GetProductCount(Eval("SupplierID")) %></td>
                                <td><%#GetProductsUsedTimes(Eval("SupplierID"))%></td>
                                <td><%#GetErroStateCount(Eval("SupplierID")) %></td>
                                <td><%#GetAveragePoint(Eval("SupplierID")) %></td>
                                <td><%#GetOrderfinalCost(Eval("SupplierID"), false) %></td>
                                <td><%#GetOrderfinalCost(Eval("SupplierID"), true) %></td>--%>
                                <td><a href="FD_SupplierUpdate.aspx?SupplierID=<%#Eval("SupplierID") %>" target="_blank" class="btn btn-primary  btn-mini">修改供应商</a>
                                    <a class="btn btn-primary  btn-mini" href='FD_SupplierProductCreate.aspx?find=1&supplierId=<%#Eval("SupplierID") %>' target="_blank">查看产品</a>
                                    <a class="btn btn-primary  btn-mini" href='FD_SupplierProductCreate.aspx?supplierId=<%#Eval("SupplierID") %>' target="_blank">录入产品</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="DeliveryPager" PageSize="12" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
