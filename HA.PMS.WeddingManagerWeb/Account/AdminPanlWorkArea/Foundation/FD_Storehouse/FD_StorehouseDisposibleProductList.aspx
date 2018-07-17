<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseDisposibleProductList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseDisposibleProductList" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".grouped_elements").each(function (indexs, values) {
                if ($.trim($(this).html()) == "") {
                    $(this).remove();
                }
            });
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));

            });
            $("a.grouped_elements").fancybox();
            $(".remainder").each(function (indexs, values) {
                var currentText = parseInt($.trim($(this).text()));
                if (currentText <= 3) {
                    $(this).parent("tr").css({ "color": "red" });
                }
            });

            $('#purchaseprice').highcharts({
                chart: {
                    type: 'line',
                    marginRight: 130,
                    marginBottom: 25
                },
                title: {
                    text: '成本总价指标',
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
                    enabled: true //用来设置是否显示‘打印’ '导出'等功能按钮，不设置时默认为
                },
                series: [{
                    name: '成本总价',
                    data: [<%=ViewState["KL_PurchasePrice"]%>]
                }]
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="width:98%">
        <div style="display:none" class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>一次性商品 -> 产品明细</h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table class="queryTable">
                   <tr>
                       <td>类别：
                           <cc2:categorydropdownlist ID="ddlCategory" ParentID="0" AutoPostBack="true" OnInit="ddlCategory_Init" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></cc2:categorydropdownlist>
                           <cc2:categorydropdownlist ID="ddlProject" runat="server"></cc2:categorydropdownlist>
                       </td>
                       <td>产品名称：<asp:TextBox ID="txtProductName" runat="server" MaxLength="20"></asp:TextBox></td>
                       <td><HA:DateRanger runat="server" ID="CreateDateRanger" Title="入库时间：" /></td>
                       <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查找" /></td>
                   </tr>
                </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>产品名称</th>
                        <th>入库时间</th>
                        <th>产品类别</th>
                        <th>项目</th>
                        <th>产品\服务描述</th>
                        <th>资料</th>
                        <th>采购单价</th>
                        <th>销售单价</th>
                        <th>数量</th>
                        <th>单位</th>
                        <th>剩余数量</th>
                        <th>最近使用时间</th>
                        <th>使用总次数</th>
                        <th>仓位</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptMain" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("SourceProductName") %></td>
                                <td><%#ShowShortDate(Eval("PutStoreDate")) %></td>
                                <td><%#GetCategoryName(Eval("ProductCategory")) %></td>
                                <td><%#GetCategoryName(Eval("ProductProject")) %></td>
                                <td><%#Eval("Specifications") %></td>
                                <td><a class="grouped_elements" href="#" rel="group1"><asp:Image ID="imgStore" ImageUrl='<%#Eval("Data") %>' Width="100" Height="50" runat="server" /></a></td>
                                <td><%#Eval("PurchasePrice") %></td>
                                <td><%#Eval("SaleOrice") %></td>
                                <td><%#GetStoreCount(Eval("SourceProductId")) %></td>
                                <td><%#Eval("Unit") %></td>
                                <td class="remainder"><%#GetLeaveCount(Eval("SourceProductId")) %></td>
                                <td><%#GetLastUsedDate(Eval("SourceProductId")) %></td>
                                <td><%#GetUsedTimes(Eval("SourceProductId")) %></td>
                                <td><%#Eval("Position")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="14"><cc1:AspNetPagerTool ID="MainPager" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool></td>
                    </tr>
                </tfoot>
            </table>
            请选择年份:
                 <asp:DropDownList ID="ddlChooseYear" AutoPostBack="true" OnSelectedIndexChanged="BinderData" runat="server">
                     <asp:ListItem Text="2013年" Value="2013"></asp:ListItem>
                     <asp:ListItem Text="2014年" Value="2014"></asp:ListItem>
                     <asp:ListItem Text="2015年" Value="2015"></asp:ListItem>
                     <asp:ListItem Text="2016年" Value="2016"></asp:ListItem>
                     <asp:ListItem Text="2017年" Value="2017"></asp:ListItem>
                     <asp:ListItem Text="2018年" Value="2018"></asp:ListItem>
                     <asp:ListItem Text="2019年" Value="2019"></asp:ListItem>
                     <asp:ListItem Text="2020年" Value="2020"></asp:ListItem>
                     <asp:ListItem Text="2021年" Value="2021"></asp:ListItem>
                     <asp:ListItem Text="2022年" Value="2022"></asp:ListItem>
                     <asp:ListItem Text="2023年" Value="2023"></asp:ListItem>
                     <asp:ListItem Text="2024年" Value="2024"></asp:ListItem>
                 </asp:DropDownList>
            <!--数量指标 -->
            <div id="purchaseprice" style="height: 272px;"></div>
            <script src="/Scripts/highcharts.js"></script>
            <script src="/Scripts/exporting.js"></script>
        </div>
    </div>
</asp:Content>
