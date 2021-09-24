<%@ Page Title="档期预定" StylesheetTheme="Default" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="QuotedPriceScheduleManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceScheduleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
    <%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/CellphoneSelector.ascx" TagPrefix="HA" TagName="CellphoneSelector" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div ui-menu-divider">
        <div class="div_SearchCon" style="height: auto">
            <table>
                <tr>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title="策划师" />
                    </td>
                    <td>
                        <HA:CstmNameSelector runat="server" ID="CstmNameSelector" Title="新人姓名:" />
                    </td>
                    <td>酒店
                        <cc2:ddlHotel ID="ddlHotel1" runat="server">
                        </cc2:ddlHotel>
                    </td>
                    <td>
                        <HA:CellphoneSelector runat="server" ID="CellphoneSelector" />
                    </td>
                </tr>
                <tr>
                    <td>预定情况
                        <asp:DropDownList runat="server" ID="ddlPreSchedule">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="已预订" Value="1" />
                            <asp:ListItem Text="未预定" Value="2" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" Title="婚期" />
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnLookFor" Text="查询" CssClass="btn btn-primary" OnClick="btnLookFor_Click" />
                        <cc2:btnReload runat="server" ID="bntReload" />
                    </td>
                </tr>
            </table>
        </div>
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th width="120">新人姓名</th>
                    <th>联系电话</th>
                    <th>婚期</th>
                    <th>酒店</th>
                    <th>状态</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="rptSchedule" OnItemDataBound="rptSchedule_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan").ToString().Length > 6 ? Eval("ContactMan").ToString().Substring(0,5) : Eval("ContactMan").ToString() %></a></td>
                            <td><%#Eval("ContactPhone") %></td>
                            <td><%#Eval("PartyDate","{0:yyyy-MM-dd}") %></td>
                            <td><%#Eval("WineShop") %></td>
                            <td><%#GetCustomerStateStr(Eval("State")) %></td>
<%--                            <td><%#GetGuardianName(Eval("CustomerID")) %></td>--%>
                            <td>
                                <a target="_blank" href='CreateSchedule.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&Type=Edit' class="btn btn-primary btn-mini">预定</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="9">
                        <cc1:AspNetPagerTool runat="server" ID="CtrPageIndex" PageSize="10" OnPageChanged="CtrPageIndex_PageChanged" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>
