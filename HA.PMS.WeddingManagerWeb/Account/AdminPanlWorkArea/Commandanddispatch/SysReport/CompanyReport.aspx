<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyReport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.SysReport.CompanyReport" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">

    <table style="border-collapse: collapse; width: 100%; border: inset;">

        <tr height="19">
            <td></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
            <td height="19"></td>
        </tr>
        <tr height="19">
            <td>月份</td>
            <td></td>
            <td>1</td>
            <td>2</td>
            <td>3</td>
            <td>4</td>
            <td>5</td>
            <td>6</td>
            <td>7</td>
            <td>8</td>
            <td>9</td>
            <td>10</td>
            <td>11</td>
            <td>12</td>
            <td>当年合计</td>
            <td>上年合计</td>
        </tr>

        <tr>
            <td rowspan="8">当期销售报表</td>
            <td>新签定金</td>
            <td><%=BinderSaleReport(1,1) %></td>
            <td><%=BinderSaleReport(2,1) %></td>
            <td><%=BinderSaleReport(3,1) %></td>
            <td><%=BinderSaleReport(4,1) %></td>
            <td><%=BinderSaleReport(5,1) %></td>
            <td><%=BinderSaleReport(6,1) %></td>
            <td><%=BinderSaleReport(7,1) %></td>
            <td><%=BinderSaleReport(8,1) %></td>
            <td><%=BinderSaleReport(9,1) %></td>
            <td><%=BinderSaleReport(10,1) %></td>
            <td><%=BinderSaleReport(11,1) %></td>
            <td><%=BinderSaleReport(12,1) %></td>
            <%=BinderSumReport(1,1) %>
        </tr>
        <tr height="19">
            <td>新签对数</td>
            <td><%=BinderSaleReport(1,2) %></td>
            <td><%=BinderSaleReport(2,2) %></td>
            <td><%=BinderSaleReport(3,2) %></td>
            <td><%=BinderSaleReport(4,2) %></td>
            <td><%=BinderSaleReport(5,2) %></td>
            <td><%=BinderSaleReport(6,2) %></td>
            <td><%=BinderSaleReport(7,2) %></td>
            <td><%=BinderSaleReport(8,2) %></td>
            <td><%=BinderSaleReport(9,2) %></td>
            <td><%=BinderSaleReport(10,2) %></td>
            <td><%=BinderSaleReport(11,2) %></td>
            <td><%=BinderSaleReport(12,2) %></td>
            <%=BinderSumReport(1,2) %>
        </tr>
        <tr height="19">
            <td>新签合同金额</td>
            <td><%=BinderSaleReport(1,3) %></td>
            <td><%=BinderSaleReport(2,3) %></td>
            <td><%=BinderSaleReport(3,3) %></td>
            <td><%=BinderSaleReport(4,3) %></td>
            <td><%=BinderSaleReport(5,3) %></td>
            <td><%=BinderSaleReport(6,3) %></td>
            <td><%=BinderSaleReport(7,3) %></td>
            <td><%=BinderSaleReport(8,3) %></td>
            <td><%=BinderSaleReport(9,3) %></td>
            <td><%=BinderSaleReport(10,3) %></td>
            <td><%=BinderSaleReport(11,3) %></td>
            <td><%=BinderSaleReport(12,3) %></td>
            <%=BinderSumReport(1,3) %>
        </tr>
        <tr height="19">
            <td>新签合同对数</td>
            <td><%=BinderSaleReport(1,4) %></td>
            <td><%=BinderSaleReport(2,4) %></td>
            <td><%=BinderSaleReport(3,4) %></td>
            <td><%=BinderSaleReport(4,4) %></td>
            <td><%=BinderSaleReport(5,4) %></td>
            <td><%=BinderSaleReport(6,4) %></td>
            <td><%=BinderSaleReport(7,4) %></td>
            <td><%=BinderSaleReport(8,4) %></td>
            <td><%=BinderSaleReport(9,4) %></td>
            <td><%=BinderSaleReport(10,4) %></td>
            <td><%=BinderSaleReport(11,4) %></td>
            <td><%=BinderSaleReport(12,4) %></td>
            <%=BinderSumReport(1,4) %>
        </tr>
        <tr height="19">
            <td>平均消费金额</td>
            <td><%=BinderSaleReport(1,5) %></td>
            <td><%=BinderSaleReport(2,5) %></td>
            <td><%=BinderSaleReport(3,5) %></td>
            <td><%=BinderSaleReport(4,5) %></td>
            <td><%=BinderSaleReport(5,5) %></td>
            <td><%=BinderSaleReport(6,5) %></td>
            <td><%=BinderSaleReport(7,5) %></td>
            <td><%=BinderSaleReport(8,5) %></td>
            <td><%=BinderSaleReport(9,5) %></td>
            <td><%=BinderSaleReport(10,5) %></td>
            <td><%=BinderSaleReport(11,5) %></td>
            <td><%=BinderSaleReport(12,5) %></td>
            <%=BinderSumReport(1,5) %>
        </tr>
        <tr height="19">
            <td>入客量</td>
            <td><%=BinderSaleReport(1,6) %></td>
            <td><%=BinderSaleReport(2,6) %></td>
            <td><%=BinderSaleReport(3,6) %></td>
            <td><%=BinderSaleReport(4,6) %></td>
            <td><%=BinderSaleReport(5,6) %></td>
            <td><%=BinderSaleReport(6,6) %></td>
            <td><%=BinderSaleReport(7,6) %></td>
            <td><%=BinderSaleReport(8,6) %></td>
            <td><%=BinderSaleReport(9,6) %></td>
            <td><%=BinderSaleReport(10,6) %></td>
            <td><%=BinderSaleReport(11,6) %></td>
            <td><%=BinderSaleReport(12,6) %></td>
            <%=BinderSumReport(1,6) %>
        </tr>
        <tr height="19">
            <td>成交率</td>
            <td><%=BinderSaleReport(1,7) %></td>
            <td><%=BinderSaleReport(2,7) %></td>
            <td><%=BinderSaleReport(3,7) %></td>
            <td><%=BinderSaleReport(4,7) %></td>
            <td><%=BinderSaleReport(5,7) %></td>
            <td><%=BinderSaleReport(6,7) %></td>
            <td><%=BinderSaleReport(7,7) %></td>
            <td><%=BinderSaleReport(8,7) %></td>
            <td><%=BinderSaleReport(9,7) %></td>
            <td><%=BinderSaleReport(10,7) %></td>
            <td><%=BinderSaleReport(11,7) %></td>
            <td><%=BinderSaleReport(12,7) %></td>
            <%=BinderSumReport(1,7) %>
        </tr>
        <tr height="19">
            <td>现金流</td>
            <td><%=BinderSaleReport(1,8) %></td>
            <td><%=BinderSaleReport(2,8) %></td>
            <td><%=BinderSaleReport(3,8) %></td>
            <td><%=BinderSaleReport(4,8) %></td>
            <td><%=BinderSaleReport(5,8) %></td>
            <td><%=BinderSaleReport(6,8) %></td>
            <td><%=BinderSaleReport(7,8) %></td>
            <td><%=BinderSaleReport(8,8) %></td>
            <td><%=BinderSaleReport(9,8) %></td>
            <td><%=BinderSaleReport(10,8) %></td>
            <td><%=BinderSaleReport(11,8) %></td>
            <td><%=BinderSaleReport(12,8) %></td>
            <%=BinderSumReport(1,8) %>
        </tr>
        <tr>
            <td rowspan="6">当期完工财务报表</td>
            <td>完工金额</td>
            <td><%=BinderSaleReport(1,9) %></td>
            <td><%=BinderSaleReport(2,9) %></td>
            <td><%=BinderSaleReport(3,9) %></td>
            <td><%=BinderSaleReport(4,9) %></td>
            <td><%=BinderSaleReport(5,9) %></td>
            <td><%=BinderSaleReport(6,9) %></td>
            <td><%=BinderSaleReport(7,9) %></td>
            <td><%=BinderSaleReport(8,9) %></td>
            <td><%=BinderSaleReport(9,9) %></td>
            <td><%=BinderSaleReport(10,9) %></td>
            <td><%=BinderSaleReport(11,9) %></td>
            <td><%=BinderSaleReport(12,9) %></td>
            <%=BinderSumReport(1,9) %>
        </tr>
        <tr>
            <td>服务对数</td>
            <td><%=BinderSaleReport(1,10) %></td>
            <td><%=BinderSaleReport(2,10) %></td>
            <td><%=BinderSaleReport(3,10) %></td>
            <td><%=BinderSaleReport(4,10) %></td>
            <td><%=BinderSaleReport(5,10) %></td>
            <td><%=BinderSaleReport(6,10) %></td>
            <td><%=BinderSaleReport(7,10) %></td>
            <td><%=BinderSaleReport(8,10) %></td>
            <td><%=BinderSaleReport(9,10) %></td>
            <td><%=BinderSaleReport(10,10) %></td>
            <td><%=BinderSaleReport(11,10) %></td>
            <td><%=BinderSaleReport(12,10) %></td>

            <%=BinderSumReport(1,10) %>
        </tr>

        <tr>
            <td>毛利润</td>
            <td><%=BinderSaleReport(1,11) %></td>
            <td><%=BinderSaleReport(2,11) %></td>
            <td><%=BinderSaleReport(3,11) %></td>
            <td><%=BinderSaleReport(4,11) %></td>
            <td><%=BinderSaleReport(5,11) %></td>
            <td><%=BinderSaleReport(6,11) %></td>
            <td><%=BinderSaleReport(7,11) %></td>
            <td><%=BinderSaleReport(8,11) %></td>
            <td><%=BinderSaleReport(9,11) %></td>
            <td><%=BinderSaleReport(10,11) %></td>
            <td><%=BinderSaleReport(11,11) %></td>
            <td><%=BinderSaleReport(12,11) %></td>
            <%=BinderSumReport(1,11) %>
        </tr>

        <tr height="19">

            <td>当期应收款</td>
            <td><%=BinderSaleReport(1,10) %></td>
            <td><%=BinderSaleReport(2,10) %></td>
            <td><%=BinderSaleReport(3,10) %></td>
            <td><%=BinderSaleReport(4,10) %></td>
            <td><%=BinderSaleReport(5,10) %></td>
            <td><%=BinderSaleReport(6,10) %></td>
            <td><%=BinderSaleReport(7,10) %></td>
            <td><%=BinderSaleReport(8,10) %></td>
            <td><%=BinderSaleReport(9,10) %></td>
            <td><%=BinderSaleReport(10,10) %></td>
            <td><%=BinderSaleReport(11,10) %></td>
            <td><%=BinderSaleReport(12,10) %></td>

            <td></td>
            <td></td>
        </tr>
        <tr height="19">

            <td>实际婚期入客量</td>
            <td><%=BinderSaleReport(1,10) %></td>
            <td><%=BinderSaleReport(2,10) %></td>
            <td><%=BinderSaleReport(3,10) %></td>
            <td><%=BinderSaleReport(4,10) %></td>
            <td><%=BinderSaleReport(5,10) %></td>
            <td><%=BinderSaleReport(6,10) %></td>
            <td><%=BinderSaleReport(7,10) %></td>
            <td><%=BinderSaleReport(8,10) %></td>
            <td><%=BinderSaleReport(9,10) %></td>
            <td><%=BinderSaleReport(10,10) %></td>
            <td><%=BinderSaleReport(11,10) %></td>
            <td><%=BinderSaleReport(12,10) %></td>
        </tr>
        <tr height="19">

            <td>实际婚期成交率</td>
            <td><%=BinderSaleReport(1,10) %></td>
            <td><%=BinderSaleReport(2,10) %></td>
            <td><%=BinderSaleReport(3,10) %></td>
            <td><%=BinderSaleReport(4,10) %></td>
            <td><%=BinderSaleReport(5,10) %></td>
            <td><%=BinderSaleReport(6,10) %></td>
            <td><%=BinderSaleReport(7,10) %></td>
            <td><%=BinderSaleReport(8,10) %></td>
            <td><%=BinderSaleReport(9,10) %></td>
            <td><%=BinderSaleReport(10,10) %></td>
            <td><%=BinderSaleReport(11,10) %></td>
            <td><%=BinderSaleReport(12,10) %></td>
        </tr>
        <tr>
            <td rowspan="4">渠道分析<td>客源量</td>
                <td><%=BinderSaleReport(1,12) %></td>
                <td><%=BinderSaleReport(2,12) %></td>
                <td><%=BinderSaleReport(3,12) %></td>
                <td><%=BinderSaleReport(4,12) %></td>
                <td><%=BinderSaleReport(5,12) %></td>
                <td><%=BinderSaleReport(6,12) %></td>
                <td><%=BinderSaleReport(7,12) %></td>
                <td><%=BinderSaleReport(8,12) %></td>
                <td><%=BinderSaleReport(9,12) %></td>
                <td><%=BinderSaleReport(10,12) %></td>
                <td><%=BinderSaleReport(11,12) %></td>
                <td><%=BinderSaleReport(12,12) %></td>
                <%=BinderSumReport(1,12) %>
            </td>
        </tr>
        <tr>
            <td>有效率</td>
            <td><%=BinderSaleReport(1,13) %></td>
            <td><%=BinderSaleReport(2,13) %></td>
            <td><%=BinderSaleReport(3,13) %></td>
            <td><%=BinderSaleReport(4,13) %></td>
            <td><%=BinderSaleReport(5,13) %></td>
            <td><%=BinderSaleReport(6,13) %></td>
            <td><%=BinderSaleReport(7,13) %></td>
            <td><%=BinderSaleReport(8,13) %></td>
            <td><%=BinderSaleReport(9,13) %></td>
            <td><%=BinderSaleReport(10,13) %></td>
            <td><%=BinderSaleReport(11,13) %></td>
            <td><%=BinderSaleReport(12,13) %></td>
            <%=BinderSumReport(1,13) %>
        </tr>
        <tr>
            <td>到店率</td>
            <td><%=BinderSaleReport(1,14) %></td>
            <td><%=BinderSaleReport(2,14) %></td>
            <td><%=BinderSaleReport(3,14) %></td>
            <td><%=BinderSaleReport(4,14) %></td>
            <td><%=BinderSaleReport(5,14) %></td>
            <td><%=BinderSaleReport(6,14) %></td>
            <td><%=BinderSaleReport(7,14) %></td>
            <td><%=BinderSaleReport(8,14) %></td>
            <td><%=BinderSaleReport(9,14) %></td>
            <td><%=BinderSaleReport(10,14) %></td>
            <td><%=BinderSaleReport(11,14) %></td>
            <td><%=BinderSaleReport(12,14) %></td>
            <%=BinderSumReport(1,14) %>
        </tr>
        <tr>
            <td>当期渠道费用</td>
            <td><%=BinderSaleReport(1,15) %></td>
            <td><%=BinderSaleReport(2,15) %></td>
            <td><%=BinderSaleReport(3,15) %></td>
            <td><%=BinderSaleReport(4,15) %></td>
            <td><%=BinderSaleReport(5,15) %></td>
            <td><%=BinderSaleReport(6,15) %></td>
            <td><%=BinderSaleReport(7,15) %></td>
            <td><%=BinderSaleReport(8,15) %></td>
            <td><%=BinderSaleReport(9,15) %></td>
            <td><%=BinderSaleReport(10,15) %></td>
            <td><%=BinderSaleReport(11,15) %></td>
            <td><%=BinderSaleReport(12,15) %></td>
            <%=BinderSumReport(1,15) %>
        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
</asp:Content>

