<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreSalesList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.StoreSalesList" Title="新人明细表" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/YearSelector.ascx" TagPrefix="HA" TagName="YearSelector" %>

<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#container').highcharts({
                chart: {
                    type: 'line',
                    marginRight: 130,
                    marginBottom: 25
                },
                title: {
                    text: '数量指标',
                    x: -20 //center
                },
                subtitle: {
                    text: 'Source: 好爱科技',
                    x: -20
                },
                xAxis: {
                    categories: ['一月', '二月', '三月', '四月', '五月', '六月',
                        '七月', '八月', '九月', '十月', '十一月', '十二月']
                },
                yAxis: {
                    title: {
                        text: '单位 (个数)'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    valueSuffix: '个'
                },
                legend: {
                    layout: 'horizontal',
                    align: 'center',
                    verticalAlign: 'buttom',
                    x: -10,
                    y: 210,
                    borderWidth: 0
                },
                exporting: {
                    enabled: true //用来设置是否显示‘打印’ '导出'等功能按钮，不设置时默认为
                },
                series: [{
                    name: '总预约量',
                    data: [<%=ViewState["totalOrderCount"]%>]
                }, {
                    name: '实际到店量',
                    data: [<%=ViewState["actualOrderCount"]%>]
                }, {
                    name: '邀约成功量',
                    data: [<%=ViewState["successOrderCount"]%>]
                }, {
                    name: '流失量',
                    data: [<%=ViewState["loseCount"]%>]
                }]
            });


            //质量指标
            $('#quality').highcharts({
                chart: {
                    type: 'line',
                    marginRight: 130,
                    marginBottom: 25
                },
                exporting: {
                    enabled: true //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为
                },
                title: {
                    text: '质量指标',
                    x: -20 //center
                },
                subtitle: {
                    text: 'Source: 好爱科技',
                    x: -20
                },
                xAxis: {
                    categories: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']
                },
                yAxis: {
                    title: {
                        text: '单位 (百分比)'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#83C87B'
                    }]
                },
                tooltip: {
                    valueSuffix: '%'
                },
                legend: {
                    layout: 'horizontal',
                    align: 'center',
                    verticalAlign: 'buttom',
                    x: -10,
                    y: 210,
                    borderWidth: 0
                },
                exporting: {
                    enabled: true //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为
                },
                series: [{
                    name: '到店率',
                    data: [<%=ViewState["actualOrderRate"]%>]
                }, {
                    name: '预订率',
                    data: [<%=ViewState["successOrderRate"]%>]
                }, {
                    name: '流失率',
                    data: [<%=ViewState["loseRate"]%>]
                }]
            });

            //财务指标
            $('#moneyChart').highcharts({
                chart: {
                    type: 'line',
                    marginRight: 130,
                    marginBottom: 25
                },
                title: {
                    text: '财务指标',
                    x: -20 //center
                },
                subtitle: {
                    text: 'Source: 好爱科技',
                    x: -20
                },
                xAxis: {
                    categories: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']
                },
                yAxis: {
                    title: {
                        text: '单位 (元)'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#bbffaa'
                    }]
                },
                tooltip: {
                    valueSuffix: '元'
                },
                legend: {
                    layout: 'horizontal',
                    align: 'center',
                    verticalAlign: 'buttom',
                    x: -10,
                    y: 210,
                    borderWidth: 0
                },
                series: [{
                    name: '订单总额',
                    data: [<%=ViewState["totalEarnestMoney"]%>]
                }, {
                    name: '定金总额',
                    data: [<%=ViewState["totalFinishAmount"]%>]
                }]
            });
            $("html").css({ "overflow-x": "hidden" });
        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">

    <asp:Button ID="btnRefresh" Style="display: none" OnClick="BinderData" ClientIDMode="Static" Text="刷新" runat="server" />
    <div class="widget-box" style="overflow-y: scroll; height: 1000px">
        <div class="widget-content">
            <table>
                <tr>
                    <td>新人状态<cc2:ddlCustomersState ID="DdlCustomersState1" runat="server"></cc2:ddlCustomersState></td>
                    <td>时间<asp:DropDownList runat="server" Width="88" ID="ddlDateType">
                        <asp:ListItem Text="请选择" Value="0" />
                        <asp:ListItem Text="到店时间" Value="1" />
                        <asp:ListItem Text="预订时间" Value="2" />
                        <asp:ListItem Text="婚期时间" Value="3" />
                    </asp:DropDownList>
                    </td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td>新人姓名
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>联系电话
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnQuery_Click" />
                        <cc2:btnReload ID="btnReload2" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>到店时间</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>新人状态</th>
                        <th>婚礼顾问</th>
                        <th>跟单次数</th>
                        <th>已收款</th>
                        <th>策划师</th>
                        <th>订单总额</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom"),Eval("OldB")) %></a>
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#GetShortDateString(Eval("ComeDate")) %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                <td><%#Eval("FlowCount") %></td>
                                <td><%#GetRealityAmount(Eval("OrderID")) %></td>
                                <td><%#GetQuotedEmpLoyeeName(Eval("OrderID"))%></td>
                                <td><%#GetFinishAmount(Eval("OrderID")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <div style="margin-top: 21px;">
                <strong>汇总统计</strong>
                <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-mini btn-primary" Text="导出到Excel" OnClick="btnExportExcel_Click" />
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>统计对象</th>
                        <th>总预约量</th>
                        <th>实际到店量</th>
                        <th>成功预订量</th>
                        <th>定金总额</th>
                        <th>总订单额</th>
                        <th>流失量</th>
                        <th>到店率</th>
                        <th>预订率</th>
                        <th>流失率</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>当前</td>
                        <td>
                            <asp:Literal ID="ltlCurrentTotalOrderCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentActualOrderCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentSuccessOrderCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentTotalEarnestMoney" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentTotalFinishAmount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentLoseCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentActualOrderRate" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentSuccessOrderRate" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentLoseRate" runat="server"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td>历史</td>
                        <td>
                            <asp:Literal ID="ltlHistoryTotalOrderCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlHistoryActualOrderCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlHistorySuccessOrderCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlHistoryTotalEarnestMoney" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlHistoryTotalFinishAmount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlHistoryLoseCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlHistoryActualOrderRate" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlHistorySuccessOrderRate" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlHistoryLoseRate" runat="server"></asp:Literal></td>
                    </tr>
                </tbody>
            </table>
            <HA:YearSelector Title="请选择年份" runat="server" ID="YearSelector" />
            <!--数量指标 -->
            <div id="container" style="height: 272px;"></div>
            <!--质量指标-->
            <div id="quality" style="height: 272px;"></div>
            <!--财务指标-->
            <div id="moneyChart" style="height: 272px;"></div>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
            <script src="../../Scripts/highcharts.js"></script>
            <script src="../../Scripts/exporting.js"></script>
        </div>
    </div>
</asp:Content>
