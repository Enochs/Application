<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_TelemarketingDetailsCustomer.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing.FD_TelemarketingDetailsCustomer" %>


<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CountMananger/TelemarketingCustomersNumberIndexCount.ascx" TagPrefix="HA" TagName="TelemarketingCustomersNumberIndexCount" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html").css({ "overflow-x": "hidden", "background-color": "transparent" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="width: 99%; height: 980px; overflow-y: auto">
        <div class="widget-content">
            <table>
                <tr>
                    <td>渠道类型<cc2:ddlChannelType Width="72" ID="ddlChanneType" runat="server" OnSelectedIndexChanged="ddlChanneType_SelectedIndexChanged" AutoPostBack="true"></cc2:ddlChannelType></td>
                    <td>渠道名称<cc2:ddlChannelName Width="90" ID="DdlChannelName1" runat="server"></cc2:ddlChannelName></td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager1" Title="电销" />
                    </td>
                    <td>新人姓名<asp:TextBox ID="txtBrideName" runat="server" Width="70"></asp:TextBox>
                    </td>


                </tr>
                <tr>
                    <td>客户状态<cc2:ddlCustomersState ID="ddlState" runat="server">
                    </cc2:ddlCustomersState>
                    </td>
                    <td>邀约次数
                        <asp:TextBox runat="server" ID="txtFollowCount" /></td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title="录入人" />
                    </td>

                    <td>联系电话<asp:TextBox ID="txtCellPhone" runat="server" Width="100"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>时间<asp:DropDownList Width="85" ID="ddltimerType" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">录入时间</asp:ListItem>
                        <asp:ListItem Value="1">婚期</asp:ListItem>
                        <asp:ListItem Value="2">到店时间</asp:ListItem>
                        <asp:ListItem Value="3">计划沟通时间</asp:ListItem>
                    </asp:DropDownList></td>

                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                    <td>
                        <asp:Button ID="btnQuery" Height="27" ClientIDMode="Static" runat="server" CssClass="btn btn-primary" Text="查找" OnClick="btnQuery_Click" />
                        <cc2:btnReload ID="btnReload1" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>渠道名称</th>
                        <th>渠道类型</th>
                        <%--<th>推荐人</th>--%>
                        <th>顾问</th>
                        <th>电销</th>
                        <th>录入</th>
                        <th>录入时间</th>
                        <th>实际到店时间</th>
                        <th>计划沟通时间</th>
                        <th>新人姓名</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>已收款</th>
                        <th>状态</th>
                        <th>订单总额</th>
                        <th>返利支出</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTelemarketingManager" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#GetChannelHref(Eval("Channel")) %></td>
                                <td>
                                    <%#GetChannelTypeName(Eval("ChannelType")) %>
                                </td>
                                <%--<td><%#Eval("Referee") %></td>--%>
                                <td><%#GetEmployeeName(Eval("OrderEmployee")) %></td>
                                <td><%#GetEmployeeName(Eval("InviteEmployee")) %></td>
                                <td><%#GetEmployeeName(Eval("CreateEmpLoyee")) %></td>
                                <td><%#GetShortDateString(Eval("CreateDate")) %></td>
                                <td><%#GetShortDateString(Eval("OrderCreateDate")) %></td>
                                <td><%#GetShortDateString(Eval("AgainDate")) %></td>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan").ToString().Length > 6 ? Eval("ContactMan").ToString().Substring(0,6) : Eval("ContactMan").ToString() %></a></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td>0</td>
                                <td><%#GetCustomerStateStr(Eval("State")) %>(<%#GetFollowCount(Eval("CustomerID")) %>)</td>
                                <td><%#GetAggregateAmount(Eval("CustomerID")) %></td>
                                <td><%#GetMoney(Eval("CustomerID")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="TelemarketingPager" PageSize="20" AlwaysShow="true" OnPageChanged="TelemarketingPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            汇总统计&nbsp;<asp:Button ID="btnDownLoadReport" runat="server" CssClass="btn btn-primary btn-mini" Text="导出报表" OnClick="btnDownLoadReport_Click" />
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th width="40">客源量</th>
                        <th width="65">邀约成功量</th>
                        <th width="70">邀约成功率</th>
                        <th width="60">成交量</th>
                        <th width="40">成交率</th>
                        <th width="60">订单总额</th>

                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Literal ID="ltlCurrentCustomerCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentInviteSuccess" runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="ltlCurrentInviteSuccessRate" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentClinchaDeal" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlClinchaDealRate" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Literal ID="ltlCurrentOrderSumMoney" runat="server"></asp:Literal>
                        </td>

                    </tr>
                </tbody>
            </table>
        </div>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>

</asp:Content>

