<%@ Page Title="查看订单成本明细" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="OrderDirectCostShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.OrderDirectCostShow" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">

    <script type="text/javascript">
        function HideCtable(ControlID) {
            $(".WorkTable").show();
            $(ControlID).hidden();

        }

        $(document).ready(function () {

            $(".NeedCost").change(function () {
                GetTotal();
            });
        });


        function GetTotal() {
            var TotalSum = 0;
            $(".NeedCost").each(function () {
                if ($(this).val() != "") {
                    TotalSum += parseFloat($(this).val());
                }
            });
            $("#txtCost").attr("value", TotalSum);
            var GetOnlyData = (TotalSum / parseFloat($("#hideTotal").val())).toFixed(2);
            $("#txtProfitMargin").attr("value", ((1 - GetOnlyData) * 100).toFixed(2) + "%");
        }


    </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
    <br />

    <table class="table table-bordered table-striped" style="width: 85%;">

        <thead>

            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">执行团队：
                </td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <th width="100">姓名</th>
                <th>说明</th>
                <th>计划支出</th>
                <th>实际支出</th>
            </tr>

        </thead>
        <tbody>
            <asp:Repeater ID="repEmployeeCost" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100">
                            <%#Eval("Name") %></td>

                        <td><%#Eval("Content") %></td>
                        <td>
                            <%#Eval("Sumtotal") %></td>
                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label>
                        </td>
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


    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">供应商：
                </td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <th width="100">供应商</th>
                <th>说明</th>
                <th>计划支出</th>
                <th>实际支出</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repSupplierCost" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100"><%#Eval("Name") %></td>
                        <td>
                            <asp:Label ID="txtInsideRemark" Enabled="false" runat="server" Text='<%#Eval("Content") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="txtPlannedExpenditure" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>

                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">库房：
                </td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <th width="100">供应商</th>
                <th>说明</th>
                <th>计划支出</th>
                <th>实际支出</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptStore" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100"><%#Eval("Name") %></td>
                        <td>
                            <asp:Label ID="txtInsideRemark" Enabled="false" runat="server" Text='<%#Eval("Content") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="txtPlannedExpenditure" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>

                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">采购物料：
                </td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <th width="100">供应商</th>
                <th>说明</th>
                <th>计划支出</th>
                <th>实际支出</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repBuyCost" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100"><%#Eval("Name") %></td>
                        <td>
                            <asp:Label ID="txtInsideRemark" Enabled="false" runat="server" Text='<%#Eval("Content") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="txtPlannedExpenditure" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>

                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">花艺成本：
                </td>
                <td colspan="4"></td>
            </tr>
            <tr>
                <th width="100">供应商</th>
                <th>说明</th>
                <th>计划支出</th>
                <th>实际支出</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repFlowerCost" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100"><%#Eval("Name") %></td>
                        <td>
                            <asp:Label ID="txtInsideRemark" Enabled="false" runat="server" Text='<%#Eval("Content") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="txtPlannedExpenditure" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>

                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">其他：
                </td>
                <td colspan="3"><a href="/AdminPanlWorkArea/Carrytask/ProductListforWareHouse.aspx?DispatchingID=<%=Request["DispatchingID"] %>&CustomerID=<%=Request["CustomerID"] %>&NeedPopu=1" target="_blank" class="btn btn-success">查看本订单库房使用产品</a></td>
            </tr>
            <tr>
                <th>类别</th>
                <th>说明</th>
                <th>计划支出</th>
                <th>实际支出</th>
            </tr>


        </thead>
        <tbody>
            <asp:Repeater ID="repOther" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="100"><%#Eval("Name") %></td>
                        <td>
                            <asp:Label ID="txtInsideRemark" Enabled="false" runat="server" Text='<%#Eval("Content") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="txtPlannedExpenditure" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>

                        <td>
                            <asp:Label ID="txtActualExpenditure" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <table class="table table-bordered table-striped" style="width: 85%;">
        <thead>
            <tr>
                <td style="font-weight: bold; background-color: #808080" width="100">订单：
                </td>
                <td colspan="7"></td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>订单总金额</td>
                <td><asp:Label ID="lblTotalAmount" Enabled="false" runat="server"></asp:Label></td>
                <td>订单总成本</td>
                <td><asp:Label ID="lblCost" runat="server"></asp:Label></td>
                <td>利润</td>
                <td><asp:Label ID="lblProfit" runat="server"></asp:Label></td>
                <td>利润率</td>
                <td><asp:Label ID="lblProfitMargin" runat="server"></asp:Label></td>
            </tr>

            <tr>
                <td colspan="8" style="text-align: center;">
                    <asp:HiddenField ID="hideTotal" runat="server" ClientIDMode="Static" />
                </td>
            </tr>
        </tbody>
    </table>

</asp:Content>
