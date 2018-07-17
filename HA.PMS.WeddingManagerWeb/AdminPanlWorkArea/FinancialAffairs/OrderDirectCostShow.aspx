<%@ Page Title="查看订单成本明细" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="OrderDirectCostShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.OrderDirectCostShow" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
    <br />

    <!--人员-->
    <table class="table table-bordered table-striped" style="width: 85%;">

        <thead>

            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">执行团队：
                </td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <th width="10%">姓名</th>
                <th width="30%">说明</th>
                <th width="10%">评价</th>
                <th width="15%">计划支出</th>
                <th width="15%">实际支出</th>
                <th width="20%">备注</th>
            </tr>

        </thead>
        <tbody>
            <asp:Repeater ID="repEmployeeCost" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100">
                            <%#Eval("Name") %></td>

                        <td><%#Eval("Content") %></td>
                        <td><%#GetNameByEvaulationId(Eval("Evaluation")) %></td>
                        <td>
                            <%#Eval("Sumtotal") %></td>
                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label>
                        </td>
                        <td><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>

        </tfoot>
    </table>

    <!--物料-->
    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">物料：
                </td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <th width="10%">名称</th>
                <th width="30%">说明</th>
                <th width="10%">评价</th>
                <th width="15%">计划支出</th>
                <th width="15%">实际支出</th>
                <th width="20%">备注</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repSupplierCost" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100"><%#Eval("Name") %></td>
                        <td>
                            <asp:Label ID="txtInsideRemark" Enabled="false" runat="server" Text='<%#Eval("Content") %>'></asp:Label></td>
                        <td><%#GetNameByEvaulationId(Eval("Evaluation")) %></td>
                        <td>
                            <asp:Label ID="txtPlannedExpenditure" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>

                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>
                        <td><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <!--其他-->
    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">其它：
                </td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <th width="10%">名称</th>
                <th width="30%">说明</th>
                <th width="10%">评价</th>
                <th width="15%">计划支出</th>
                <th width="15%">实际支出</th>
                <th width="20%">备注</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repOtherCost" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100"><%#Eval("Name") %></td>
                        <td>
                            <asp:Label ID="txtInsideRemark" Enabled="false" runat="server" Text='<%#Eval("Content") %>'></asp:Label></td>
                        <td><%#GetNameByEvaulationId(Eval("Evaluation")) %></td>
                        <td>
                            <asp:Label ID="txtPlannedExpenditure" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>

                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>
                        <td><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <!--销售成本-->
    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">销售成本：
                </td>
                <td colspan="4"></td>
            </tr>
            <tr>
               <th width="10%">名称</th>
                <th width="40%">说明</th>
                <th width="15%">计划支出</th>
                <th width="15%">实际支出</th>
                <th width="20%">备注</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repSaleCost" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100"><%#Eval("Name") %></td>
                        <td>
                            <asp:Label ID="txtInsideRemark" Enabled="false" runat="server" Text='<%#Eval("Content") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="txtPlannedExpenditure" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>
                        <td><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>


    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="218" colspan="2">订单：
                </td>
                <td colspan="7"></td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td width="109">订单总金额</td>
                <td width="109">
                    <asp:Label ID="lblTotalAmount" Enabled="false" runat="server"></asp:Label></td>
                <td>订单总成本</td>
                <td>
                    <asp:Label ID="lblCost" runat="server"></asp:Label></td>
                <td>利润</td>
                <td>
                    <asp:Label ID="lblProfit" runat="server"></asp:Label></td>
                <td>利润率</td>
                <td>
                    <asp:Label ID="lblProfitMargin" runat="server"></asp:Label></td>
            </tr>

            <tr>
                <td colspan="8" style="text-align: center;">
                    <asp:HiddenField ID="hideTotal" runat="server" ClientIDMode="Static" />
                </td>
            </tr>
        </tbody>
    </table>

</asp:Content>
