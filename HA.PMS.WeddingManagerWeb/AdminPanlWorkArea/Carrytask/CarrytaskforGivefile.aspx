<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskforGivefile.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskforGivefile" Title="代订单" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
    <table class="table table-bordered table-striped">
        <thead>
            <tr>
                <th>新人姓名</th>
                <th>电话</th>
                <th>婚期</th>
                <th>酒店</th>
                <th>婚礼顾问</th>
                <th>策划师</th>
                <th>计划取件时间</th>
                <th>收件时间</th>
                <th>通知新人时间</th>
                <th>实际取件时间</th>

            </tr>
        </thead>

        <tbody>
            <asp:Repeater ID="rptTalkeDisk" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>" target="_blank">
                            <%#Eval("Groom") %>
                        </a></td>
                        <td><%#Eval("GroomCellPhone") %></td>
                        <td><%#Eval("PartyDate") %></td>
                        <td><%#Eval("Wineshop") %></td>
                        <td><%#Eval("Wineshop") %></td>
                        <td><%#GetPlannerName(Eval("Planner")) %></td>
                        <td><%#Eval("TakePlanTime") %></td>
                        <td><%#Eval("ConsigneeTime") %></td>
                        <td><%#Eval("NoticeTime") %></td>
                        <td>
                            <%#Eval("realityTime") %>
                        </td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="10">
                    <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"   OnPageChanged="CtrPageIndex_PageChanged">
                    </cc1:AspNetPagerTool>
                </td>
            </tr>

        </tbody>
    </table>
    <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
</asp:Content>

