<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedCollectionList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedCollectionList" Title="个人收款计划" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <style type="text/css">
        .tableTotals tr th {
            border: 1px solid black;
            border-top: none;
            border-right: none;
        }

        .tableTotals tr td {
            border: 1px solid black;
            border-bottom: none;
            border-top: none;
            border-right: none;
            text-align: center;
        }
    </style>
    <script src="/Scripts/trselection.js"></script>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table>
                <tr>
                    <td>新人姓名</td>
                    <td>
                        <asp:TextBox ID="txtBridename" runat="server"></asp:TextBox>
                    </td>
                    <td>联系电话</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtContactPhone" /></td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td>时间：<asp:DropDownList Width="85" ID="ddltimerType" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">婚期</asp:ListItem>
                        <%--                        <asp:ListItem Value="2">到店时间</asp:ListItem>--%>
                        <asp:ListItem Value="3">收款时间</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                    <td>
                        <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSerch_Click" />
                    </td>
                    <td>
                        <cc2:btnReload ID="btnReload1" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>婚礼策划</th>
                        <th>收款项目</th>
                        <th>收款时间</th>
                        <th>收款金额</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#Eval("ContactMan").ToString().Length > 6 ? Eval("ContactMan").ToString().Substring(0,6) : Eval("ContactMan") %>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' /></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#Eval("Partydate", "{0:yyyy/MM/dd}")%></td>
                                <td><%#GetEmployeeName(Eval("CreateEmpLoyee")) %></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNode" Text='<%#Eval("Node") %>' Title='<%#Eval("Node") %>' /></td>
                                <td><%#GetShortDateString(Eval("CollectionTime"))%></td>
                                <td><%#Eval("RealityAmount") %></td>
                                <td><a class="btn btn-primary btn-mini"
                                    href="QuotedCollectionsPlanCreate.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=2">查看<a /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>

                        <td></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>本页合计：<asp:Label ID="lblSumneedMoneyfopage" runat="server" Text=""></asp:Label></td>
                        <td></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td></td>

                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>当期合计：<asp:Label ID="lblSumMoneyall" runat="server" Text=""></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <!--统计-->
            <table class="table table-bordered table-striped" style="width: 40%;">
                <tr id="trSum">
                    <th width="56">统计</th>
                    <th width="90">今日合计</th>
                    <th width="90">本月合计</th>
                    <th width="90">本年合计</th>
                </tr>
                <tr>
                    <td>现金流
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblMoneySumToday" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblMoneySumToMonth" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblMoneySumToYear" /></td>
                </tr>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>

    </div>
</asp:Content>
