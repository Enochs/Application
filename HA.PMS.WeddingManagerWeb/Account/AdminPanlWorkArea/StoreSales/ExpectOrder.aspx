<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpectOrder.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.ExpectOrder" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>

<%@ Register Src="../Control/DateRanger.ascx" TagName="DateRanger" TagPrefix="uc1" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hideeIsManager").val() == "1") {
                $(".DepartmentUse").hide();
            }
            $("html,body").css({ "background-color": "transparent" });
        });

        //显示记录沟通记录
        function ShowContent(CustomerID, OrderID, Control) {
            var Url = "/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=" + CustomerID + "&OrderID=" + OrderID + "&FlowOrder=1";
            window.location.href = Url + "&NeedPopu=1";
            //$(Control).attr("id", "updateShow" + CustomerID);

            //showPopuWindows(Url, 900, 600, "a#" + $(Control).attr("id"));
        }

        function CheckState() {
            if (confirm("确认客户到店?确认之后客户将进入跟单中模块！")) {

                return true;
            } else {

                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <asp:HiddenField ID="hideeIsManager" runat="server" ClientIDMode="Static" />
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title=" 跟单责任人" />
                    </td>
                    <td>时间<asp:DropDownList runat="server" ID="ddlDateRanger">
                        <asp:ListItem Text="请选择" Value="请选择"></asp:ListItem>
                        <asp:ListItem Text="婚期" Value="Partydate"></asp:ListItem>
                        <asp:ListItem Text="录入时间" Value="RecorderDate"></asp:ListItem>
                        <asp:ListItem Text="到店时间" Value="ComeDate"></asp:ListItem>
                        <asp:ListItem Text="预计到店时间" Value="PlanComeDate"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        <uc1:DateRanger ID="DateRanger" runat="server" />
                    </td>
                    <td>新人姓名
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                    </td>
                    <td>联系电话
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <cc2:btnManager ID="btnserchforemployee" runat="server" OnClick="btnserchforemployee_Click" />
                        <cc2:btnReload ID="btnReload1" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <td colspan="11"></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="保存" OnClick="btnSaveDate_Click" CssClass="btn btn-primary" Visible="false" /></td>
                    </tr>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>

                        <th>预计到店时间</th>
                        <th>店名</th>
                        <th>新人状态</th>
                        <th>渠道名称</th>
                        <th>电销</th>
                        <th class="DepartmentUse">责任人</th>
                        <th>沟通记录</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand">
                        <ItemTemplate>
                            <tr skey='StoreSalesCustomerID<%#Eval("CustomerID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom"),Eval("OldB")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>

                                <td><%#GetShortDateString(Eval("PlanComeDate")) %></td>
                                <td><%#GetDepartmentByEnpLoyeeID(Eval("EmployeeID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#Eval("Channel") %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                <td class="DepartmentUse"><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                <td>
                                   <%-- <a href="#" class="btn btn-primary btnSaveSubmit<%#Eval("EmployeeID")%> btnSumbmit" onclick="ShowContent(<%#Eval("CustomerID") %>,<%#Eval("OrderID") %>,this);">记录/查看</a>--%>
                                    <asp:LinkButton ID="lnkbtnComeon" CssClass='btn btn-primary ' runat="server" OnClientClick="return CheckState();" CommandArgument='<%#Eval("OrderID") %>'>确认到店</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="11">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveDate" runat="server" Text="保存" Visible="false" OnClick="btnSaveDate_Click" CssClass="btn btn-primary" /></td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
