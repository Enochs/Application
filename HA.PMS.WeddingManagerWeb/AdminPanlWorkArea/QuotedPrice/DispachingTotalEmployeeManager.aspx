<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="DispachingTotalEmployeeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.DispachingTotalEmployeeManager" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hiddeEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: 30px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>新人姓名：<asp:TextBox ID="txtBride" runat="server" /></td>
                        <td>联系方式：<asp:TextBox ID="txtBrideCellPhone" Visible="false" runat="server" /></td>
                        <td>日期：<asp:DropDownList ID="ddltype" runat="server">
                            <asp:ListItem Value="1" Text="婚期" />
                            <asp:ListItem Value="2" Text="派工日期" />
                        </asp:DropDownList>
                        </td>
                        <td><HA:DateRanger runat="server" ID="QueryDateRanger" /></td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td><asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="BindData" /></td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>联系方式</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>任务类型</th>
                        <th>派工日期</th>
                        <th>总派工</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" OnItemCommand="repCustomer_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr skey='QuotedPriceDispatchingID<%#Eval("DispatchingID") %>'>
                                <td><%#Eval("ParentDispatchingID").ToString()=="0"?string.Empty:"(变更)" %><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#string.Format("{0}\\{1}",Eval("Bride"),Eval("Groom")).Trim('\\') %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#Eval("EmpLoyeeID").ToString()==User.Identity.Name.ToString()?"派工任务":"执行任务" %></td>
                                <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                                <td id="<%=Guid.NewGuid() %>" style="width:165px">
                                    <input runat="server" id="txtEmpLoyee" style="padding: 0; margin: 0;width:65px" readonly="readonly" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("EmpLoyeeID")) %>' />
                                    <a class="btn btn-primary btn-mini" onclick="ShowPopu(this)">改派</a>
                                    <asp:LinkButton CssClass="btn btn-success btn-mini" CommandArgument='<%#Eval("DispatchingID") %>' CommandName="Save" Text="保存" runat="server" />
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hiddeEmpLoyeeID" Value='<%#Eval("EmpLoyeeID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="8">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BindData"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
