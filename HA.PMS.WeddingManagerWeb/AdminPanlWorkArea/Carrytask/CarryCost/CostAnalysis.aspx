<%@ Page Title="成本分析" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CostAnalysis.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost.CostAnalysis" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                                <HA:MyManager runat="server" ID="MyManager" Title="策划师" />
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
                    <th>婚期</th>
                    <th>策划师</th>
                    <th>人员售价</th>
                    <th>人员成本</th>
                    <th>人员利润率</th>

                    <th>物料售价</th>
                    <th>物料成本</th>
                    <th>物料利润率</th>

                    <th>其它售价</th>
                    <th>其它成本</th>
                    <th>其他利润率</th>
                    <th>订单总金额</th>
                    <th>订单总成本</th>
                    <th>利润率</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                    <ItemTemplate>
                        <tr skey='<%#Eval("CustomerID") %><%#Eval("EmployeeID") %>'>
                            <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %>
                                <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' /></td>
                            <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                            <td><%#GetQuotedEmployee(Eval("CustomerID")) %></td>

                            <td><%#GetFinishAmount(Eval("CustomerID"),1) %></td>
                            <td><%#GetCostAmounts(Eval("CustomerID"),1) %></td>
                            <td><%#GetProfitRate(Eval("CustomerID"),1) %></td>

                            <td><%#GetFinishAmount(Eval("CustomerID"),2) %></td>
                            <td><%#GetCostAmounts(Eval("CustomerID"),2) %></td>
                            <td><%#GetProfitRate(Eval("CustomerID"),2) %></td>

                            <td><%#GetFinishAmount(Eval("CustomerID"),3) %></td>
                            <td><%#GetCostAmounts(Eval("CustomerID"),3) %></td>
                            <td><%#GetProfitRate(Eval("CustomerID"),3) %></td>

                            <td><%#GetMoney(Eval("CustomerID"),2) %></td>
                            <td><%#GetMoney(Eval("CustomerID"),1) %></td>
                            <td><%#GetMoney(Eval("CustomerID"),3) %></td>
                            <td>
                                <a target="_blank" href="/AdminPanlWorkArea/FinancialAffairs/OrderDirectCostShow.aspx?OrderID=<%#GetByCusomerId(Eval("CustomerID"),1) %>&DispatchingID=<%#GetByCusomerId(Eval("CustomerID"),2) %>&CustomerID=<%#Eval("CustomerID") %>" class="btn btn-success btn-mini">查看</a>
                                <%--<%#GetIsLock(Eval("CustomerID"),2) %>--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td></td>
                    <td></td>
                    <td>本页合计:</td>
                    <!--人员-->
                    <td>
                        <asp:Label runat="server" ID="lblPagePersonSale" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblPagePersonCost" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblPagePersonRate" /></td>

                    <!--物料-->
                    <td>
                        <asp:Label runat="server" ID="lblPageMaterialSale" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblPageMaterialCost" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblPageMaterialRate" /></td>

                    <!--其它-->
                    <td>
                        <asp:Label runat="server" ID="lblPageOtherSale" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblPageOtherCost" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblPageOtherRate" /></td>

                    <!--合计-->
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
                    <td>本期合计:</td>
                    <td><asp:Label runat="server" ID="lblSumPersonSale" /></td>
                    <td><asp:Label runat="server" ID="lblSumPersonCost" /></td>
                    <td><asp:Label runat="server" ID="lblSumPersonRate" /></td>
                    <td><asp:Label runat="server" ID="lblSumMaterialSale" /></td>
                    <td><asp:Label runat="server" ID="lblSumMaterialCost" /></td>
                    <td><asp:Label runat="server" ID="lblSumMaterialRate" /></td>
                    <td><asp:Label runat="server" ID="lblSumOtherSale" /></td>
                    <td><asp:Label runat="server" ID="lblSumOtherCost" /></td>
                    <td><asp:Label runat="server" ID="lblSumOtherRate" /></td>
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
