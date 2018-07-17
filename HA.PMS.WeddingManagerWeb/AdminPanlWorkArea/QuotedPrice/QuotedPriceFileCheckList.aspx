<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceFileCheckList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceFileCheckList" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>



<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Upload").each(function () {
                showPopuWindows($(this).attr("href"), 800, 600, $(this));
            });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>渠道名称</th>
                        <th>渠道类型</th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th style="display: none;">录入日期</th>
                        <th style="display: none;">录入人</th>
                        <th>新人状态</th>
                        <th>邀约负责人</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("QuotedID") %>'>
                                <td style="height: 16px;"><%#Eval("Channel") %></td>
                                <td><%#GetChannelTypeName(Eval("ChannelType")) %></td>
                                <td>
                                    <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>">
                                        <%#Eval("Bride") %></a>
                                </td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#GetShortDateString(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td style="display: none;"><%#GetShortDateString(Eval("CreateDate")) %></td>
                                <td style="display: none;"><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetInviteEmployee(Eval("CustomerID")) %></td>
                                <td>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Checks" CommandArgument='<%#Eval("QuotedID") %>'>审核通过</asp:LinkButton>
                                    <a href="QuotedPricefileDownload.aspx?QuotedID=<%#Eval("QuotedID") %>">下载提案</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="11">

                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>

                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
