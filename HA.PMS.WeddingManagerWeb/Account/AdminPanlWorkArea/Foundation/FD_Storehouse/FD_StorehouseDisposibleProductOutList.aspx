<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseDisposibleProductOutList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseDisposibleProductOutList" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#unitPrice').highcharts({
                chart: {
                    type: 'line',
                    marginRight: 130,
                    marginBottom: 25
                },
                title: {
                    text: '销售总价指标',
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
                    name: '销售总价',
                    data: [<%=ViewState["KL_UnitPrice"]%>]
                }]
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="width:98%">
        <div style="display:none" class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>一次性商品 -> 出库记录</h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table>
                <tr>
                    <td>产品名称：<asp:TextBox runat="server" ID="txtProductName" /></td>
                    <td><HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" /></td>
                    <td><asp:LinkButton Text="查询" CssClass="btn btn-primary" ID="BtnQuery" OnClick="BinderData" runat="server" /></td>
                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>产品名称</th>
                        <th>入库时间</th>
                        <th>婚期</th>
                        <th>产品\服务描述</th>
                        <th>成本价</th>
                        <th>单位</th>
                        <th>销售价</th>
                        <th>出库数量</th>
                        <th>小计</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RptMain" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ProductName") %></td>
                                <td><%#ShowShortDate(GetPutStoreDate(Eval("KindID"))) %></td>
                                <td><%#ShowPartyDate(GetPartyDate(Eval("Expr6"))) %></td>
                                <td><%#Eval("Specifications") %></td>
                                <td><%#Eval("PurchasePrice") %></td>
                                <td><%#Eval("Unit") %></td>
                                <td><%#Eval("UnitPrice") %></td>
                                <td><%#Eval("Quantity") %></td>
                                <td><%#Eval("Subtotal") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9"><cc1:AspNetPagerTool ID="PagerMain" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool></td>
                    </tr>
                </tfoot>
            </table>
            <div>请选择年份:
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
                
                <!--指标-->
                <div id="unitPrice" style="height: 272px;"></div>
                <script src="/Scripts/highcharts.js"></script>
                <script src="/Scripts/exporting.js"></script>
            </div>
        </div>
    </div>
</asp:Content>
