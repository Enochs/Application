<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuptedCollectionsPlanList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.QuptedCollectionsPlanList" Title="确认收款" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>
    <style type="text/css">
        .table-select tr th {
            text-align: left;
        }
    </style>

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
                        <asp:ListItem Value="3">收款时间</asp:ListItem>
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
                        <th style="width: 95px;">联系电话</th>
                        <th style="width: 95px;">婚期</th>
                        <th>电销</th>
                        <th>顾问</th>
                        <th>策划师</th>
                        <th>状态</th>
                        <th>收款项目</th>
                        <th>收款时间</th>
                        <th>收款金额</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#Eval("ContactMan").ToString().Length > 8 ? Eval("ContactMan").ToString().Substring(0,6)+"..." : Eval("ContactMan").ToString() %></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#Eval("Partydate", "{0:yyyy/MM/dd}")%></td>
                                <td><%#GetInviteName(Eval("CustomerID")) %></td>
                                <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetQuotedEmployee(Eval("CustomerID")) %></td>
                                <td><%#GetCustomerState(Eval("CustomerID")) %></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNode" Text='<%#Eval("Node") %>' Title='<%#Eval("Node") %>' Width="200px" /></td>
                                <td><%#Eval("CollectionTime", "{0:yyyy/MM/dd}") %></td>
                                <td><%#Eval("RealityAmount") %></td>
                                <td>
                                    <a class="btn btn-primary btn-mini" href="../QuotedPrice/QuotedCollectionsPlanCreate.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=2">查看</a>
                                    <a <%#GetLock(Eval("OrderID")).ToString()=="False"?"":"style='display:none;'" %> class="btn btn-primary btn-mini" href="QuptedCollectionsPlanFinish.aspx?NeedPopu=1&OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>">确认收款</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style="text-align: right;">本页合计：</td>
                        <td>
                            <asp:Label ID="lblSumRealityAmountPage" runat="server" Text=""></asp:Label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td style="text-align: right;">本期合计：</td>
                        <td>
                            <asp:Label ID="lblSumRealityAmountAll" runat="server" Text=""></asp:Label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="11">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="11">
                            <asp:Button runat="server" ID="btnExport" Text="导出Excel" CssClass="btn btn-primary" OnClick="btnExport_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>

            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <table class="table table-bordered table-striped table-select">
                        <thead>
                            <tr>
                                <th>新人</th>
                                <th style="width: 95px;">联系电话</th>
                                <th style="width: 95px;">婚期</th>
                                <th>电销</th>
                                <th>顾问</th>
                                <th>策划师</th>
                                <th>状态</th>
                                <th>收款项目</th>
                                <th>收款时间</th>
                                <th>收款金额</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repCustomer" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#GetContactMan(Eval("ContactMan")) %></td>
                                        <td><%#Eval("ContactPhone") %></td>
                                        <td><%#Eval("Partydate", "{0:yyyy/MM/dd}")%></td>
                                        <td><%#GetInviteName(Eval("CustomerID")) %></td>
                                        <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                                        <td><%#GetEmployeeName(Eval("CreateEmpLoyee")) %></td>
                                        <td><%#GetCustomerState(Eval("CustomerID")) %></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblNode" Text='<%#Eval("Node") %>' Title='<%#Eval("Node") %>' Width="200px" /></td>
                                        <td><%#Eval("CollectionTime", "{0:yyyy/MM/dd}") %></td>
                                        <td><%#Eval("RealityAmount") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />



        </div>
    </div>


    <%--<div class="widget-box" style="height: 50px; border: 0px;">


        <table class="queryTable">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td><HA:CstmNameSelector runat="server" ID="CstmNameSelector" /></td>
                            <td>&nbsp;</td>
                            <td>
                                <HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" />
                            </td>
                            <td>

                                <asp:Button ID="btnserch" runat="server" Text="查询" CssClass="btn btn-primary" Height="27" OnClick="btnserch_Click" />
                                <cc2:btnReload runat="server" ID="btnReload" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </div>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-striped table-select" id="tbMain">
                <thead>
                    <tr id="trContent">
                        <th style="white-space: nowrap;">新人姓名</th>
                        <th style="white-space: nowrap;">联系电话</th>
                        <th style="white-space: nowrap;">婚期</th>
                        <th style="white-space: nowrap;">酒店</th>
                        <th style="white-space: nowrap;">婚礼顾问</th>
                        <th style="white-space: nowrap;">婚礼策划</th>
                        <th style="white-space: nowrap;">新人状态</th>
                        <th style="white-space: nowrap;">接收时间</th>
                        <th style="white-space: nowrap;">婚礼总金额</th>
                        <th style="white-space: nowrap;">收款金额</th>
                        <th style="white-space: nowrap;">余款</th>
                        <th style="white-space: nowrap;">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='<%#Eval("CustomerID") %>'>
                                <td style="height: 16px;">
                                    <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1">
                                        <%#ShowCstmName(Eval("Bride"),Eval("Groom")) %>
                                    </a>
                                </td>
                                <td>
                                    <%#Eval("BrideCellPhone") %>
                                </td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmployee(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetShortDateString(Eval("CreateDate")) %></td>
                                <td><%#GetQuotedFinishMoney(Eval("CustomerID")) %></td>
                                <td><%#GetOverFinishMoney(Eval("CustomerID")) %></td>
                                <td style="white-space: nowrap;">

                                    <a <%#GetLock(Eval("OrderID")).ToString()=="False"?"":"style='display:none;'" %> class="btn btn-primary btn-mini" href="QuptedCollectionsPlanFinish.aspx?NeedPopu=1&OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>">确认收款</a>
                                    <a <%#GetLock(Eval("OrderID")).ToString()=="True"?"":"style='display:none;'" %> class="btn btn-danger btn-mini" href="QuptedCollectionShow.aspx?NeedPopu=1&OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>">查看收款</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>本页合计:<asp:Label runat="server" ID="lbl_PageMoney" /> </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>本期合计:<asp:Label runat="server" ID="lblSumMoneyall" /></td>
                        <td></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="12">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>

                    </tr>

                </tfoot>
            </table>


            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

        </div>
    </div>--%>
</asp:Content>
