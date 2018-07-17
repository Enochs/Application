<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewOrderMoneyCount.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger.NewOrderMoneyCount" %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#NewOrderMoneyCount').highcharts({
            chart: {
                type: 'column'
            },
            title: {
                text: '新签订单总额指标'
            },
            subtitle: {
                //text: 'Source: 好爱科技'
            },
            xAxis: {
                categories: ['一月', '二月', '三月', '四月', '五月', '六月',

'七月', '八月', '九月', '十月', '十一月', '十二月']
            },
            yAxis: {
                min: 0,
                title: {
                    text: '单位 (元)'
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',


                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' + '<td style="padding:0"><b>{point.y:.1f} 元</b></td></tr>',

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
                name: '新签订单总额',
                data: [<%=ViewState["AggregateAmountSumMoney"]%>]

            }]

        });
    });

</script>
   <div id="NewOrderMoneyCount" style="height: 272px;">
     
    </div>
  <asp:Button ID="btnSerch" runat="server" Text="查看报表" OnClick="btnSerch_Click" CssClass="btn" />
<asp:Button ID="btnSum" runat="server" Text="月度销售排行榜" OnClick="btnSum_Click" CssClass="btn" />
<asp:Button ID="btnHotel" runat="server" Text="酒店渠道销售排行榜" OnClick="btnHotel_Click" CssClass="btn" />
