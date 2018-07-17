<%@ Page Title="打印成本明细" StylesheetTheme="Default" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="OrderCostPrint.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost.OrderCostPrint" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .table {
            font-size: 14px;
        }

        ul li {
            list-style-type: none;
            font-size: 14px;
            margin-right: 150px;
        }

        .liHide {
            height: 20px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "1000px", "height": "Auto", "background-color": "white" });
        });

        //打印
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
            this.window.Attr("display", "none");
        }

        /*用window.onload调用myfun()*/
        //window.onload = preview;//不要括号

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="div_MainShow">
        <!--startprint-->
        <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
        <table class="table" style="width: 98%;">
            <tbody>
                <tr>
                    <td>
                        <table class="table table-bordered" style="width: 98%;" border="1">
                            <thead>
                                <tr>
                                    <td style="font-weight: bold; background-color: #808080;">执行团队：</td>
                                    <td colspan="7"></td>
                                </tr>
                                <tr>
                                    <th width="20%">姓名</th>
                                    <th width="210" height="auto">说明</th>
                                    <th width="40">计划成本</th>
                                    <th width="40">实际成本</th>
                                    <th width="40">已付款</th>
                                    <th width="40">未付款</th>
                                    <th width="12%">评价</th>
                                    <th width="150">备注</th>
                                </tr>
                                <asp:Repeater ID="repEmployeeCost" runat="server">
                                    <ItemTemplate>
                                        <tr>

                                            <td>
                                                <%#Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="200px" ID="lblContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="85px" ID="lblPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="80px" ClientIDMode="Static" ID="lblActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="80px" ClientIDMode="Static" ID="lblPayMent" Text='<%#Eval("PayMent") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoPayMent" Text='<%#Eval("NoPayMent") %>' />
                                            </td>
                                            <td>
                                                <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" TextMode="MultiLine" ID="lblRemark" Text='<%#Eval("Remark") %>'></asp:Label></</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </thead>
                            <tfoot>
                                <%--<tr>
                                    <td <%#IsHide(1) %> colspan="8" style="height: 25px;"></td>
                                </tr>--%>
                                <tr>
                                    <td colspan="8">人员销售价:<asp:Label runat="server" ID="lblPersonSale" />&nbsp;&nbsp;
                                        人员成本价:<asp:Label runat="server" ID="lblPersonCost" />&nbsp;&nbsp;
                                        人员毛利率:<asp:Label runat="server" ID="lblPersonRate" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>

                        <table class="WorkTable table table-bordered table-striped" style="width: 98%;" border="1">
                            <thead>
                                <tr>
                                    <td style="font-weight: bold; background-color: #808080" width="150">物料成本明细</td>
                                    <td colspan="7"></td>
                                </tr>
                                <tr>
                                    <th width="20%">供应商</th>
                                    <th width="210" height="auto">说明</th>
                                    <th width="40">计划成本</th>
                                    <th width="40">实际成本</th>
                                    <th width="40">已付款</th>
                                    <th width="40">未付款</th>
                                    <th width="12%">评价</th>
                                    <th width="150">备注</th>
                                </tr>

                                <asp:Repeater ID="repSupplierCost" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>

                                                <asp:Label runat="server" ID="lblMName" Text='<%#Eval("Name") %>' /></a>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="200px" ID="lblContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="85px" ID="lblPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="80px" ID="lblActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="80px" ID="lblPayMent" Text='<%#Eval("PayMent") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoPayMent" Text='<%#Eval("NoPayMent") %>' />
                                            </td>
                                            <td><%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" TextMode="MultiLine" ID="lblRemark" Text='<%#Eval("Remark") %>'></asp:Label></</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </thead>
                            <tfoot>
                                <%--<tr>
                                    <td <%#IsHide(2) %> colspan="8" style="height: 25px;"></td>
                                </tr>--%>
                                <tr>
                                    <td colspan="8">物料销售价:<asp:Label runat="server" ID="lblMaterialSale" />&nbsp;&nbsp;
                                        物料成本价:<asp:Label runat="server" ID="lblMaterialCost" />&nbsp;&nbsp;
                                        物料毛利率:<asp:Label runat="server" ID="lblMaterialRate" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </tfoot>
                        </table>


                    </td>
                </tr>

                <tr runat="server" id="tr_Other">
                    <td>
                        <table class="WorkTable table table-bordered table-striped" style="width: 98%;" border="1">
                            <thead>
                                <tr>
                                    <td style="font-weight: bold; background-color: #808080" width="150">其他</td>
                                    <td colspan="7"></td>
                                </tr>
                                <tr>
                                    <th width="20%">名称</th>
                                    <th width="210" height="auto">说明</th>
                                    <th width="40">计划成本</th>
                                    <th width="40">实际成本</th>
                                    <th width="40">已付款</th>
                                    <th width="40">未付款</th>
                                    <th width="12%">评价</th>
                                    <th width="150">备注</th>
                                </tr>

                                <asp:Repeater ID="repOtherCost" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblMName" Text='<%#Eval("Name") %>' />
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="200px" ID="lblContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="85px" ID="lblPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="80px" ID="lblActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="80px" ID="lblPayMent" Text='<%#Eval("PayMent") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoPayMent" Text='<%#Eval("NoPayMent") %>' />
                                            </td>
                                            <td>
                                                <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" TextMode="MultiLine" ID="lblRemark" Text='<%#Eval("Remark") %>'></asp:Label></</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </thead>
                            <tfoot runat="server" id="tfoot_Others">
                                <tr>
                                    <td <%#IsHide(2) %> colspan="8" style="height: 25px;"></td>
                                </tr>
                                <tr>
                                    <td colspan="8">其他销售价:<asp:Label runat="server" ID="lblQuotedOtherSale" />&nbsp;&nbsp;
                                        其他成本价:<asp:Label runat="server" ID="lblQuotedOtherCost" />&nbsp;&nbsp;
                                        其他毛利率:<asp:Label runat="server" ID="lblQuotedOtherRate" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </td>
                </tr>

                <tr runat="server" id="tr_Sale">
                    <td>

                        <table class="table table-bordered table-striped" style="width: 98%; margin-top: 0px;">
                            <thead>
                                <tr>
                                    <td style="font-weight: bold; background-color: #808080" width="12%">销售费用： </td>
                                    <td colspan="4"></td>

                                </tr>
                                <tr>
                                    <th width="5%">项目</th>
                                    <th width="210" height="auto">说明</th>
                                    <th width="40">计划成本</th>
                                    <th width="40">实际成本</th>
                                    <th width="40">已付款</th>
                                    <th width="40">未付款</th>
                                    <th width="80">备注</th>
                                </tr>
                                <asp:Repeater ID="repSaleMoney" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblCategoryName" Text='<%#Eval("Name") %>' />
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="200px" ID="lblContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblPlanSumtotal" Width="85px" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtActualSumTotal" Width="80px" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="80px" ID="txtPayMent" Text='<%#Eval("PayMent") == null ? "0" : Eval("PayMent") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoPayMent" Text='<%#Eval("NoPayMent") == null ? "0" : Eval("NoPayMent") %>' />
                                            </td>
                                            <td colspan="2">
                                                <asp:Label runat="server" ID="txtRemark" Text='<%#Eval("Remark") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </thead>
                        </table>
                    </td>
                </tr>
            </tbody>

            <tfoot>
                <tr>
                    <td colspan="4">销售价:<asp:Label runat="server" ID="lblSaleAmount" />&nbsp;&nbsp;
                        成本:<asp:Label runat="server" ID="lblSaleCost" />&nbsp;&nbsp;
                        毛利率:<asp:Label runat="server" ID="lblSaleRate" />&nbsp;&nbsp;
                   <%--     已支付:<asp:Label runat="server" ID="lblPayMentSum" />&nbsp;&nbsp;
                        未支付:<asp:Label runat="server" ID="lblNoPayMentSum" />&nbsp;&nbsp;--%>
                    </td>
                </tr>
            </tfoot>
        </table>
        <br />
        <ul style="float: right;">
            <li>策划师:<%=GetQuotedEmployee() %></li>
            <li class="liHide"></li>
            <li class="liHide"></li>
            <li>审核签字:</li>
            <li class="liHide"></li>
            <li>日期:<%#DateTime.Now.ToShortDateString() %></li>
        </ul>

        <!--endprint-->
        <div>
            <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-primary" Text="打印" OnClientClick="return preview()" />
        </div>
    </div>
</asp:Content>
