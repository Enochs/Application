<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_TelemarketingAssignDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing.FD_TelemarketingAssignDetails" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html").css({ "overflow-x": "hidden", "background-color": "transparent" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>渠道类型：<asp:DropDownList ID="ddlChanneType" runat="server" OnSelectedIndexChanged="ddlChanneType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                    <td>渠道名称：<cc2:ddlChannelName ID="ddlChanneName" runat="server"></cc2:ddlChannelName></td>
                    <td>新人姓名：<asp:TextBox Width="85" ID="txtBride" runat="server" /></td>
                    <td><HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" /></td>
                    <td colspan="4"><asp:Button ID="btnQuery" runat="server" CssClass="btn btn-primary" Text="查找" OnClick="btnQuery_Click" /></td>
                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>渠道类型</th>
                        <th>渠道名称</th>
                        <th>推荐人</th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>录入日期</th>
                        <th>录入人</th>
                        <th>新人状态</th>
                        <th>邀约负责人</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTelemarketingManager" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#GetChannelTypeName(Eval("ChannelType")) %></td>
                                <td><%#GetChannelHref(Eval("Channel")) %></td>
                                <td><%#Eval("Referee") %></td>
                                <td><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#ShowShortDate(Eval("RecorderDate")) %></td>
                                <td><%#GetEmployeeName(Eval("Recorder")) %></td>
                                <td><%#GetCustomerStateNameByValue(Eval("State")) %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeID ")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr><td colspan="11"><cc1:AspNetPagerTool ID="TelemarketingPager" AlwaysShow="true" OnPageChanged="DataBinder" runat="server"></cc1:AspNetPagerTool></td></tr>
                </tfoot>
            </table>
            <HA:MessageBoard runat="server" ClassType="FD_TelemarketingAssignDetails" ID="MessageBoard" />
        </div>
    </div>
</asp:Content>
