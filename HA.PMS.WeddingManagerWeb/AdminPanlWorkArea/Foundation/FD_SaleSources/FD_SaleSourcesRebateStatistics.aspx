﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_SaleSourcesRebateStatistics.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SaleSourcesRebateStatistics" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 25px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }
        /*小屏幕分辨率*/
        .centerSmallTxt {
            width: 55px;
            height: 20px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {

                if (indexs != 5) {
                    $(this).css({
                        "border": "0", "vertical-align": "middle"

                    }).after("&nbsp;&nbsp;");
                }

                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            //
            $("html").css({ "overflow-x": "hidden" });
            $("html,body").css({ "background-color": "transparent" });

            if (window.screen.width >= 1280 && window.screen.width <= 1366) {

                $(":text").each(function (indexs, values) {
                    $(this).addClass("centerSmallTxt");
                });
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="overflow-x: auto;">

        <div class="widget-box" style="height: 900px; border: 0px;">


            <table class="queryTable">
                <tr>
                    <td>渠道类型:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc2:ddlChannelType ID="ddlChannelType" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelType_SelectedIndexChanged" runat="server">
                                </cc2:ddlChannelType>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>渠道名称：</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <cc2:ddlChannelName ID="ddlChanne" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChanne_SelectedIndexChanged"></cc2:ddlChannelName>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>收款人:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <cc2:ddlReferee ID="ddlReferee" runat="server"></cc2:ddlReferee>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>

                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <HA:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td>时间:<asp:DropDownList ID="ddlTimerType" runat="server" Width="75">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">婚期</asp:ListItem>
                        <asp:ListItem Value="1">支付时间</asp:ListItem>
                    </asp:DropDownList></td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnQuery" Height="27" runat="server" CssClass="btn btn-primary" Text="查找" OnClick="btnQuery_Click" />
                        <cc2:btnReload runat="server" ID="btnReload" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>

            <table class="table table-bordered table-striped">
                <thead>
                    <tr>

                        <th style="white-space: nowrap;">渠道名称</th>
                        <th style="white-space: nowrap;">推荐人</th>
                        <th style="white-space: nowrap;">新人姓名</th>
                        <th style="white-space: nowrap;">婚期</th>
                        <th style="white-space: nowrap;">新人状态</th>
                        <th style="white-space: nowrap; width: 100px;">支付规则</th>
                        <th style="white-space: nowrap;">订单金额</th>
                        <th style="white-space: nowrap;">收款人</th>
                        <th style="white-space: nowrap;">收款人电话</th>
                        <th style="white-space: nowrap;">支付金额</th>
                        <th style="white-space: nowrap;">支付日期</th>
                        <th style="white-space: nowrap;">经办人</th>

                    </tr>
                </thead>
                <tbody>

                    <asp:Repeater ID="rptTelemarketingManager" runat="server">
                        <ItemTemplate>
                            <td>
                                <asp:HiddenField ID="hiddKeyValue" runat="server" Value='<%#Eval("SSID") %>' />
                                <asp:HiddenField ID="hiddSourceKey" runat="server" Value='<%#Eval("Channel") %>' />
                                <%#Eval("Channel") %>
                            </td>
                            <td><%#Eval("MoneyPerson") %></td>
                            <td><%#GetGoomByID(Eval("CustomerID")) %></td>
                            <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                            <td><%#GetCustomerStateStr(Eval("State")) %></td>
                            <td><%#Eval("Policy") %></td>
                            <td></td>
                            <td>
                                <%#Eval("MoneyPerson") %></td>
                            <td></td>
                            <td>
                                <label class="NoEdit"><%#Eval("PayMoney") %></label></td>
                            <td>
                                <label class="NoEdit"><%#GetDateStr(Eval("PayDate")) %></label>
                            </td>
                            <td>
                                <label class="NoEdit"><%#GetEmployeeName(Eval("CreateEmployee")) %></label>
                            </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>

            </table>

            <cc1:AspNetPagerTool ID="RebateStatisticsPager" PageSize="10" AlwaysShow="true" OnPageChanged="RebateStatisticsPager_PageChanged" runat="server">
            </cc1:AspNetPagerTool>
            <table width="630" style="font-size: 12px; margin-top: 10px; display: none;">
                <tr>
                    <td width="104" style="font-weight: bold;">当前支付额:</td>
                    <td width="196">
                        <asp:Literal ID="ltlCurrentPay" runat="server"></asp:Literal>
                    </td>
                    <td width="104" style="font-weight: bold;">累计支付额:</td>
                    <td width="198">
                        <asp:Literal ID="ltlSumPay" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">当前总支付额:</td>
                    <td>
                        <asp:Literal ID="ltlCurrentSumPay" runat="server"></asp:Literal>
                    </td>
                    <td style="font-weight: bold;">总累计总支付额:</td>
                    <td>
                        <asp:Literal ID="ltlSumPayAll" runat="server"></asp:Literal>
                    </td>
                </tr>

            </table>

            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>

    </div>
</asp:Content>
