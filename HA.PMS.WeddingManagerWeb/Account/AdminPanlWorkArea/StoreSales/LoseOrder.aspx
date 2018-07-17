<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoseOrder.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.LoseOrder" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" Title="跟单" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="uc1" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="uc1" TagName="MyManager" %>




<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }
    </style>
    <script type="text/javascript">


        $(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {
                if (indexs != 3) {
                    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            //
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
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-box" style="height: 80px; border: 0px;">
                <table class="queryTable" style="height: 80px; border: 0px;">
                    <tr>
                        <td>流失原因:<asp:DropDownList runat="server" ID="ddlLoseContent">
                        </asp:DropDownList>
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
                        <td>到<cc2:DateEditTextBox onclick="WdatePicker();" ID="txtEndTime" runat="server"></cc2:DateEditTextBox></td>
                        <td>
                            <uc1:MyManager runat="server" ID="MyManager" />
                        </td>

                    </tr>
                    <tr>
                        <td>新人姓名:<asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox></td>
                        <td>联系电话:<asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox></td>
                        <td colspan="2">
                            <asp:Button ID="btnserch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnserch_Click" />
                            <cc2:btnReload ID="btnReload" runat="server" />
                        </td>
                    </tr>
                </table>



            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th style="white-space: nowrap;">新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>到店时间</th>
                        <th>跟单次数</th>
                        <th>流失时间</th>
                        <th>流失原因</th>
                        <%--   <th>查看明细</th>--%>
                        <th>操作</th>
                    </tr>

                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand">
                        <ItemTemplate>

                            <tr>
                                <td style="height: 16px;"><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom"),Eval("OldB")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetShortDateString(Eval("PlanComeDate")) %></td>
                                <td><%#Eval("FlowCount") %></td>
                                <td><%#GetShortDateString(Eval("LastFollowDate")) %></td>
                                <td>

                                    <a class="popuLoseContent " style="text-decoration: underline; color: #0094ff;" href='#Details<%#Eval("CustomerID") %>'><%#GetLoseContent(Eval("ConteenID")) %></a>
                                    <div id='Details<%#Eval("CustomerID") %>' style="display: none; width: 250px; width: 250px; vertical-align: top;">
                                        <span style="font-weight: bold;">流失具体原因说明</span>
                                        <br />
                                        <%#GetLoseContentDetails(Eval("OrderID")) %>
                                    </div>
                                </td>
                                <td>
                                    <asp:Button runat="server" CommandName="RecoOrder" CommandArgument='<%#Eval("CustomerID") %>' ID="btn_RecoOrder" OnClientClick="return confirm('确定恢复该新人到跟单中？')" class="SetState btn btn-primary" Text="恢复跟单" />
                                    <a href="../Flows/Customer/ReturnVisit/FL_ReturnVisitMessageShow.aspx?CustomerID=<%#Eval("CustomerID") %>" class="btn btn-primary">回访</a></td>
                                </td>
                                <asp:HiddenField ID="hideEmpLoyeeID" Value="-1" runat="server" />
                                <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                <%--<td><a href="FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#GetOrderIDByCustomers(Eval("CustomerID")) %>&Sucess=1" <%#Eval("Groom")==null?"style='display:none;'":"" %>>沟通记录</a>
                                </td>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>

                    </tr>
                </tfoot>
            </table>
            <uc1:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>

</asp:Content>
