<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TelemarketingCustomersNumberIndexCount.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger.TelemarketingCustomersNumberIndexCount" %>
<script type="text/javascript">

    $(document).ready(function () {
        $('#TelemarketingCustomersNumber').highcharts({
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
            series: [{
                name: '客源量',
                data: [<%=ViewState["countCustomers"]%>]
            }, {
                name: '有效量',
                data: [<%=ViewState["validCounts"]%>]
                }, {
                    name: '邀约成功量',
                    data: [<%=ViewState["inviteSuccessCounts"]%>]
                }, {
                    name: '成交量',
                    data: [<%=ViewState["ClinchaDealCounts"]%>]
                }]
        });
        //质量指标
        $('#TeleQuality').highcharts({
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
                name: '有效率',
                data: [<%=ViewState["ValidRate"]%>]
            }, {
                name: '邀约成功率',
                data: [<%=ViewState["inviteSuccessRate"]%>]
                }, {
                    name: '成交率',
                    data: [<%=ViewState["ClinchaDealRate"]%>]
                }]
        });

        //财务指标
        $('#TeleMoneyChart').highcharts({
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
                data: [<%=ViewState["OrderSumMoney"]%>]
            }, {
                name: '返利总额',
                data: [<%=ViewState["PayMoneyMoney"]%>]
                }]
        });

    });
</script>

      请选择年份: 
            <asp:DropDownList ID="ddlChooseYear" AutoPostBack="true" OnSelectedIndexChanged="ddlChooseYear_SelectedIndexChanged" runat="server">

                <%--<asp:ListItem Text="2010年" Value="2010-01-01,2010-12-31"></asp:ListItem>
                <asp:ListItem Text="2011年" Value="2011-01-01,2011-12-31"></asp:ListItem>
                <asp:ListItem Text="2012年" Value="2012-01-01,2012-12-31"></asp:ListItem>--%>
                <asp:ListItem Text="2013年" Value="2013-01-01,2013-12-31"></asp:ListItem>
                <asp:ListItem Text="2014年" Value="2014-01-01,2014-12-31"></asp:ListItem>
                <asp:ListItem Text="2015年" Value="2015-01-01,2015-12-31"></asp:ListItem>
                <asp:ListItem Text="2016年" Value="2016-01-01,2016-12-31"></asp:ListItem>
                <asp:ListItem Text="2017年" Value="2017-01-01,2017-12-31"></asp:ListItem>
                <asp:ListItem Text="2018年" Value="2018-01-01,2018-12-31"></asp:ListItem>
                <asp:ListItem Text="2019年" Value="2019-01-01,2019-12-31"></asp:ListItem>
                <asp:ListItem Text="2020年" Value="2020-01-01,2020-12-31"></asp:ListItem>
                <asp:ListItem Text="2021年" Value="2021-01-01,2021-12-31"></asp:ListItem>
                <asp:ListItem Text="2022年" Value="2022-01-01,2022-12-31"></asp:ListItem>
                <asp:ListItem Text="2023年" Value="2023-01-01,2023-12-31"></asp:ListItem>
                <asp:ListItem Text="2024年" Value="2024-01-01,2024-12-31"></asp:ListItem>
            </asp:DropDownList>
<div id="TelemarketingCustomersNumber" style="height: 272px; width: 90%;">
</div>


<!--质量指标-->
<div id="TeleQuality" style="height: 272px; width: 90%;">
</div>
<!--财务指标-->
<div id="TeleMoneyChart" style="height: 272px; width: 90%;">
</div>
