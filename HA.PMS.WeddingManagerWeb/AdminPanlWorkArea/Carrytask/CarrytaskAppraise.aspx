<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskAppraise.aspx.cs" StylesheetTheme="Default" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskAppraise" Title="需要评价的列表" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>


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

        #table_Or {
            text-align: center;
        }

        #trContent th {
            text-align: left;
        }
        .table thead tr th {
            text-align:left;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {
                if (indexs != 1) {
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


        });
    </script>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: 60px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>
                            <HA:MyManager runat="server" ID="MyManager" Title="婚礼策划:" />
                        </td>
                       
                        <td>
                            <HA:DateRanger runat="server" ID="DateRanger" Title="婚期:" />
                        </td>

                        <td>新人姓名:
                                        <asp:TextBox ID="txtContactMan" runat="server" ToolTip="请输入新娘姓名"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">联系电话:
                                        <asp:TextBox ID="txtCellPhone" runat="server" ToolTip="请输入新娘联系电话"></asp:TextBox>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSerch_Click" />
                            <cc2:btnReload ID="btnReload" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>

            <table class="table table-bordered table-striped table-select" id="table_Or">
                <thead>
                    <tr>
                        <th width="100">新人</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>已收款</th>
                        <th>订单金额</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride").ToString().Length > 6 ? Eval("Bride").ToString().Substring(0,6) : Eval("Bride"),Eval("Groom").ToString().Length > 6 ? Eval("Groom").ToString().Substring(0,6) : Eval("Groom")) %></a></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("QuotedEmployee")) %></td>
                                <td><%#GetQuotedDispatchingFinishMoney(Eval("CustomerID")) %></td>
                                <td><%#Eval("FinishAmount") %></td>
                                <td id="tbOper">
                                    <a href='CarryCost/OrderCostEvaluation.aspx?DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>' target="_blank" class="btn btn-primary">评价</a>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tr>
                        <td colspan="9">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tbody>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

        </div>
    </div>
</asp:Content>
