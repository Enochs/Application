<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenewOrder.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.RenewOrder" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" Title="跟单" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="uc1" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="uc1" TagName="MyManager" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <script type="text/javascript">
        $(function () {
            $("html").css({ "overflow-x": "hidden" });

            $(".popuLoseContent").fancybox({
                'topRatio': 0.15
            });
            $(".popuLoseContent").hover(function () {
                $(this).css({ "color": "#ff0000" });
            }, function () {
                $(this).css({ "color": "#0094ff" });
            });

        });
    </script>
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>

                    <td>流失原因:<cc2:ddlLoseContent ID="ddlLoseContent" runat="server">
                    </cc2:ddlLoseContent>
                    </td>
                    <td>查询条件:
                                    <asp:DropDownList ID="ddlType" runat="server">
                                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                                        <asp:ListItem Value="0">流失时间</asp:ListItem>
                                        <asp:ListItem Value="1">婚期</asp:ListItem>
                                    </asp:DropDownList>
                    </td>
                    <td>时间:<cc2:DateEditTextBox onclick="WdatePicker();" ID="txtStarTime" runat="server"></cc2:DateEditTextBox>
                    </td>
                    <td>到
                                        <cc2:DateEditTextBox onclick="WdatePicker();" ID="txtEndTime" runat="server"></cc2:DateEditTextBox></td>
                    <td>
                        <uc1:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td>
                        新人姓名:
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        联系电话:
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
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>到店时间</th>
                        <th>跟单次数</th>
                        <th>流失时间</th>
                        <th>流失原因</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><a target="_blank" href="FollowOrderDetails.aspx?OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#ShowShortDate(Eval("PlanComeDate")) %></td>
                                <td><%#Eval("FlowCount") %></td>
                                <td><asp:Label runat="server" ID="lblLastFollowDate" Text='<%#ShowShortDate(Eval("LastFollowDate")) %>' /></td>
                                <td><a class="popuLoseContent " style="text-decoration: underline; color: #0094ff;" href='#Details<%#Eval("ConteenID") %>'><%#GetLoseContent(Eval("ConteenID")) %></a>
                                    <div id='Details<%#Eval("ConteenID") %>' style="display: none; width: 250px; width: 250px; vertical-align:top;">
                                        <span style="font-weight: bold;">流失具体原因说明</span>
                                        <br />
                                        <%#GetLoseContentDetails(Eval("OrderID")) %>
                                    </div>
                                </td>
                                <asp:HiddenField ID="hideEmpLoyeeID" Value="-1" runat="server" />
                                <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                <td><asp:LinkButton ID="ReOrder" Text="恢复跟单" OnClientClick="return confirm('确认恢复该新人到跟单中？');" CommandArgument='<%#Eval("CustomerID") %>' CommandName="ReOrder" CssClass="btn btn-primary" runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10"><cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"  OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool></td>
                    </tr>
                </tfoot>
            </table>
            <uc1:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
