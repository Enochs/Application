<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchingforEmployee.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.DispatchingforEmployee" Title="我的派工任务" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>


<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowThis(CustomerID) {
            window.parent.ShowPopuWindow("/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?CustomerID=" + CustomerID + "&NeedPopu=1", 1280, 1500);
            return false;
        }

        //改派
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hiddeEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a id="SelectEmpLoyeeBythis" href="#" style="display: none;"></a>
    <div style="overflow-x: auto;">
        <div class="widget-box" style="height: 60px; border: 0px;">
            <table class="queryTable">
                <tr>
                    <td>
                        <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                    </td>
                    <%--<td><HA:MyManager runat="server" ID="MyManager" Title="策划师：" /></td>--%>
                    <td>
                        <HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" />
                    </td>
                    <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                </tr>
                <tr>
                    <td>状态:
                        <asp:DropDownList runat="server" ID="ddlStates">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="未完成" Value="1" />
                            <asp:ListItem Text="已完成" Value="2" />
                        </asp:DropDownList>

                    </td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager1" Title="派工人:" />
                    </td>
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
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand" OnItemDataBound="repCustomer_ItemDataBound">
                    <ItemTemplate>
                        <tr skey='CarrytaskStateKey<%#Eval("StateKey") %>'>
                            <td><%#Eval("ParentDispatchingID").ToString()=="0"?"":"(变更)" %>
                                <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1" style="float: left;"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a>
                                <a id="SelectPG" <%#IsChange(Eval("DispatchingID")) %>>(变)</a>
                                <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                            </td>
                            <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <%--                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>--%>
                            <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                            <td><%#GetEmployeeName(Eval("QuotedEmployee")) %></td>
                            <td><%#GetCustomerStateStr(Eval("UseSate")) %></td>
                            <td id="<%=Guid.NewGuid() %>">
                                <asp:TextBox runat="server" ID="txtEmpLoyee" CssClass="txtEmpLoyeeName" Width="55px" Height="18px" Text='<%#GetEmployeeName(Eval("StateEmployee")) %>' onclick="ShowPopu(this)" />
                                <asp:LinkButton runat="server" CommandName="SaveChange" CommandArgument='<%#Eval("StateKey") %>' ID="lbtnSaveChange" Text="保存" CssClass="btn btn-primary btn-mini" ClientIDMode="Static" />
                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hiddeEmpLoyeeID" Value='<%#Eval("StateEmployee") %>' />
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td><a target="_blank" href='/AdminPanlWorkArea/Carrytask/CarrytaskWork/Notassignedtsks.aspx?StateKey=<%#Eval("StateKey") %>&New=1&DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&NeedPopu=1' class="btn btn-info">处理订单</a>
                                            <a target="_blank" href="/AdminPanlWorkArea/Carrytask/CarrytaskWork/TaskWorkPanel.aspx?PageNameProductList&DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&NeedPopu=1&PageName=ProductList" class="btn btn-success">派工</a>
                                            <a href="/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?QuotedID=<%#Eval("QuotedID") %>&OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=1" class="btn btn-success" target="_blank">查看原始订单</a>
                                        </td>
                                        <td><a target="_blank" href='../QuotedPrice/CreateSchedule.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&Type=Look' <%#IsShowOrHide(Eval("CustomerID")) %> class="btn btn-primary btn-mini" style="height: 20px;">查看预定</a></td>
                                    </tr>
                                </table>


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
