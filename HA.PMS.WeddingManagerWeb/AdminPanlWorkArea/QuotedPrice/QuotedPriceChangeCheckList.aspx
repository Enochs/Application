<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceChangeCheckList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceChangeCheckList" Title="需要我审核的变更列表" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforEmpLoyee.ascx" TagPrefix="HA" TagName="MessageBoardforEmpLoyee" %>



<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>
 
    <table class="table table-bordered table-striped table-select">
        <thead>
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
            <td>制作报价单</td>
        </tr>
            </thead>
        <tbody>
        <asp:Repeater ID="repCustomer" runat="server">
            <ItemTemplate>
                <tr skey='<%#Eval("QuotedID") %>'>
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
                    <td>
                        <a href="QuotedPriceChangeChecks.aspx?QuotedID=<%#Eval("QuotedID") %>" <%#HideCreate(Eval("IsChecks"))%>>审核变更单</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
            </tbody>
        <tfoot>
        <tr>
            <td colspan="11">
                <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"  OnPageChanged="CtrPageIndex_PageChanged">
                </cc1:AspNetPagerTool>
            </td>

        </tr></tfoot>
    </table>
    <HA:MessageBoardforEmpLoyee runat="server" ID="MessageBoardforEmpLoyee" />
 </asp:Content>
