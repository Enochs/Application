<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_CustomerReturnVisitList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit.FL_CustomerReturnVisitList" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".fancya").each(function () {
                $(this).attr("href", "#" + $(this).parent("td").children("div").attr("id"));
            });
        });
        function ShowRecordWindow(ctrl) {
            $("#" + $(ctrl).attr("id")).fancybox({ width: 200, height: 200, topRatio: 0 });
        }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" id="queryBox" style="height: 40px; border: 0px;">
                <table class="queryTable" style="border-bottom: 1px solid #c7d5de;">
                    <tr>
                        <td><HA:CstmNameSelector runat="server" ID="CstmNameSelector"/></td>
                        <td><HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" /></td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" /></td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>联系方式</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>到店时间</th>
                        <th>门店</th>
                        <th>销售跟单人</th>
                        <th>预定时间</th>
                        <th>策划师</th>
                        <th>回访时间</th>
       
                        <th>回访记录</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptReturn" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>" target="_blank"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetToStrose(Eval("CustomerID")) %></td>
                                <td><%#GetStoreHouseByCustomerId(Eval("CustomerID")) %></td>
                                <td><%#GetSaleEmployeeByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetPlanComeDateByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetPlannerNameByCustomersId(Eval("CustomerID")) %></td>
                                <td><%#ShowShortDate(Eval("ReasonsDate")) %></td>
                        
                                <td>    <a href="FL_ReturnVisitMessageShow.aspx?CustomerID=<%#Eval("CustomerID") %>">查看回访信息</a>   
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            
            <cc1:AspNetPagerTool ID="ReturnPager" PageSize="10" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoard runat="server" ClassType="FL_CustomerReturnVisitManager" ID="MessageBoard" />
        </div>
    </div>
</asp:Content>
