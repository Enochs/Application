<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceCheckList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceCheckList" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>

    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>新人状态</th>
                        <th>接收时间</th>
                        <th>定金</th>
                        <th>订单金额</th>
                        <th>下次沟通时间</th>
                        <th>审核报价单</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("OrderID") %><%#Eval("QuotedID") %><%#Eval("CustomerID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#GetShortDateString(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmployee(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetShortDateString(Eval("CreateDate")) %></td>
                                <td><%#GetOrderMoney(Eval("OrderID")) %></td>
                                <td><%#Eval("RealAmount") %></td>
                                <td><%#GetShortDateString(Eval("NextFlowDate")) %></td>

                                <td><a class="btn btn-success btn-mini" target="_blank" href="QuotedPriceChecks.aspx?QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>">审核报价单</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="12">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>

                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
