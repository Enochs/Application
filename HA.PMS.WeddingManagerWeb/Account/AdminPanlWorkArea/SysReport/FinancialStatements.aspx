<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FinancialStatements.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport.FinancialStatements" %>

<%@ Register Src="../Control/YearSelector.ascx" TagName="YearSelector" TagPrefix="uc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="uc1" TagName="MyManager" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-bordered le-striped table-select" style="width:40%">
        <tr>
            <td>
                <uc1:MyManager runat="server" ID="MyManager" Title="签单人" />
            </td>

            <td>
                <uc1:YearSelector ID="ys_year" runat="server" Title="年份" />
            </td>

            <td>
                <asp:Button runat="server" ID="btn_Search" Text="查找" CssClass="btn btn-primary" OnClick="btn_Search_Click"/>&nbsp;&nbsp;
                <cc2:btnReload ID="btnReload2" runat="server" />
            </td>
        </tr>
    </table>
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
            <td>完工额</td>
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
            <td>客户数</td>
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
            <td>毛利率</td>
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
            <td>毛利润</td>
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
            <td>满意度</td>
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
</asp:Content>
