<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DispatchingOrderCount.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger.DispatchingOrderCount" %>

<script type="text/javascript">
    $(document).ready(function() {
        $('#DispatchingOrderCount').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: '执行订单总额'
            },
            subtitle: {
                //text: 'Source: 好爱科技'
            },
            xAxis: {
                categories: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月']
            },
            yAxis: {
                min: 0,
                title: {
                    text: '单位 (元)'
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f} 元</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: [{
                name: '执行订单总额',
                data: [<%=ViewState["allAggregateAmount"]%>]

                }]

         });
    });

</script>
   <div id="DispatchingOrderCount" style="height: 272px;">
    </div>
