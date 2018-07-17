<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_ComplainManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_ComplainManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "CS_ComplainUpdate.aspx?ComplainID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }

        function ShowDetailsWindows(KeyID, Control) {
            var Url = "CS_ComplainDetails.aspx?ComplainID=" + KeyID;
            $(Control).attr("id", "detailsShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {

            //   showPopuWindows($("#createComplain").attr("href"), 700, 1000, "a#createComplain");
           // $("#<//%=txtPartyDateStar.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtPartyDateEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });


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

            $("html").css({ "overflow-x": "hidden" });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">

        <%--  <a href="CS_ComplainCreate.aspx" class="btn btn-primary  btn-mini" id="createComplain">添加客户投诉</a>--%>

        <div class="widget-box">


            <div class="widget-box" style="height: 30px; border: 0px;">

                <table class="queryTable">
                    <tr>
                        <td>
                            <table>
                                <tr>

                                    <td>姓名:
                  
                                        <asp:TextBox ID="txtCustomrer" runat="server"></asp:TextBox>

                                    </td>
                                    <td>电话:
                                        <asp:TextBox ID="txtCelPhone" runat="server"></asp:TextBox>
                                    </td>

                                    <td>婚期:
                    <asp:TextBox ID="txtPartyDateStar" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                    </td>

                                    <td>到
                  <asp:TextBox ID="txtPartyDateEnd" onclick="WdatePicker();" runat="server"></asp:TextBox>

                                    </td>

                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnQuery" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="btnQuery_Click" /></td>
                                </tr>



                            </table>
                        </td>
                    </tr>
                </table>
            </div>


            <table class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th>订单总金额</th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>派工人</th>
                        <th>订单状态</th>
                        <th>订单时间</th>
                        <th>投诉受理人</th>
                        <th>受理时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>

                    <asp:Repeater ID="rptComplain" runat="server" OnItemCommand="rptComplain_ItemCommand">

                        <ItemTemplate>

                            <tr>
                                <td><%#GetAggregateAmount(Eval("CustomerID"))%></td>
                                <td><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate( Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetPlannerNameByCustomersId(Eval("CustomerID")) %></td>
                                <td><%#GetDispatchingEmpLoyee(Eval("CustomerID")) %> </td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetLastFollowDateByCustomerId(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("ComplainEmployeeId")) %></td>
                                <td><%#GetDateStr( Eval("ReturnDate")) %></td>
                                <td>
                                    <a href="CS_ComplainCreate.aspx?ComplainID=<%#Eval("ComplainID") %>" class="btn btn-primary  btn-mini" <%#Eval("ComplainEmployeeId").ToString()==User.Identity.Name?"":"style='display:none;'" %>>处理投诉</a>
                                    <%-- <a  href="CS_ComplainCreate.aspx?ComplainID=<%#Eval("ComplainID") %>" class="btn btn-primary  btn-mini">查看处理结果</a>--%>
                                    <%--      <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ComplainID") %>' runat="server">删除</asp:LinkButton>--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="ComplainPager" OnPageChanged="ComplainPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
