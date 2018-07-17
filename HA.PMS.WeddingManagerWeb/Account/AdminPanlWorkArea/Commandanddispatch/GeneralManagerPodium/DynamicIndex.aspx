<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="DynamicIndex.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium.DynamicIndex" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CountMananger/DispatchingOrderCount.ascx" TagPrefix="HA" TagName="DispatchingOrderCount" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CountMananger/NewOrderMoneyCount.ascx" TagPrefix="HA" TagName="NewOrderMoneyCount" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {


        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <HA:DispatchingOrderCount runat="server" id="DispatchingOrderCount" />
   
    <br />
    
    <HA:NewOrderMoneyCount runat="server" id="NewOrderMoneyCount" />

    <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    <script src="../../../Scripts/highcharts.js"></script>
    <script src="../../../Scripts/exporting.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
           
        });
    </script>--%>

    <div class="div_all">
        <div style="overflow: auto;">
            请选择年份: 
            <asp:DropDownList ID="ddlChooseYear" AutoPostBack="true" OnSelectedIndexChanged="DataKChartBinder" runat="server">
                <asp:ListItem Text="2013" Value="2013-01-01,2013-12-31"></asp:ListItem>
                <asp:ListItem Text="2014" Value="2014-01-01,2014-12-31"></asp:ListItem>
                <asp:ListItem Text="2015" Value="2015-01-01,2015-12-31"></asp:ListItem>
                <asp:ListItem Text="2016" Value="2016-01-01,2016-12-31"></asp:ListItem>
                <asp:ListItem Text="2017" Value="2017-01-01,2017-12-31"></asp:ListItem>
                <asp:ListItem Text="2018" Value="2018-01-01,2018-12-31"></asp:ListItem>
                <asp:ListItem Text="2019" Value="2019-01-01,2019-12-31"></asp:ListItem>
                <asp:ListItem Text="2020" Value="2020-01-01,2020-12-31"></asp:ListItem>
                <asp:ListItem Text="2021" Value="2021-01-01,2021-12-31"></asp:ListItem>
                <asp:ListItem Text="2022" Value="2022-01-01,2022-12-31"></asp:ListItem>
                <asp:ListItem Text="2023" Value="2023-01-01,2023-12-31"></asp:ListItem>
                <asp:ListItem Text="2024" Value="2024-01-01,2024-12-31"></asp:ListItem>
            </asp:DropDownList>
            <div class="div_tables">
                <table class="table table-bordered table-striped table-select" style="width: 100%;">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>

                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>月份</td>
                        <td style="width: 80px;">1</td>
                        <td style="width: 80px;">2</td>
                        <td style="width: 80px;">3</td>
                        <td style="width: 80px;">4</td>
                        <td style="width: 80px;">5</td>
                        <td style="width: 80px;">6</td>
                        <td style="width: 80px;">7</td>
                        <td style="width: 80px;">8</td>
                        <td style="width: 80px;">9</td>
                        <td style="width: 80px;">10</td>
                        <td style="width: 80px;">11</td>
                        <td style="width: 80px;">12</td>
                        <td>合计</td>
                    </tr>
                    <tr>
                        <td rowspan="4" style="white-space: nowrap;">销售指标</td>
                        <td style="white-space: nowrap;">现金流</td>
                        <td><%=BinderReturnMoney(1) %></td>
                        <td><%=BinderReturnMoney(2) %></td>
                        <td><%=BinderReturnMoney(3) %></td>
                        <td><%=BinderReturnMoney(4) %></td>
                        <td><%=BinderReturnMoney(5) %></td>
                        <td><%=BinderReturnMoney(6) %></td>
                        <td><%=BinderReturnMoney(7) %></td>
                        <td><%=BinderReturnMoney(8) %></td>
                        <td><%=BinderReturnMoney(9) %></td>
                        <td><%=BinderReturnMoney(10) %></td>
                        <td><%=BinderReturnMoney(11) %></td>
                        <td><%=BinderReturnMoney(12) %></td>
                        <td><%=BinderReturnMoney(13) %></td>
                    </tr>

                    <tr>
                        <td>入客量</td>

                        <td><%=GetNewCustomerByMonth(1) %></td>
                        <td><%=GetNewCustomerByMonth(2) %></td>
                        <td><%=GetNewCustomerByMonth(3) %></td>
                        <td><%=GetNewCustomerByMonth(4) %></td>
                        <td><%=GetNewCustomerByMonth(5) %></td>
                        <td><%=GetNewCustomerByMonth(6) %></td>
                        <td><%=GetNewCustomerByMonth(7) %></td>
                        <td><%=GetNewCustomerByMonth(8) %></td>
                        <td><%=GetNewCustomerByMonth(9) %></td>
                        <td><%=GetNewCustomerByMonth(10) %></td>
                        <td><%=GetNewCustomerByMonth(11) %></td>
                        <td><%=GetNewCustomerByMonth(12) %></td>
                        <td><%=GetNewCustomerByMonth(13) %></td>


                    </tr>

                    <tr>
                        <td>签单量</td>
                        <td><%=GetSucessCustomerByMonth(1) %></td>
                        <td><%=GetSucessCustomerByMonth(2) %></td>
                        <td><%=GetSucessCustomerByMonth(3) %></td>
                        <td><%=GetSucessCustomerByMonth(4) %></td>
                        <td><%=GetSucessCustomerByMonth(5) %></td>
                        <td><%=GetSucessCustomerByMonth(6) %></td>
                        <td><%=GetSucessCustomerByMonth(7) %></td>
                        <td><%=GetSucessCustomerByMonth(8) %></td>
                        <td><%=GetSucessCustomerByMonth(9) %></td>
                        <td><%=GetSucessCustomerByMonth(10) %></td>
                        <td><%=GetSucessCustomerByMonth(11) %></td>
                        <td><%=GetSucessCustomerByMonth(12) %></td>
                        <td><%=GetSucessCustomerByMonth(13) %></td>
                    </tr>

                    <tr>
                        <td>成交率</td>
                        <td><%=GetTurnoverRateByMonth(1) %></td>
                        <td><%=GetTurnoverRateByMonth(2) %></td>
                        <td><%=GetTurnoverRateByMonth(3) %></td>
                        <td><%=GetTurnoverRateByMonth(4) %></td>
                        <td><%=GetTurnoverRateByMonth(5) %></td>
                        <td><%=GetTurnoverRateByMonth(6) %></td>
                        <td><%=GetTurnoverRateByMonth(7) %></td>
                        <td><%=GetTurnoverRateByMonth(8) %></td>
                        <td><%=GetTurnoverRateByMonth(9) %></td>
                        <td><%=GetTurnoverRateByMonth(10) %></td>
                        <td><%=GetTurnoverRateByMonth(11) %></td>
                        <td><%=GetTurnoverRateByMonth(12) %></td>
                        <td><%=GetTurnoverRateByMonth(13) %></td>

                    </tr>

                    <tr>
                        <td rowspan="7">财务指标</td>
                        <td>执行额</td>
                        <td><%=GetCustomFinishSumMoneyByMonth(1) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(2) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(3) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(4) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(5) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(6) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(7) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(8) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(9) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(10) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(11) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(12) %></td>
                        <td><%=GetCustomFinishSumMoneyByMonth(13) %></td>

                    </tr>

                    <tr>
                        <td>执行量</td>
                        <td><%=GetSucessCustomerCountByYearMonth(1) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(2) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(3) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(4) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(5) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(6) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(7) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(8) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(9) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(10) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(11) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(12) %></td>
                        <td><%=GetSucessCustomerCountByYearMonth(13) %></td>

                    </tr>
                    <tr>
                        <td>平均消费</td>
                        <td><%=GeAvgtQuotedMoneyByMonth(1) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(2) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(3) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(4) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(5) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(6) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(7) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(8) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(9) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(10) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(11) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(12) %></td>
                        <td><%=GeAvgtQuotedMoneyByMonth(13) %></td>
                    </tr>
                    <tr>
                        <td>成本</td>
                        <td><%=GetCostMoneyByMonth(1) %></td>
                        <td><%=GetCostMoneyByMonth(2) %></td>
                        <td><%=GetCostMoneyByMonth(3) %></td>
                        <td><%=GetCostMoneyByMonth(4) %></td>
                        <td><%=GetCostMoneyByMonth(5) %></td>
                        <td><%=GetCostMoneyByMonth(6) %></td>
                        <td><%=GetCostMoneyByMonth(7) %></td>
                        <td><%=GetCostMoneyByMonth(8) %></td>
                        <td><%=GetCostMoneyByMonth(9) %></td>

                        <td><%=GetCostMoneyByMonth(10) %></td>
                        <td><%=GetCostMoneyByMonth(11) %></td>
                        <td><%=GetCostMoneyByMonth(12) %></td>
                        <td><%=GetCostMoneyByMonth(13) %></td>

                    </tr>
                    <tr>
                        <td>毛利润</td>
                        <td><%=GetProfitByMonth(1) %></td>
                        <td><%=GetProfitByMonth(2) %></td>
                        <td><%=GetProfitByMonth(3) %></td>
                        <td><%=GetProfitByMonth(4) %></td>
                        <td><%=GetProfitByMonth(5) %></td>
                        <td><%=GetProfitByMonth(6) %></td>
                        <td><%=GetProfitByMonth(7) %></td>
                        <td><%=GetProfitByMonth(8) %></td>
                        <td><%=GetProfitByMonth(9) %></td>

                        <td><%=GetProfitByMonth(10) %></td>
                        <td><%=GetProfitByMonth(11) %></td>
                        <td><%=GetProfitByMonth(12) %></td>
                        <td><%=GetProfitByMonth(13) %></td>

                    </tr>
                    <tr>
                        <td>毛利率</td>
                        <td><%=GetProfitRateByMonth(1) %></td>
                        <td><%=GetProfitRateByMonth(2) %></td>
                        <td><%=GetProfitRateByMonth(3) %></td>
                        <td><%=GetProfitRateByMonth(4) %></td>
                        <td><%=GetProfitRateByMonth(5) %></td>
                        <td><%=GetProfitRateByMonth(6) %></td>
                        <td><%=GetProfitRateByMonth(7) %></td>
                        <td><%=GetProfitRateByMonth(8) %></td>
                        <td><%=GetProfitRateByMonth(9) %></td>

                        <td><%=GetProfitRateByMonth(10) %></td>
                        <td><%=GetProfitRateByMonth(11) %></td>
                        <td><%=GetProfitRateByMonth(12) %></td>
                        <td><%=GetProfitRateByMonth(13) %></td>

                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>

                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>

                    </tr>

                </table>
            </div>
            <br />
            <div id="container" style="height: 272px;">
            </div>

            <br />
            <div id="containercount" style="height: 272px;">
            </div>
        </div>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
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
                            text: '单位 (元)'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
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
                    exporting: {
                        enabled: true //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为
                    },
                    series: [
                        {
                            name: '执行订单金额',
                            data: [<%=ViewState["sbFinishAmount"]%>]
                        },
                        {
                            name: '现金流',
                            data: [<%=ViewState["sbRealityAmount"]%>]
                        }
                    ]
                });
            });
        </script>

        <script type="text/javascript">

            $(document).ready(function () {
                $('#containercount').highcharts({
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
                        enabled: true //用来设置是否显示‘打印’,'导出'等功能按钮，不设置时默认为
                    },
                    series: [
                        {
                            name: '执行订单总数',
                            data: [<%=ViewState["sbOrderCount"]%>]
                        }, {
                            name: '新订单',
                            data: [<%=ViewState["sbNewOrderCount"]%>]
                        }
                    ]
                });
            });
        </script>
        <script src="../../../Scripts/highcharts.js"></script>
        <script src="../../../Scripts/exporting.js"></script>
    </div>
</asp:Content>
