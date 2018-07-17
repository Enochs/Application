<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InviteListCount.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger.InviteListCount" %>
<script type="text/javascript">
    $(document).ready(function () {

        // $(".DateTimeTxt").datepicker({ dateFormat: 'yy-mm-dd ' });

        $('#InviteContainer').highcharts({
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
                name: '邀约中量',
                data: [<%=ViewState["inviteCounts"]%>]
            }, {
                name: '邀约成功量',
                data: [<%=ViewState["inviteSuccessCounts"]%>]
             }, {
                 name: '流失量',
                 data: [<%=ViewState["loseCounts"]%>]
             }, {
                 name: '未邀约',
                 data: [<%=ViewState["dontCounts"]%>]
             }]
        });


        //质量指标
        $('#InviteQuality').highcharts({
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
                name: '邀约中率',
                data: [<%=ViewState["inviteRate"]%>]
            }, {
                name: '邀约成功率',
                data: [<%=ViewState["inviteSuccessRate"]%>]
             }, {
                 name: '流失率',
                 data: [<%=ViewState["loseRate"]%>]
             }, {
                 name: '未邀约率',
                 data: [<%=ViewState["dontRate"]%>]
             }]
        });


        //财务指标
        $('#InviteMoneyChart').highcharts({
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
            }]
        });
    });
</script>
<br />
请选择年份: 
    <asp:DropDownList ID="ddlChooseYear" AutoPostBack="true" runat="server">
        <asp:ListItem Text="2000" Value="2000-01-01,2000-12-31"></asp:ListItem>
        <asp:ListItem Text="2001" Value="2001-01-01,2001-12-31"></asp:ListItem>
        <asp:ListItem Text="2002" Value="2002-01-01,2002-12-31"></asp:ListItem>
        <asp:ListItem Text="2003" Value="2003-01-01,2003-12-31"></asp:ListItem>
        <asp:ListItem Text="2004" Value="2004-01-01,2004-12-31"></asp:ListItem>
        <asp:ListItem Text="2005" Value="2005-01-01,2005-12-31"></asp:ListItem>
        <asp:ListItem Text="2006" Value="2006-01-01,2006-12-31"></asp:ListItem>
        <asp:ListItem Text="2007" Value="2007-01-01,2007-12-31"></asp:ListItem>
        <asp:ListItem Text="2008" Value="2008-01-01,2008-12-31"></asp:ListItem>
        <asp:ListItem Text="2009" Value="2009-01-01,2009-12-31"></asp:ListItem>
        <asp:ListItem Text="2010" Value="2010-01-01,2010-12-31"></asp:ListItem>
        <asp:ListItem Text="2011" Value="2011-01-01,2011-12-31"></asp:ListItem>
        <asp:ListItem Text="2012" Value="2012-01-01,2012-12-31"></asp:ListItem>
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
<!--数量指标 -->
<div id="InviteContainer" style="height: 272px;">
</div>

<!--质量指标-->
<div id="InviteQuality" style="height: 272px;">
</div>
<!--财务指标-->
<div id="InviteMoneyChart" style="height: 272px;">
</div>
