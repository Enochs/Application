<%@ Page Title="订单退订" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="QuotedLoss.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager.QuotedLoss" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>

    <script type="text/javascript">
        function CheckLose() {
            if (confirm("请注意！将流失客户！流失后只能由管理员恢复！")) {
                return true;
            }
            return false;
        }

        function ShowLossReason(QuotedID, OrderID, CustomerID, Type, Control) {
            var Url = "QuotedLossReason.aspx?QuotedID=" + QuotedID + "&OrderID=" + OrderID + "&CustomerID=" + CustomerID + "&Type=" + Type;
            $(Control).attr("id", "updateShow" + QuotedID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }

    </script>

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <div class="widget-box">
        <table>
            <tr>
                <td>
                    <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                </td>
                <td>联系电话</td>
                <td>
                    <asp:TextBox runat="server" ID="txtContactPhone" /></td>
                <td>
                    <HA:MyManager runat="server" ID="MyManager" />
                </td>
                <td>
                    <HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" />
                </td>
            </tr>
            <tr>
                <td>
                    <HA:DateRanger Title="签单日期：" runat="server" ID="CreateDateRanger" />
                </td>
                <td>
                    <cc2:btnManager ID="BtnQuery" Text="查询" Visible="true" runat="server" OnClick="BtnQuery_Click" />
                    <cc2:btnReload runat="server" ID="BtnReload" />
                </td>
            </tr>
        </table>
        <table class="table table-bordered table-striped table-select">
            <thead>
                <tr>
                    <th>新人姓名</th>
                    <th>电话</th>
                    <th>婚期</th>
                    <th>签单日期</th>
                    <th>酒店</th>
                    <th>婚礼顾问</th>
                    <th>婚礼策划</th>
                    <th>新人状态</th>
                    <th>已收款</th>
                    <th>退款金额</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand">
                    <ItemTemplate>
                        <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                            <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                            <td><%#Eval("BrideCellPhone") %></td>
                            <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                            <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                            <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                            <td><%#GetCustomerStateStr(Eval("State")) %></td>
                            <td><%#GetQuotedDispatchingFinishMoney(Eval("CustomerID")) %></td>
                            <td><%#GetBackMoney(Eval("OrderID")) %></td>
                            <td id="tbOper">
                                <a href="#" <%#Eval("LoseContentID") != null ? "style='display:none;'" : "" %> onclick='ShowLossReason(<%#Eval("QuotedID") %>,<%#Eval("OrderID") %>,<%#Eval("CustomerID") %>,"Edit",this);' class="btn btn-primary btn-mini">订单退订</a>
                                <a href="#" <%#Eval("LoseContentID") != null ? "" : "style='display:none;'" %> onclick='ShowLossReason(<%#Eval("QuotedID") %>,<%#Eval("OrderID") %>,<%#Eval("CustomerID") %>,"Look",this);' class="btn btn-primary btn-mini">查看</a>

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
                        <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                    </td>
                </tr>
            </tfoot>
        </table>

    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
</asp:Content>

