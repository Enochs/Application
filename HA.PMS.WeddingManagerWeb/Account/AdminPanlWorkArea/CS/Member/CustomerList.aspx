<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.CustomerList" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<%@ Register Src="../../Control/MyManager.ascx" TagName="MyManager" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="css/lrtk.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {

                /*JQuery 限制文本框只能输入数字和小数点*/
                $("#txtStartMoney").keyup(function () {
                    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                }).bind("paste", function () {  //CTR+V事件处理    
                    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                }).css("ime-mode", "disabled"); //CSS设置输入法不可用    
            });

            $(function () {

                /*JQuery 限制文本框只能输入数字和小数点*/
                $("#txtEndMoney").keyup(function () {
                    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                }).bind("paste", function () {  //CTR+V事件处理    
                    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                }).css("ime-mode", "disabled"); //CSS设置输入法不可用    
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box" style="padding: 2px 16px">
            <table>
                <tr>
                    <td>新人姓名：<asp:TextBox ID="txtGroom" runat="server" MaxLength="10"></asp:TextBox></td>
                    <td>
                        <HA:DateRanger Title="新人婚期：" runat="server" ID="PartyDateRanger" />
                    </td>
                    <td>联系电话：<asp:TextBox ID="txtGroomCellPhone" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                </tr>
                <tr>
                    <td>
                        <uc1:MyManager ID="MyManager1" runat="server" Title="婚礼顾问：" />
                    </td>
                    <td>
                        <HA:DateRanger ID="DateRanger2" runat="server" Title="订单日期：" />
                    </td>
                    <td>
                        <asp:Button ID="BtnQuery" OnClick="BinderData" CssClass="btn btn-primary" runat="server" Text="查询" />
                        <cc2:btnReload ID="btnReload2" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>新人状态</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>邀约人</th>
                        <th>跟单销售人</th>
                        <th>策划师</th>
                        <th>来源渠道</th>
                        <th>消费金额</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCustomer" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><a target="_blank" href="CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID ")%>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowShortDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetInviteName(Eval("CustomerID ")) %></td>
                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#GetQuotedEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#Eval("Channel") %></td>
                                <td><%#GetRealityAmount(Eval("OrderID ")) %></td>
                                <td><a href='CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>' target="_blank" class="btn btn-info btn-mini">查看客户详细信息</a> </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <asp:Label runat="server" ID="lblCustomerCount" Style="font-size: 13px; font-weight: bold; color: #f00;" />
            <cc1:AspNetPagerTool ID="CtrPageIndex" PageSize="10" OnPageChanged="BinderData" AlwaysShow="true" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>

