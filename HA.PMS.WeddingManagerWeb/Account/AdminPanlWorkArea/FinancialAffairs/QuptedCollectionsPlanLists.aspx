<%@ Page Title="收款管理" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="QuptedCollectionsPlanLists.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.QuptedCollectionsPlanLists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
    <%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2">
    <script src="/Scripts/trselection.js"></script>

    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table>
                <tr>
                    <td>新人姓名</td>
                    <td>
                        <asp:TextBox ID="txtBridename" runat="server"></asp:TextBox>
                    </td>
                    <td>联系电话</td>
                    <td>
                        <asp:TextBox ID="txtContactPhone" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td>时间：<asp:DropDownList Width="85" ID="ddltimerType" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">婚期</asp:ListItem>
                        <asp:ListItem Value="1">录入时间</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                    <td>
                        <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSerch_Click" />
                    </td>
                    <td>
                        <cc2:btnReload ID="btnReload1" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>责任人</th>
                        <th>新人状态</th>
                        <th colspan="2" style="text-align: left">婚礼总金额</th>
                        <th colspan="2" style="text-align: left">收款金额</th>
                        <th style="text-align: left">余款</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%--<%#Eval("ContactMan").ToString().Length > 8 ? Eval("ContactMan").ToString().Substring(0,6)+"..." : Eval("ContactMan").ToString() %>--%>
                                        <%#ShowCstmName(Eval("Bride"),Eval("Groom")) %>
                                        <%-- <a id="SelectPG" <%#GetLoseState(Eval("CustomerID")) == "20" ? "style='color:red;display:block;'" : "style='display:none;'" %>>(退)</a>--%>
                                        <a id="SelectPG" <%#Eval("LoseContentID") != null ? "" : "style='display:none;'" %> style="color: red;">(退)</a>
                                </td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#Eval("Partydate", "{0:yyyy/MM/dd}")%></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td colspan="2"><%#GetQuotedFinishMoney(Eval("CustomerID")) %></td>
                                <td colspan="2"><%#GetFinishMoney(Eval("CustomerID")) %></td>
                                <td><%#GetOverFinishMoney(Eval("CustomerID")) %></td>
                                <td>
                                    <a class="btn btn-primary btn-mini" href="../QuotedPrice/QuotedCollectionsPlanCreate.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=2">查看</a>
                                    <a class="btn btn-primary btn-mini" href="../QuotedPrice/QuotedCollectionsPlanCreate.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&Type=Loss&NeedPopu=1">退款</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style="text-align: right;">本页合计：</td>

                        <td>
                            <asp:Label ID="lblSumQuotedFinishfoPage" runat="server" Text="" /></td>
                        <td style="text-align: right;">本页合计：</td>

                        <td>
                            <asp:Label ID="lblSumRealityAmountPage" runat="server" Text=""></asp:Label></td>
                        <td style="text-align: right;">本页合计：</td>

                        <td>
                            <asp:Label ID="lblSumOverFinishPage" runat="server" Text=""></asp:Label></td>
                        <td></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style="text-align: right;">本期合计：</td>

                        <td>
                            <asp:Label ID="lblSumQuotedFinishfoAll" runat="server" Text=""></asp:Label></td>
                        <td style="text-align: right;">本期合计：</td>

                        <td>
                            <asp:Label ID="lblSumRealityAmountAll" runat="server" Text=""></asp:Label></td>
                        <td style="text-align: right;">本期合计：</td>

                        <td>
                            <asp:Label ID="lblSumOverFinishAll" runat="server" Text=""></asp:Label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>

            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

        </div>
    </div>

</asp:Content>
