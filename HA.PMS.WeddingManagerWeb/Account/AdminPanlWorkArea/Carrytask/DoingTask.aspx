<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoingTask.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.DoingTask" Title="本人的执行任务" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <table class="table table-bordered table-striped">
        <tr>
            <td>新人姓名</td>
            <td>婚期</td>
            <td>酒店</td>
            <td>婚礼顾问</td>
            <td>策划师</td>
            <td>订单状态</td>
            <td>派工人</td>
            <td>处理任务</td>
        </tr>
        <asp:Repeater ID="repCustomer" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="height: 16px;"><%#Eval("Groom") %></td>
                    <td><%#Eval("PartyDate") %></td>
                    <td><%#Eval("Wineshop") %></td>
                    <td><%#Eval("Wineshop") %></td>
                    <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                    <td><%#Eval("State") %></td>
                    <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                    <td>
                        <a target="_blank" href="MyCarrytaskList.aspx?DispatchingID=<%#Eval("DispatchingID") %>&NeedPopu=1&CustomerID=<%#Eval("CustomerID") %>&PageName=MyCarrytaskList">处理任务</a>
                        <asp:HiddenField ID="hideEmpLoyeeID" Value="1" runat="server" />
                        <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("DispatchingID") %>' runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="11">
                <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"   OnPageChanged="CtrPageIndex_PageChanged">
                </cc1:AspNetPagerTool>
            </td>

        </tr>
    </table>
    <HA:MessageBoard runat="server" ClassType="DoingTask" ID="MessageBoard" />
</asp:Content>

