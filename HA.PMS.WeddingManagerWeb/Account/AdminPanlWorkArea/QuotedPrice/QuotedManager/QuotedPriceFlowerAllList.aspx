<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceFlowerAllList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager.QuotedPriceFlowerAllList" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div class="widget-box">
        <div class="widget-content">
            <table style="display:none">
                <tr>
                    <td><HA:MyManager runat="server" ID="MyManager" /></td>
                    <td><HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" /></td>
                    <td><HA:DateRanger Title="签单日期：" runat="server" ID="CreateDateRanger" /></td>
                    <td><cc2:btnManager ID="BtnQuery" Text="查询" Visible="true" runat="server" OnClick="BtnQuery_Click" /></td>
                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th colspan="6">额外单独售价花艺及定制婚礼花艺</th>
                    </tr>
                    <tr>
                        <th colspan="2">婚礼场次明细</th>
                        <th rowspan="2">花艺对客总售价</th>
                        <th rowspan="2">实际总成本</th>
                        <th rowspan="2">成本率</th>
                        <th rowspan="2">备注</th>
                    </tr>
                    <tr>
                        <th>婚期</th>
                        <th>客人姓名</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></td>
                                <td><%#GetTotalUnitPrice(Eval("QuotedID")) %></td>
                                <td><%#GetTotalPurchasePrice(Eval("QuotedID")) %></td>
                                <td><%#GetPriceRate(GetTotalUnitPrice(Eval("QuotedID")),GetTotalPurchasePrice(Eval("QuotedID"))) %></td>
                                <td style="width:400px"></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="6">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>