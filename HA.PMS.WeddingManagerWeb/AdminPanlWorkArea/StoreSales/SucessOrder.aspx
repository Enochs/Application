<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SucessOrder.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.SucessOrder" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        $(function () {
            $("html,body").css({ "background-color": "transparent" });
        });
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ClassType=QuotedPriceWorkPanel&ALL=1";
            showPopuWindows(Url, 450, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>时间<asp:DropDownList ID="ddlTimeSpan" runat="server">
                        <asp:ListItem Value="0">婚期</asp:ListItem>
                        <asp:ListItem Value="1">订单时间</asp:ListItem>
                    </asp:DropDownList></td>
                    <td>
                        <HA:DateRanger runat="server" ID="MainDateRanger" />
                    </td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title="顾问" />
                    </td>
                    <td>新人姓名
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                    </td>
                    <td>联系电话
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnserch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnserch_Click" />
                        <cc2:btnReload ID="btnReload2" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <td colspan="9"></td>
                        <td>
                            <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="保存" OnClick="btnSaveDate_Click" /></td>
                    </tr>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>订单时间</th>
                        <th>婚礼顾问</th>
                        <th>定金</th>
                        <th>提案时间</th>
                        <th>策划师</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom"),Eval("OldB")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#ShowShortDate(Eval("LastFollowDate")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetRealityAmount(Eval("OrderID")) %></td>
                                <td><%#Eval("NextFlowDate","{0:yyyy-MM-dd}") %></td>
                                <td>
                                    <asp:Label runat="server" ID="lblQuotedEmployee" Text='<%#GetQuotedEmpLoyeeName(Eval("OrderID")) %>' />
                                </td>
                                <td>
                                    <%-- <a href="FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&FlowOrder=1" class="btn btn-primary btnSaveSubmit<%#Eval("EmployeeID")%> btnSumbmit">记录/跟踪</a>--%>
                                    <a target="_blank" class="btn btn-primary " href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1">查看跟踪</a></td>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>

                        <td></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>本页合计：<asp:Label ID="lblSumRealitypage" runat="server" Text=""></asp:Label></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>

                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>当期合计：<asp:Label ID="lblSumRealityall" runat="server" Text=""></asp:Label>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="9">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveDate" CssClass="btn btn-primary" runat="server" Text="保存" OnClick="btnSaveDate_Click" /></td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
