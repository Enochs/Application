<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InviteList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer.InviteList" Title="所有邀约明细" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="../../Control/SetIntiveSerch.ascx" TagName="SetIntiveSerch" TagPrefix="uc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/SetIntiveSerch.ascx" TagPrefix="HA" TagName="SetIntiveSerch" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CountMananger/InviteListCount.ascx" TagPrefix="HA" TagName="InviteListCount" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function ShowInviteCommunicationContent(CustomerID, Control) {
            var Url = "/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + CustomerID;
            showPopuWindows(Url, 800, 600, Control);
        }
        $(function () { $("th").css("white-space", "nowrap"); });
    </script>
    <script src="/Scripts/highcharts.js"></script>
    <script src="/Scripts/exporting.js"></script>
    <style type="text/css">
        .auto-style1 {
            color: #FF3300;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>渠道</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc2:ddlChannelType ID="ddlChanneltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChanneltype_SelectedIndexChanged"></cc2:ddlChannelType><cc2:ddlChannelName ID="ddlChannelname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelname_SelectedIndexChanged"></cc2:ddlChannelName><cc2:ddlReferee ID="ddlreferrr" runat="server"></cc2:ddlReferee>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>新人状态<cc2:ddlCustomersState runat="server" ID="ddlCustomersState1"></cc2:ddlCustomersState></td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" />
                    </td>

                    <td>时间:&nbsp;<asp:DropDownList ID="ddlDate" runat="server">
                        <asp:ListItem Text="请选择" Value="0"></asp:ListItem>
                        <asp:ListItem Text="接收时间" Value="CreateDate"></asp:ListItem>
                        <asp:ListItem Text="邀约成功时间" Value="Todate"></asp:ListItem>
                        <asp:ListItem Text="婚期" Value="PartyDate"></asp:ListItem>
                        <asp:ListItem Text="录入时间" Value="RecorderDate"></asp:ListItem>
                        <asp:ListItem Text="到店时间" Value="ComeDate"></asp:ListItem>
                    </asp:DropDownList>
                    </td>

                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">新人姓名
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                        联系电话
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSerch_Click" />
                        <cc2:btnReload ID="btnReload2" runat="server" />
                    </td>

                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>渠道名称</th>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>新人状态</th>
                        <th>邀约人</th>
                        <th>录入人</th>
                        <th>录入时间</th>
                        <th width="40px">邀约次数</th>
                        <th>邀约时间</th>
                        <th>上次沟通时间</th>
                        <th>下次沟通时间</th>
                        <th>订单总额</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repTelemarketingManager" runat="server" OnItemDataBound="repTelemarketingManager_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><span style="cursor: default" title="<%#Eval("Channel") %>"><%#ToInLine(Eval("Channel"),8) %></span></td>

                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("WineShop") %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetEmpLoyeeNameByID(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetEmployeeName(Eval("Recorder")) %></td>
                                <td><%#Eval("RecorderDate","{0:yyyy-MM-dd}") %></td>
                                <td><%#Eval("FllowCount") %></td>
                                <td><%#GetShortDateString(Eval("ComeDate")) %></td>
                                <td><%#Eval("LastFollowDate","{0:yyyy-MM-dd}") %></td>
                                <td><%#GetNextDate(Eval("CustomerID")) %></td>
                                <td><%#GetAggregateAmount(Eval("CustomerID")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" OnPageChanged="CtrPageIndex_PageChanged" runat="server" PageSize="20"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
                <tr>
                    <td colspan="10">
                        <span class="auto-style1"><strong>客户量:</strong></span>

                        <asp:Label ID="lblCustomerCount" runat="server" Text="" Style="font-weight: 700; color: #FF3300"></asp:Label>


                    </td>
                </tr>
            </table>
            <div style="overflow-y: auto; height: 1650px;">
                <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
            </div>
        </div>

    </div>


</asp:Content>
