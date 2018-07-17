<%@ Page Title="渠道客源分析" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="ChannelCustomerAnalysis.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport.ChannelCustomerAnalysis" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/jquery-1.7.1.js"></script>
    <script src="../../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript">
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        }
        $(document).ready(function () {
            if ($("#HideValues").val() == "1") {        //显示渠道类型
                $(".div_Source").hide();
            } else {                //显示渠道名称
                $(".div_Channel").hide();
            }
            $("#btnSwitch").click(function () {
                if ($('.div_Channel').is(':hidden')) {
                    $(".div_Source").hide();
                    $(".div_Channel").show();
                    document.getElementById("<%=HideValues.ClientID %>").value = "1";
                }
                else {
                    $(".div_Source").show();
                    $(".div_Channel").hide();
                    document.getElementById("<%=HideValues.ClientID %>").value = "0";
                }
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow: auto; height: 990px;">
        <table class="table table-bordered" style="width: 100%; height: 30px;">
            <tr>
                <td>渠道类型</td>
                <td>
                    <cc1:ddlChannelType ID="ddlChannelType" runat="server" Width="115px">
                    </cc1:ddlChannelType>
                </td>
                <td>
                    <input type="button" id="btnSwitch" value="切换" class="btn btn-primary" /></td>
                <td colspan="2">
                    <HA:DateRanger runat="server" ID="DateRanger" Title="时间" />
                </td>
               
                <td colspan="2">
                    <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSerch_Click" />&nbsp;
                <cc2:btnReload ID="btnReload2" runat="server" />
                </td>

                <td colspan="8">
                    <div class="div_PrintOrExport">
                        <asp:Button runat="server" ID="btnPrint" Text="打印" CssClass="btn btn-primary" OnClientClick="return preview()" />
                        <asp:Button runat="server" ID="btnExport" Text="导出" CssClass="btn btn-primary" OnClick="btnExport_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <!--startprint-->
        <div class="div_Source">
            <table class="table table-bordered" style="width: 98%; height: auto;">
                <tr>
                    <td>名次</td>
                    <td>
                        <asp:LinkButton runat="server" ID="lbtnSourceName" Text="渠道名称" CommandName="Sourcename" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="btnCustomerCount" Text="客源量" CommandName="CustomerCount" OnClick="lbtnSourceName_Click" /></td>

                    <td>
                        <asp:LinkButton runat="server" ID="bntComeCount" Text="入客量" CommandName="ComeCount" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="btnTurnoveRate" Text="转换率" CommandName="TurnoveRate" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="btnAdvanceCount" Text="签单量" CommandName="AdvanceCount" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="btnComeDealRate" Text="成交率" CommandName="ComeDealRate" OnClick="lbtnSourceName_Click" /></td>
                    <%--<td>
                <asp:LinkButton runat="server" ID="btnFinisMoneySum" Text="销售额" CommandName="FinisMoneySum" OnClick="lbtnSourceName_Click" /></td>--%>

                    <td>
                        <asp:LinkButton runat="server" ID="btnFinishCount" Text="执行量" CommandName="FinishCount" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <!--原始 完工额-->
                        <asp:LinkButton runat="server" ID="btnFinishSumMoney" Text="执行额" CommandName="FinishSumMoney" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="btnFinishAvgMoney" Text="平均消费金额" CommandName="FinishAvgMoney" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="btnCost" Text="总成本" CommandName="Cost" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="btnGross" Text="毛利率" CommandName="Gross" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="btnSourceSumMoney" Text="渠道费用" CommandName="SourceSumMoney" OnClick="lbtnSourceName_Click" /></td>
                </tr>
                <asp:Repeater ID="repList" runat="server" ClientIDMode="Static">
                    <ItemTemplate>
                        <tr>
                            <td><%#(Container.ItemIndex+1).ToString() %></td>
                            <td><%#Eval("Sourcename") %></td>
                            <td><%#Eval("CustomerCount") %></td>

                            <td><%#Eval("ComeCount") %></td>
                            <td><%#Eval("TurnoveRate") %></td>
                            <td><%#Eval("AdvanceCount") %></td>
                            <td><%#Eval("ComeDealRate") %></td>
                            <%-- <td><%#Eval("FinisMoneySum") %></td>--%>

                            <td><%#Eval("FinishCount") %></td>
                            <td><%#Eval("FinishSumMoney") %></td>
                            <td><%#Eval("FinishAvgMoney") %></td>
                            <td><%#Eval("Cost") %></td>
                            <td><%#Eval("Gross") %></td>
                            <td><%#Eval("SourceSumMoney") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="tr_Total">
                    <td>&nbsp;</td>
                    <td>合计</td>
                    <td>
                        <asp:Label runat="server" ID="lblCustomerCountSum" /></td>

                    <td>
                        <asp:Label runat="server" ID="lblComeCountSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblTurnoveRateSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblAdvanceCountSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblComeDealRateSum" /></td>

                    <td>
                        <asp:Label runat="server" ID="lblFinishCountSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblFinishSumMoneySum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblFinishAvgMoneySum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblCostSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblGrossSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblSourceSumMoneySum" /></td>
                </tr>
            </table>
        </div>

        <div class="div_Channel">
            <table class="table table-bordered" style="width: 98%; height: auto;">
                <tr>
                    <td>名次</td>
                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton1" Text="渠道名称" CommandName="Sourcename" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton2" Text="客源量" CommandName="CustomerCount" OnClick="lbtnSourceName_Click" /></td>

                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton3" Text="入客量" CommandName="ComeCount" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton4" Text="转换率" CommandName="TurnoveRate" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton5" Text="签单量" CommandName="AdvanceCount" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton6" Text="成交率" CommandName="ComeDealRate" OnClick="lbtnSourceName_Click" /></td>
                    <%--<td>
                <asp:LinkButton runat="server" ID="btnFinisMoneySum" Text="销售额" CommandName="FinisMoneySum" OnClick="lbtnSourceName_Click" /></td>--%>

                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton7" Text="执行量" CommandName="FinishCount" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <!--原始 完工额-->
                        <asp:LinkButton runat="server" ID="LinkButton8" Text="执行额" CommandName="FinishSumMoney" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton9" Text="平均消费金额" CommandName="FinishAvgMoney" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton10" Text="总成本" CommandName="Cost" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton11" Text="毛利率" CommandName="Gross" OnClick="lbtnSourceName_Click" /></td>
                    <td>
                        <asp:LinkButton runat="server" ID="LinkButton12" Text="渠道费用" CommandName="SourceSumMoney" OnClick="lbtnSourceName_Click" /></td>
                </tr>

                <asp:Repeater runat="server" ID="repChannelList" OnItemCommand="repCahnnelList_ItemCommand" ClientIDMode="Static">
                    <ItemTemplate>
                        <tr>
                            <td><%#(Container.ItemIndex+1).ToString() %></td>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnSourceName" Text='<%#Eval("Sourcename") %>' CommandName="Details" CommandArgument='<%#Eval("ChannelTypeID") %>' ClientIDMode="Static" /></td>
                            <td><%#Eval("CustomerCount") %></td>
                            <td><%#Eval("ComeCount") %></td>
                            <td><%#Eval("TurnoveRate") %></td>
                            <td><%#Eval("AdvanceCount") %></td>
                            <td><%#Eval("ComeDealRate") %></td>
                            <td><%#Eval("FinishCount") %></td>
                            <td><%#Eval("FinishSumMoney") %></td>
                            <td><%#Eval("FinishAvgMoney") %></td>
                            <td><%#Eval("Cost") %></td>
                            <td><%#Eval("Gross") %></td>
                            <td><%#Eval("SourceSumMoney") %></td>
                        </tr>
                        <asp:Repeater runat="server" ID="repDetaislList">
                            <ItemTemplate>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td><%#(Container.ItemIndex+1).ToString() %>.<%#Eval("Sourcename") %></td>
                                    <td><%#Eval("CustomerCount") %></td>
                                    <td><%#Eval("ComeCount") %></td>
                                    <td><%#Eval("TurnoveRate") %></td>
                                    <td><%#Eval("AdvanceCount") %></td>
                                    <td><%#Eval("ComeDealRate") %></td>
                                    <td><%#Eval("FinishCount") %></td>
                                    <td><%#Eval("FinishSumMoney") %></td>
                                    <td><%#Eval("FinishAvgMoney") %></td>
                                    <td><%#Eval("Cost") %></td>
                                    <td><%#Eval("Gross") %></td>
                                    <td><%#Eval("SourceSumMoney") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td></td>
                    <td>合计</td>
                    <td>
                        <asp:Label runat="server" ID="lblCustomerCounSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblComeSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblTurnRateSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblAdvanceSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblComeRateSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblFinishSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblFinishMoneySum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblFinishAvgSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblCostCountSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblGrossCountSum" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblSourceMoneySum" /></td>

                </tr>
            </table>
        </div>
        <!--endprint-->
        <asp:HiddenField runat="server" ID="HideValues" ClientIDMode="Static" Value="" />
    </div>

</asp:Content>
