<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDirectCost.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.OrderDirectCost" Title="订单成本明细" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>



<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>

    <div class="widget-box" style="height: 50px; border: 0px;">


        <table class="queryTable">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                            </td>
                            <td>联系电话：</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtContactPhone" /></td>
                            <td>&nbsp;</td>
                            <td>
                                <HA:DateRanger runat="server" ID="PartyDateRanger" Title="婚期：" />
                            </td>
                            <td>

                                <asp:Button ID="btnserch" runat="server" Text="查询" CssClass="btn btn-primary" Height="27" OnClick="btnserch_Click" />
                                <cc2:btnReload runat="server" ID="btnReload" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </div>
    <div style="overflow-y: auto; height: 800px;">
        <table class="table table-bordered table-striped table-select">
            <thead>
                <tr>
                    <th>新人姓名</th>
                    <th>联系电话</th>
                    <th>婚期</th>
                    <th>酒店</th>
                    <th>电销</th>
                    <th>婚礼顾问</th>
                    <th>策划师</th>
                    <th>订单总金额</th>
                    <th>订单总成本</th>
                    <th>利润率</th>
                    <th>填写成本明细</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                    <ItemTemplate>
                        <tr skey='<%#Eval("CustomerID") %><%#Eval("EmployeeID") %>'>
                            <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></td>
                            <td><%#Eval("BrideCellPhone") %></a></td>
                            <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <td><%#GetInviteName(Eval("CustomerID")) %></td>
                            <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                            <td><%#GetQuotedEmployee(Eval("CustomerID")) %></td>
                            <td><%#GetMoney(Eval("CustomerID"),2) %></td>
                            <td><%#GetMoney(Eval("CustomerID"),1) %></td>
                            <td><%#GetMoney(Eval("CustomerID"),3) %></td>
                            <td>
                                <a target="_blank" href="OrderDirectCostCreate.aspx?OrderID=<%#GetByCusomerId(Eval("CustomerID"),1) %>&DispatchingID=<%#GetByCusomerId(Eval("CustomerID"),2) %>&CustomerID=<%#Eval("CustomerID") %>" <%#GetIsLock(Eval("CustomerID"),1) %> class="btn btn-danger btn-mini">填写成本明细</a>
                                <a target="_blank" href="OrderDirectCostShow.aspx?OrderID=<%#GetByCusomerId(Eval("CustomerID"),1) %>&DispatchingID=<%#GetByCusomerId(Eval("CustomerID"),2) %>&CustomerID=<%#Eval("CustomerID") %>" <%#GetIsLock(Eval("CustomerID"),2) %> class="btn btn-success btn-mini">查看成本明细</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>本页合计:</td>
                    <td>
                        <asp:Label runat="server" ID="lblPageOrderSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblPageCostSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblPageRaeSum" /></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>本期合计:</td>
                    <td>
                        <asp:Label runat="server" ID="lblOrderSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblCostSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblRaeSum" /></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="9">
                        <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                        </cc1:AspNetPagerTool>
                    </td>
                </tr>
                <tr>
                    <td colspan="9">
                        <asp:Button runat="server" ID="btnExport" Text="导出Excel" CssClass="btn btn-primary" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </tfoot>
        </table>

        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" Visible="false">
            <ItemTemplate>
                <table class="table table-bordered table-striped table-select">
                    <thead>
                        <tr>
                            <th>新人姓名</th>
                            <th>联系电话</th>
                            <th>婚期</th>
                            <th>酒店</th>
                            <th>婚礼顾问</th>
                            <th>策划师</th>
                            <th>订单总金额</th>
                            <th>订单总成本</th>
                            <th>利润率</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                            <ItemTemplate>
                                <tr skey='<%#Eval("CustomerID") %><%#Eval("EmployeeID") %>'>
                                    <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></td>
                                    <td><%#Eval("BrideCellPhone") %></a></td>
                                    <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                    <td><%#Eval("Wineshop") %></td>
                                    <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                                    <td><%#GetQuotedEmployee(Eval("CustomerID")) %></td>
                                    <td><%#GetMoney(Eval("CustomerID"),2) %></td>
                                    <td><%#GetMoney(Eval("CustomerID"),1) %></td>
                                    <td><%#GetMoney(Eval("CustomerID"),3) %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </ItemTemplate>
        </asp:Repeater>

        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>

