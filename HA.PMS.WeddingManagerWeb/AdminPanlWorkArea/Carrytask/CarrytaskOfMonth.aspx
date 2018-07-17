<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskOfMonth.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskOfMonth" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>


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

            if ($("#hideIsmanager").val() != "True") {
                $(".FinishMoney").remove();
            }
        });
    </script>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: 60px; border: 0px;">

                <table>


                    <tr>
                        <td>酒店:<cc2:ddlHotel ID="ddlHotel1" runat="server"></cc2:ddlHotel>
                            <asp:HiddenField ID="hideIsmanager" ClientIDMode="Static" runat="server" />

                        </td>
                        <td>签约时间:
                        </td>
                        <td>
                            <HA:DateRanger runat="server" ID="DateRanger" />
                        </td>
                        <td>部门</td>
                        <td>
                            <cc2:DepartmentDropdownList ID="DepartmentDropdownList1" runat="server">
                            </cc2:DepartmentDropdownList>
                        </td>
                        <td>
                            <HA:MyManager runat="server" Title="策划师" ID="MyManager" />
                        </td>
                        <td>
                            <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSerch_Click" /></td>
                    </tr>

                </table>
                <br />
            </div>

            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr id="trContent">
                        <th>新人姓名</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>订单状态</th>
                        <%-- <th>派工人</th>--%>
                        <th>应收款</th>
                        <th>订单总金额</th>
                        <th>操作</th>
                    </tr>

                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='<%#Eval("CustomerID") %><%#Eval("DispatchingID") %>'>
                                <td style="height: 16px;"><%#Eval("ParentDispatchingID").ToString()=="0"?"":"(变更)" %>
                                    <a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?OnlyView=1&CustomerID=<%#Eval("CustomerID") %>">
                                        <%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#GetShortDateString(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#GetEmployeeName(Eval("QuotedEmpLoyee")) %></td>
                                <td><%#GetCustomerStateStr(Eval("UseSate")) %></td>
                                <%--<td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>--%>
                                <td><%#GetMoney(Eval("CustomerID")) %></td>
                                <td class="FinishMoney"><%#GetFinishMoney(Eval("OrderID")) %></td>
                                <td>
                                    <a <%#Eval("Groom")==null?"style='display:none;'":"" %> href="CarryCost/OrderCost.aspx?DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=&Type=Look&NeedPopu=1" class="btn btn-success  btn-mini  <%#Eval("IsOver").ToString()=="False"?"":"RemoveClass"%>" target="_blank">执行清单</a>

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
