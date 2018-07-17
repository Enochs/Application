<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierCostList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.SupplierCostList" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Contenet1">
    <script src="/Scripts/trselection.js"></script>
</asp:Content>



<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div class="widget-box" style="height: 30px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>供应商名称：<asp:TextBox ID="txtSupplierName" runat="server" /></td>
                        <td><HA:DateRanger runat="server" Title="婚期：" ID="PartyDateRanger" /></td>
                        <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" /></td>
                    </tr>
                </table>
            </div>
    <div style="overflow-y: auto; height: 800px;">
        <table class="table table-bordered table-striped table-select">
            <thead>
                <tr>
                    
                    <th>新人姓名</th>
                    <th>供应商名称</th>
                    <th>婚期</th>
                    <th>酒店</th>
                    <th>婚礼顾问</th>
                    <th>婚礼策划</th>
                    <th>付款总金额</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server">

                    <ItemTemplate>
                        <tr skey='<%#Eval("CustomerID") %><%#Eval("SupplierName") %>'>
                            <td><%#GetCustomerByDispatchingID(Eval("DispatchingID")).Bride %></td>
                            <td><%#Eval("SupplierName") %></td>
                            <td><%#ShowPartyDate(GetCustomerByDispatchingID(Eval("DispatchingID")).PartyDate) %></td>
                            <td><%#GetCustomerByDispatchingID(Eval("DispatchingID")).Wineshop %></td>
                            <td><%#GetOrderEmpLoyeeNameByCustomerID(GetCustomerByDispatchingID(Eval("DispatchingID")).CustomerID) %></td>
                            <td><%#GetQuotedEmployee(GetCustomerByDispatchingID(Eval("DispatchingID")).CustomerID) %></td>
                            <td><%#GetSupplierFinalCostsByDispatchingID(Eval("DispatchingID"),Eval("SupplierName")) %></td>
                            <td><a href="SupplierProductList.aspx?OrderID=<%#Eval("OrderID") %>&DispatchingID=<%#Eval("DispatchingID") %>&SupplierID=<%#GetSupplierID(Eval("SupplierName")) %>&CustomerID=<%#GetCustomerByDispatchingID(Eval("DispatchingID")).CustomerID %>" class="btn btn-info btn-mini">查看支付明细</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="8"><cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool></td>
                </tr>
            </tfoot>
        </table>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>



