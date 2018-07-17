<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCostEvaulationManager.aspx.cs" StylesheetTheme="Default" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost.OrderCostEvaulationManager" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="../../Control/MyManager.ascx" TagName="MyManager" TagPrefix="uc1" %>

<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowThis(CustomerID) {
            window.parent.ShowPopuWindow("/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?CustomerID=" + CustomerID + "&NeedPopu=1", 1280, 1500);
            return false;
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-x: auto;">
            <div class="widget-box" style="height: 30px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td><HA:CstmNameSelector runat="server" ID="CstmNameSelector"/></td>
                        <td><HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" /></td>
                        <td><uc1:MyManager runat="server" ID="MyManager1" Title="策划师" /></td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td><asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="DataBinder" runat="server" Text="查询" />
                            <cc2:btnReload runat="server" ID="btnReload" />
                        </td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>订单状态</th>
                        <th>派工人</th>
                        <th>责任人</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                             <tr skey='CarrytaskStateKey<%#Eval("StateKey") %>'>
                                <td><%#Eval("ParentDispatchingID").ToString()=="0"?"":"(变更)" %><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#GetEmployeeName(Eval("QuotedEmpLoyee")) %></td>
                                <td><%#GetCustomerStateStr(Eval("UseSate")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetEmployeeName(User.Identity.Name) %></td>
                                 <td>
                                     <a  href="/AdminPanlWorkArea/Carrytask/CarryCost/OrderCostEvaluation.aspx?DispatchingID=<%#Eval("DispatchingID") %>&NeedPopu=1" class="btn btn-success" target="_blank">查看</a>
                                 </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="DataBinder"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label runat="server" ID="lblPersonSum">当前查询总人数为</asp:Label></td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
</asp:Content>
