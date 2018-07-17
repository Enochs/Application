<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDirectCost.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.OrderDirectCost" Title="订单成本" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-x: auto;">

        <table class="table table-bordered table-striped">
            <thead>
                <tr>
                    <th>新人姓名</th>
                    <th>联系电话</th>
                    <th>婚期</th>
                    <th>酒店</th>
                    <th>订单总金额</th>
                    <th>订单总成本</th>
                    <th>利润率</th>
                    <th>成本明细</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>">

                                    <%#Eval("Bride") %></a>

                            </td>
                            <td><%#Eval("BrideCellPhone") %></td>
                            <td><%#GetShortDateString(Eval("PartyDate")) %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <td><%#GetOtherItem(Eval("OrderID"),1) %></td>
                            <td><%#GetOtherItem(Eval("OrderID"),3) %></td>
                            <td><%#GetOtherItem(Eval("OrderID"),2) %></td>
                            <td>
                                <a href="OrderDirectShow.aspx?OrderID=<%#Eval("OrderID") %>&DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>">查看成本明细</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="8">
                        <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"   OnPageChanged="CtrPageIndex_PageChanged">
                        </cc1:AspNetPagerTool>
                    </td>
                </tr>
            </tfoot>
        </table>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
</asp:Content>

