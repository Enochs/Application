<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderProjectfileList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.OrderProjectfileList"  MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>


<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }
    </style>


    <script>

        $(document).ready(function () {

            $(".DateTimeTxt").datepicker({ dateFormat: 'yy-mm-dd ' });

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
                    data: [<%=ViewState["countCustomers"]%>]
                }, {
                    name: '实际到店量',
                    data: [<%=ViewState["validCounts"]%>]
                }, {
                    name: '邀约成功量',
                    data: [<%=ViewState["inviteSuccessCounts"]%>]
                }, {
                    name: '流失量',
                    data: [<%=ViewState["loseCounts"]%>]
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
                    data: [<%=ViewState["ValidRate"]%>]
                }, {
                    name: '预订率',
                    data: [<%=ViewState["inviteSuccessRate"]%>]
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
                    data: [<%=ViewState["OrderSumMoney"]%>]
                }, {
                    name: '定金总额',
                    data: [<%=ViewState["EarnestMoney"]%>]
                }]
            });


            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {
                if (indexs != 3) {
                    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            //
            $("html").css({ "overflow-x": "hidden" });



        });
    </script>
    <%--   //预访新人
        //派单--%>


    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-box" style="height: 30px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>
                            <table>
                                <tr>

                                    <td>新人状态:<cc2:ddlCustomersState ID="DdlCustomersState1" runat="server"></cc2:ddlCustomersState>
                                    </td>
                                    <td>时间类别 
                                        <asp:DropDownList runat="server" ID="ddlDateType">
                                            <asp:ListItem Text="请选择" Value="0" />
                                            <asp:ListItem Text="到店时间" Value="PlanComeDate" />
                                            <asp:ListItem Text="预订时间" Value="LastFollowDate" />
                                            <asp:ListItem Text="婚期时间" Value="PartyDate" />
                                        </asp:DropDownList>
                                    </td>
                                    <td><cc2:DateEditTextBox onclick="WdatePicker();" ID="DateEditTextBox1" runat="server"></cc2:DateEditTextBox>
                                    </td>
                                    <td>到
                                          <cc2:DateEditTextBox onclick="WdatePicker();" ID="DateEditTextBox2" runat="server"></cc2:DateEditTextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnserch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnserch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>

            <table class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th style="white-space: nowrap;">新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>新人状态</th>
                        <th>婚礼顾问</th>
                        <th>跟单次数</th>
                        <th>发生时间</th>
                        <th>定金</th>
                        <th>策划师</th>
                        <th>订单总额</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="height: 16px;"><a target="_blank" href="FollowOrderDetails.aspx?Sucess=1&CustomerID=<%#Eval("CustomerID") %>">
                                    <%#Eval("Bride") %>
                                </a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#GetShortDateString(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                <td><%#Eval("FlowCount") %></td>
                                <td><%#GetShortDateString(Eval("LastFollowDate")) %></td>
                                <td><%#Eval("EarnestMoney")%></td>
                                <td><%#GetQuotedEmpLoyee(Eval("OrderID"))%></td>
                                <td>
                                    <%#GetQuotedAggregateAmount(Eval("OrderID")) %>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkbtnChecks" CommandName="Checks" runat="server">审核通过</asp:LinkButton>
                                    <a href="OrderfilDownLoad.aspx?OrderID=<%#Eval("OrderID") %>">查看/下载文件</a> 
                                     
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="11">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"   OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>

                    </tr>
                </tfoot>
            </table>


            <uc1:MessageBoard ClassType="FollowUpOrder" runat="server" ID="MessageBoard" />

        </div>
    </div>
</asp:Content>
