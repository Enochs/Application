<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="QuotedPriceLosslist.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager.QuotedPriceLosslist" %>

         
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table>
                <tr>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td>
                        <HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" />
                    </td>
                    <td>
                        <HA:DateRanger Title="签单日期：" runat="server" ID="CreateDateRanger" />
                    </td>
                    <td>
                        <cc2:btnManager ID="BtnQuery" Text="查询" Visible="true" runat="server" OnClick="BtnQuery_Click" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>签单日期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>新人状态</th>
                        <th>已收款</th>
                        <th>定金</th>
                        <th>订单金额</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetQuotedDispatchingFinishMoney(Eval("CustomerID")) %></td>
                                <td><%#GetOrderEmoney(Eval("OrderID")) %></td>
                                <td><%#Eval("FinishAmount") %></td>
                                <td id="tbOper">
                                   
                                    <a class="btn btn-primary btn-mini" href="../QuotedPriceShow.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&Delete=1" >查看报价单</a>
 
                                    <asp:HiddenField ID="hideEmpLoyeeID" Value="1" runat="server" />
                                    <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                    <a target="_blank" class="btn btn-primary btn-mini <%#SetClass(Eval("QuotedID")) %>" href="/AdminPanlWorkArea/Carrytask/ProductList.aspx?DispatchingID=<%#GetDispatchingIDByQuotedID(Eval("QuotedID").ToString())%>&CustomerID=<%#Eval("CustomerID")%>&OnlyView=1&NeedPopu=1">物料单</a>
                                    <a target="_blank" class="btn btn-primary btn-mini <%#SetClass(Eval("QuotedID")) %>" href="/AdminPanlWorkArea/Carrytask/CarrytaskProfessionalteam.aspx?DispatchingID=<%#GetDispatchingIDByQuotedID(Eval("QuotedID").ToString())%>&CustomerID=<%#Eval("CustomerID")%>&NeedPopu=1">专业团队</a>
                                    <a target="_blank" class="btn btn-primary btn-mini <%#SetClass(Eval("QuotedID")) %>" href="/AdminPanlWorkArea/Carrytask/DispatchingEmpLoyeeManager.aspx?DispatchingID=<%#GetDispatchingIDByQuotedID(Eval("QuotedID").ToString())%>&CustomerID=<%#Eval("CustomerID")%>&OnlyView=1&NeedPopu=1">执行团队</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
 
        </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
</asp:Content>

