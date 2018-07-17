<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedCollectionsPlan.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedCollectionsPlan" Title="个人收款计划" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table>
                <tr>
                    <td>新人姓名:
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title="策划师" />
                    </td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager1" Title="统筹师" />
                    </td>
                    <td>时间：<asp:DropDownList Width="85" ID="ddltimerType" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">婚期</asp:ListItem>
                        <asp:ListItem Value="2">到店时间</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                </tr>
                <tr>
                    <td>联系电话:
                         <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <cc2:btnManager ID="BtnQuery" Text="查询" runat="server" OnClick="BinderData" />
                        <cc2:btnReload ID="btnReload" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>统筹师</th>
                        <th>策划师</th>
                        <th>新人状态</th>
                        <th>订单金额</th>
                        <th>已收款</th>

                        <th>余款</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetEmployeeName(Eval("OrderEmployee")) %><%--<%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %>--%></td>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#Eval("FinishAmount") %></td>
                                <td><%#GetFinishMoney(Eval("CustomerID")) %></td>
                                <td><%#GetOverFinishMoney(Eval("CustomerID")) %></td>
                                <td><a target="_blank" class="btn btn-primary btn-mini"
                                    href="QuotedCollectionsPlanCreate.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=1">收款
                                </a>&nbsp;&nbsp;
                                    <a class="btn btn-danger btn-mini" href="QuotedPriceShow.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" target="_blank">查看报价单</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>本页合计：<asp:Label ID="lblSumMoneyfopage" runat="server" Text=""></asp:Label>
                        </td>
                        <td>本页合计:<asp:Label ID="lblFinishMoney" runat="server" Text=""></asp:Label></td>
                        <td>本页合计：<asp:Label ID="lblSumneedMoneyfopage" runat="server" Text=""></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td>服务对数：<asp:Label ID="lblSumCustomer" runat="server" Text=""></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>当期合计：<asp:Label ID="lblSumMoneyall" runat="server" Text=""></asp:Label>
                        </td>
                        <td>当期合计：<asp:Label ID="lblSumFinishMoneyall" runat="server" Text=""></asp:Label></td>
                        <td>当期合计:
                            <asp:Label ID="lblSumNeedall" runat="server" Text=""></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
