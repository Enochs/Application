<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="DesignTaskWorkManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.DesignTaskWorkManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
    <%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box" style="height: 30px; border: 0px;">
            <table class="queryTable">
                <tr>
                    <td>
                        <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                    </td>
                    <%--<td><HA:MyManager runat="server" ID="MyManager" Title="策划师：" /></td>--%>
                    <td>
                        <HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" />
                    </td>
                    <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                    <td>
                        <asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" />
                        <cc2:btnReload runat="server" ID="btnReload" />
                    </td>
                </tr>
            </table>
        </div>
        <table class="table table-bordered table-striped table-select">
            <thead>
                <tr>
                    <th>新人姓名</th>
                    <th>婚期</th>
                    <th>酒店</th>
                    <th>婚礼顾问</th>
                    <th>策划师</th>
                    <th>订单状态</th>
                    <th>派工人</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server">
                    <ItemTemplate>
                        <tr skey='CarrytaskStateKey<%#Eval("StateKey") %>'>
                            <td><%#Eval("ParentDispatchingID").ToString()=="0"?"":"(变更)" %><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                            <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <td><%#GetEmployeeName(Eval("QuotedEmployee")) %></td>
                            <td><%#GetQuotedEmpLoyeeName(Eval("OrderID")) %></td>
                            <td><%#GetCustomerStateStr(Eval("UseSate")) %></td>
                            <td><%#GetEmployeeName(Eval("DesignEmployee")) %></td>
                            <td>
                                <a target="_blank" href="DesignTaskWork.aspx?DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&NeedPopu=1" class="btn btn-success">派工</a>
                                <a href="/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?QuotedID=<%#Eval("QuotedID") %>&OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=1" class="btn btn-success" target="_blank">查看原始订单</a>

                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="9">
                        <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool>
                    </td>
                </tr>
            </tfoot>
        </table>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
