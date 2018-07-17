<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskOfNew.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskOfNew" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowUpdateWindows(DispatchingID, CustomerID, OrderID, Control) {
            var Url = "CarrytaskGoNextPage.aspx?DispatchingID=" + DispatchingID + "&CustomerID=" + CustomerID + "&NeedPopu=1&OrderID=" + OrderID;
            $(Control).attr("id", "updateShow" + DispatchingID);
            showPopuWindows(Url, 1000, 600, "a#" + $(Control).attr("id"));
        }
        function ShowFindExcuteDetailsWindows(DispatchingID, Control) {
            var Url = "CarrytaskShow.aspx?DispatchingID=" + DispatchingID;
            $(Control).attr("id", "updateShow" + DispatchingID);
            showPopuWindows(Url, 1000, 600, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            var URL = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Notassignedtsks.aspx?StateKey=" + Request["StateKey"] + "&New=1&DispatchingID=" + Request["DispatchingID"] + "&CustomerID=" + Request["CustomerID"] + "&OrderID=" + Request["OrderID"] + "&NeedPopu=1";
            $("#a_handle").click(function () {
                $("#a_handle").attr("href", URL);
            });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-x: auto;">
        <div class="div_box">
            <asp:HiddenField ID="hideOpen" runat="server" ClientIDMode="Static" Value="1" />
            <asp:HiddenField ID="hideLocation" runat="server" ClientIDMode="Static" Value="1" />
        </div>
        <div class="widget-box" style="height: 30px; border: 0px;">
            <table class="queryTable">
                <tr>
                    <td>
                        <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                    </td>
                    <td>
                        <HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" />
                    </td>
                    <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" />
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
                    <th>任务类型</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server">
                    <ItemTemplate>
                        <tr skey='CarrytaskStateKey<%#Eval("StateKey") %>'>
                            <td><%#Eval("ParentDispatchingID").ToString()=="0"?"":"(变更)" %><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                            <td><%#GetShortDateString(Eval("PartyDate")) %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                            <td><%#GetEmployeeName(Eval("QuotedEmployee")) %></td>
                            <td><%#GetCustomerStateStr(Eval("UseSate")) %></td>
                            <td><%#GetEmployeeName(Eval("StateEmployee")) %></td>
                            <td><%#Eval("EmpLoyeeID").ToString()==User.Identity.Name.ToString()?"派工任务":"执行任务" %></td>
                            <td>
                                <a target="_blank" href='/AdminPanlWorkArea/Carrytask/CarrytaskWork/Notassignedtsks.aspx?StateKey=<%#Eval("StateKey") %>&New=1&DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&NeedPopu=1' class="btn btn-info btnSaveSubmit<%#Eval("StateEmpLoyee") %> btnSumbmit">处理订单</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="9">
                        <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool>
                    </td>
                </tr>
            </tfoot>
        </table>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
