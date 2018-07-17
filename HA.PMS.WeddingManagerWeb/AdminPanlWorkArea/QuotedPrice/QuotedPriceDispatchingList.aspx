<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="QuotedPriceDispatchingList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceDispatchingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
    <%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
    <%@ Register Src="../Control/MyManager.ascx" TagName="MyManager" TagPrefix="uc1" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: auto; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>
                            <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                        </td>
                        <td>婚期
                        </td>
                        <td>
                            <HA:DateRanger runat="server" ID="DateRanger" />
                        </td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td>
                            <uc1:MyManager ID="MyManager1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">联系电话
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" />
                            <cc2:btnReload ID="btnReload" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼策划</th>
                        <th>婚礼顾问</th>
                        <th>已付款</th>
                        <th>订单总金额</th>
                        <th>下次沟通时间</th>
                        <th>签约时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride").ToString().Length > 6 ? Eval("Bride").ToString().Substring(0,6) : Eval("Bride"),Eval("Groom").ToString().Length > 6 ? Eval("Groom").ToString().Substring(0,6) : Eval("Groom")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>

                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetEmployeeName(Eval("OrderEmployee")) %></td>
                                <td><%#GetFinishMoney(Eval("OrderID")) %></td>
                                <td><%#Eval("FinishAmount") %></td>
                                <td><%#ShowShortDate(Eval("NextFlowDate")) %></td>
                                <td><%#ShowShortDate(Eval("QuotedDateSucessDate")) %></td>
                                <td>
                                    <a class="btn btn-primary" href="QuotedPriceDispatchingUpdate.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&Type=Dispatching&QuotedEmployee=<%#Eval("EmployeeID") %>" target="_blank">审核</a>
                                    <a class="btn btn-primary" href="QuotedPriceDispatchingUpdate.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&Type=Look&QuotedEmployee=<%#Eval("EmployeeID") %>" target="_blank">查看</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9">
                            <asp:Label runat="server" ID="lblTotalSums" Text="" Style="font-size: 14px; font-weight: bolder; color: red;" />
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
