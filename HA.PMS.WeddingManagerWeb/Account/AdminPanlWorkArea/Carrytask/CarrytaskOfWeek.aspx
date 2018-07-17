<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskOfWeek.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskOfWeek" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script src="/Scripts/trselection.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>订单状态</th>
                        <th>派工人</th>
                        <th>责任人</th>
                        <th>处理任务</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                             <tr skey='CarrytaskStateKey<%#Eval("StateKey") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#GetQuotedEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("UseSate")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetEmployeeName(User.Identity.Name) %></td>
                                <td><a <%#Eval("Groom")==null?"style='display:none;'":"" %> href="CarrytaskGoNextPage.aspx?DispatchingID=<%#Eval("DispatchingID") %>&OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=1"  class="btn btn-info  btn-mini">处理订单</a>
                                    <a <%#Eval("Groom")==null?"style='display:none;'":"" %> href="CarrytaskTab.aspx?PageNameProductList&DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=1&PageName=ProductList" class="btn btn-success  btn-mini">执行清单</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"  OnPageChanged="BinderData"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
