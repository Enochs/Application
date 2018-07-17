<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseMarkProductsList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseMarkProductsList" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("html").css({ "overflow-x": "hidden", "background-color": "transparent" }); });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: 30px; border: 0px;">
                <table>
                    <tr>
                        <td>新人姓名：<asp:TextBox runat="server" Width="85" ID="txtBride"/></td>
                        <td><HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" /></td>
                        <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查找" /></td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>订单编号</th>
                        <th>新人姓名</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>策划师</th>
                        <th>派工人</th>
                        <th>产品明细</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptStorehouse" runat="server">
                        <ItemTemplate>
                            <tr skey='FD_StorehouseCustomerID<%#Eval("CustomerID") %>'>
                                <td><%#GetOrderCodeByCustomerID(Eval("CustomerID"))  %></td>
                                <td><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetPlannerNameByCustomersId(Eval("CustomerID")) %></td>
                                <td><%#GetDispatchingEmpLoyee(Eval("CustomerID"))  %></td>
                                <td><a target="_blank" class="btn btn-info btn-mini" href='/AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StorehouseMarkProductsDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&NeedPopu=1'>查看</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="StorePager" PageSize="10" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
