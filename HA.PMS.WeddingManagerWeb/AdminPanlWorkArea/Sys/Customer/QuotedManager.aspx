<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="QuotedManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer.QuotedManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/SelectEmployee.ascx" TagPrefix="HA" TagName="SelectEmployee" %>





<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2">


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

        $(document).ready(function () {
            //$("#trContent th").css({ "white-space": "nowrap" });

            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {

                $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");

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

        });
        function CheckDelete()
        {
            if (confirm('确认要删除该报价单，删除后无法恢复，是否继续？'))
            {
                return confirm("真的要删除吗？");
            }
            return false;
        }
    </script>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: 30px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>新人姓名：<asp:TextBox ID="txtBride" runat="server" /></td>
                        <td><HA:DateRanger runat="server" Title="婚期：" ID="PartyDateRanger" /></td>
                        <td style="display:none">新人状态：<cc2:ddlCustomersState ID="DdlCustomersState1" runat="server"></cc2:ddlCustomersState></td>
                        <td>联系电话：<asp:TextBox runat="server" ID="txtCellPhone" /></td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td><asp:Button ID="btnQuery" CssClass="btn btn-primary" OnClick="btnQuery_Click" runat="server" Text="查询" />
                            <cc2:btnReload ID="btnReload2" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>

            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>定金</th>
                        <th>婚礼预算</th>
                        <th>下次沟通时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmployee(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetOrderMoney(Eval("CustomerID"),1) %></td>
                                <td><%#GetOrderMoney(Eval("CustomerID"),2) %></td>
                                <td><%#ShowShortDate(Eval("NextFlowDate")) %></td>
                                <td><asp:LinkButton ID="lnkbtnDelete" CssClass="btn btn-mini btn-danger" OnClientClick="return CheckDelete();" runat="server" CommandArgument='<%#Eval("CustomerID") %>'>删除报价单</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"   OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>

                    </tr>

                </tfoot>
            </table>


        </div>
    </div>
</asp:Content>
 