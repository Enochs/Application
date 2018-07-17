<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchingDegreeOfSatisfaction.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.DispatchingDegreeOfSatisfaction" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="CS_DegreeOfSatisfactionCreate.aspx" class="btn btn-primary  btn-mini" id="createDegrees">创建客户满意度</a>

    <div class="widget-box">

        <div class="widget-content">
            <div class="widget-box">
                <div class="widget-content">

                </div>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>到店时间</th>
                        <th>预定时间</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>取件时间</th>
                        <th>调查状态</th>
                        <th>调查时间</th>
                        <th>调查结果</th>
                        <th>总体满意度</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDegree" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><a href="/AdminPanlWorkArea/StoreSales/StoreSalesList.aspx" target="_blank">
                                    <%#Eval("Groom") %>
                                </a></td>
                                <td><%#Eval("GroomCellPhone") %></td>
                                <td><%#Eval("PartyDate") %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#Eval("ConvenientIinvitationTime") %></td>
                                <td><%#Eval("LastFollowDate") %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetPlannerName(Eval("Planner")) %></td>
                                <td><%#Eval("realityTime") %></td>
                                <td><%#Eval("StateContent") %></td>
                                <td><%#Eval("DofDate") %></td>
                                <td><%#Eval("DegreeResult") %></td>
                                <td><a href="DispatchingDegreeOfSatisfactionShow.aspx">总体满意度</a> </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="DegreePager" AlwaysShow="true"  runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
