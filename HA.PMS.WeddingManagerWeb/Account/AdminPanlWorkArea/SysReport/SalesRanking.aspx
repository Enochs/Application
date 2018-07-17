<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SalesRanking.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport.SalesRanking" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/YearSelector.ascx" TagPrefix="HA" TagName="YearSelector" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <%--            <td>
                <HA:MyManager runat="server" ID="MyManager" Title="婚礼顾问"/>
            </td>--%>
            <td>部门</td>
            <td>
                <cc2:DepartmentDropdownList ID="ddlDepartment" runat="server">
                </cc2:DepartmentDropdownList>
            </td>
            <td>
                <HA:DateRanger runat="server" ID="DateRanger" Title="时间" />
            </td>
            <td>
                <asp:Button runat="server" ID="btn_Search" Text="查找" CssClass="btn btn-primary" OnClick="btn_Search_Click" />&nbsp;&nbsp;
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
        </tr>
        <tr>
            <th>名次</th>
            <th>姓名</th>
            <th>
                <asp:LinkButton runat="server" ID="lbtnSumReturnMoney" CommandName="SumReturnMoney" Text="现金流" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th> <asp:LinkButton runat="server" ID="lbtnSourceCount" CommandName="SourceCount" Text="客源量" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th> <asp:LinkButton runat="server" ID="lbtnInSourceCount" CommandName="InSourceCount" Text="入客量" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th> <asp:LinkButton runat="server" ID="lbtnInSourceRate" CommandName="InSourceRate" Text="转换率" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th>
                <asp:LinkButton runat="server" ID="lbtnComeOrderCount" CommandName="ComeOrderCount" Text="新客户" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th>
                <asp:LinkButton runat="server" ID="lbtnNewOrderByMonth" CommandName="NewOrderByMonth" Text="新订单" OnClick="lbtnSumReturnMoney_Click" /></th>

            <th>
                <asp:LinkButton runat="server" ID="lbtnTurnoveRate" CommandName="TurnoveRate" Text="成交率" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th>
                <asp:LinkButton runat="server" ID="lbtnFinisMoneySum" CommandName="FinisMoneySum" Text="执行额" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th>
                <asp:LinkButton runat="server" ID="lbtnFinishCount" CommandName="FinishCount" Text="执行量" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th>
                <asp:LinkButton runat="server" ID="lbtnAvgQuotedMoney" CommandName="AvgQuotedMoney" Text="平均消费" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th>
                <asp:LinkButton runat="server" ID="lbtnCost" CommandName="Cost" Text="总成本" OnClick="lbtnSumReturnMoney_Click" /></th>

            <th>
                <asp:LinkButton runat="server" ID="lbtnMineMoney" CommandName="MineMoney" Text="毛利" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th>
                <asp:LinkButton runat="server" ID="lbtnGross" CommandName="Gross" Text="毛利率" OnClick="lbtnSumReturnMoney_Click" /></th>
            <th>满意度</th>
        </tr>
        <asp:Repeater ID="repdataList" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Container.ItemIndex+1 %></td>
                    <td><%#Eval("EmployeeName") %></td>
                    <td><%#Eval("SumReturnMoney") %></td>
                    <td><%#Eval("SourceCount") %></td>
                    <td><%#Eval("InSourceCount") %></td>
                    <td><%#Eval("InSourceRate") %></td>
                    <td><%#Eval("ComeOrderCount") %></td>
                    <td><%#Eval("NewOrderByMonth") %></td>

                    <td><%#Eval("TurnoveRate") %></td>
                    <td><%#Eval("FinisMoneySum") %></td>
                    <td><%#Eval("FinishCount") %></td>
                    <td><%#Eval("AvgQuotedMoney") %></td>
                    <td><%#Eval("Cost") %></td>

                    <td><%#Eval("MineMoney") %></td>
                    <td><%#Eval("Gross") %></td>
                    <td>&nbsp;</td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td></td>
            <td>合计</td>
            <td>
                <asp:Label runat="server" ID="lblSumReturnMoney" /></td>
            <td><asp:Label runat="server" ID="lblSourceCount" /></td>
            <td><asp:Label runat="server" ID="lblInSourceCount" /></td>
            <td><asp:Label runat="server" ID="lblInSourceRate" /></td>
            <td>
                <asp:Label runat="server" ID="lblComeOrderCount" /></td>
            <td>
                <asp:Label runat="server" ID="lblNewOrderByMonth" /></td>

            <td>
                <asp:Label runat="server" ID="lblTurnoveRate" /></td>
            <td>
                <asp:Label runat="server" ID="lblFinisMoneySum" /></td>
            <td>
                <asp:Label runat="server" ID="lblFinishCount" /></td>
            <td>
                <asp:Label runat="server" ID="lblAvgQuotedMoney" /></td>
            <td>
                <asp:Label runat="server" ID="lblCostSum" /></td>

            <td>
                <asp:Label runat="server" ID="lblMineMoney" /></td>
            <td>
                <asp:Label runat="server" ID="lblGross" /></td>
            <td></td>

        </tr>
    </table>
    <asp:Button runat="server" ID="btn_Export" Text="导出Excel" OnClick="btn_Export_Click" />
</asp:Content>
