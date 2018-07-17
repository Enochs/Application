<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FollowUpOrder.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.FollowUpOrder" Title="跟单" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

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

    <script>

        $(function () {

            //$(".DateTimeTxt").datepicker({ dateFormat: 'yy-mm-dd ' });


            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {
                if (indexs != 2) {
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
            $("html,body").css({ "background-color": "transparent" });
        });
        //

        //显示记录沟通记录
        function ShowContent(CustomerID, OrderID, Control) {
            var Url = "FollowOrderDetails.aspx?CustomerID=" + CustomerID + "&OrderID=" + OrderID + "&FlowOrder=1";
            window.location.href = Url + "&NeedPopu=1";
            //$(Control).attr("id", "updateShow" + CustomerID);

            //showPopuWindows(Url, 900, 600, "a#" + $(Control).attr("id"));
        }
    </script>
    <%--   //预访新人 
        //派单--%>
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-box" style="height: 60px; border: 0px;">
                <table class="queryTable" style="height: 60px; border: 0px;">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>渠道名称:<cc2:ddlChannelName ID="DdlChannelName1" runat="server"></cc2:ddlChannelName></td>
                                    <td>类型<asp:DropDownList ID="ddlType" runat="server">
                                        <asp:ListItem Value="0">到店时间</asp:ListItem>
                                        <asp:ListItem Value="1">婚期</asp:ListItem>
                                        <asp:ListItem Value="2">上次沟通时间</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                    <td colspan="2">时间<cc2:DateEditTextBox onclick="WdatePicker();" ID="txtStarTime" runat="server"></cc2:DateEditTextBox>
                                        -
                                        <cc2:DateEditTextBox onclick="WdatePicker();" ID="txtEndTime" runat="server"></cc2:DateEditTextBox>
                                    </td>
                                    <td>
                                        <uc1:MyManager runat="server" ID="MyManager" Title="顾问" />
                                    </td>

                                </tr>
                                <tr>
                                    <td>新人姓名
                                    <asp:TextBox ID="txtContactMan" runat="server" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>联系电话
                                     <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnserch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnserch_Click" />
                                        <cc2:btnReload ID="btnReload2" runat="server" />
                                    </td>
                                </tr>
                            </table>
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
                        <th>新人状态</th>
                        <th>跟单次数</th>
                        <th>到店时间</th>
                        <th>上次沟通时间</th>
                        <th>下次沟通时间</th>
                        <th>电销</th>
                        <th>顾问</th>
                        <th>成交概率</th>
                        <th>操作</th>
                    </tr>
                </thead>

                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>

                            <tr <%#OverChange(Eval("CustomerID")) %>>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom"),Eval("OldB")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#Eval("FlowCount") %></td>
                                <td><%#Eval("OrderCreateDate","{0:yyyy-MM-dd}") %></td>
                                <td><%#GetShortDateString(Eval("LastFollowDate")) %></td>
                                <td><%#GetShortDateString(Eval("NextFlowDate")) %></td>
                                <td><%#GetInviteName(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                <td><%#Eval("TransactionProba") %>%</td>
                                <td>
                                    <asp:HiddenField ID="hideEmpLoyeeID" Value="-1" runat="server" />
                                    <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                    <%--<a href="FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#GetOrderIDByCustomers(Eval("CustomerID")) %>">查看邀约记录</a>--%>
                                    <a href="FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&FlowOrder=1" class="btn btn-primary btnSaveSubmit<%#Eval("EmployeeID")%> btnSumbmit">记录/查看跟单信息</a>
                                    <a target="_blank" class="btn btn-primary " <%=StatuHideViewInviteInfo() %> href="FollowOrderDetails.aspx?Sucess=1&CustomerID=<%#Eval("CustomerID") %>&OnlyView=1">查看跟踪</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="11">
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
