<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="DesignclassforWarehouse.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass.DesignclassforWarehouse" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="text-align: center; width: 100%;" border="1" class="table table-bordered table-striped">
        <thead>
            <tr id="trContent">
                <th>新人姓名</th>
                <th>联系电话</th>
                <th>婚期</th>
                <th>酒店</th>
                <th>婚礼顾问</th>
                <th>婚礼策划</th>
                <th>定稿时间</th>
                <th>设计师</th>
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
                        <td><%#Eval("Wineshop") %></td>
                        <td><%#GetOrderEmployee(Eval("CustomerID")) %></td>
                        <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                        <td><%#string.Format("{0:yyyy-MM-dd}",Eval("AutualFinishDate")) %></td>
                        <td>
                            <%#GetEmployeeName(Eval("DesignerEmployee")) %>
                        </td>
                        <td style="white-space: nowrap;">
                            <a class="btn btn-primary btn-mini" href="/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignclassReports.aspx?DispatchingID=&CustomerID=<%#Eval("CustomerID") %>&OrderID=&NeedPopu=1">查看</a>
                        </td>
                    </tr>

                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="12">
                    <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                </td>
            </tr>
        </tfoot>
    </table>

</asp:Content>
