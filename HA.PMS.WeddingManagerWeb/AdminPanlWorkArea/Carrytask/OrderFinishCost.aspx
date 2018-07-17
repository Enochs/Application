<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="OrderFinishCost.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.OrderFinishCost" %>

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
        <table class="table table-bordered table-striped table-select" style="width: 98%;">
            <thead>
                <tr>
                    <th>新人姓名</th>
                    <%--<th>联系电话</th>--%>
                    <th>婚期</th>
                    <%--<th>酒店</th>--%>
                    <th>电销</th>
                    <th>婚礼顾问</th>
                    <th>策划师</th>

                    <%--<th>人员价格</th>
                    <th>人员成本</th>--%>
                    <th>人员利润率</th>

                    <%--<th>物料价格</th>
                    <th>物料成本</th>--%>
                    <th>物料利润率</th>

                    <%--<th>其他价格</th>
                    <th>其他成本</th>--%>
                    <th>其他利润率</th>

                    <th>订单总金额</th>
                    <th>订单总成本</th>
                    <th>利润率</th>
                    <th>客户满意度</th>
                    <th>内部评价</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                    <ItemTemplate>
                        <tr skey='<%#Eval("CustomerID") %><%#Eval("EmployeeID") %>'>
                            <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %>
                                <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' /></td>
                            <%--<td><%#Eval("BrideCellPhone") %></td>--%>
                            <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                            <%--<td><%#Eval("Wineshop") %></td>--%>
                            <td><%#GetInviteName(Eval("CustomerID")) %></td>
                            <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                            <td><%#GetQuotedEmployee(Eval("CustomerID")) %></td>

                            <%--<td><%#GetFinishAmount(Eval("CustomerID"),1) %></td>
                            <td><%#GetCostAmounts(Eval("CustomerID"),1) %></td>--%>
                            <td><%#GetProfitRate(Eval("CustomerID"),1) %></td>

                            <%--<td><%#GetFinishAmount(Eval("CustomerID"),2) %></td>
                            <td><%#GetCostAmounts(Eval("CustomerID"),2) %></td>--%>
                            <td><%#GetProfitRate(Eval("CustomerID"),2) %></td>

                            <%--<td><%#GetFinishAmount(Eval("CustomerID"),3) %></td>
                            <td><%#GetCostAmounts(Eval("CustomerID"),3) %></td>--%>
                            <td><%#GetProfitRate(Eval("CustomerID"),3) %></td>

                            <td><%#GetMoney(Eval("CustomerID"),2) %></td>
                            <td><%#GetMoney(Eval("CustomerID"),1) %></td>
                            <td><%#GetMoney(Eval("CustomerID"),3) %></td>

                            <td><a <%#GetSacNameByCustomernId(Eval("CustomerID")).ToString() == "很糟糕" ? "style='color:red;'" : "" %> style="cursor: pointer;" href="../CS/CS_DegreeOfSatisfactionShow.aspx?DofKey=<%#GetDofKeyByCustomernId(Eval("CustomerID")) %>" target="_blank"><%#GetSacNameByCustomernId(Eval("CustomerID")) %></a></td>
                            <td><a <%#GetSacNameByCustomernId(Eval("CustomerID")).ToString() == "很糟糕" ? "style='color:red;'" : "" %> style="cursor: pointer;" href="../Carrytask/CarryCost/OrderCost.aspx?DispatchingID=<%#GetByCusomerId(Eval("CustomerID"),2) %>&CustomerID=<%#Eval("CustomerID") %>&Type=Details&NeedPopu=1" target="_blank"><%#GetNameByEvaulationId(Eval("EvaluationId")) %><a></td>
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


