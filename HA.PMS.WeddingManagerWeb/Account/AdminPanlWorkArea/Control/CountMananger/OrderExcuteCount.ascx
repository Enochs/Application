<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderExcuteCount.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger.OrderExcuteCount" %>
 <script type="text/javascript">

     $(document).ready(function () {
         $('#OrderExcuteCount').highcharts({
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
                 name: '订单总数',
                 data: [<%=ViewState["sbOrderCount"]%>]
                }, {
                    name: '执行评价',
                    data: [<%=ViewState["sbExcuteAssess"]%>]
                }, {
                    name: '投诉次数',
                    data: [<%=ViewState["sbComplainCount"]%>]
                }, {
                    name: '满意度',
                    data: [<%=ViewState["sbDegreeCount"]%>]
                }]
            });
        });
    </script>
  请选择年份: 
            <asp:DropDownList ID="ddlChooseYear" AutoPostBack="true" runat="server">

     
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

        <br />
        <div id="OrderExcuteCount" style="height: 272px;">
        </div>