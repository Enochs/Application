<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CanpanyReport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CanpanyReport" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <span style="color: white; background-color: gray; width: 100%;">经营指标动态</span>年份
    <asp:HiddenField ID="hideKey" runat="server" />
    <cc1:ddlRangeYear ID="DdlRangeYear1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlRangeYear1_SelectedIndexChanged"></cc1:ddlRangeYear>
    部门<cc1:DepartmentDropdownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
    </cc1:DepartmentDropdownList>
    员工<cc1:ddlEmployee ID="ddlEmployee" runat="server">
    </cc1:ddlEmployee>
    <asp:Button ID="btnSerch" runat="server" Text="查询" OnClick="btnSerch_Click" CssClass="btn" />
    <asp:Button ID="btnExport" runat="server" Text="导出报表" OnClick="btnExport_Click" CssClass="btn" />
    <table class="table table-bordered table-striped">
        <thead>
            <tr id="trContent">
                <th>目标</th>
                <th></th>
                <th>1</th>
                <th>2</th>
                <th>3</th>
                <th>4</th>
                <th>5</th>
                <th>6</th>
                <th>7</th>
                <th>8</th>
                <th>9</th>
                <th>10</th>
                <th>11</th>
                <th>12</th>
                <th>当年合计</th>
                <th>上年合计</th>
                <th>历史累计</th>
            </tr>
            <asp:Repeater ID="repReport" runat="server">



                <ItemTemplate>
                    <tr id="trContent">

                        <td>销售额</td>
                        <td>计划</td>
                        <td><%#Eval("MonthPlan1") %></td>
                        <td><%#Eval("MonthPlan2") %></td>
                        <td><%#Eval("MonthPlan3") %></td>
                        <td><%#Eval("MonthPlan4") %></td>
                        <td><%#Eval("MonthPlan5") %></td>
                        <td><%#Eval("MonthPlan6") %></td>
                        <td><%#Eval("MonthPlan7") %></td>
                        <td><%#Eval("MonthPlan8") %></td>
                        <td><%#Eval("MonthPlan9") %></td>
                        <td><%#Eval("MonthPlan10") %></td>
                        <td><%#Eval("MonthPlan11") %></td>
                        <td><%#Eval("MonthPlan12") %></td>
                        <td><%#decimal.Parse(Eval("MonthPlan1").ToString())+decimal.Parse(Eval("MonthPlan2").ToString())+decimal.Parse(Eval("MonthPlan3").ToString())+decimal.Parse(Eval("MonthPlan4").ToString())+decimal.Parse(Eval("MonthPlan5").ToString())+decimal.Parse(Eval("MonthPlan6").ToString())+decimal.Parse(Eval("MonthPlan7").ToString())+decimal.Parse(Eval("MonthPlan8").ToString())+decimal.Parse(Eval("MonthPlan9").ToString())+decimal.Parse(Eval("MonthPlan10").ToString())+decimal.Parse(Eval("MonthPlan11").ToString())+decimal.Parse(Eval("MonthPlan12").ToString()) %></td>
                        <td><%#GetSumByYear((DateTime.Now.Year-1),1) %></td>
                        <td><%#GetSumByYear((DateTime.Now.Year-1),2) %></td>
                    </tr>
                    <tr id="tr1">
                        <td style="border-bottom-style: none; border-top-style: none"></td>
                        <td>实际完成</td>
                        <td><%#Eval("MonthFinsh1") %></td>
                        <td><%#Eval("MonthFinish2") %></td>
                        <td><%#Eval("MonthFinish3") %></td>
                        <td><%#Eval("MonthFinish4") %></td>
                        <td><%#Eval("MonthFinish5") %></td>
                        <td><%#Eval("MonthFinish6") %></td>
                        <td><%#Eval("MonthFinish7") %></td>
                        <td><%#Eval("MonthFinish8") %></td>
                        <td><%#Eval("MonthFinish9") %></td>
                        <td><%#Eval("MonthFinish10") %></td>
                        <td><%#Eval("MonthFinish11") %></td>
                        <td><%#Eval("MonthFinish12") %></td>
                        <td><%#decimal.Parse(Eval("MonthFinsh1").ToString())+decimal.Parse(Eval("MonthFinish2").ToString())+decimal.Parse(Eval("MonthFinish3").ToString())+decimal.Parse(Eval("MonthFinish4").ToString())+decimal.Parse(Eval("MonthFinish5").ToString())+decimal.Parse(Eval("MonthFinish6").ToString())+decimal.Parse(Eval("MonthFinish7").ToString())+decimal.Parse(Eval("MonthFinish8").ToString())+decimal.Parse(Eval("MonthFinish9").ToString())+decimal.Parse(Eval("MonthFinish10").ToString())+decimal.Parse(Eval("MonthFinish11").ToString())+decimal.Parse(Eval("MonthFinish12").ToString()) %></td>
                        <td><%#GetSumByYear((DateTime.Now.Year-1),3) %></td>
                        <td><%#GetSumByYear((DateTime.Now.Year-1),4) %></td>


                    </tr>
                    <tr id="tr16">

                        <td style="border-bottom-style: none; border-top-style: none"></td>
                        <td>服务对数</td>
                        <td><%=GetQuotedCountByYearmonth(1) %></td>
                        <td><%=GetQuotedCountByYearmonth(2) %></td>
                        <td><%=GetQuotedCountByYearmonth(3) %></td>
                        <td><%=GetQuotedCountByYearmonth(4) %></td>
                        <td><%=GetQuotedCountByYearmonth(5) %></td>
                        <td><%=GetQuotedCountByYearmonth(6) %></td>
                        <td><%=GetQuotedCountByYearmonth(7) %></td>
                        <td><%=GetQuotedCountByYearmonth(8) %></td>
                        <td><%=GetQuotedCountByYearmonth(9) %></td>
                        <td><%=GetQuotedCountByYearmonth(10) %></td>
                        <td><%=GetQuotedCountByYearmonth(11) %></td>
                        <td><%=GetQuotedCountByYearmonth(12) %></td>
                        <td><%=GetQuotedCountByYearmonth(13) %></td>
                        <td><%=GetQuotedCountByYearmonth(14) %></td>
                        <td><%=GetQuotedCountByYearmonth(15) %></td>
                    </tr>


                </ItemTemplate>

            </asp:Repeater>
            <tr id="tr17">
                <td rowspan="2">定金</td>
                <td>已收定金</td>
                <td><%=GetEarnestMoneyByOrderYearMonth(1) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(2) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(3) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(4) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(5) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(6) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(7) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(8) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(9) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(10) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(11) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(12) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(13) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(14) %></td>
                <td><%=GetEarnestMoneyByOrderYearMonth(15) %></td>
            </tr>
            <tr id="tr18">
                <td>服务对数</td>
                <td><%=GetOrderCountByYearmonth(1) %></td>
                <td><%=GetOrderCountByYearmonth(2) %></td>
                <td><%=GetOrderCountByYearmonth(3) %></td>
                <td><%=GetOrderCountByYearmonth(4) %></td>
                <td><%=GetOrderCountByYearmonth(5) %></td>
                <td><%=GetOrderCountByYearmonth(6) %></td>
                <td><%=GetOrderCountByYearmonth(7) %></td>
                <td><%=GetOrderCountByYearmonth(8) %></td>
                <td><%=GetOrderCountByYearmonth(9) %></td>
                <td><%=GetOrderCountByYearmonth(10) %></td>
                <td><%=GetOrderCountByYearmonth(11) %></td>
                <td><%=GetOrderCountByYearmonth(12) %></td>
                <td><%=GetOrderCountByYearmonth(13) %></td>
                <td><%=GetOrderCountByYearmonth(14) %></td>
                <td><%=GetOrderCountByYearmonth(15) %></td>
            </tr>
            <tr id="tr3">
                <td rowspan="3">完工额</td>
                <td>金额</td>
                <td><%=GetOrderSumByDateTime(1) %></td>
                <td><%=GetOrderSumByDateTime(2) %></td>
                <td><%=GetOrderSumByDateTime(3) %></td>
                <td><%=GetOrderSumByDateTime(4) %></td>
                <td><%=GetOrderSumByDateTime(5) %></td>
                <td><%=GetOrderSumByDateTime(6) %></td>
                <td><%=GetOrderSumByDateTime(7) %></td>
                <td><%=GetOrderSumByDateTime(8) %></td>
                <td><%=GetOrderSumByDateTime(9) %></td>
                <td><%=GetOrderSumByDateTime(10) %></td>
                <td><%=GetOrderSumByDateTime(11) %></td>
                <td><%=GetOrderSumByDateTime(12) %></td>
                <td><%=GetOrderSumByDateTime(13) %></td>
                <td><%=GetOrderSumByDateTime(14) %></td>
                <td><%=GetOrderSumByDateTime(15) %></td>
            </tr>
            <tr id="tr3">
                <td>服务对数</td>
                <td><%=GetOrderFinishCountByDateTime(1) %></td>
                <td><%=GetOrderFinishCountByDateTime(2) %></td>
                <td><%=GetOrderFinishCountByDateTime(3) %></td>
                <td><%=GetOrderFinishCountByDateTime(4) %></td>
                <td><%=GetOrderFinishCountByDateTime(5) %></td>
                <td><%=GetOrderFinishCountByDateTime(6) %></td>
                <td><%=GetOrderFinishCountByDateTime(7) %></td>
                <td><%=GetOrderFinishCountByDateTime(8) %></td>
                <td><%=GetOrderFinishCountByDateTime(9) %></td>
                <td><%=GetOrderFinishCountByDateTime(10) %></td>
                <td><%=GetOrderFinishCountByDateTime(11) %></td>
                <td><%=GetOrderFinishCountByDateTime(12) %></td>
                <td><%=GetOrderFinishCountByDateTime(13) %></td>
                <td><%=GetOrderFinishCountByDateTime(14) %></td>
                <td><%=GetOrderFinishCountByDateTime(15) %></td>
            </tr>
            <tr id="tr3">
                <td>平均消费金额</td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(1) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(2) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(3) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(4) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(5) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(6) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(7) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(8) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(9) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(10) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(11) %></td>
                <td><%=GetOrderFinishAvgMoneyByDateTime(12) %></td>
                <td>0</td>
                <td>0</td>
                <td>0</td>
            </tr>

            <tr id="tr4">
                <td>当期流水</td>
                <td></td>
                <td><%=GetLiuShui(1) %></td>
                <td><%=GetLiuShui(2) %></td>
                <td><%=GetLiuShui(3) %></td>
                <td><%=GetLiuShui(4) %></td>
                <td><%=GetLiuShui(5) %></td>
                <td><%=GetLiuShui(6) %></td>
                <td><%=GetLiuShui(7) %></td>
                <td><%=GetLiuShui(8) %></td>
                <td><%=GetLiuShui(9) %></td>
                <td><%=GetLiuShui(10) %></td>
                <td><%=GetLiuShui(11) %></td>
                <td><%=GetLiuShui(12) %></td>
                <td><%=GetLiuShui(13) %></td>
                <td><%=GetLiuShui(14) %></td>
                <td><%=GetLiuShui(15) %></td>
            </tr>
            <tr id="tr12">
                <td>客源量</td>
                <td></td>
                <td><%=GetKeYuanliang(1) %></td>
                <td><%=GetKeYuanliang(2) %></td>
                <td><%=GetKeYuanliang(3) %></td>
                <td><%=GetKeYuanliang(4) %></td>
                <td><%=GetKeYuanliang(5) %></td>
                <td><%=GetKeYuanliang(6) %></td>
                <td><%=GetKeYuanliang(7) %></td>
                <td><%=GetKeYuanliang(8) %></td>
                <td><%=GetKeYuanliang(9) %></td>
                <td><%=GetKeYuanliang(10) %></td>
                <td><%=GetKeYuanliang(11) %></td>
                <td><%=GetKeYuanliang(12) %></td>
                <td><%=GetKeYuanliang(13) %></td>
                <td><%=GetKeYuanliang(14) %></td>
                <td><%=GetKeYuanliang(15) %></td>
            </tr>

            <tr id="tr13">
                <td>有效量</td>
                <td></td>
                <td><%=GetSucessTel(1) %></td>
                <td><%=GetSucessTel(2) %></td>
                <td><%=GetSucessTel(3) %></td>
                <td><%=GetSucessTel(4) %></td>
                <td><%=GetSucessTel(5) %></td>
                <td><%=GetSucessTel(6) %></td>
                <td><%=GetSucessTel(7) %></td>
                <td><%=GetSucessTel(8) %></td>
                <td><%=GetSucessTel(9) %></td>
                <td><%=GetSucessTel(10) %></td>
                <td><%=GetSucessTel(11) %></td>
                <td><%=GetSucessTel(12) %></td>
                <td><%=GetSucessTel(13) %></td>
                <td><%=GetSucessTel(14) %></td>
                <td><%=GetSucessTel(15) %></td>
            </tr>



            <tr id="tr9">
                <td>成交量</td>
                <td></td>
                <td><%=GetSucessOrderCount(1) %></td>
                <td><%=GetSucessOrderCount(2) %></td>
                <td><%=GetSucessOrderCount(3) %></td>
                <td><%=GetSucessOrderCount(4) %></td>
                <td><%=GetSucessOrderCount(5) %></td>
                <td><%=GetSucessOrderCount(6) %></td>
                <td><%=GetSucessOrderCount(7) %></td>
                <td><%=GetSucessOrderCount(8) %></td>
                <td><%=GetSucessOrderCount(9) %></td>
                <td><%=GetSucessOrderCount(10) %></td>
                <td><%=GetSucessOrderCount(11) %></td>
                <td><%=GetSucessOrderCount(12) %></td>
                <td><%=GetSucessOrderCount(13) %></td>
                <td><%=GetSucessOrderCount(14) %></td>
                <td><%=GetSucessOrderCount(15) %></td>
            </tr>


            <tr id="tr11">
                <td>成交率</td>
                <td></td>
                <td><%=GetSucessCustomer(1) %></td>
                <td><%=GetSucessCustomer(2) %></td>
                <td><%=GetSucessCustomer(3) %></td>
                <td><%=GetSucessCustomer(4) %></td>
                <td><%=GetSucessCustomer(5) %></td>
                <td><%=GetSucessCustomer(6) %></td>
                <td><%=GetSucessCustomer(7) %></td>
                <td><%=GetSucessCustomer(8) %></td>
                <td><%=GetSucessCustomer(9) %></td>
                <td><%=GetSucessCustomer(10) %></td>
                <td><%=GetSucessCustomer(11) %></td>
                <td><%=GetSucessCustomer(12) %></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>



            <tr id="tr3">
                <td>当期新签订订单金额</td>
                <td></td>
                <td><%=GetNewOrder(1) %></td>
                <td><%=GetNewOrder(2) %></td>
                <td><%=GetNewOrder(3) %></td>
                <td><%=GetNewOrder(4) %></td>
                <td><%=GetNewOrder(5) %></td>
                <td><%=GetNewOrder(6) %></td>
                <td><%=GetNewOrder(7) %></td>
                <td><%=GetNewOrder(8) %></td>
                <td><%=GetNewOrder(9) %></td>
                <td><%=GetNewOrder(10) %></td>
                <td><%=GetNewOrder(11) %></td>
                <td><%=GetNewOrder(12) %></td>
                <td><%=GetNewOrder(13) %></td>
                <td><%=GetNewOrder(14) %></td>
                <td><%=GetNewOrder(15) %></td>
            </tr>
            <tr id="tr5">
                <td>当期应收款</td>
                <td></td>
                <td><%=GetYinShou(1) %></td>
                <td><%=GetYinShou(2) %></td>
                <td><%=GetYinShou(3) %></td>
                <td><%=GetYinShou(4) %></td>
                <td><%=GetYinShou(5) %></td>
                <td><%=GetYinShou(6) %></td>
                <td><%=GetYinShou(7) %></td>
                <td><%=GetYinShou(8) %></td>
                <td><%=GetYinShou(9) %></td>
                <td><%=GetYinShou(10) %></td>
                <td><%=GetYinShou(11) %></td>
                <td><%=GetYinShou(12) %></td>
                <td><%=GetYinShou(13) %></td>
                <td><%=GetYinShou(14) %></td>
                <td><%=GetYinShou(15) %></td>
            </tr>

            <tr id="tr6">
                <td>当期渠道费用</td>
                <td></td>
                <td><%=GetPayNeedByMonth(1) %></td>
                <td><%=GetPayNeedByMonth(2) %></td>
                <td><%=GetPayNeedByMonth(3) %></td>
                <td><%=GetPayNeedByMonth(4) %></td>
                <td><%=GetPayNeedByMonth(5) %></td>
                <td><%=GetPayNeedByMonth(6) %></td>
                <td><%=GetPayNeedByMonth(7) %></td>
                <td><%=GetPayNeedByMonth(8) %></td>
                <td><%=GetPayNeedByMonth(9) %></td>
                <td><%=GetPayNeedByMonth(10) %></td>
                <td><%=GetPayNeedByMonth(11) %></td>
                <td><%=GetPayNeedByMonth(12) %></td>
                <td><%=GetPayNeedByMonth(12) %></td>
                <td><%=GetPayNeedByMonth(12) %></td>
                <td><%=GetPayNeedByMonth(12) %></td>
            </tr>

            <tr id="tr7">
                <td>当期毛利率</td>
                <td></td>
                <td><%=GetCostByMOnth(1) %></td>
                <td><%=GetCostByMOnth(2) %></td>
                <td><%=GetCostByMOnth(3) %></td>
                <td><%=GetCostByMOnth(4) %></td>
                <td><%=GetCostByMOnth(5) %></td>
                <td><%=GetCostByMOnth(6) %></td>
                <td><%=GetCostByMOnth(7) %></td>
                <td><%=GetCostByMOnth(8) %></td>
                <td><%=GetCostByMOnth(9) %></td>
                <td><%=GetCostByMOnth(10) %></td>
                <td><%=GetCostByMOnth(11) %></td>
                <td><%=GetCostByMOnth(12) %></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>



            <tr id="tr14">
                <td>满意度</td>
                <td><%=Getmanyidu(1) %></td>
                <td><%=Getmanyidu(2) %></td>
                <td><%=Getmanyidu(3) %></td>
                <td><%=Getmanyidu(4) %></td>
                <td><%=Getmanyidu(5) %></td>
                <td><%=Getmanyidu(6) %></td>
                <td><%=Getmanyidu(7) %></td>
                <td><%=Getmanyidu(8) %></td>
                <td><%=Getmanyidu(9) %></td>
                <td><%=Getmanyidu(10) %></td>
                <td><%=Getmanyidu(11) %></td>
                <td><%=Getmanyidu(12) %></td>
                <td>&nbsp;</td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr id="tr15">
                <td>投诉量</td>
                <td></td>

                <td><%=GetComPlainSum(1) %></td>
                <td><%=GetComPlainSum(2) %></td>
                <td><%=GetComPlainSum(3) %></td>
                <td><%=GetComPlainSum(4) %></td>
                <td><%=GetComPlainSum(5) %></td>
                <td><%=GetComPlainSum(6) %></td>
                <td><%=GetComPlainSum(7) %></td>
                <td><%=GetComPlainSum(8) %></td>
                <td><%=GetComPlainSum(9) %></td>
                <td><%=GetComPlainSum(10) %></td>
                <td><%=GetComPlainSum(11) %></td>
                <td><%=GetComPlainSum(12) %></td>
                <td></td>
                <td></td>
                <td></td>

            </tr>
        </thead>
    </table>


</asp:Content>
