<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_CustomerReturnVisitManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit.FL_CustomerReturnVisitManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="../../../Control/DateRanger.ascx" TagName="DateRanger" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {


                if (indexs != 3) {
                    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });

            $(".queryTable2").css({ "margin-left": "15px", "margin-top": "5px" });//98    24
            $(".queryTable2 td").each(function (indexs, values) {


                if (indexs != 2) {
                    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (indexs == $(".queryTable2 td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });

            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            $("html").css({ "overflow-x": "hidden" });
            if (window.screen.width >= 1280 && window.screen.width <= 1366) {
                $("#queryBox").css({ "height": "40px" });

            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-box" id="queryBox" style="height: 40px; border: 0px;">

                <table class="queryTables" style="border-bottom: 1px solid #c7d5de;">
                    <tr>
                        <td>
                            <table>
                                <tr>

                                    <td>姓名:
                                <asp:TextBox ID="txtGroom" runat="server"></asp:TextBox>
                                    </td>
                                    <td>电话号码:
                                         <asp:TextBox ID="txtBrideCellPhone" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <uc1:DateRanger ID="DateRanger" Title="到店时间:" runat="server" />
                                    </td>
                                    <td>&nbsp;&nbsp;</td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <table class="queryTable2">
                    <tr>
                        <td>
                            <table>
                                <tr>

                                    <td colspan="2">
                                        <%--  <cc2:DateEditTextBox onclick="WdatePicker();" ID="txtReturnDateStar" runat="server"></cc2:DateEditTextBox>
                                    至
                                <cc2:DateEditTextBox onclick="WdatePicker();" ID="txtReturnDateEnd" runat="server"></cc2:DateEditTextBox>
                                        --%>
                                        <uc1:DateRanger ID="DateRanger1" Title="回访时间:" runat="server" />
                                    </td>

                                    <td>新人状态:<asp:DropDownList ID="ddlCustomerState" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnQuery" Height="27" runat="server" CssClass="btn btn-primary" Text="查询" OnClick="btnQuery_Click" />
                                        <cc2:btnReload ID="btnReload" runat="server" />
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>

            </div>
            <br />
            <br />
            <table class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th>姓名</th>
                        <th>联系方式</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>到店时间</th>
                        <th>跟单人</th>
                        <th>新人状态</th>
                        <th>回访时间</th>
                        <th>查看</th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptReturn" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>" target="_blank">
                                    <%#Eval("Bride") %>
                                </a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#Eval("OrderCreateDate","{0:yyyy-MM-dd}") %></td>
                                <td><%#GetSaleEmployeeByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetState(Eval("CustomerID")) %></td>
                                <td><%#GetDateStr(Eval("ReasonsDate")) %></td>
                                <td><a href="FL_ReturnVisitMessageShow.aspx?CustomerID=<%#Eval("CustomerID") %>" class="btn btn-primary">查看回访结果</a></td>

                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="ReturnPager" PageSize="10" AlwaysShow="true" OnPageChanged="ReturnPager_PageChanged" runat="server">
            </cc1:AspNetPagerTool>

            <HA:MessageBoard runat="server" ClassType="FL_CustomerReturnVisitManager" ID="MessageBoard" />

        </div>
    </div>

</asp:Content>
