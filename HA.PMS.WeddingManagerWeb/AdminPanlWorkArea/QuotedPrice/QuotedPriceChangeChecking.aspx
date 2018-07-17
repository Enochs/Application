<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceChangeChecking.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceChangeChecking" Title="我的正在审核的变更单" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforEmpLoyee.ascx" TagPrefix="HA" TagName="MessageBoardforEmpLoyee" %>



<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <table class="table table-bordered table-striped">
        <tr>
            <td>渠道名称</td>
            <td>渠道类型</td>
            <td>新人姓名</td>
            <td>联系电话</td>
            <td>婚期</td>
            <td>酒店</td>
            <td>录入日期</td>
            <td>录入人</td>
            <td>新人状态</td>
            <td>邀约负责人</td>
 
        </tr>
        <asp:Repeater ID="repCustomer" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("Channel") %></td>
                    <td><%#Eval("ChannelType") %></td>
                    <td><%#Eval("Groom") %></td>
                    <td><%#Eval("GroomCellPhone") %></td>
                    <td><%#GetShortDateString(Eval("PartyDate")) %></td>
                    <td><%#Eval("Address") %></td>
                    <td><%#Eval("Wineshop") %></td>
                    <td><%#GetEmployeeName(Eval("Recorder")) %></td>
                    <td><%#Eval("State") %></td>
                    <td><%#Eval("Operator") %></td>
           
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="11">
                <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"  OnPageChanged="CtrPageIndex_PageChanged">
                </cc1:AspNetPagerTool>
            </td>

        </tr>
    </table>
    <HA:MessageBoardforEmpLoyee runat="server" ID="MessageBoardforEmpLoyee" />
</asp:Content>
