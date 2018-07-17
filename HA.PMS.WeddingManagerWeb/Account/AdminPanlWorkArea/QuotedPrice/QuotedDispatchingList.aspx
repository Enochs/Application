<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedDispatchingList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedDispatchingList" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/trselection.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table>
               <tr>
                <td><HA:MyManager runat="server" ID="MyManager" /></td>
                <td><cc2:btnManager ID="btnSerch" runat="server" OnClick="btnSerch_Click" /></td>
            </tr>
        </table>
 
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>新人状态</th>
                        <th>接收时间</th>
                        <th>已付款</th>
                        <th>订单金额</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                           <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmployee(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                                <td><%#GetQuotedDispatchingFinishMoney(Eval("CustomerID")) %></td>
                                <td><%#Eval("RealAmount") %></td>
                                <td><a target="_blank" class="btn btn-primary btn-mini btnSaveSubmit<%#Request["SaleEmployee"]!=""?Eval("SaleEmployee"):Eval("EmployeeID")%> btnSumbmit" href="DispatchingManager.aspx?QuotedID=<%#Eval("QuotedID") %>&NeedPopu=1&OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>">制作执行明细</a>
                                    <a class="btn btn-primary btn-mini" href="QuotedPriceShow.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" <%#ShowUpdate(Eval("IsFirstCreate"))%> target="_blank">查看报价单</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="11"><cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"  OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool></td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

        </div>
    </div>
</asp:Content>


