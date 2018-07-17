<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CustomerCredit.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Credit.CustomerCredit" %>


<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="margin-top: 10px; margin-left: 15px">
        <tr>
            <td>
                <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
            </td>
            <td>电话：<asp:TextBox ID="txtBrideCellPhone" runat="server" MaxLength="20"></asp:TextBox></td>
            <td>身份证号:<asp:TextBox ID="txtCustomerKey" runat="server" MaxLength="20"></asp:TextBox></td>
            <td>
                <HA:DateRanger Title="婚期：" runat="server" ID="DateRanger" />
            </td>
            <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
            <td>
                <asp:Button ID="BtnQuery" OnClick="BinderData" CssClass="btn btn-primary" runat="server" Text="查询" />
                <cc2:btnReload runat="server" ID="btnReload" />
            </td>
        </tr>
    </table>
    <table class="table table-bordered table-striped">
        <thead>
            <tr id="trContent">
                <th style="white-space: nowrap;">新人姓名</th>
                <th>联系电话</th>
                <th>身份证号</th>
                <th>婚期</th>
                <th>酒店</th>
                <th>新人状态</th>

                <th>婚礼顾问</th>
                <th>VIP卡号</th>



                <th>领卡时间</th>
                <th>积分</th>

                <th>操作</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repCustomer" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><a target="_blank" href='../../StoreSales/FollowOrderDetails.aspx?Sucess=1&CustomerID=<%#Eval("CustomerID") %>&amp;OnlyView=1'><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                        <td><%#Eval("BrideCellPhone") %></td>
                        <td><%#Eval("GroomIdCard") == null || Eval("GroomIdCard").ToString() == "" ? Eval("BrideIdCard") : Eval("GroomIdCard") %></td>
                        <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                        <td><%#Eval("Wineshop") %></td>
                        <td><%#GetCustomerStateStr(Eval("State")) %></td>

                        <td><%#GetEmployeeName(Eval("OrderEmployee")) %></td>
                        <td><%#Eval("CardID") %></td>



                        <th><%#GetShortDateString(Eval("GetCardDate")) %></th>
                        <td><%#GetPointtoCustomerID(Eval("CustomerID")) %></td>
                        <td>
                            <a href="CreditAddDecrease.aspx?CustomerID=<%#Eval("CustomerID") %>&Type=1" class="btn btn-primary">增加积分</a>
                            <a href="CreditAddDecrease.aspx?CustomerID=<%#Eval("CustomerID") %>&Type=2" class="btn btn-primary">减少积分</a>
                            <a href="CustomerKeyID.aspx?CustomerID=<%#Eval("CustomerID") %>&Type=2" class="btn btn-primary">会员信息</a>
                            <a href="CreditLogList.aspx?CustomerID=<%#Eval("CustomerID") %>&Type=2" class="btn btn-primary">查看</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="8">
                    <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData">
                    </cc1:AspNetPagerTool>
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
