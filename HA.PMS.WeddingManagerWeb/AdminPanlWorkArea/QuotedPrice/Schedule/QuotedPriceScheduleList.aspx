<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="QuotedPriceScheduleList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.Schedule.QuotedPriceScheduleList" %>

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
                    <td>商家名称:<asp:TextBox ID="txtSupplierName" runat="server" /></td>
                    <td>时间：<asp:DropDownList Width="85" ID="ddltimerType" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="PartyDate">婚期</asp:ListItem>
                        <%--<asp:ListItem Value="QuotedCreateDate">订单时间</asp:ListItem>--%>
                        <asp:ListItem Value="ScheCreateDate">预定时间</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                    <td>
                        <HA:CstmNameSelector runat="server" ID="CstmNameSelector" Title="新人姓名:" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title="推荐人" />
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
                    <th>婚期</th>
                    <th>酒店</th>
                    <th>商家名称</th>
                    <th>预定时间</th>
                    <th>预定价格</th>
                    <th>返佣金额</th>
                    <th>收款金额</th>
                    <th>备注</th>
                    <th>推荐人</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="rptSchedule" OnItemDataBound="rptSchedule_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                            <td><%#Eval("PartyDate","{0:yyyy-MM-dd}") %></td>
                            <td><%#Eval("WineShop") %></td>
                            <td><a href="/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierUpdate.aspx?SupplierID=<%#Eval("ScheGuardianID") %>" target="_blank"><%#Eval("Name") %></a></td>
                            <td><%#Eval("ScheCreateDate") %></td>
                            <td><%#Eval("ScheGuardianPrice") %></td>
                            <td><%#Eval("SchePayMent") %></td>
                            <td><%#Eval("ScheCollectionAmount") %></td>
                            <td><%#Eval("ScheReamrk") %></td>
                            <td><%#GetEmployeeName(Eval("ScheCreateEmployee")) %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>

                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>本页合计：<asp:Label ID="lblScheGuardianPrice" runat="server" Text=""></asp:Label></td>
                    <td>本页合计:<asp:Label ID="lblSchePayMent" runat="server" Text=""></asp:Label></td>
                    <td>本页合计：<asp:Label ID="lblScheCollectionAmount" runat="server" Text=""></asp:Label></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
            <tfoot>
                <tr>


                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>当期合计：<asp:Label ID="lblScheGuardianPriceAll" runat="server" Text=""></asp:Label>
                    </td>
                    <td>当期合计：<asp:Label ID="lblSchePayMentAll" runat="server" Text=""></asp:Label></td>
                    <td>当期合计:
                            <asp:Label ID="lblScheCollectionAmountAll" runat="server" Text=""></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="9">
                        <cc1:AspNetPagerTool runat="server" ID="CtrPageIndex" PageSize="10" OnPageChanged="CtrPageIndex_PageChanged" />
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>
