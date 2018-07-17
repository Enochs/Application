<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_CustomerReturnVisitManagerTswk.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit.FL_CustomerReturnVisitManagerTswk" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 25px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });

            $("html").css({ "overflow-x": "hidden" });
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

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-box" style="height: 30px; border: 0px;">

                <table class="queryTable">
                    <tr>
                        <td>
                            <table>
                                <tr>

                                    <td>姓名:<asp:TextBox ID="txtGroom" runat="server"></asp:TextBox>
                                    </td>
                                    <td>电话:
                                        <asp:TextBox ID="txtGroomCellPhone" runat="server"></asp:TextBox>
                                    </td>
                                    <td>婚期:<cc2:DateEditTextBox onclick="WdatePicker();" ID="txtPartDateStar" runat="server"></cc2:DateEditTextBox></td>
                                    <td>至<cc2:DateEditTextBox onclick="WdatePicker();" ID="txtPartDateEnd" runat="server"></cc2:DateEditTextBox></td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnQuery" Height="27" runat="server" CssClass="btn btn-primary" Text="查找" OnClick="btnQuery_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr id="trContent">
                        <th>姓名</th>
                        <th>联系方式</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>到店时间</th>
                        <th>门店</th>
                        <th>销售跟单人</th>
                        <th>预定时间</th>
                        <th>新人状态</th>

                        <th>策划师</th>
                        <th>回访时间</th>
                        <th>回访结果</th>
                        <th>回访记录</th>
                        <th>责任人</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptReturn" OnItemDataBound="rptReturn_ItemDataBound" OnItemCommand="rptReturn_ItemCommand"  runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("CustomerID") %>'>
                                <asp:HiddenField ID="hfReturnId" Value='<%#Eval("ReturnId") %>' runat="server"></asp:HiddenField>
                                <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>" target="_blank"> <%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetToStrose(Eval("CustomerID")) %></td>
                                <td><%#GetStoreHouseByCustomerId(Eval("CustomerID")) %></td>
                                <td><%#GetSaleEmployeeByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetOrderDate(Eval("CustomerID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetPlannerNameByCustomersId(Eval("CustomerID")) %></td>
                                <td><cc2:DateEditTextBox onclick="WdatePicker();" ID="txtReturnDate" Text='<%#GetDateStr(Eval("ReturnDate")) %>' Width="50" runat="server"></cc2:DateEditTextBox></td>
                                <td>

                                    <asp:DropDownList ID="ddlReturnResult" Width="70" runat="server">
                                        <asp:ListItem Value="0" Text="不满意"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="一般"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="满意"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>

                                    <asp:TextBox ID="txtReturnRecoder" ToolTip='<%#Eval("ReturnRecoder") %>' Text='<%#Eval("ReturnRecoder") %>' Width="50" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <cc2:ddlOrderEmployee ID="ddlOrderEmployee" runat="server"></cc2:ddlOrderEmployee>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbtnEdit" CssClass="btn btn-primary btn-mini" CommandName="Edit"
                                        CommandArgument='<%#Eval("CustomerID") %>' runat="server">确定</asp:LinkButton>
                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="ReturnPager"  OnPageChanged="ReturnPager_PageChanged" runat="server">
            </cc1:AspNetPagerTool>

            <HA:MessageBoard runat="server" ClassType="FL_CustomerReturnVisitManager" ID="MessageBoard" />

        </div>
    </div>

</asp:Content>
