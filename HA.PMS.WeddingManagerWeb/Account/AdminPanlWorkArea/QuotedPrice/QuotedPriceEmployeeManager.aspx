<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="QuotedPriceEmployeeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceEmployeeManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>

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
            <div class="widget-box" style="height: 60px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td><HA:MyManager runat="server" ID="MyManager" /></td>
                        <td>订单编号：<asp:TextBox ID="txtOrderCoder" runat="server" /></td>
                        <td>日期：<asp:DropDownList ID="ddltype" runat="server">
                            <asp:ListItem Value="PartyDate" Text="婚期" />
                            <asp:ListItem Value="CreateDate" Text="订单日期" />
                        </asp:DropDownList>
                        </td>
                        <td>
                            <HA:DateRanger runat="server" ID="QueryDateRanger" />
                        </td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td>新人姓名
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">联系电话
                         <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="BindData" />
                            <cc2:btnReload ID="btnReload" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>订单编号</th>
                        <th>新人姓名</th>
                        <th>联系方式</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>任务类型</th>
                        <th>订单日期</th>
                        <th>策划师</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" OnItemCommand="repCustomer_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr skey='QuotedPrice<%#Eval("QuotedID") %>'>
                                <td><%#Eval("OrderCoder") %></td>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#string.Format("{0}\\{1}",Eval("Bride"),Eval("Groom")).Trim('\\') %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#Eval("EmpLoyeeID").ToString()==User.Identity.Name.ToString()?"派工任务":"执行任务" %></td>
                                <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                                <td id="<%=Guid.NewGuid() %>" style="width: 165px">
                                    <input runat="server" id="txtEmpLoyee" style="padding: 0; margin: 0; width: 65px" readonly="readonly" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("EmpLoyeeID")) %>' />
                                    <a class="btn btn-primary btn-mini" onclick="ShowPopu(this)">改派</a>
                                    <asp:LinkButton CssClass="btn btn-success btn-mini" CommandArgument='<%#Eval("QuotedID") %>' CommandName="Save" Text="保存" runat="server" />
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hiddeEmpLoyeeID" Value='<%#Eval("EmpLoyeeID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BindData"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
