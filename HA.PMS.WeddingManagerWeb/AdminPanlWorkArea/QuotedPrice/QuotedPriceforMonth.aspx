<%@ Page Language="C#" Title="新签订单明细" AutoEventWireup="true" CodeBehind="QuotedPriceforMonth.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceforMonth" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".download").each(function () { showPopuWindows($(this).attr("href"), 450, 450, $(this)); });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div class="widget-box">
        <div class="widget-content" style="overflow-y: auto">
            <table>
                <tr>
                    <td>部门</td>
                    <td>
                        <cc2:DepartmentDropdownList ID="ddlDepartment" runat="server"></cc2:DepartmentDropdownList>
                    </td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td>
                        <HA:DateRanger runat="server" Title="婚期：" ID="PartyDateRanger" />
                    </td>
                    <td>
                        <HA:DateRanger runat="server" Title="签单日期：" ID="CreateDateRanger" />
                    </td>
                    <td>
                        <cc2:btnManager ID="btnSerch" runat="server" OnClick="btnSerch_Click" />
                    </td>
                </tr>

            </table>
            <table class="table table-bordered table-striped" id="tblMain">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>签单日期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>新人状态</th>
                        <th>已收款</th>
                        <th>订单金额</th>
                        <th id="thOper">操作</th>
                    </tr>

                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("OrderID") %><%#Eval("QuotedID") %><%#Eval("CustomerID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td style="word-break: break-all;"><%#ShowShortDate(Eval("PartyDate")) %></td>
                                <td style="word-break: break-all;"><%#GetShortDateString(Eval("OrderCreateDate")) %></td>
                                <td style="word-break: break-all;"><%#Eval("Wineshop") %></td>
                                <td style="word-break: break-all;"><%#GetOrderEmployee(Eval("CustomerID")) %></td>
                                <td style="word-break: break-all;"><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td style="word-break: break-all;"><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td style="word-break: break-all;"><%#GetMoneyByOrderID(Eval("OrderID")) %></td>
                                <td style="word-break: break-all;"><%#Eval("RealAmount") %></td>
                                <td id="tbOper">
                                    <a class="btn btn-primary btn-mini" href="QuotedPriceShow.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" <%#ShowUpdate(Eval("IsFirstCreate"))%> target="_blank">查看报价单</a>
                                    <a class="btn btn-primary btn-mini" href="QuotedPriceListCreateEdit.aspx?OrderID=<%#Eval("OrderID") %>&IsFinish=1&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" <%#HideChecks(Eval("IsChecks"))%> target="_blank">确定订单</a>
                                    <asp:HiddenField ID="hideEmpLoyeeID" Value="1" runat="server" />
                                    <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>

                    </tr>
                </tfoot>
            </table>
            <div id="tongji" style="">
                <table class="table table-bordered table-striped" style="width: 40%">
                    <thead>
                        <tr id="trSum">
                            <th width="56">统计</th>
                            <th width="90">今日合计</th>
                            <th width="90">本月合计</th>
                            <th width="90">本年合计</th>
                        </tr>

                    </thead>
                    <tbody>
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
                    </tbody>
                </table>

                <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

            </div>
        </div>
    </div>
</asp:Content>


