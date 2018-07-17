<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Otherall.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Other.Otherall"  MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function AddContent(CustomerID) {
            var Url = "/AdminPanlWorkArea/CS/Member/Anniversary/AnniversaryCreate.aspx?CustomerID=" + CustomerID + "&Type=3";
            showPopuWindows(Url, 765, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;"></a>
        <div class="widget-box">
            <div class="widget-box" style="height: 60px; border: 0px;">
                <table>
                    <tr>
                        <td>新人姓名：<asp:TextBox ID="txtBride" runat="server" MaxLength="10"></asp:TextBox></td>
                        <td>联系电话：<asp:TextBox ID="txtBrideCellPhone" runat="server" MaxLength="20"></asp:TextBox></td>
                        <td>时间：<asp:DropDownList ID="ddlDateType" Width="80" runat="server">
                            <asp:ListItem Text="婚期" Value="1" />
                            <asp:ListItem Text="生日" Value="2" />
                        </asp:DropDownList>
                        </td>
                        <td><ha:dateranger runat="server" ID="QueryDateRanger" /></td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td><asp:Button ID="Button1" Height="27" OnClick="BinderData" CssClass="btn btn-primary" runat="server" Text="查询" /></td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>生日</th>
                        <th>服务方式</th>
                        <th>服务完成时间</th>
                        <th>补充记录</th>
                        <th>服务人员</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("CustomerID") %>'>
                                <td><a target="_blank" href="..\CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID")%>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowShortDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#GetQuotedEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#ShowShortDate(Eval("BrideBirthday")) %></td>
                                <td><%#GetMember(Eval("CustomerID")).ServiceType %></td>
                                <td><%#ShowShortDate(GetMember(Eval("CustomerID")).CreateDate) %></td>
                                <td><%#GetMember(Eval("CustomerID")).ServiceContent %></td>
                                <td><%#GetEmployeeName(GetMember(Eval("CustomerID")).CreateEmployee) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="CtrPageIndex" PageSize="10" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
</asp:Content>
