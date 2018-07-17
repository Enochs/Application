<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_DegreeOfSatisfactionList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_DegreeOfSatisfactionList" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".view").each(function () { showPopuWindows($(this).attr("href"), 500, 600, $(this)); });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" id="queryBox" style="height: 40px; border: 0px;">
                <table class="queryTable" style="border-bottom: 1px solid #c7d5de;">
                    <tr>
                        <td><ha:cstmnameselector runat="server" ID="CstmNameSelector"/></td>
                        <td><HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" /></td>
                        <td>酒店：<cc2:ddlhotel ID="ddlHotel" runat="server"></cc2:ddlhotel></td>
                        <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" /></td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr skey='<%#Eval("DofKey") %>'>
                        <th>姓名</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>到店时间</th>
                        <th>预定时间</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>调查结果</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDegree" runat="server">
                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField ID="hfCustomerId" Value='<%#Eval("CustomerID") %>' runat="server" />
                                <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>" target="_blank"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td style="word-break: break-all;"><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#ShowShortDate(GetComeDate(Eval("CustomerID"))) %></td>
                                <td><%#ShowShortDate(GetOrderDate(Eval("CustomerID"))) %></td>
                                <td><%#GetOrderEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                                <td><%#GetQuotedEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                                <td><%#Eval("SumDof") %></td>
                                <td><a class="btn btn-mini btn-info view" href="CS_DegreeOfSatisfactionShow.aspx?DofKey=<%#Eval("DofKey") %>">查看满意度调查结果</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="DegreePager" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
